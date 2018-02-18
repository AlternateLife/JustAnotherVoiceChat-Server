/*
 * File: VoiceServer.Events.Dispatcher.cs
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
using JustAnotherVoiceChat.Server.Wrapper.Elements.Client;
using JustAnotherVoiceChat.Server.Wrapper.Interfaces;

namespace JustAnotherVoiceChat.Server.Wrapper.Elements.Server
{
    public partial class VoiceServer<TClient, TIdentifier> where TClient : IVoiceClient
    {
        
        private bool OnClientConnectedFromVoice(ushort handle)
        {
            return RunWhenClientValid(handle, client =>
            {
                if (client.Connected)
                {
                    return false;
                }
                
                client.Connected = true;
                OnClientConnected?.Invoke(client);

                return true;
            });
        }

        private void OnClientDisconnectedFromVoice(ushort handle)
        {
            RunWhenClientConnected(handle, client =>
            {
                client.Connected = false;
                
                OnClientDisconnected?.Invoke(client);
            });
        }

        private void OnClientTalkingStatusChangedFromVoice(ushort handle, bool newStatus)
        {
            RunWhenClientConnected(handle, client =>
            {
                OnClientTalkingChanged?.Invoke(client, newStatus);
            });
        }

        private void OnClientSpeakersMuteChangedFromVoice(ushort handle, bool newStatus)
        {
            RunWhenClientConnected(handle, client =>
            {
                OnClientSpeakersMuteChanged?.Invoke(client, newStatus);
            });
        }

        private void OnClientMicrophoneMuteChangedFromVoice(ushort handle, bool newStatus)
        {
            RunWhenClientConnected(handle, client =>
            {
                OnClientMicrophoneMuteChanged?.Invoke(client, newStatus);
            });
        }
        
        
        
        private T RunWhenClientValid<T>(ushort handle, Func<VoiceClient, T> callback)
        {
            var client = GetVoiceClient(handle) as VoiceClient;

            if (client == null)
            {
                return default(T);
            }

            return callback(client);
        }

        private void RunWhenClientValid(ushort handle, Action<VoiceClient> callback)
        {
            var client = GetVoiceClient(handle) as VoiceClient;

            if (client == null)
            {
                return;
            }
            
            callback(client);
        }
        
        private T RunWhenClientConnected<T>(ushort handle, Func<VoiceClient, T> callback)
        {
            return RunWhenClientValid(handle, client =>
            {
                if (!client.Connected)
                {
                    return default(T);
                }

                return callback(client);
            });
        }

        private void RunWhenClientConnected(ushort handle, Action<VoiceClient> callback)
        {
            RunWhenClientValid(handle, client =>
            {
                if (!client.Connected)
                {
                    return;
                }

                callback(client);
            });
        }
    }
}
