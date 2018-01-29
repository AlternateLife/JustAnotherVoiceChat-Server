/*
 * File: VoiceServer.Clients.cs
 * Date: 29.1.2018,
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

using System;
using System.Collections.Concurrent;
using System.Linq;
using AlternateVoice.Server.Wrapper.Elements.Client;
using AlternateVoice.Server.Wrapper.Enums;
using AlternateVoice.Server.Wrapper.Interfaces;
using AlternateVoice.Server.Wrapper.Structs;

namespace AlternateVoice.Server.Wrapper.Elements.Server
{
    internal partial class VoiceServer
    {
        private readonly ConcurrentDictionary<ushort, VoiceClient> _clients = new ConcurrentDictionary<ushort, VoiceClient>();
        
        private readonly object _voiceHandleGenerationLock = new object();
        
        public IVoiceClient CreateClient()
        {
            lock (_voiceHandleGenerationLock)
            {
                VoiceHandle handle;
                try
                {
                    handle = CreateFreeVoiceHandle();
                }
                catch (InvalidOperationException)
                {
                    return null;
                }
                
                var createdClient = new VoiceClient(this, handle);

                if (!_clients.TryAdd(handle.Identifer, createdClient))
                {
                    return null;
                }

                return createdClient;
            }
        }

        public bool RemoveClient(IVoiceClient client)
        {
            lock (_voiceHandleGenerationLock)
            {
                if (client.Connected)
                {
                    OnClientDisconnectedFromVoice(client.Handle.Identifer);
                }
                
                AL_RemoveClient(client.Handle.Identifer);
                
                VoiceClient removedClient;
                return _clients.TryRemove(client.Handle.Identifer, out removedClient);
            }
        }

        public IVoiceClient GetClientByHandle(VoiceHandle handle)
        {
            return GetClientByHandle(handle.Identifer);
        }

        public IVoiceClient GetClientByHandle(ushort handle)
        {
            lock (_voiceHandleGenerationLock)
            {
                VoiceClient result;
                if (_clients.TryGetValue(handle, out result))
                {
                    return result;
                }
                return null;
            }
        }

        private VoiceHandle CreateFreeVoiceHandle()
        {
            var freeHandle = Enumerable
                .Range(ushort.MinValue + 1, ushort.MaxValue)
                .Select(v => (ushort) v)
                .Except(_clients.Keys.ToArray())
                .First();
            
            return new VoiceHandle(freeHandle);
        }

        private bool OnClientConnectedFromVoice(ushort handle)
        {
            var client = GetClientByHandle(handle) as VoiceClient;

            if (client == null || client.Connected)
            {
                return false;
            }

            client.Connected = true;
            
            OnClientConnected?.Invoke(client);

            return true;
        }

        private void OnClientDisconnectedFromVoice(ushort handle)
        {
            var client = GetClientByHandle(handle) as VoiceClient;

            if (client == null || !client.Connected)
            {
                return;
            }

            client.Connected = false;
            
            OnClientDisconnected?.Invoke(client, DisconnectReason.Quit);
        }
    }
}
