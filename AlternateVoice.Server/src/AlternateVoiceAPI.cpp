/*
 * File: AlternateVoiceServer.cpp
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

#include "AlternateVoiceAPI.h"

#include "AlternateVoiceServer.h"

#include <enet/enet.h>

AL_NewClientCallback_t _newClientCallback = 0;
AlternateVoice::AlternateVoiceServer *_server = nullptr;

void AL_StartServer(const char *hostname, uint16_t port, int channelId) {
  if (_server == nullptr) {
    return;
  }

  _server = new AlternateVoice::AlternateVoiceServer(port);
}

void AL_StopServer() {
  if (_server == nullptr) {
    return;
  }

  _server->close();
}

bool AL_IsServerRunning() {
  return (_server != nullptr && _server->isRunning());
}

void AL_RegisterNewClientCallback(AL_NewClientCallback_t callback) {
  _newClientCallback = callback;
}

void AL_UnregisterNewClientCallback() {
  _newClientCallback = 0;
}

int AL_GetNumberOfClients() {
  return 0;
}

void AL_GetClientIds(uint16_t *, size_t maxLength) {

}

void AL_RemoveClient(uint16_t clientId) {

}

void AL_MuteClientFor(uint16_t listenerId, uint16_t clientId, bool muted) {

}

void ALTest_CallNewClientCallback(uint16_t id) {
  if (_newClientCallback != 0) {
    _newClientCallback(id);
  }
}
