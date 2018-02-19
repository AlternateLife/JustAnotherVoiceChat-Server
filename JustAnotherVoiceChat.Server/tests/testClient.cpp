/*
 * File: tests/testClient.cpp
 * Date: 09.02.2018
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

#include "testClient.h"

#include "../thirdparty/JustAnotherVoiceChat/include/protocol.h"

TestClient::TestClient(uint16_t gameId, uint16_t teamspeakId) {
  _client = nullptr;
  _peer = nullptr;
  _gameId = gameId;
  _teamspeakId = teamspeakId;
}

TestClient::~TestClient() {
  if (_peer != nullptr) {
    enet_peer_reset(_peer);
  }

  if (_client != nullptr) {
    enet_host_destroy(_client);
  }
}

bool TestClient::connect(std::string host, uint16_t port) {
  // create host
  _client = enet_host_create(NULL, 1, NETWORK_CHANNELS, 0, 0);
  if (_client == NULL) {
    _client = nullptr;
    return false;
  }

  ENetAddress address;
  enet_address_set_host(&address, host.c_str());
  address.port = port;

  // connect
  _peer = enet_host_connect(_client, &address, NETWORK_CHANNELS, 0);
  if (_peer == NULL) {
    _peer = nullptr;
    return false;
  }

  return true;
}

void TestClient::disconnect() {
  enet_peer_disconnect(_peer, 0);
}

bool TestClient::isConnected() const {
  return _client != nullptr && _peer != nullptr;
}

void TestClient::sendHandshake(uint16_t gameId, uint16_t teamspeakId) {
  handshakePacket_t packet;
  packet.gameId = gameId;
  packet.teamspeakId = teamspeakId;

  std::ostringstream os;
  {
    cereal::BinaryOutputArchive archive(os);
    archive(packet);
  }

  auto data = os.str();
  sendPacket((void *)data.c_str(), data.size(), NETWORK_HANDSHAKE_CHANNEL);
}

void TestClient::update() {
  ENetEvent event;

  if (enet_host_service(_client, &event, 100) > 0) {
    switch (event.type) {
      case ENET_EVENT_TYPE_CONNECT:
        sendHandshake(_gameId, _teamspeakId);
        break;

      case ENET_EVENT_TYPE_DISCONNECT:

        break;

      case ENET_EVENT_TYPE_RECEIVE:

        break;

      default:

        break;
    }
  }
}

void TestClient::sendPacket(void *data, size_t length, int channel, bool reliable) {
  enet_uint32 flags = 0;

  if (reliable) {
    flags |= ENET_PACKET_FLAG_RELIABLE;
  }

  ENetPacket *packet = enet_packet_create(data, length, flags);
  enet_peer_send(_peer, channel, packet);
}
