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

  _muted = false;
  _position.x = 0;
  _position.y = 0;
  _position.z = 0;
  _rotation = 0;
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
  std::lock_guard<std::mutex> guard(_peerMutex);

  if (_peer == nullptr) {
    return;
  }

  enet_peer_disconnect_now(_peer, 0);
  _peer = nullptr;
}

bool Client::isConnected() const {
  return _peer != nullptr;
}

void Client::cleanupKnownClient(std::shared_ptr<Client> client) {
  // remove client reference from muted list
  std::unique_lock<std::mutex> mutedGuard(_mutedClientsMutex);

  auto mutedIt = _mutedClients.begin();
  while (mutedIt != _mutedClients.end()) {
    if (*mutedIt == client) {
      mutedIt = _mutedClients.erase(mutedIt);
    } else {
      mutedIt++;
    }
  }

  mutedGuard.unlock();

  // remove client reference from audible lists
  std::lock_guard<std::mutex> guard(_audibleClientsMutex);

  auto addAudibleIt = _addAudibleClients.begin();
  while (addAudibleIt != _addAudibleClients.end()) {
    if (*addAudibleIt == client) {
      addAudibleIt = _addAudibleClients.erase(addAudibleIt);
    } else {
      addAudibleIt++;
    }
  }

  auto removeAudibleIt = _removeAudibleClients.begin();
  while (removeAudibleIt != _removeAudibleClients.end()) {
    if (*removeAudibleIt == client) {
      removeAudibleIt = _removeAudibleClients.erase(removeAudibleIt);
    } else {
      removeAudibleIt++;
    }
  }

  auto addRelativeAudibleIt = _addRelativeAudibleClients.begin();
  while (addRelativeAudibleIt != _addRelativeAudibleClients.end()) {
    if (*addRelativeAudibleIt.client == client) {
      addRelativeAudibleIt = _addRelativeAudibleClients.erase(addRelativeAudibleIt);
    } else {
      addRelativeAudibleIt++;
    }
  }

  auto removeRelativeAudibleIt = _removeRelativeAudibleClients.begin();
  while (removeRelativeAudibleIt != _removeRelativeAudibleClients.end()) {
    if (*removeRelativeAudibleIt.client == client) {
      removeRelativeAudibleIt = _removeRelativeAudibleClients.erase(removeRelativeAudibleIt);
    } else {
      removeRelativeAudibleIt++;
    }
  }

  auto audibleIt = _audibleClients.begin();
  while (audibleIt != _audibleClients.end()) {
    if (*audibleIt == client) {
      audibleIt = _audibleClients.erase(audibleIt);
    } else {
      audibleIt++;
    }
  }

  auto relativeIt = _relativeAudibleClients.begin();
  while (relativeIt != _relativeAudibleClients.end()) {
    if ((*relativeIt).client == client) {
      relativeIt = _relativeAudibleClients.erase(relativeIt);
    } else {
      relativeIt++;
    }
  }
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

std::string Client::endpoint() {
  std::lock_guard<std::mutex> guard(_peerMutex);

  if (_peer == nullptr) {
    return "";
  }

  // get ip and port from peer
  char ip[20];
  enet_address_get_host_ip(&(_peer->address), ip, 20);

  return std::string(ip) + ":" + std::to_string(_peer->address.port);
}

bool Client::isPeer(ENetPeer *peer) {
  std::lock_guard<std::mutex> guard(_peerMutex);

  return _peer == peer;
}

void Client::setMuted(bool muted) {
  _muted = muted;
}

bool Client::isMuted() const {
  return _muted;
}

void Client::setMutedClient(std::shared_ptr<Client> client, bool muted) {
  std::lock_guard<std::mutex> guard(_mutedClientsMutex);

  if (muted) {
    // search for already existing muted client
    for (auto it = _mutedClients.begin(); it != _mutedClients.end(); it++) {
      if (*it == client) {
        return;
      }
    }

    _mutedClients.push_back(client);
  } else {
    // erase all entries pointing to this client
    auto it = _mutedClients.begin();
    while (it != _mutedClients.end()) {
      if (*it == client) {
        it = _mutedClients.erase(it);
      } else {
        it++;
      }
    }
  }
}

bool Client::isMutedClient(std::shared_ptr<Client> client) {
  std::lock_guard<std::mutex> guard(_mutedClientsMutex);

  for (auto it = _mutedClients.begin(); it != _mutedClients.end(); it++) {
    if (*it == client) {
      return true;
    }
  }
    
  return false;
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

void Client::addAudibleClient(std::shared_ptr<Client> client) {
  std::unique_lock<std::mutex> muteGuard(_mutedClientsMutex);

  if (client == nullptr || client->isMuted()) {
    return;
  }

  for (auto it = _mutedClients.begin(); it != _mutedClients.end(); it++) {
    if (*it == client) {
      return;
    }
  }

  muteGuard.unlock();

  std::lock_guard<std::mutex> guard(_audibleClientsMutex);

  for (auto it = _audibleClients.begin(); it != _audibleClients.end(); it++) {
    if (*it == client) {
      return;
    }
  }

  _addAudibleClients.push_back(client);
}

void Client::removeAudibleClient(std::shared_ptr<Client> client) {
  std::lock_guard<std::mutex> guard(_audibleClientsMutex);

  for (auto it = _audibleClients.begin(); it != _audibleClients.end(); it++) {
    if (*it == client) {
      _removeAudibleClients.push_back(client);
      return;
    }
  }
}

void Client::addRelativeAudibleClient(std::shared_ptr<Client> client, linalg::aliases::float3 position) {
  std::unique_lock<std::mutex> muteGuard(_mutedClientsMutex);

  if (client->isMuted()) {
    return;
  }

  for (auto it = _mutedClients.begin(); it != _mutedClients.end(); it++) {
    if (*it == client) {
      return;
    }
  }

  muteGuard.unlock();

  std::lock_guard<std::mutex> guard(_audibleClientsMutex);

  if (isRelativeClient(client)) {
    return;
  }

  _addRelativeAudibleClients.push_back(relativeClient_t());
  _addRelativeAudibleClients.back().client = client;
  _addRelativeAudibleClients.back().offset = position;
}

void Client::removeRelativeAudibleClient(std::shared_ptr<Client> client) {
  std::lock_guard<std::mutex> guard(_audibleClientsMutex);

  if (isRelativeClient(client) == false) {
    return;
  }

  _removeRelativeAudibleClients.push_back(client);
}

void Client::removeAllRelativeAudibleClients() {
  std::lock_guard<std::mutex> guard(_audibleClientsMutex);

  for (auto it = _relativeAudibleClients.begin(); it != _relativeAudibleClients.end(); it++) {
    _removeRelativeAudibleClients.push_back((*it).client);
  }
}

void Client::sendUpdate() {
  // create update packet
  updatePacket_t updatePacket;

  // send new audible clients
  std::unique_lock<std::mutex> guard(_audibleClientsMutex);

  for (auto it = _addAudibleClients.begin(); it != _addAudibleClients.end(); it++) {
    if (*it == nullptr) {
      continue;
    }

    if (isRelativeClient(*it) == false) {
      // add to update packet
      clientAudioUpdate_t audioUpdate;
      audioUpdate.teamspeakId = (*it)->teamspeakId();
      audioUpdate.muted = false;
      updatePacket.audioUpdates.push_back(audioUpdate);
    }

    _audibleClients.push_back(*it);
  }

  for (auto it = _addRelativeAudibleClients.begin(); it != _addRelativeAudibleClients.end(); it++) {
    if ((*it).client == nullptr) {
      continue;
    }

    // only unmute if also not normal list
    bool unmute = true;

    for (auto audibleIt = _audibleClients.begin(); audibleIt != _audibleClients.end(); audibleIt++) {
      if ((*it).client == *audibleIt) {
        unmute = false;
        break;
      }
    }

    if (unmute) {
      // add update packet
      clientAudioUpdate_t audioUpdate;
      audioUpdate.teamspeakId = (*it).client->teamspeakId();
      audioUpdate.muted = false;
      updatePacket.audioUpdates.push_back(audioUpdate);
    }

    // send offset once
    clientPositionUpdate_t positionUpdate;
    positionUpdate.teamspeakId = (*it).client->teamspeakId();
    positionUpdate.x = (*it).offset.x;
    positionUpdate.y = (*it).offset.y;
    positionUpdate.z = (*it).offset.z;
    positionUpdate.voiceRange = 10;

    updatePacket.positionUpdates.push_back(positionUpdate);

    _relativeAudibleClients.push_back(*it);
  }

  // send removed audible clients
  for (auto it = _removeAudibleClients.begin(); it != _removeAudibleClients.end(); it++) {
    if (*it == nullptr) {
      continue;
    }

    // only mute if also not relative list
    if (isRelativeClient(*it) == false) {
      // add to update packet
      clientAudioUpdate_t audioUpdate;
      audioUpdate.teamspeakId = (*it)->teamspeakId();
      audioUpdate.muted = true;
      updatePacket.audioUpdates.push_back(audioUpdate);
    }

    auto removeIt = _audibleClients.begin();
    while (removeIt != _audibleClients.end()) {
      if (*removeIt == *it) {
        removeIt = _audibleClients.erase(removeIt);
      } else {
        removeIt++;
      }
    }
  }

  for (auto it = _removeRelativeAudibleClients.begin(); it != _removeRelativeAudibleClients.end(); it++) {
    if (*it == nullptr) {
      continue;
    }

    // only mute if also not in normal list
    bool mute = true;

    for (auto audibleIt = _audibleClients.begin(); audibleIt != _audibleClients.end(); audibleIt++) {
      if (*it == *audibleIt) {
        mute = false;
        break;
      }
    }

    if (mute) {
      // add update packet
      clientAudioUpdate_t audioUpdate;
      audioUpdate.teamspeakId = (*it)->teamspeakId();
      audioUpdate.muted = true;
      updatePacket.audioUpdates.push_back(audioUpdate);
    }

    auto removeIt = _relativeAudibleClients.begin();
    while (removeIt != _relativeAudibleClients.end()) {
      if ((*removeIt).client == *it) {
        removeIt = _relativeAudibleClients.erase(removeIt);
      } else {
        removeIt++;
      }
    }
  }

  if (updatePacket.audioUpdates.empty() && updatePacket.positionUpdates.empty()) {
    // clear update lists
    _addAudibleClients.clear();
    _removeAudibleClients.clear();
    _addRelativeAudibleClients.clear();
    _removeRelativeAudibleClients.clear();

    guard.unlock();

    return;
  }

  guard.unlock();

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
  guard.lock();

  _addAudibleClients.clear();
  _removeAudibleClients.clear();
  _addRelativeAudibleClients.clear();
  _removeRelativeAudibleClients.clear();

  guard.unlock();
}

void Client::sendPositions() {
  positionPacket_t packet;
  packet.x = _position.x;
  packet.y = _position.y;
  packet.z = _position.z;
  packet.rotation = _rotation;

  std::unique_lock<std::mutex> guard(_audibleClientsMutex);

  for (auto it = _audibleClients.begin(); it != _audibleClients.end(); it++) {
    if (*it == nullptr) {
      continue;
    }

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

  guard.unlock();

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
  std::lock_guard<std::mutex> guard(_peerMutex);

  if (_peer == nullptr) {
    return;
  }

  enet_uint32 flags = 0;

  if (reliable) {
    flags = ENET_PACKET_FLAG_RELIABLE;
  }

  ENetPacket *packet = enet_packet_create(data, (int)length, flags);
  enet_peer_send(_peer, (enet_uint8)channel, packet);
}

bool Client::isRelativeClient(std::shared_ptr<Client> client) const {
  for (auto it = _relativeAudibleClients.begin(); it != _relativeAudibleClients.end(); it++) {
    if ((*it).client == client) {
      return true;
    }
  }

  return false;
}
