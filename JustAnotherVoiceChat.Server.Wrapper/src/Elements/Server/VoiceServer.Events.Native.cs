/*
 * File: VoiceServer.Events.Native.cs
 * Date: 11.2.2018,
 *
 * MIT License
 *
 * Copyright (c) 2018 Alternate-Life.de
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

namespace JustAnotherVoiceChat.Server.Wrapper.Elements.Server
{
    internal partial class VoiceServer
    {
        private readonly List<GCHandle> _garbageCollectorHandles = new List<GCHandle>();

        private void RegisterEvent<T>(Action<T> register, T callback)
        {
            _garbageCollectorHandles.Add(GCHandle.Alloc(callback));

            register(callback);
        }

        private void AttachToNativeEvents()
        {
            RegisterEvent<Delegates.ClientConnectCallback>(_voiceWrapper.RegisterClientConnectedCallback, OnClientConnectedFromVoice);
            RegisterEvent<Delegates.ClientCallback>(_voiceWrapper.RegisterClientDisconnectedCallback, OnClientDisconnectedFromVoice);
            RegisterEvent<Delegates.ClientStatusCallback>(_voiceWrapper.RegisterClientTalkingChangedCallback, OnClientTalkingStatusChangedFromVoice);
            RegisterEvent<Delegates.ClientStatusCallback>(_voiceWrapper.RegisterClientSpeakersMuteChangedCallback, OnClientSpeakersMuteChangedFromVoice);
            RegisterEvent<Delegates.ClientStatusCallback>(_voiceWrapper.RegisterClientMicrophoneMuteChangedCallback, OnClientMicrophoneMuteChangedFromVoice);
        }

        private void DisposeNativeEvents()
        {
            _voiceWrapper.UnregisterClientConnectedCallback();
            _voiceWrapper.UnregisterClientDisconnectedCallback();
            
            _voiceWrapper.UnregisterClientTalkingChangedCallback();
            _voiceWrapper.UnregisterClientSpeakersMuteChangedCallback();
            _voiceWrapper.UnregisterClientMicrophoneMuteChangedCallback();

            foreach (var handle in _garbageCollectorHandles)
            {
                handle.Free();
            }
        }
    }
}
