/*
 * File: VoiceServer.Events.Native.cs
 * Date: 15.2.2018,
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

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using JustAnotherVoiceChat.Server.Wrapper.Delegates;
using JustAnotherVoiceChat.Server.Wrapper.Interfaces;

namespace JustAnotherVoiceChat.Server.Wrapper.Elements.Server
{
    public partial class VoiceServer<TClient, TIdentifier> where TClient : IVoiceClient
    {
        private readonly List<GCHandle> _garbageCollectorHandles = new List<GCHandle>();

        internal void RegisterEvent<T>(Action<T> register, T callback)
        {
            _garbageCollectorHandles.Add(GCHandle.Alloc(callback));

            register(callback);
        }

        private void AttachToNativeEvents()
        {
            RegisterEvent<NativeDelegates.ClientConnectCallback>(NativeWrapper.RegisterClientConnectedCallback, OnClientConnectedFromVoice);
            RegisterEvent<NativeDelegates.ClientCallback>(NativeWrapper.RegisterClientDisconnectedCallback, OnClientDisconnectedFromVoice);
            RegisterEvent<NativeDelegates.ClientStatusCallback>(NativeWrapper.RegisterClientTalkingChangedCallback, OnClientTalkingStatusChangedFromVoice);
            RegisterEvent<NativeDelegates.ClientStatusCallback>(NativeWrapper.RegisterClientSpeakersMuteChangedCallback, OnClientSpeakersMuteChangedFromVoice);
            RegisterEvent<NativeDelegates.ClientStatusCallback>(NativeWrapper.RegisterClientMicrophoneMuteChangedCallback, OnClientMicrophoneMuteChangedFromVoice);
            RegisterEvent<NativeDelegates.LogMessageCallback>(NativeWrapper.RegisterLogMessageCallback, OnLogMessageFromVoice);
        }

        private void DisposeNativeEvents()
        {
            NativeWrapper.UnregisterClientConnectedCallback();
            NativeWrapper.UnregisterClientDisconnectedCallback();
            
            NativeWrapper.UnregisterClientTalkingChangedCallback();
            NativeWrapper.UnregisterClientSpeakersMuteChangedCallback();
            NativeWrapper.UnregisterClientMicrophoneMuteChangedCallback();
            
            //NativeWrapper.UnregisterLogMessageCallback();

            foreach (var handle in _garbageCollectorHandles)
            {
                handle.Free();
            }
        }
    }
}
