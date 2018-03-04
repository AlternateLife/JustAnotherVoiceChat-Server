/*
 * File: VoiceWrapperEventInvoker.cs
 * Date: 25.2.2018,
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

using System.Collections.Generic;
using JustAnotherVoiceChat.Server.Wrapper.Delegates;
using JustAnotherVoiceChat.Server.Wrapper.Elements.Models;
using JustAnotherVoiceChat.Server.Wrapper.Enums;
using JustAnotherVoiceChat.Server.Wrapper.Interfaces;
using JustAnotherVoiceChat.Server.Wrapper.Math;
using JustAnotherVoiceChat.Server.Wrapper.Structs;
using Moq;

namespace JustAnotherVoiceChat.Server.Wrapper.Tests.Fakes
{
    public class VoiceWrapperEventInvoker : IVoiceWrapper
    {

        public readonly Mock<IVoiceWrapper> Mock = new Mock<IVoiceWrapper>();
        
        private NativeDelegates.ClientConnectingCallback _clientConnecting;
        private NativeDelegates.ClientCallback _clientConnected;
        private NativeDelegates.ClientRejectedCallback _clientRejected;
        private NativeDelegates.ClientCallback _clientDisconnected;
        private NativeDelegates.ClientStatusCallback _clientTalkingChanged;
        private NativeDelegates.ClientStatusCallback _clientSpeakersMuteChanged;
        private NativeDelegates.ClientStatusCallback _clientMicrophoneMuteChanged;
        private NativeDelegates.LogMessageCallback _logMessage;

        public bool InvokeClientConnectingCallback(ushort handle, string teamspeakId)
        {
            return _clientConnecting(handle, teamspeakId);
        }

        public void InvokeClientConnectedCallback(ushort handle)
        {
            _clientConnected(handle);
        }

        public void InvokeClientRejectedCallback(ushort handle, int statusCode)
        {
            _clientRejected(handle, statusCode);
        }

        public void InvokeClientDisconnectedCallback(ushort handle)
        {
            _clientDisconnected(handle);
        }

        public void InvokeClientTalkingChangedCallback(ushort handle, bool newStatus)
        {
            _clientTalkingChanged(handle, newStatus);
        }

        public void InvokeClientSpeakersMuteChangedCallback(ushort handle, bool newStatus)
        {
            _clientSpeakersMuteChanged(handle, newStatus);
        }

        public void InvokeClientMicrophoneMuteChangedCallback(ushort handle, bool newStatus)
        {
            _clientMicrophoneMuteChanged(handle, newStatus);
        }

        public void InvokeLogMessageCallback(string message, int logLevel)
        {
            _logMessage(message, logLevel);
        }
        
        public void RegisterClientConnectingCallback(NativeDelegates.ClientConnectingCallback callback)
        {
            _clientConnecting = callback;
        }

        public void RegisterClientConnectedCallback(NativeDelegates.ClientCallback callback)
        {
            _clientConnected = callback;
        }

        public void RegisterClientRejectedCallback(NativeDelegates.ClientRejectedCallback callback)
        {
            _clientRejected = callback;
        }

        public void RegisterClientDisconnectedCallback(NativeDelegates.ClientCallback callback)
        {
            _clientDisconnected = callback;
        }

        public void RegisterClientTalkingChangedCallback(NativeDelegates.ClientStatusCallback callback)
        {
            _clientTalkingChanged = callback;
        }

        public void RegisterClientSpeakersMuteChangedCallback(NativeDelegates.ClientStatusCallback callback)
        {
            _clientSpeakersMuteChanged = callback;
        }

        public void RegisterClientMicrophoneMuteChangedCallback(NativeDelegates.ClientStatusCallback callback)
        {
            _clientMicrophoneMuteChanged = callback;
        }

        public void RegisterLogMessageCallback(NativeDelegates.LogMessageCallback callback)
        {
            _logMessage = callback;
        }
        
        public void UnregisterClientConnectedCallback()
        {
            _clientConnected = null;
        }

        public void UnregisterClientConnectingCallback()
        {
            _clientConnecting = null;
        }

        public void UnregisterClientDisconnectedCallback()
        {
            _clientDisconnected = null;
        }

        public void UnregisterClientRejectedCallback()
        {
            _clientRejected = null;
        }

        public void UnregisterClientTalkingChangedCallback()
        {
            _clientTalkingChanged = null;
        }

        public void UnregisterClientSpeakersMuteChangedCallback()
        {
            _clientSpeakersMuteChanged = null;
        }

        public void UnregisterClientMicrophoneMuteChangedCallback()
        {
            _clientMicrophoneMuteChanged = null;
        }

        public void UnregisterLogMessageCallback()
        {
            _logMessage = null;
        }

        public bool SetClientVoiceRange(IVoiceClient client, float voiceRange)
        {
            return Mock.Object.SetClientVoiceRange(client, voiceRange);
        }

        public bool SetListenerPosition(IVoiceClient listener, Vector3 position, float rotation)
        {
            return Mock.Object.SetListenerPosition(listener, position, rotation);
        }

        public bool SetListenerPositions(IEnumerable<ClientPosition> clientPositions)
        {
            return Mock.Object.SetListenerPositions(clientPositions);
        }

        public bool SetRelativeSpeakerPositionForListener(IVoiceClient listener, IVoiceClient speaker, Vector3 position)
        {
            return Mock.Object.SetRelativeSpeakerPositionForListener(listener, speaker, position);
        }

        public bool ResetRelativeSpeakerPositionForListener(IVoiceClient listener, IVoiceClient speaker)
        {
            return Mock.Object.ResetRelativeSpeakerPositionForListener(listener, speaker);
        }

        public bool ResetAllRelativePositionsForListener(IVoiceClient listener)
        {
            return Mock.Object.ResetAllRelativePositionsForListener(listener);
        }
        
        public void CreateNativeServer(VoiceServerConfiguration configuration)
        {
            Mock.Object.CreateNativeServer(configuration);
        }

        public void DestroyNativeServer()
        {
            Mock.Object.DestroyNativeServer();
        }

        public bool StartNativeServer()
        {
            return Mock.Object.StartNativeServer();
        }

        public void StopNativeServer()
        {
            Mock.Object.StopNativeServer();
        }

        public bool RemoveClient(IVoiceClient client)
        {
            return Mock.Object.RemoveClient(client);
        }

        public bool SetClientNickname(IVoiceClient client, string nickname)
        {
            return Mock.Object.SetClientNickname(client, nickname);
        }

        public void Set3DSettings(float distanceFactor, float rolloffFactor)
        {
            Mock.Object.Set3DSettings(distanceFactor, rolloffFactor);
        }

        public bool MuteClientForAll(IVoiceClient client, bool muted)
        {
            return Mock.Object.MuteClientForAll(client, muted);
        }

        public bool IsClientMutedForAll(IVoiceClient client)
        {
            return Mock.Object.IsClientMutedForAll(client);
        }

        public bool MuteClientForClient(IVoiceClient speaker, IVoiceClient listener, bool muted)
        {
            return Mock.Object.MuteClientForClient(speaker, listener, muted);
        }

        public bool IsClientMutedForClient(IVoiceClient speaker, IVoiceClient listener)
        {
            return Mock.Object.IsClientMutedForClient(speaker, listener);
        }

        public void SetLogLevel(LogLevel logLevel)
        {
            Mock.Object.SetLogLevel(logLevel);
        }

        public bool IsClientConnected(IVoiceClient client)
        {
            return Mock.Object.IsClientConnected(client);
        }
    }
}
