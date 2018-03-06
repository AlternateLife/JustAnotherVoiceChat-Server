/*
 * File: src/api.cpp
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

#include "api.h"

#include "server.h"

#include <memory>
#include <mutex>

static std::shared_ptr<justAnotherVoiceChat::Server> _server = nullptr;
static std::mutex _serverMutex;

void JV_SetLogLevel(int logLevel) {
  setLogLevel(logLevel);
}

void JV_RegisterLogMessageCallback(logMessageCallback_t callback) {
  setLogMessageCallback(callback);
}

void JV_UnregisterLogMessageCallback() {
  setLogMessageCallback(0);
}

void JV_CreateServer(uint16_t port, const char *teamspeakServerId, uint64_t teamspeakChannelId, const char *teamspeakChannelPassword) {
  logMessage("Creating server", LOG_LEVEL_DEBUG);

  logMessage("Locking api server in JV_CreateServer", LOG_LEVEL_TRACE);
  std::lock_guard<std::mutex> guard(_serverMutex);
  logMessage("Locked api server in JV_CreateServer", LOG_LEVEL_TRACE);
  if (_server != nullptr) {
    logMessage("Server already created", LOG_LEVEL_WARNING);

    return;
  }

  _server = std::make_shared<justAnotherVoiceChat::Server>(port, std::string(teamspeakServerId), teamspeakChannelId, std::string(teamspeakChannelPassword));
}

void JV_DestroyServer() {
  logMessage("Destroying server", LOG_LEVEL_DEBUG);

  logMessage("Locking api server  in JV_DestroyServer", LOG_LEVEL_TRACE);
  std::lock_guard<std::mutex> guard(_serverMutex);
  logMessage("Locked api server in JV_DestroyServer", LOG_LEVEL_TRACE);
  if (_server == nullptr) {
    logMessage("Server already destroyed", LOG_LEVEL_WARNING);
    return;
  }

  _server = nullptr;
}

bool JV_StartServer() {
  logMessage("Starting server", LOG_LEVEL_DEBUG);

  logMessage("Locking api server in JV_StartServer", LOG_LEVEL_TRACE);
  std::lock_guard<std::mutex> guard(_serverMutex);
  logMessage("Locked api server in JV_StartServer", LOG_LEVEL_TRACE);
  if (_server == nullptr) {
    logMessage("Server not created", LOG_LEVEL_WARNING);
    return false;
  }

  return _server->create();
}

void JV_StopServer() {
  logMessage("Stopping server", LOG_LEVEL_DEBUG);

  logMessage("Locking api server in JV_StopServer", LOG_LEVEL_TRACE);
  std::lock_guard<std::mutex> guard(_serverMutex);
  logMessage("Locked api server in JV_StopServer", LOG_LEVEL_TRACE);
  if (_server == nullptr) {
    logMessage("Server not created", LOG_LEVEL_WARNING);
    return;
  }

  _server->close();
}

bool JV_IsServerRunning() {
  logMessage("Locking api server in JV_IsServerRunning", LOG_LEVEL_TRACE);
  std::lock_guard<std::mutex> guard(_serverMutex);
  logMessage("Locked api server in JV_IsServerRunning", LOG_LEVEL_TRACE);
  if (_server == nullptr) {
    return false;
  }

  return _server->isRunning();
}

void JV_RegisterClientConnectingCallback(JV_ClientConnectingCallback_t callback) {
  logMessage("Locking api server in JV_RegisterClientConnectingCallback", LOG_LEVEL_TRACE);
  std::lock_guard<std::mutex> guard(_serverMutex);
  logMessage("Locked api server in JV_RegisterClientConnectingCallback", LOG_LEVEL_TRACE);
  if (_server == nullptr) {
    return;
  }

  _server->registerClientConnectingCallback(callback);
}

void JV_UnregisterClientConnectingCallback() {
  logMessage("Locking api server in JV_UnregisterClientConnectingCallback", LOG_LEVEL_TRACE);
  std::lock_guard<std::mutex> guard(_serverMutex);
  logMessage("Locked api server in JV_UnregisterClientConnectingCallback", LOG_LEVEL_TRACE);
  if (_server == nullptr) {
    return;
  }

  _server->registerClientConnectedCallback(nullptr);
}

void JV_RegisterClientConnectedCallback(JV_ClientCallback_t callback) {
  logMessage("Locking api server in JV_RegisterClientConnectedCallback", LOG_LEVEL_TRACE);
  std::lock_guard<std::mutex> guard(_serverMutex);
  logMessage("Locked api server in JV_RegisterClientConnectedCallback", LOG_LEVEL_TRACE);
  if (_server == nullptr) {
    return;
  }

  _server->registerClientConnectedCallback(callback);
}

void JV_UnregisterClientConnectedCallback() {
  logMessage("Locking api server in JV_UnregisterClientConnectedCallback", LOG_LEVEL_TRACE);
  std::lock_guard<std::mutex> guard(_serverMutex);
  logMessage("Locked api server in JV_UnregisterClientConnectedCallback", LOG_LEVEL_TRACE);
  if (_server == nullptr) {
    return;
  }

  _server->registerClientConnectedCallback(nullptr);
}

void JV_RegisterClientRejectedCallback(JV_ClientRejectedCallback_t callback) {
  logMessage("Locking api server in JV_RegisterClientRejectedCallback", LOG_LEVEL_TRACE);
  std::lock_guard<std::mutex> guard(_serverMutex);
  logMessage("Locked api server in JV_RegisterClientRejectedCallback", LOG_LEVEL_TRACE);
  if (_server == nullptr) {
    return;
  }

  _server->registerClientRejectedCallback(callback);
}

void JV_UnregisterClientRejectedCallback() {
  logMessage("Locking api server in JV_UnregisterClientRejectedCallback", LOG_LEVEL_TRACE);
  std::lock_guard<std::mutex> guard(_serverMutex);
  logMessage("Locking api server in JV_UnregisterClientRejectedCallback", LOG_LEVEL_TRACE);
  if (_server == nullptr) {
    return;
  }

  _server->registerClientRejectedCallback(nullptr);
}

void JV_RegisterClientDisconnectedCallback(JV_ClientCallback_t callback) {
  logMessage("Locking api server in JV_RegisterClientDisconnectedCallback", LOG_LEVEL_TRACE);
  std::lock_guard<std::mutex> guard(_serverMutex);
  logMessage("Locked api server in JV_RegisterClientDisconnectedCallback", LOG_LEVEL_TRACE);
  if (_server == nullptr) {
    return;
  }

  _server->registerClientDisconnectedCallback(callback);
}

void JV_UnregisterClientDisconnectedCallback() {
  logMessage("Locking api server in JV_UnregisterClientDisconnectedCallback", LOG_LEVEL_TRACE);
  std::lock_guard<std::mutex> guard(_serverMutex);
  logMessage("Locked api server in JV_UnregisterClientDisconnectedCallback", LOG_LEVEL_TRACE);
  if (_server == nullptr) {
    return;
  }

  _server->registerClientDisconnectedCallback(nullptr);
}

void JV_RegisterClientTalkingChangedCallback(JV_ClientStatusCallback_t callback) {
  logMessage("Locking api server in JV_RegisterClientTalkingChangedCallback", LOG_LEVEL_TRACE);
  std::lock_guard<std::mutex> guard(_serverMutex);
  logMessage("Locked api server in JV_RegisterClientTalkingChangedCallback", LOG_LEVEL_TRACE);
  if (_server == nullptr) {
    return;
  }

  _server->registerClientTalkingChangedCallback(callback);
}

void JV_UnregisterClientTalkingChangedCallback() {
  logMessage("Locking api server in JV_RegisterClientTalkingChangedCallback", LOG_LEVEL_TRACE);
  std::lock_guard<std::mutex> guard(_serverMutex);
  logMessage("Locked api server in JV_UnregisterClientTalkingChangedCallback", LOG_LEVEL_TRACE);
  if (_server == nullptr) {
    return;
  }

  _server->registerClientTalkingChangedCallback(nullptr);
}

void JV_RegisterClientSpeakersMuteChangedCallback(JV_ClientStatusCallback_t callback) {
  logMessage("Locking api server in JV_RegisterClientSpeakersMuteChangedCallback", LOG_LEVEL_TRACE);
  std::lock_guard<std::mutex> guard(_serverMutex);
  logMessage("Locked api server in JV_RegisterClientSpeakersMuteChangedCallback", LOG_LEVEL_TRACE);
  if (_server == nullptr) {
    return;
  }

  _server->registerClientSpeakersMuteChangedCallback(callback);
}

void JV_UnregisterClientSpeakersMuteChangedCallback() {
  logMessage("Locking api server in JV_UnregisterClientSpeakersMuteChangedCallback", LOG_LEVEL_TRACE);
  std::lock_guard<std::mutex> guard(_serverMutex);
  logMessage("Locked api server in JV_UnregisterClientSpeakersMuteChangedCallback", LOG_LEVEL_TRACE);
  if (_server == nullptr) {
    return;
  }

  _server->registerClientSpeakersMuteChangedCallback(nullptr);
}

void JV_RegisterClientMicrophoneMuteChangedCallback(JV_ClientStatusCallback_t callback) {
  logMessage("Locking api server in JV_RegisterClientMicrophoneMuteChangedCallback", LOG_LEVEL_TRACE);
  std::lock_guard<std::mutex> guard(_serverMutex);
  logMessage("Locked api server in JV_RegisterClientMicrophoneMuteChangedCallback", LOG_LEVEL_TRACE);
  if (_server == nullptr) {
    return;
  }

  _server->registerClientMicrophoneMuteChangedCallback(callback);
}

void JV_UnregisterClientMicrophoneMuteChangedCallback() {
  logMessage("Locking api server in JV_UnregisterClientMicrophoneMuteChangedCallback", LOG_LEVEL_TRACE);
  std::lock_guard<std::mutex> guard(_serverMutex);
  logMessage("Locked api server in JV_UnregisterClientMicrophoneMuteChangedCallback", LOG_LEVEL_TRACE);
  if (_server == nullptr) {
    return;
  }

  _server->registerClientMicrophoneMuteChangedCallback(nullptr);
}

int JV_GetNumberOfClients() {
  logMessage("Locking api server in JV_GetNumberOfClients", LOG_LEVEL_TRACE);
  std::lock_guard<std::mutex> guard(_serverMutex);
  logMessage("Locked api server in JV_GetNumberOfClients", LOG_LEVEL_TRACE);
  if (_server == nullptr) {
    return 0;
  }

  return _server->numberOfClients();
}

void JV_GetClientGameIds(uint16_t *, size_t) {

}

bool JV_RemoveClient(uint16_t clientId) {
  logMessage("Locking api server in JV_RemoveClient", LOG_LEVEL_TRACE);
  std::lock_guard<std::mutex> guard(_serverMutex);
  logMessage("Locked api server in JV_RemoveClient", LOG_LEVEL_TRACE);
  if (_server == nullptr) {
    logMessage("JV server is not available", LOG_LEVEL_WARNING);
    return false;
  }

  return _server->removeClient(clientId);
}

void JV_RemoveAllClients() {
  logMessage("Locking api server in JV_RemoveAllClients", LOG_LEVEL_TRACE);
  std::lock_guard<std::mutex> guard(_serverMutex);
  logMessage("Locked api server in JV_RemoveAllClients", LOG_LEVEL_TRACE);
  if (_server == nullptr) {
    return;
  }

  _server->removeAllClients();
}

bool JV_SetClientPosition(uint16_t clientId, float x, float y, float z, float rotation) {
  logMessage("Locking api server in JV_SetClientPosition", LOG_LEVEL_TRACE);
  std::lock_guard<std::mutex> guard(_serverMutex);
  logMessage("Locked api server in JV_SetClientPosition", LOG_LEVEL_TRACE);
  if (_server == nullptr) {
    return false;
  }

  return _server->setClientPosition(clientId, linalg::aliases::float3(x, y, z), rotation);
}

bool JV_SetClientPositions(clientPosition_t *positionUpdates, int length) {
  logMessage("Locking api server in JV_SetClientPositions", LOG_LEVEL_TRACE);
  std::lock_guard<std::mutex> guard(_serverMutex);
  logMessage("Locked api server in JV_SetClientPositions", LOG_LEVEL_TRACE);
  if (_server == nullptr) {
    return false;
  }

  return _server->setClientPositions(positionUpdates, length);
}

bool JV_SetClientVoiceRange(uint16_t clientId, float voiceRange) {
  logMessage("Locking api server in JV_SetClientVoiceRange", LOG_LEVEL_TRACE);
  std::lock_guard<std::mutex> guard(_serverMutex);
  logMessage("Locked api server in JV_SetClientVoiceRange", LOG_LEVEL_TRACE);
  if (_server == nullptr) {
    return false;
  }

  return _server->setClientVoiceRange(clientId, voiceRange);
}

bool JV_SetClientNickname(uint16_t clientId, const char *nickname) {
  logMessage("Locking api server in JV_SetClientNickname", LOG_LEVEL_TRACE);
  std::lock_guard<std::mutex> guard(_serverMutex);
  logMessage("Locked api server in JV_SetClientNickname", LOG_LEVEL_TRACE);
  if (_server == nullptr) {
    return false;
  }

  return _server->setClientNickname(clientId, std::string(nickname));
}

void JV_Set3DSettings(float distanceFactor, float rolloffFactor) {
  logMessage("Locking api server in JV_Set3DSettings", LOG_LEVEL_TRACE);
  std::lock_guard<std::mutex> guard(_serverMutex);
  logMessage("Locked api server in JV_Set3DSettings", LOG_LEVEL_TRACE);
  if (_server == nullptr) {
    return;
  }

  _server->set3DSettings(distanceFactor, rolloffFactor);
}

bool JV_SetRelativePositionForClient(uint16_t listenerId, uint16_t speakerId, float x, float y, float z) {
  logMessage("Locking api server in JV_SetRelativePositionForClient", LOG_LEVEL_TRACE);
  std::lock_guard<std::mutex> guard(_serverMutex);
  logMessage("Locked api server in JV_SetRelativePositionForClient", LOG_LEVEL_TRACE);
  if (_server == nullptr) {
    return false;
  }

  return _server->setRelativePositionForClient(listenerId, speakerId, linalg::aliases::float3(x, y, z));
}

bool JV_ResetRelativePositionForClient(uint16_t listenerId, uint16_t speakerId) {
  logMessage("Locking api server in JV_ResetRelativePositionForClient", LOG_LEVEL_TRACE);
  std::lock_guard<std::mutex> guard(_serverMutex);
  logMessage("Locked api server in JV_ResetRelativePositionForClient", LOG_LEVEL_TRACE);
  if (_server == nullptr) {
    return false;
  }

  return _server->resetRelativePositionForClient(listenerId, speakerId);
}

bool JV_ResetAllRelativePositions(uint16_t clientId) {
  logMessage("Locking api server in JV_ResetAllRelativePositions", LOG_LEVEL_TRACE);
  std::lock_guard<std::mutex> guard(_serverMutex);
  logMessage("Locked api server in JV_ResetAllRelativePositions", LOG_LEVEL_TRACE);
  if (_server == nullptr) {
    return false;
  }

  return _server->resetAllRelativePositions(clientId);
}

bool JV_MuteClientForAll(uint16_t clientId, bool muted) {
  logMessage("Locking api server in JV_MuteClientForAll", LOG_LEVEL_TRACE);
  std::lock_guard<std::mutex> guard(_serverMutex);
  logMessage("Locked api server in JV_MuteClientForAll", LOG_LEVEL_TRACE);
  if (_server == nullptr) {
    return false;
  }

  return _server->muteClientForAll(clientId, muted);
}

bool JV_IsClientMutedForAll(uint16_t clientId) {
  logMessage("Locking api server in JV_MuteClientForAll", LOG_LEVEL_TRACE);
  std::lock_guard<std::mutex> guard(_serverMutex);
  logMessage("Locked api server in JV_MuteClientForAll", LOG_LEVEL_TRACE);
  if (_server == nullptr) {
    return false;
  }

  return _server->isClientMutedForAll(clientId);
}

bool JV_MuteClientForClient(uint16_t speakerId, uint16_t listenerId, bool muted) {
  logMessage("Locking api server in JV_MuteClientForClient", LOG_LEVEL_TRACE);
  std::lock_guard<std::mutex> guard(_serverMutex);
  logMessage("Locked api server in JV_MuteClientForClient", LOG_LEVEL_TRACE);
  if (_server == nullptr) {
    return false;
  }

  return _server->muteClientForClient(speakerId, listenerId, muted);
}

bool JV_IsClientMutedForClient(uint16_t speakerId, uint16_t listenerId) {
  logMessage("Locking api server in JV_IsClientMutedForClient", LOG_LEVEL_TRACE);
  std::lock_guard<std::mutex> guard(_serverMutex);
  logMessage("Locked api server in JV_IsClientMutedForClient", LOG_LEVEL_TRACE);
  if (_server == nullptr) {
    return false;
  }

  return _server->isClientMutedForClient(speakerId, listenerId);
}

bool JV_IsClientConnected(uint16_t gameId) {
  logMessage("Locking api server in JV_IsClientConnected", LOG_LEVEL_TRACE);
  std::lock_guard<std::mutex> guard(_serverMutex);
  logMessage("Locked api server in JV_IsClientConnected", LOG_LEVEL_TRACE);
  if (_server == nullptr) {
    return false;
  }

  return _server->isClientConnected(gameId);
}
