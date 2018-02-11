/*
 * File: Server.h
 * Date: 25.01.2018
 *
 * MIT License
 *
 * Copyright (c) 2018 AlternateVoice
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

#include "JustAnotherVoiceChat.h"

#include <enet/enet.h>
#include <stdint.h>

namespace JustAnotherVoiceChat {
  typedef bool (* ClientCallback_t)(uint16_t);
  typedef void (* ClientStatusCallback_t)(uint16_t, bool);

  class JUSTANOTHERVOICECHAT_API Server {
  private:
    ENetAddress _address;
    ENetHost *_server;

    ClientCallback_t _clientConnectedCallback;
    ClientCallback_t _clientDisconnectedCallback;
    ClientStatusCallback_t _clientTalkingChangedCallback;
    ClientStatusCallback_t _clientSpeakersMuteChangedCallback;
    ClientStatusCallback_t _clientMicrophoneMuteChangedCallback;

  public:
    Server(uint16_t port);
    virtual ~Server();

    bool create();
    void close();
    bool isRunning() const;

    uint16_t port() const;
    int maxClients() const;
    int numberOfClients() const;

    void muteClientForClient(uint16_t listenerId, uint16_t clientId, bool muted = true);
    void setClientPositionForClient(uint16_t listenerId, uint16_t clientId, float x, float y, float z);
    void setClientVolumeForClient(uint16_t listenerId, uint16_t clientId, float volume);

    void registerClientConnectedCallback(ClientCallback_t callback);
    void unregisterClientConnectedCallback();

    void registerClientDisconnectedCallback(ClientCallback_t callback);
    void unregisterClientDisconnectedCallback();

    void registerClientTalkingChangedCallback(ClientStatusCallback_t callback);
    void unregisterClientTalkingChangedCallback();

    void registerClientSpeakersMuteChangedCallback(ClientStatusCallback_t callback);
    void unregisterClientSpeakersMuteChangedCallback();

    void registerClientMicrophoneMuteChangedCallback(ClientStatusCallback_t callback);
    void unregisterClientMicrophoneMuteChangedCallback();
  };
}
