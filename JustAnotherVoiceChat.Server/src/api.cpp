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

#include <iostream>

static justAnotherVoiceChat::Server *_server = nullptr;

void JV_RegisterLogMessageCallback(logMessageCallback_t callback) {
  setLogMessageCallback(callback);
}

void JV_CreateServer(uint16_t port) {
  if (_server != nullptr) {
    return;
  }

  _server = new justAnotherVoiceChat::Server(port);
}

bool JV_StartServer() {
  if (_server == nullptr) {
    return false;
  }

  return _server->create();
}

void JV_StopServer() {
  if (_server == nullptr) {
    return;
  }

  _server->close();

  delete _server;
  _server = nullptr;
}

bool JV_IsServerRunning() {
  if (_server == nullptr) {
    return false;
  }

  return _server->isRunning();
}

void JV_RegisterClientConnectedCallback(JV_ClientCallback_t callback) {
  if (_server == nullptr) {
    return;
  }

  _server->registerClientConnectedCallback(callback);
}

void JV_UnregisterClientConnectedCallback() {
  if (_server == nullptr) {
    return;
  }

  _server->unregisterClientConnectedCallback();
}

void JV_RegisterClientDisconnectedCallback(JV_ClientCallback_t callback) {
  if (_server == nullptr) {
    return;
  }

  _server->registerClientDisconnectedCallback(callback);
}

void JV_UnregisterClientDisconnectedCallback() {
  if (_server == nullptr) {
    return;
  }

  _server->unregisterClientDisconnectedCallback();
}

void JV_RegisterClientTalkingChangedCallback(JV_ClientStatusCallback_t callback) {
  if (_server == nullptr) {
    return;
  }

  _server->registerClientTalkingChangedCallback(callback);
}

void JV_UnregisterClientTalkingChangedCallback() {
  if (_server == nullptr) {
    return;
  }

  _server->unregisterClientTalkingChangedCallback();
}

void JV_RegisterClientSpeakersMuteChangedCallback(JV_ClientStatusCallback_t callback) {
  if (_server == nullptr) {
    return;
  }

  _server->registerClientSpeakersMuteChangedCallback(callback);
}

void JV_UnregisterClientSpeakersMuteChangedCallback() {
  if (_server == nullptr) {
    return;
  }

  _server->unregisterClientSpeakersMuteChangedCallback();
}

void JV_RegisterClientMicrophoneMuteChangedCallback(JV_ClientStatusCallback_t callback) {
  if (_server == nullptr) {
    return;
  }

  _server->registerClientMicrophoneMuteChangedCallback(callback);
}

void JV_UnregisterClientMicrophoneMuteChangedCallback() {
  if (_server == nullptr) {
    return;
  }

  _server->unregisterClientMicrophoneMuteChangedCallback();
}

int JV_GetNumberOfClients() {
  if (_server == nullptr || _server->isRunning() == false) {
    return 0;
  }

  return _server->numberOfClients();
}

void JV_GetClientGameIds(uint16_t *gameIds, size_t maxLength) {

}

void JV_RemoveClient(uint16_t clientId) {
  if (_server == nullptr || _server->isRunning() == false) {
    return;
  }

  _server->removeClient(clientId);
}

void JV_RemoveAllClients() {
  if (_server == nullptr || _server->isRunning() == false) {
    return;
  }

  _server->removeAllClients();
}

void JV_SetClientPosition(uint16_t clientId, float x, float y, float z, float rotation) {
  if (_server == nullptr || _server->isRunning() == false) {
    return;
  }

  _server->setClientPosition(clientId, linalg::aliases::float3(x, y, z), rotation);
}

void JV_Set3DSettings(float distanceFactor, float rolloffFactor) {
  if (_server == nullptr) {
    return;
  }

  _server->set3DSettings(distanceFactor, rolloffFactor);
}

void JV_SetRelativePositionForClient(uint16_t listenerId, uint16_t speakerId, float x, float y, float z) {
  
}

void JV_ResetRelativePositionForClient(uint16_t listenerId, uint16_t speakerId) {

}

void JV_ResetAllRelativePositions(uint16_t clientId) {
  
}

void JVTest_CallClientConnectedCallback(uint16_t id) {
  if (_server == nullptr) {
    return;
  }

  auto callback = _server->clientConnectedCallback();

  if (callback != 0) {
    callback(id);
  }
}

void JVTest_CallClientDisconnectedCallback(uint16_t id) {
  if (_server == nullptr) {
    return;
  }

  auto callback = _server->clientDisconnectedCallback();

  if (callback != 0) {
    callback(id);
  }
}

void JVTest_CallClientTalkingChangedCallback(uint16_t id, bool state) {
  if (_server == nullptr) {
    return;
  }

  auto callback = _server->clientTalkingChangedCallback();

  if (callback != 0) {
    callback(id, state);
  }
}

void JVTest_CallClientSpeakersMuteChangedCallback(uint16_t id, bool state) {
  if (_server == nullptr) {
    return;
  }

  auto callback = _server->clientSpeakersMuteChangedCallback();

  if (callback != 0) {
    callback(id, state);
  }
}

void JVTest_CallClientMicrophoneMuteChangedCallback(uint16_t id, bool state) {
  if (_server == nullptr) {
    return;
  }

  auto callback = _server->clientMicrophoneMuteChangedCallback();

  if (callback != 0) {
    callback(id, state);
  }
}
