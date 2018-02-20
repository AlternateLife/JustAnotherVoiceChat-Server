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

#include "../thirdparty/JustAnotherVoiceChat/include/protocol.h"

using namespace justAnotherVoiceChat;

Client::Client(ENetPeer *peer, uint16_t gameId, uint16_t teamspeakId) {
  _peer = peer;
  _gameId = gameId;
  _teamspeakId = teamspeakId;

  _talking = false;
  _microphoneMuted = false;
  _speakersMuted = false;
  _positionChanged = false;
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

void Client::sendUpdate() {
  // create update packet
  updatePacket_t updatePacket;

  // send new audible clients
  for (auto it = _addAudibleClients.begin(); it != _addAudibleClients.end(); it++) {
    // add to update packet
    clientAudioUpdate_t audioUpdate;
    audioUpdate.teamspeakId = (*it)->teamspeakId();
    audioUpdate.muted = false;
    updatePacket.audioUpdates.push_back(audioUpdate);

    _audibleClients.insert(*it);
  }

  // send removed audible clients
  for (auto it = _removeAudibleClients.begin(); it != _removeAudibleClients.end(); it++) {
    // add to update packet
    clientAudioUpdate_t audioUpdate;
    audioUpdate.teamspeakId = (*it)->teamspeakId();
    audioUpdate.muted = true;
    updatePacket.audioUpdates.push_back(audioUpdate);

    _audibleClients.erase(*it);
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
}

void Client::setPosition(linalg::aliases::float3 position) {
  _position = position;
  _positionChanged = true;
}

linalg::aliases::float3 Client::position() const {
  return _position;
}

void Client::setRotation(float rotation) {
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

void Client::sendPacket(void *data, size_t length, int channel, bool reliable) {
  enet_uint32 flags;

  if (reliable) {
    flags = ENET_PACKET_FLAG_RELIABLE;
  }

  ENetPacket *packet = enet_packet_create(data, length, flags);
  enet_peer_send(_peer, channel, packet);
}
