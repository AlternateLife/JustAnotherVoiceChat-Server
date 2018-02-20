/*
 * File: include/server.h
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

#pragma once

#include "justAnotherVoiceChat.h"

#include <enet/enet.h>
#include <stdint.h>
#include <thread>
#include <vector>
#include <linalg.h>

namespace justAnotherVoiceChat {
  typedef bool (* ClientCallback_t)(uint16_t);
  typedef void (* ClientStatusCallback_t)(uint16_t, bool);

  class Client;

  class JUSTANOTHERVOICECHAT_API Server {
  private:
    ENetAddress _address;
    ENetHost *_server;

    std::thread *_thread;
    std::thread *_clientUpdateThread;
    std::vector<Client *> _clients;

    ClientCallback_t _clientConnectedCallback;
    ClientCallback_t _clientDisconnectedCallback;
    ClientStatusCallback_t _clientTalkingChangedCallback;
    ClientStatusCallback_t _clientSpeakersMuteChangedCallback;
    ClientStatusCallback_t _clientMicrophoneMuteChangedCallback;

    bool _running;
    float _distanceFactor;
    float _rolloffFactor;
    std::string _teamspeakServerId;
    uint64_t _teamspeakChannelId;
    std::string _teamspeakChannelPassword;

  public:
    Server(uint16_t port, std::string teamspeakServerId, uint64_t teamspeakChannelId, std::string teamspeakChannelPassword);
    virtual ~Server();

    bool create();
    void close();
    bool isRunning() const;

    std::string teamspeakServerId() const;
    uint64_t teamspeakChannelId() const;
    std::string teamspeakChannelPassword() const;

    uint16_t port() const;
    int maxClients() const;
    int numberOfClients() const;
    void removeClient(uint16_t gameId);
    bool removeAllClients();

    void setClientPosition(uint16_t gameId, linalg::aliases::float3 position, float rotation);
    void set3DSettings(float distanceFactor, float rolloffFactor);

    void registerClientConnectedCallback(ClientCallback_t callback);
    void unregisterClientConnectedCallback();
    ClientCallback_t clientConnectedCallback() const;

    void registerClientDisconnectedCallback(ClientCallback_t callback);
    void unregisterClientDisconnectedCallback();
    ClientCallback_t clientDisconnectedCallback() const;

    void registerClientTalkingChangedCallback(ClientStatusCallback_t callback);
    void unregisterClientTalkingChangedCallback();
    ClientStatusCallback_t clientTalkingChangedCallback() const;

    void registerClientSpeakersMuteChangedCallback(ClientStatusCallback_t callback);
    void unregisterClientSpeakersMuteChangedCallback();
    ClientStatusCallback_t clientSpeakersMuteChangedCallback() const;

    void registerClientMicrophoneMuteChangedCallback(ClientStatusCallback_t callback);
    void unregisterClientMicrophoneMuteChangedCallback();
    ClientStatusCallback_t clientMicrophoneMuteChangedCallback() const;

  private:
    void update();
    void updateClients();
    void abortThreads();

    Client *clientByGameId(uint16_t gameId) const;
    Client *clientByTeamspeakId(uint16_t teamspeakId) const;
    Client *clientByPeer(ENetPeer *peer) const;

    void onClientConnect(ENetEvent &event);
    void onClientDisconnect(ENetEvent &event);
    void onClientMessage(ENetEvent &event);

    void handleHandshake(ENetEvent &event);
    void sendHandshakeResponse(ENetPeer *peer, int statusCode, std::string reason);
  };
}
