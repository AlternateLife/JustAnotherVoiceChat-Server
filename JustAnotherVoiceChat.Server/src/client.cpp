/*
 * File: src/client.cpp
 * Date: 29.01.2018
 *
 * MIT License
 *
 * Copyright (c) 2018 JustAnotherVoiceChat
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 */

#include "client.h"

#include "log.h"

#include <math.h>

#include "../thirdparty/JustAnotherVoiceChat/include/protocol.h"

using namespace justAnotherVoiceChat;

Client::Client(ENetPeer *peer, uint16_t gameId, uint16_t teamspeakId) {
  _peer = peer;
  _gameId = gameId;
  _teamspeakId = teamspeakId;

  _talking = false;
  _microphoneMuted = false;
  _speakersMuted = false;
  _positionChanged = true;  // set true to update position on first updateClients loop
  _voiceRange = 10;
  _nickname = "";
}

Client::~Client() {
  disconnect();
}

uint16_t Client::gameId() const {
  return _gameId;
}

uint16_t Client::teamspeakId() const {
  return _teamspeakId;
}

void Client::disconnect() {
  if (_peer == nullptr) {
    return;
  }

  enet_peer_disconnect(_peer, 0);
}

bool Client::isConnected() const {
  return _peer != nullptr;
}

bool Client::isTalking() const {
  return _talking;
}

bool Client::hasMicrophoneMuted() const {
  return _microphoneMuted;
}

bool Client::hasSpeakersMuted() const {
  return _speakersMuted;
}

ENetPeer *Client::peer() const {
  return _peer;
}

bool Client::handleStatus(ENetPacket *packet, bool *talkingChanged, bool *microphoneChanged, bool *speakersChanged) {
  statusPacket_t statusPacket;

  std::string data((char *)packet->data, packet->dataLength);
  std::istringstream is(data);

  try {
    cereal::BinaryInputArchive archive(is);
    archive(statusPacket);
  } catch (std::exception &e) {
    logMessage(e.what(), LOG_LEVEL_ERROR);
    return false;
  }

  // track changed status
  *talkingChanged = _talking != statusPacket.talking;
  *microphoneChanged = _microphoneMuted != statusPacket.microphoneMuted;
  *speakersChanged = _speakersMuted != statusPacket.speakersMuted;

  // update current status
  _talking = statusPacket.talking;
  _microphoneMuted = statusPacket.microphoneMuted;
  _speakersMuted = statusPacket.speakersMuted;

  // TODO: Send response?

  return *talkingChanged || *microphoneChanged || *speakersChanged;
}

void Client::addAudibleClient(Client *client) {
  if (_audibleClients.find(client) != _audibleClients.end()) {
    return;
  }

  _addAudibleClients.insert(client);
}

void Client::removeAudibleClient(Client *client) {
  if (_audibleClients.find(client) == _audibleClients.end()) {
    return;
  }

  _removeAudibleClients.insert(client);
}

void Client::addRelativeAudibleClient(Client *client, linalg::aliases::float3 position) {
  if (isRelativeClient(client)) {
    return;
  }

  auto *relativeClient = new relativeClient_t;
  relativeClient->client = client;
  relativeClient->offset = position;

  _addRelativeAudibleClients.insert(relativeClient);
}

void Client::removeRelativeAudibleClient(Client *client) {
  if (isRelativeClient(client) == false) {
    return;
  }

  _removeRelativeAudibleClients.insert(client);
}

void Client::removeAllRelativeAudibleClients() {
  for (auto it = _relativeAudibleClients.begin(); it != _relativeAudibleClients.end(); it++) {
    _removeRelativeAudibleClients.insert((*it)->client);
  }
}

