/*
 * File: tests/test_api.cpp
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

#include "test_api.h"

#include "api.h"

#include "../thirdparty/JustAnotherVoiceChat/include/protocol.h"

void test_api() {
  // test if methods exists
  JV_RegisterLogMessageCallback(NULL);
  JV_CreateServer(ENET_PORT, "", 0, "");
  JV_StartServer();
  JV_IsServerRunning();
  JV_StopServer();
  JV_RegisterClientConnectedCallback(NULL);
  JV_UnregisterClientConnectedCallback();
  JV_RegisterClientDisconnectedCallback(NULL);
  JV_UnregisterClientDisconnectedCallback();
  JV_RegisterClientMicrophoneMuteChangedCallback(NULL);
  JV_UnregisterClientMicrophoneMuteChangedCallback();
  JV_RegisterClientSpeakersMuteChangedCallback(NULL);
  JV_UnregisterClientSpeakersMuteChangedCallback();
  JV_RegisterClientTalkingChangedCallback(NULL);
  JV_UnregisterClientTalkingChangedCallback();
  JV_GetNumberOfClients();
  JV_GetClientGameIds(NULL, 0);
  JV_RemoveClient(0);
  JV_RemoveAllClients();
  JV_SetClientPosition(0, 0, 0, 0, 0);
  JV_Set3DSettings(0, 0);
  JV_SetRelativePositionForClient(0, 0, 0, 0, 0);
  JV_ResetRelativePositionForClient(0, 0);
  JV_ResetAllRelativePositions(0);
  
  JVTest_CallClientConnectedCallback(0);
  JVTest_CallClientDisconnectedCallback(0);
  JVTest_CallClientTalkingChangedCallback(0, false);
  JVTest_CallClientMicrophoneMuteChangedCallback(0, false);
  JVTest_CallClientSpeakersMuteChangedCallback(0, false);
  JVTest_CallClientTalkingChangedCallback(0, false);
}
