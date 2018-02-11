/*
 * File: Server.cpp
 * Date: 29.01.2018
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

#include "API.h"

#include "Server.h"

JV_ClientCallback_t _clientConnectedCallback = 0;
JV_ClientCallback_t _clientDisconnectedCallback = 0;
JV_ClientStatusCallback_t _clientTalkingChangedCallback = 0;
JV_ClientStatusCallback_t _clientSpeakersMuteChangedCallback = 0;
JV_ClientStatusCallback_t _clientMicrophoneMuteChangedCallback = 0;

JustAnotherVoiceChat::Server *_server = nullptr;

void JV_StartServer(uint16_t port) {
  if (_server != nullptr) {
    return;
  }

  _server = new JustAnotherVoiceChat::Server(port);
}

void JV_StopServer() {
  if (_server == nullptr) {
    return;
  }

  _server->close();
}

bool JV_IsServerRunning() {
  return (_server != nullptr && _server->isRunning());
}

void JV_RegisterClientConnectedCallback(JV_ClientCallback_t callback) {
  _clientConnectedCallback = callback;
}

void JV_UnregisterClientConnectedCallback() {
  _clientConnectedCallback = 0;
}

void JV_RegisterClientDisconnectedCallback(JV_ClientCallback_t callback) {
  _clientDisconnectedCallback = callback;
}

void JV_UnregisterClientDisconnectedCallback() {
  _clientDisconnectedCallback = 0;
}

void JV_RegisterClientTalkingChangedCallback(JV_ClientStatusCallback_t callback) {
  _clientTalkingChangedCallback = callback;
}

void JV_UnregisterClientTalkingChangedCallback() {
  _clientTalkingChangedCallback = 0;
}

void JV_RegisterClientSpeakersMuteChangedCallback(JV_ClientStatusCallback_t callback) {
  _clientSpeakersMuteChangedCallback = callback;
}

void JV_UnregisterClientSpeakersMuteChangedCallback() {
  _clientSpeakersMuteChangedCallback = 0;
}

void JV_RegisterClientMicrophoneMuteChangedCallback(JV_ClientStatusCallback_t callback) {
  _clientMicrophoneMuteChangedCallback = callback;
}

void JV_UnregisterClientMicrophoneMuteChangedCallback() {
  _clientMicrophoneMuteChangedCallback = 0;
}

int JV_GetNumberOfClients() {
  return 0;
}

void JV_GetClientIds(uint16_t *, size_t maxLength) {

}

void JV_RemoveClient(uint16_t clientId) {

}

void JV_RemoveAllClients() {

}

void JV_MuteClientForClient(uint16_t listenerId, uint16_t clientId, bool muted) {

}

void JV_SetClientPositionForClient(uint16_t listenerId, uint16_t clientId, float x, float y, float z) {

}

void JV_SetClientVolumeForClient(uint16_t listenerId, uint16_t clientId, float volume) {

}

void JV_SetListenerDirection(uint16_t clientId, float rotation) {

}

void JV_Set3DSettings(uint16_t clientId, float distanceFactor, float rolloffScale) {
  
}

void JVTest_CallClientConnectedCallback(uint16_t id) {
  if (_clientConnectedCallback != 0) {
    _clientConnectedCallback(id);
  }
}

void JVTest_CallClientDisconnectedCallback(uint16_t id) {
  if (_clientDisconnectedCallback != 0) {
    _clientDisconnectedCallback(id);
  }
}

void JVTest_CallClientTalkingChangedCallback(uint16_t id, bool state) {
  if (_clientTalkingChangedCallback != 0) {
    _clientTalkingChangedCallback(id, state);
  }
}

void JVTest_CallClientSpeakersMuteChangedCallback(uint16_t id, bool state) {
  if (_clientSpeakersMuteChangedCallback != 0) {
    _clientSpeakersMuteChangedCallback(id, state);
  }
}

void JVTest_CallClientMicrophoneMuteChangedCallback(uint16_t id, bool state) {
  if (_clientMicrophoneMuteChangedCallback != 0) {
    _clientMicrophoneMuteChangedCallback(id, state);
  }
}