void Client::sendUpdate() {
  // create update packet
  updatePacket_t updatePacket;

  // send new audible clients
  for (auto it = _addAudibleClients.begin(); it != _addAudibleClients.end(); it++) {
    if (isRelativeClient(*it) == false) {
      // add to update packet
      clientAudioUpdate_t audioUpdate;
      audioUpdate.teamspeakId = (*it)->teamspeakId();
      audioUpdate.muted = false;
      updatePacket.audioUpdates.push_back(audioUpdate);
    }

    _audibleClients.insert(*it);
  }

  for (auto it = _addRelativeAudibleClients.begin(); it != _addRelativeAudibleClients.end(); it++) {
    // only unmute if also not normal list
    if (_audibleClients.find((*it)->client) == _audibleClients.end()) {
      // add update packet
      clientAudioUpdate_t audioUpdate;
      audioUpdate.teamspeakId = (*it)->client->teamspeakId();
      audioUpdate.muted = false;
      updatePacket.audioUpdates.push_back(audioUpdate);
    }

    // send offset once
    clientPositionUpdate_t positionUpdate;
    positionUpdate.teamspeakId = (*it)->client->teamspeakId();
    positionUpdate.x = (*it)->offset.x;
    positionUpdate.y = (*it)->offset.y;
    positionUpdate.z = (*it)->offset.z;
    positionUpdate.voiceRange = 10;

    updatePacket.positionUpdates.push_back(positionUpdate);

    _relativeAudibleClients.insert(*it);
  }

  // send removed audible clients
  for (auto it = _removeAudibleClients.begin(); it != _removeAudibleClients.end(); it++) {
    // only mute if also not relative list
    if (isRelativeClient(*it) == false) {
      // add to update packet
      clientAudioUpdate_t audioUpdate;
      audioUpdate.teamspeakId = (*it)->teamspeakId();
      audioUpdate.muted = true;
      updatePacket.audioUpdates.push_back(audioUpdate);
    }

    _audibleClients.erase(*it);
  }

  for (auto it = _removeRelativeAudibleClients.begin(); it != _removeRelativeAudibleClients.end(); it++) {
    // only mute if also not in normal list
    if (_audibleClients.find(*it) == _audibleClients.end()) {
      // add update packet
      clientAudioUpdate_t audioUpdate;
      audioUpdate.teamspeakId = (*it)->teamspeakId();
      audioUpdate.muted = true;
      updatePacket.audioUpdates.push_back(audioUpdate);
    }

    auto removeIt = _relativeAudibleClients.begin();
    while (removeIt != _relativeAudibleClients.end()) {
      if ((*removeIt)->client == *it) {
        relativeClient_t *relativeClient = *removeIt;
        removeIt = _relativeAudibleClients.erase(removeIt);
        delete relativeClient;
      } else {
        removeIt++;
      }
    }
  }

  if (updatePacket.audioUpdates.empty() && updatePacket.positionUpdates.empty()) {
    return;
  }

  // send update packet
  std::ostringstream os;

  try {
    cereal::BinaryOutputArchive archive(os);
    archive(updatePacket);
  } catch (std::exception &e) {
    logMessage(e.what(), LOG_LEVEL_ERROR);
    return;
  }

  auto data = os.str();
  sendPacket((void *)data.c_str(), data.size(), NETWORK_UPDATE_CHANNEL);

  // clear update lists
  _addAudibleClients.clear();
  _removeAudibleClients.clear();
  _addRelativeAudibleClients.clear();
  _removeRelativeAudibleClients.clear();
}

void Client::sendPositions() {
  positionPacket_t packet;
  packet.x = _position.x;
  packet.y = _position.y;
  packet.z = _position.z;
  packet.rotation = _rotation;

  for (auto it = _audibleClients.begin(); it != _audibleClients.end(); it++) {
    if (isRelativeClient(*it)) {
      continue;
    }

    // calculate relative position
    float x = (*it)->position().x - _position.x;
    float y = (*it)->position().y - _position.y;

    float rotatedX = x * cos(_rotation) - y * sin(_rotation);
    float rotatedY = x * sin(_rotation) + y * cos(_rotation);

    rotatedX *= 10 / (*it)->voiceRange();
    rotatedY *= 10 / (*it)->voiceRange();

    // create update packet
    clientPositionUpdate_t positionUpdate;
    positionUpdate.teamspeakId = (*it)->teamspeakId();
    positionUpdate.x = rotatedX;
    positionUpdate.y = rotatedY;
    positionUpdate.z = 0;
    positionUpdate.voiceRange = (*it)->voiceRange();

    packet.positions.push_back(positionUpdate);
  }

  // serialize packet
  std::ostringstream os;

  try {
    cereal::BinaryOutputArchive archive(os);
    archive(packet);
  } catch (std::exception &e) {
    logMessage(e.what(), LOG_LEVEL_ERROR);
    return;
  }

  auto data = os.str();
  sendPacket((void *)data.c_str(), data.size(), NETWORK_POSITION_CHANNEL, false);
}

void Client::setPosition(linalg::aliases::float3 position) {
  if (_position == position) {
    return;
  }

  _position = position;
  _positionChanged = true;
}

linalg::aliases::float3 Client::position() const {
  return _position;
}

void Client::setRotation(float rotation) {
  if (_rotation == rotation) {
    return;
  }

  _rotation = rotation;
  _positionChanged = true;
}

float Client::rotation() const {
  return _rotation;
}

void Client::resetPositionChanged() {
  _positionChanged = false;
}

bool Client::positionChanged() const {
  return _positionChanged;
}

void Client::setVoiceRange(float range) {
  if (range == _voiceRange) {
    return;
  }

  _voiceRange = range;
  _positionChanged = true;
}

float Client::voiceRange() const {
  return _voiceRange;
}

void Client::setNickname(std::string nickname) {
  _nickname = nickname;

  sendControlMessage();
}

std::string Client::nickname() const {
  return _nickname;
}

void Client::sendControlMessage() {
  // create control packet
  controlPacket_t controlPacket;
  controlPacket.nickname = _nickname;

  // send update packet
  std::ostringstream os;

  try {
    cereal::BinaryOutputArchive archive(os);
    archive(controlPacket);
  } catch (std::exception &e) {
    logMessage(e.what(), LOG_LEVEL_ERROR);
    return;
  }

  auto data = os.str();
  sendPacket((void *)data.c_str(), data.size(), NETWORK_CONTROL_CHANNEL);
}

void Client::sendPacket(void *data, size_t length, int channel, bool reliable) {
  enet_uint32 flags = 0;

  if (reliable) {
    flags = ENET_PACKET_FLAG_RELIABLE;
  }

  ENetPacket *packet = enet_packet_create(data, (int)length, flags);
  enet_peer_send(_peer, (enet_uint8)channel, packet);
}

bool Client::isRelativeClient(Client *client) const {
  for (auto it = _relativeAudibleClients.begin(); it != _relativeAudibleClients.end(); it++) {
    if ((*it)->client == client) {
      return true;
    }
  }

  return false;
}
