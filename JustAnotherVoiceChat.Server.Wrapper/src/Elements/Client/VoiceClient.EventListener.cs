/*
 * File: VoiceClient.EventListener.cs
 * Date: 24.2.2018,
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
using JustAnotherVoiceChat.Server.Wrapper.Interfaces;

namespace JustAnotherVoiceChat.Server.Wrapper.Elements.Client
{
    public partial class VoiceClient<TClient> where TClient : IVoiceClient
    {

        private void AttachToStatusChangeEvents()
        {
            Server.OnClientConnected += OnClientConnected;
            Server.OnClientDisconnected += OnClientDisconnected;
            Server.OnClientMicrophoneMuteChanged += OnClientMicrophoneChanged;
            Server.OnClientSpeakersMuteChanged += OnClientSpeakersMuteChanged;
        }

        private void DetachFromStatusChangeEvents()
        {
            Server.OnClientConnected -= OnClientConnected;
            Server.OnClientDisconnected -= OnClientDisconnected;
            Server.OnClientMicrophoneMuteChanged -= OnClientMicrophoneChanged;
            Server.OnClientSpeakersMuteChanged -= OnClientSpeakersMuteChanged;
        }

        private void OnClientConnected(TClient client)
        {
            ExecuteOnMe(client, () => { Connected = true; });
        }

        private void OnClientDisconnected(TClient client)
        {
            ExecuteOnMe(client, () => { Connected = false; });
        }

        private void OnClientSpeakersMuteChanged(TClient client, bool isMuted)
        {
            ExecuteOnMe(client, () => { Speakers = !isMuted; });
        }

        private void OnClientMicrophoneChanged(TClient client, bool isMuted)
        {
            ExecuteOnMe(client, () => { Microphone = !isMuted; });
        }

        private void ExecuteOnMe(TClient client, Action callback)
        {
            if (!ReferenceEquals(this, client))
            {
                return;
            }

            callback();
        }
        
    }
}
