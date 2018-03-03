/*
 * File: include/client.h
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

#pragma once

#include "justAnotherVoiceChat.h"

#include <string>
#include <enet/enet.h>
#include <linalg.h>
#include <vector>
#include <mutex>
#include <memory>

namespace justAnotherVoiceChat {
  class JUSTANOTHERVOICECHAT_API Client {
  private:
    typedef struct {
      std::shared_ptr<Client> client;
      linalg::aliases::float3 offset;
    } relativeClient_t;

    ENetPeer *_peer;
    uint16_t _gameId;
    uint16_t _teamspeakId;

    linalg::aliases::float3 _position;
    float _rotation;
    bool _positionChanged;
    float _voiceRange;
    std::vector<std::shared_ptr<Client>> _audibleClients;
    std::vector<std::shared_ptr<Client>> _addAudibleClients;
    std::vector<std::shared_ptr<Client>> _removeAudibleClients;

    std::vector<relativeClient_t> _relativeAudibleClients;
    std::vector<relativeClient_t> _addRelativeAudibleClients;
    std::vector<std::shared_ptr<Client>> _removeRelativeAudibleClients;

    bool _talking;
    bool _microphoneMuted;
    bool _speakersMuted;
    std::string _nickname;

    bool _muted;
    std::vector<std::shared_ptr<Client>> _mutedClients;

    std::mutex _audibleClientsMutex;
    std::mutex _mutedClientsMutex;
    std::mutex _peerMutex;

  public:
    Client(ENetPeer *peer, uint16_t gameId, uint16_t teamspeakId);
    virtual ~Client();

    uint16_t gameId() const;
    uint16_t teamspeakId() const;

    void disconnect();
    bool isConnected() const;

    void cleanupKnownClient(std::shared_ptr<Client> client);

    bool isTalking() const;
    bool hasMicrophoneMuted() const;
    bool hasSpeakersMuted() const;
    std::string endpoint();
    bool isPeer(ENetPeer *peer);

    void setMuted(bool muted);
    bool isMuted() const;
    void setMutedClient(std::shared_ptr<Client> client, bool muted);
    bool isMutedClient(std::shared_ptr<Client> client);

    bool handleHandshake(ENetPacket *packet);
    bool handleStatus(ENetPacket *packet, bool *talkingChanged, bool *microphoneChanged, bool *speakersChanged);

    void addAudibleClient(std::shared_ptr<Client> client);
    void removeAudibleClient(std::shared_ptr<Client> client);
    void addRelativeAudibleClient(std::shared_ptr<Client> client, linalg::aliases::float3 position);
    void removeRelativeAudibleClient(std::shared_ptr<Client> client);
    void removeAllRelativeAudibleClients();

    void sendUpdate();
    void sendPositions();

    void setPosition(linalg::aliases::float3 position);
    linalg::aliases::float3 position() const;
    void setRotation(float rotation);
    float rotation() const;
    void resetPositionChanged();
    bool positionChanged() const;
    void setVoiceRange(float range);
    float voiceRange() const;

    void setNickname(std::string nickname);
    std::string nickname() const;

  private:
    void sendControlMessage();
    void sendPacket(void *data, size_t length, int channel, bool reliable = true);
  
    bool isRelativeClient(std::shared_ptr<Client> client) const;
  };
}
