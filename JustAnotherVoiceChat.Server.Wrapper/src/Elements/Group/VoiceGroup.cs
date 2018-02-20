/*
 * File: VoiceGroup.cs
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
using JustAnotherVoiceChat.Server.Wrapper.Delegates;
using JustAnotherVoiceChat.Server.Wrapper.Interfaces;
using JustAnotherVoiceChat.Server.Wrapper.Structs;

namespace JustAnotherVoiceChat.Server.Wrapper.Elements.Group
{
    internal class VoiceGroup<TClient> : IVoiceGroup<TClient> where TClient : IVoiceClient
    {
        public event Delegates<TClient>.ClientEvent OnClientJoined;
        public event Delegates<TClient>.ClientEvent OnClientLeft;
        
        private readonly IVoiceServer<TClient> _server;
        private readonly ConcurrentDictionary<VoiceHandle, TClient> _clients = new ConcurrentDictionary<VoiceHandle, TClient>();

        public IEnumerable<TClient> Clients
        {
            get
            {
                return _clients.Values;
            }
        }
        
        internal VoiceGroup(IVoiceServer<TClient> server)
        {
            _server = server;
        }

        public bool AddClient(TClient client)
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            if (!_clients.TryAdd(client.Handle, client))
            {
                return false;
            }
            
            // Todo: Add trigger for client joining group
            //_server.FireClientJoinedGroup(client, this);
            return true;
        }

        public bool TryRemoveClient(TClient client, out TClient removedVoiceClient)
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            if (!_clients.TryRemove(client.Handle, out removedVoiceClient))
            {
                return false;
            }

            // Todo: Add trigger for client leaving group
            //_server.FireClientLeftGroup(client, this);
            return true;
        }

        public bool HasClient(TClient client)
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            return _clients.ContainsKey(client.Handle);
        }

        public void Dispose()
        {
            foreach (var client in Clients)
            {
                // Todo: Add trigger for client leaving group
                //_server.FireClientLeftGroup(client, this);
            }

            OnClientJoined = null;
            OnClientLeft = null;

            _clients.Clear();

            GC.SuppressFinalize(this);
        }
    }
}
