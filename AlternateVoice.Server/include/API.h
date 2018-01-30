/*
 * File: API.h
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

#pragma once

#include "AlternateVoice.h"

#include <stdint.h>
#include <stdlib.h>

#ifdef __cplusplus
extern "C" {
#endif

/**
 * 
 */
typedef bool (* AV_ClientCallback_t)(uint16_t);

/**
 * 
 */
typedef void (* AV_ClientStatusCallback_t)(uint16_t, bool);

/**
 * 
 */
void ALTERNATEVOICE_API AV_StartServer(uint16_t port);

/**
 * 
 */
void ALTERNATEVOICE_API AV_StopServer();

/**
 * 
 */
bool ALTERNATEVOICE_API AV_IsServerRunning();

/**
 * 
 */
void ALTERNATEVOICE_API AV_RegisterClientConnectedCallback(AV_ClientCallback_t callback);

/**
 * 
 */
void ALTERNATEVOICE_API AV_UnregisterClientConnectedCallback();

/**
 * 
 */
void ALTERNATEVOICE_API AV_RegisterClientDisconnectedCallback(AV_ClientCallback_t callback);

/**
 * 
 */
void ALTERNATEVOICE_API AV_UnregisterClientDisconnectCallback();

/**
 * 
 */
void ALTERNATEVOICE_API AV_RegisterClientTalkingChangedCallback(AV_ClientStatusCallback_t callback);

/**
 * 
 */
void ALTERNATEVOICE_API AV_UnregisterClientTalkingChangedCallback();

/**
 * 
 */
void ALTERNATEVOICE_API AV_RegisterClientSpeakersMuteChangedCallback(AV_ClientStatusCallback_t callback);

/**
 * 
 */
void ALTERNATEVOICE_API AV_UnregisterClientSpeakersMuteChangedCallback();

/**
 * 
 */
void ALTERNATEVOICE_API AV_RegisterClientMicrophoneMuteChangedCallback(AV_ClientStatusCallback_t callback);

/**
 * 
 */
void ALTERNATEVOICE_API AV_UnregisterClientMicrophoneMuteChangedCallback();

/**
 * 
 */
int ALTERNATEVOICE_API AV_GetNumberOfClients();

/**
 * 
 */
void ALTERNATEVOICE_API AV_GetClientIds(uint16_t *, size_t maxLength);

/**
 * 
 */
void ALTERNATEVOICE_API AV_RemoveClient(uint16_t clientId);

/**
 * 
 */
void ALTERNATEVOICE_API AV_RemoveAllClients();

/**
 * 
 */
void ALTERNATEVOICE_API AV_MuteClientForClient(uint16_t listenerId, uint16_t clientId, bool muted = true);

/**
 * 
 */
void ALTERNATEVOICE_API AV_SetClientPositionForClient(uint16_t listenerId, uint16_t clientId, float x, float y, float z);

/**
 * 
 */
void ALTERNATEVOICE_API AV_SetClientVolumeForClient(uint16_t listenerId, uint16_t clientId, float volume);

/**
 * 
 */
void ALTERNATEVOICE_API AVTest_CallClientConnectedCallback(uint16_t id);

/**
 * 
 */
void ALTERNATEVOICE_API AVTest_CallClientDisconnectedCallback(uint16_t id);

/**
 * 
 */
void ALTERNATEVOICE_API AVTest_CallClientTalkingChangedCallback(uint16_t id, bool state);

/**
 * 
 */
void ALTERNATEVOICE_API AVTest_CallClientSpeakersMuteChangedCallback(uint16_t id, bool state);

/**
 * 
 */
void ALTERNATEVOICE_API AVTest_CallClientMicrophoneMuteChangedCallback(uint16_t id, bool state);

#ifdef __cplusplus
}
#endif
