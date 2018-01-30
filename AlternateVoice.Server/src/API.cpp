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

#include <enet/enet.h>

AV_ClientCallback_t _newClientCallback = 0;
AV_ClientCallback_t _clientDisconnectCallback = 0;
AV_ClientCallback_t _clientStartsTalkingCallback = 0;
AV_ClientCallback_t _clientStopsTalkingCallback = 0;
AV_ClientMuteCallback_t _clientSpeakersMuteChangedCallback = 0;
AV_ClientMuteCallback_t _clientMicrophoneMuteChangedCallback = 0;

AlternateVoice::Server *_server = nullptr;

void AV_StartServer(const char *hostname, uint16_t port, int channelId) {
  if (_server != nullptr) {
    return;
  }

  _server = new AlternateVoice::Server(port);
}

void AV_StopServer() {
  if (_server == nullptr) {
    return;
  }

  _server->close();
}

bool AV_IsServerRunning() {
  return (_server != nullptr && _server->isRunning());
}

void AV_RegisterNewClientCallback(AV_ClientCallback_t callback) {
  _newClientCallback = callback;
}

void AV_UnregisterNewClientCallback() {
  _newClientCallback = 0;
}

void AV_ReigsterClientDisconnectCallback(AV_ClientCallback_t callback) {
  _clientDisconnectCallback = callback;
}

void AV_UnregisterClientDisconnectCallback() {
  _clientDisconnectCallback = 0;
}

void AV_RegisterClientStartsTalkingCallback(AV_ClientCallback_t callback) {
  _clientStartsTalkingCallback = callback;
}

void AV_UnregisterClientStartsTalkingCallback() {
  _clientStartsTalkingCallback = 0;
}

void AV_RegisterClientStopsTalkingCallback(AV_ClientCallback_t callback) {
  _clientStopsTalkingCallback = callback;
}

void AV_UnregisterClientStopsTalkingCallback() {
  _clientStopsTalkingCallback = 0;
}

void AV_RegisterClientSpeakersMuteChangedCallback(AV_ClientMuteCallback_t callback) {
  _clientSpeakersMuteChangedCallback = callback;
}

void AV_UnregisterClientSpeakersMuteChangedCallback() {
  _clientSpeakersMuteChangedCallback = 0;
}

void AV_RegisterClientMicrophoneMuteChangedCallback(AV_ClientMuteCallback_t callback) {
  _clientMicrophoneMuteChangedCallback = callback;
}

void AV_UnregisterClientMicrophoneMuteChangedCallback() {
  _clientMicrophoneMuteChangedCallback = 0;
}

int AV_GetNumberOfClients() {
  return 0;
}

void AV_GetClientIds(uint16_t *, size_t maxLength) {

}

void AV_RemoveClient(uint16_t clientId) {

}

void AV_RemoveAllClients() {

}

void AV_MuteClientForClient(uint16_t listenerId, uint16_t clientId, bool muted) {

}

void AV_SetClientPositionForClient(uint16_t listenerId, uint16_t clientId, float x, float y, float z) {

}

void AV_SetClientVolumeForClient(uint16_t listenerId, uint16_t clientId, float volume) {

}

void AVTest_CallNewClientCallback(uint16_t id) {
  if (_newClientCallback != 0) {
    _newClientCallback(id);
  }
}

void AVTest_CallClientDisconnectCallback(uint16_t id) {
  if (_clientDisconnectCallback != 0) {
    _clientDisconnectCallback(id);
  }
}

void AVTest_CallClientStartsTalkingCallback(uint16_t id) {
  if (_clientStartsTalkingCallback != 0) {
    _clientStartsTalkingCallback(id);
  }
}

void AVTest_CallClientStopsTalkingCallback(uint16_t id) {
  if (_clientStopsTalkingCallback != 0) {
    _clientStopsTalkingCallback(id);
  }
}

void AVTest_CallClientSpeakersMuteChangedCallback(uint16_t id, bool state) {
  if (_clientSpeakersMuteChangedCallback != 0) {
    _clientSpeakersMuteChangedCallback(id, state);
  }
}

void AVTest_CallClientMicrophoneMuteChangedCallback(uint16_t id, bool state) {
  if (_clientMicrophoneMuteChangedCallback != 0) {
    _clientMicrophoneMuteChangedCallback(id, state);
  }
}
