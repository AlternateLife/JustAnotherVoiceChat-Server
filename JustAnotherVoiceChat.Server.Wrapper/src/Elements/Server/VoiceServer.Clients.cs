/*
 * File: VoiceServer.Clients.cs
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
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using JustAnotherVoiceChat.Server.Wrapper.Interfaces;
using JustAnotherVoiceChat.Server.Wrapper.Structs;

namespace JustAnotherVoiceChat.Server.Wrapper.Elements.Server
{
    public partial class VoiceServer<TClient, TIdentifier> where TClient : IVoiceClient
    {
        private readonly ConcurrentDictionary<ushort, IVoiceClient> _clients = new ConcurrentDictionary<ushort, IVoiceClient>();
        private readonly object _voiceHandleGenerationLock = new object();

        protected internal TClient PrepareClient(TIdentifier identifier)
        {
            var client = CreateClient(identifier);
            if (ReferenceEquals(client, default(TClient)) || !RegisterClient(client))
            {
                return default(TClient);
            }

            return client;
        }
        
        private TClient CreateClient(TIdentifier identifier)
        {
            var voiceHandle = CreateVoiceHandle();
            if (voiceHandle == VoiceHandle.Empty)
            {
                return default(TClient);
            }

            return _factory.MakeClient(identifier, this, voiceHandle);
        }

        private VoiceHandle CreateVoiceHandle()
        {
            lock (_voiceHandleGenerationLock)
            {
                try
                {
                    return CreateFreeVoiceHandle();
                }
                catch (InvalidOperationException)
                {
                    return VoiceHandle.Empty;
                }
            }
        }
        
        private bool RegisterClient(IVoiceClient client)
        {
            lock (_voiceHandleGenerationLock)
            {
                if (client == null)
                {
                    throw new ArgumentNullException(nameof(client));
                }
                
                if (client.Handle.IsEmpty)
                {
                    return false;
                }

                return _clients.TryAdd(client.Handle.Identifer, client);
            }
        }

        protected internal bool RemoveClient(IVoiceClient client)
        {
            lock (_voiceHandleGenerationLock)
            {
                if (client == null)
                {
                    throw new ArgumentNullException(nameof(client));
                }

                if (client.Connected)
                {
                    OnClientDisconnectedFromVoice(client.Handle.Identifer);
                }
                
                _voiceWrapper.RemoveClient(client);
                
                return _clients.TryRemove(client.Handle.Identifer, out _);
            }
        }

        public IVoiceClient GetVoiceClient(VoiceHandle handle)
        {
            return GetVoiceClient(handle.Identifer);
        }

        public IVoiceClient GetVoiceClient(ushort handle)
        {
            lock (_voiceHandleGenerationLock)
            {
                if (_clients.TryGetValue(handle, out var result))
                {
                    return result;
                }
                return null;
            }
        }

        public T FindClient<T>(Func<T, bool> filter) where T : IVoiceClient
        {
            lock (_voiceHandleGenerationLock)
            {
                return _clients.ToArray().Select(c => (T) c.Value).FirstOrDefault(filter);
            }
        }

        public IEnumerable<T> GetClients<T>(Func<T, bool> filter) where T : IVoiceClient
        {
            lock (_voiceHandleGenerationLock)
            {
                return _clients.ToArray().Select(c => (T) c.Value).Where(filter);
            }
        }

        public IEnumerable<T> GetClients<T>() where T : IVoiceClient
        {
            lock (_voiceHandleGenerationLock)
            {
                return _clients.ToArray().Select(c => (T) c.Value);
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
    }
}
