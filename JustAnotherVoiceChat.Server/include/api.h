/*
 * File: include/api.h
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
#include "log.h"

#include <stdint.h>
#include <stdlib.h>

#ifdef __cplusplus
extern "C" {
#endif

/**
 * 
 */
typedef bool (* JV_ClientCallback_t)(uint16_t);

/**
 * 
 */
typedef void (* JV_ClientStatusCallback_t)(uint16_t, bool);

/**
 *
 */
void JUSTANOTHERVOICECHAT_API JV_RegisterLogMessageCallback(logMessageCallback_t callback);

/**
 *
 */
void JUSTANOTHERVOICECHAT_API JV_CreateServer(uint16_t port);

/**
 * 
 */
bool JUSTANOTHERVOICECHAT_API JV_StartServer();

/**
 * 
 */
void JUSTANOTHERVOICECHAT_API JV_StopServer();

/**
 * 
 */
bool JUSTANOTHERVOICECHAT_API JV_IsServerRunning();

/**
 * 
 */
void JUSTANOTHERVOICECHAT_API JV_RegisterClientConnectedCallback(JV_ClientCallback_t callback);

/**
 * 
 */
void JUSTANOTHERVOICECHAT_API JV_UnregisterClientConnectedCallback();

/**
 * 
 */
void JUSTANOTHERVOICECHAT_API JV_RegisterClientDisconnectedCallback(JV_ClientCallback_t callback);

/**
 * 
 */
void JUSTANOTHERVOICECHAT_API JV_UnregisterClientDisconnectedCallback();

/**
 * 
 */
void JUSTANOTHERVOICECHAT_API JV_RegisterClientTalkingChangedCallback(JV_ClientStatusCallback_t callback);

/**
 * 
 */
void JUSTANOTHERVOICECHAT_API JV_UnregisterClientTalkingChangedCallback();

/**
 * 
 */
void JUSTANOTHERVOICECHAT_API JV_RegisterClientSpeakersMuteChangedCallback(JV_ClientStatusCallback_t callback);

/**
 * 
 */
void JUSTANOTHERVOICECHAT_API JV_UnregisterClientSpeakersMuteChangedCallback();

/**
 * 
 */
void JUSTANOTHERVOICECHAT_API JV_RegisterClientMicrophoneMuteChangedCallback(JV_ClientStatusCallback_t callback);

/**
 * 
 */
void JUSTANOTHERVOICECHAT_API JV_UnregisterClientMicrophoneMuteChangedCallback();

/**
 * 
 */
int JUSTANOTHERVOICECHAT_API JV_GetNumberOfClients();

/**
 * 
 */
void JUSTANOTHERVOICECHAT_API JV_GetClientGameIds(uint16_t *gameIds, size_t maxLength);

/**
 * 
 */
void JUSTANOTHERVOICECHAT_API JV_RemoveClient(uint16_t clientId);

/**
 * 
 */
void JUSTANOTHERVOICECHAT_API JV_RemoveAllClients();

/**
 * 
 */
void JUSTANOTHERVOICECHAT_API JV_SetClientPosition(uint16_t clientId, float x, float y, float z, float rotation);

/**
 * 
 */
void JUSTANOTHERVOICECHAT_API JV_Set3DSettings(float distanceFactor, float rolloffFactor);

/**
 * 
 */
void JUSTANOTHERVOICECHAT_API JV_SetRelativePositionForClient(uint16_t listenerId, uint16_t speakerId, float x, float y, float z);

/**
 * 
 */
void JUSTANOTHERVOICECHAT_API JV_ResetRelativePositionForClient(uint16_t listenerId, uint16_t speakerId);

/**
 * 
 */
void JUSTANOTHERVOICECHAT_API JV_ResetAllRelativePositions(uint16_t clientId);

/**
 * 
 */
void JUSTANOTHERVOICECHAT_API JVTest_CallClientConnectedCallback(uint16_t id);

/**
 * 
 */
void JUSTANOTHERVOICECHAT_API JVTest_CallClientDisconnectedCallback(uint16_t id);

/**
 * 
 */
void JUSTANOTHERVOICECHAT_API JVTest_CallClientTalkingChangedCallback(uint16_t id, bool state);

/**
 * 
 */
void JUSTANOTHERVOICECHAT_API JVTest_CallClientSpeakersMuteChangedCallback(uint16_t id, bool state);

/**
 * 
 */
void JUSTANOTHERVOICECHAT_API JVTest_CallClientMicrophoneMuteChangedCallback(uint16_t id, bool state);

#ifdef __cplusplus
}
#endif
