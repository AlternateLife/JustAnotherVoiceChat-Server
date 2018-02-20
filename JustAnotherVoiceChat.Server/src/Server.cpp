/*
 * File: src/server.cpp
 * Date: 25.01.2018
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

#include "server.h"

#include "internal.h"
#include "client.h"
#include "log.h"

#include "../thirdparty/JustAnotherVoiceChat/include/protocol.h"

#include <iostream>

using namespace justAnotherVoiceChat;

Server::Server(uint16_t port) {
  _address.host = ENET_HOST_ANY;
  _address.port = port;

  _server = nullptr;
  _thread = nullptr;
  _running = false;
  _distanceFactor = 1;
  _rolloffFactor = 1;

  _clientConnectedCallback = nullptr;
  _clientDisconnectedCallback = nullptr;
  _clientTalkingChangedCallback = nullptr;
  _clientMicrophoneMuteChangedCallback = nullptr;
  _clientSpeakersMuteChangedCallback = nullptr;

  initialize();
}

Server::~Server() {
  close();
}

bool Server::create() {
  if (isInitialized() == false) {
    return false;
  }

  if (_server != nullptr) {
    return false;
  }

  _server = enet_host_create(&_address, maxClients(), NETWORK_CHANNELS, 0, 0);
  if (_server == NULL) {
    _server = nullptr;
    return false;
  }

  if (_thread != nullptr) {
    delete _thread;
  }

  _running = true;
  _thread = new std::thread(&Server::update, this);
  _clientUpdateThread = new std::thread(&Server::updateClients, this);

  return true;
}

void Server::close() {
  if (removeAllClients()) {
    // wait for clients to gracefully disconnect
    std::this_thread::sleep_for(std::chrono::seconds(3));
  }

  abortThreads();

  if (_server != nullptr) {
    enet_host_destroy(_server);
    _server = nullptr;
  }
}

bool Server::isRunning() const {
  return (_server != nullptr && _running);
}

uint16_t Server::port() const {
  return _address.port;
}

int Server::maxClients() const {
  return 256;
}

int Server::numberOfClients() const {
  return _clients.size();
}

void Server::removeClient(uint16_t gameId) {
  auto client = clientByGameId(gameId);
  if (client == nullptr) {
    return;
  }

  client->disconnect();
}

bool Server::removeAllClients() {
  if (_clients.empty()) {
    return false;
  }

  for (auto it = _clients.begin(); it != _clients.end(); it++) {
    (*it)->disconnect();
  }

  return true;
}

void Server::setClientPosition(uint16_t gameId, linalg::aliases::float3 position, float rotation) {
  auto client = clientByGameId(gameId);
  if (client == nullptr) {
    return;
  }

  client->setPosition(position);
  client->setRotation(rotation);
}

void Server::set3DSettings(float distanceFactor, float rolloffFactor) {
  _distanceFactor = distanceFactor;
  _rolloffFactor = rolloffFactor;

  // TODO: Update settings on clients
}

void Server::registerClientConnectedCallback(ClientCallback_t callback) {
  _clientConnectedCallback = callback;
}

void Server::unregisterClientConnectedCallback() {
  _clientConnectedCallback = nullptr;
}

ClientCallback_t Server::clientConnectedCallback() const {
  return _clientConnectedCallback;
}

void Server::registerClientDisconnectedCallback(ClientCallback_t callback) {
  _clientDisconnectedCallback = callback;
}

void Server::unregisterClientDisconnectedCallback() {
  _clientDisconnectedCallback = nullptr;
}

ClientCallback_t Server::clientDisconnectedCallback() const {
  return _clientDisconnectedCallback;
}

void Server::registerClientTalkingChangedCallback(ClientStatusCallback_t callback) {
  _clientTalkingChangedCallback = callback;
}

void Server::unregisterClientTalkingChangedCallback() {
  _clientTalkingChangedCallback = nullptr;
}

ClientStatusCallback_t Server::clientTalkingChangedCallback() const {
  return _clientTalkingChangedCallback;
}

void Server::registerClientSpeakersMuteChangedCallback(ClientStatusCallback_t callback) {
  _clientSpeakersMuteChangedCallback = callback;
}

void Server::unregisterClientSpeakersMuteChangedCallback() {
  _clientSpeakersMuteChangedCallback = nullptr;
}

ClientStatusCallback_t Server::clientSpeakersMuteChangedCallback() const {
  return _clientSpeakersMuteChangedCallback;
}

void Server::registerClientMicrophoneMuteChangedCallback(ClientStatusCallback_t callback) {
  _clientMicrophoneMuteChangedCallback = callback;
}

void Server::unregisterClientMicrophoneMuteChangedCallback() {
  _clientMicrophoneMuteChangedCallback = nullptr;
}

ClientStatusCallback_t Server::clientMicrophoneMuteChangedCallback() const {
  return _clientMicrophoneMuteChangedCallback;
}

void Server::update() {
  ENetEvent event;

  while (_running) {
    int code = enet_host_service (_server, &event, 100);

    if (code > 0) {
      switch (event.type) {
        case ENET_EVENT_TYPE_CONNECT:
          onClientConnect(event);
          break;

        case ENET_EVENT_TYPE_DISCONNECT:
          onClientDisconnect(event);
          break;

        case ENET_EVENT_TYPE_RECEIVE:
          onClientMessage(event);
          break;

        default:
          break;
      }
    } else if (code < 0) {
      logMessage("Network error: " + std::to_string(code), LOG_LEVEL_ERROR);
    }
  }
}

void Server::updateClients() {
  while (_running) {
    // calculate update for clients
    for (auto it = _clients.begin(); it != _clients.end(); it++) {
      // calculate update packet for this client
      Client *client = *it;

      for (auto clientIt = _clients.begin(); clientIt != _clients.end(); clientIt++) {
        if ((*clientIt)->positionChanged() == false) {
          continue;
        }

        if (linalg::distance((*clientIt)->position(), client->position()) < 10) {
          client->addAudibleClient(*clientIt);
        } else {
          client->removeAudibleClient(*clientIt);
        }
      }

      // create update packet
      client->sendUpdate();
    }

    // reset all position flags
    for (auto it = _clients.begin(); it != _clients.end(); it++) {
      (*it)->resetPositionChanged(); 
    }

    // wait for next update
    std::this_thread::sleep_for(std::chrono::milliseconds(50));
  }
}

void Server::abortThreads() {
  _running = false;

  if (_thread != nullptr) {
    _thread->join();

    delete _thread;
    _thread = nullptr;
  }

  if (_clientUpdateThread != nullptr) {
    _clientUpdateThread->join();

    delete _clientUpdateThread;
    _clientUpdateThread = nullptr;
  }
}

Client *Server::clientByGameId(uint16_t gameId) const {
  for (auto it = _clients.begin(); it != _clients.end(); it++) {
    if ((*it)->gameId() == gameId) {
      return *it;
    }
  }

  return nullptr;
}

Client *Server::clientByTeamspeakId(uint16_t teamspeakId) const {
  for (auto it = _clients.begin(); it != _clients.end(); it++) {
    if ((*it)->teamspeakId() == teamspeakId) {
      return *it;
    }
  }

  return nullptr;
}

Client *Server::clientByPeer(ENetPeer *peer) const {
  for (auto it = _clients.begin(); it != _clients.end(); it++) {
    if ((*it)->peer() == peer) {
      return *it;
    }
  }

  return nullptr;
}

void Server::onClientConnect(ENetEvent &event) {
  // get client ip
  char ip[20];
  enet_address_get_host_ip(&(event.peer->address), ip, 20);

  logMessage(std::string("New client connected ") + ip + ":" + std::to_string(event.peer->address.port), LOG_LEVEL_INFO);

  // get client
  auto client = clientByPeer(event.peer);
  if (client != nullptr) {
    logMessage("Client with that peer is already in list", LOG_LEVEL_WARNING);
    return;
  }

  client = new Client(event.peer);
  _clients.push_back(client);
}

void Server::onClientDisconnect(ENetEvent &event) {
  // get client ip
  char ip[20];
  enet_address_get_host_ip(&(event.peer->address), ip, 20);

  logMessage(std::string("Client disconnected ") + ip + ":" + std::to_string(event.peer->address.port), LOG_LEVEL_INFO);

  auto it = _clients.begin();
  while (it != _clients.end()) {
    if ((*it)->peer() == event.peer) {
      // send callback
      if (_clientDisconnectedCallback != nullptr) {
        _clientDisconnectedCallback((*it)->gameId());
      }

      it = _clients.erase(it);
    } else {
      it++;
    }
  }
}

void Server::onClientMessage(ENetEvent &event) {
  logMessage("Message received on channel " + std::to_string(event.channelID), LOG_LEVEL_DEBUG);

  // get client for message
  auto client = clientByPeer(event.peer);
  if (client == nullptr) {
    logMessage("Client not found for peer", LOG_LEVEL_WARNING);
    enet_packet_destroy(event.packet);
    return;
  }

  switch (event.channelID) {
    case NETWORK_HANDSHAKE_CHANNEL:
      if (client->handleHandshake(event.packet)) {
        if (client->teamspeakId() != 0) {
          // new client connected
          logMessage("New client established " + std::to_string(client->gameId()) + " " + std::to_string(client->teamspeakId()), LOG_LEVEL_INFO);

          if (_clientConnectedCallback != nullptr) {
            _clientConnectedCallback(client->gameId());
          }
        }
      }
      break;

    case NETWORK_STATUS_CHANNEL:
      bool talkingChanged;
      bool microphoneChanged;
      bool speakersChanged;

      if (client->handleStatus(event.packet, &talkingChanged, &microphoneChanged, &speakersChanged)) {
        // status changed, call callbacks
        if (talkingChanged && _clientTalkingChangedCallback != nullptr) {
          _clientTalkingChangedCallback(client->gameId(), client->isTalking());
        }

        if (microphoneChanged && _clientMicrophoneMuteChangedCallback != nullptr) {
          _clientMicrophoneMuteChangedCallback(client->gameId(), client->hasMicrophoneMuted());
        }

        if (speakersChanged && _clientSpeakersMuteChangedCallback != nullptr) {
          _clientSpeakersMuteChangedCallback(client->gameId(), client->hasSpeakersMuted());
        }
      }
      break;

    default:
      logMessage("Unhandled message received", LOG_LEVEL_WARNING);
      break;
  }

  enet_packet_destroy(event.packet);
}
