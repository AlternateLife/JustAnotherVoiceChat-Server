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
using System.Collections.Generic;
using JustAnotherVoiceChat.Server.Wrapper.Elements.Server;
using JustAnotherVoiceChat.Server.Wrapper.Interfaces;
using JustAnotherVoiceChat.Server.Wrapper.Structs;

namespace JustAnotherVoiceChat.Server.Wrapper.Elements.Group
{
    internal class VoiceGroup : IVoiceGroup
    {
        public event Delegates.ClientEvent OnClientJoined;
        public event Delegates.ClientEvent OnClientLeft;
        
        private readonly VoiceServer _server;
        private readonly IDictionary<VoiceHandle, IVoiceClient> _clients = new Dictionary<VoiceHandle, IVoiceClient>();

        public IEnumerable<IVoiceClient> Clients
        {
            get
            {
                lock (_clients)
                {
                    return _clients.Values;
                }
            }
        }
        
        internal VoiceGroup(VoiceServer server)
        {
            _server = server;
        }

        public void AddClient(IVoiceClient client)
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            lock (_clients)
            {
                _clients.Add(client.Handle, client);
            }
            
            _server.FireClientJoinedGroup(client, this);
        }

        public void RemoveClient(IVoiceClient client)
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }
            
            _server.FireClientLeftGroup(client, this);
            
            lock (_clients)
            {
                _clients.Remove(client.Handle);
            }
        }

        public bool HasClient(IVoiceClient client)
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            lock (_clients)
            {
                return _clients.ContainsKey(client.Handle);
            }
        }

        public void Dispose()
        {
            foreach (var client in Clients)
            {
                _server.FireClientLeftGroup(client, this);
            }

            OnClientJoined = null;
            OnClientLeft = null;
            
            _clients.Clear();
            
            GC.SuppressFinalize(this);
        }
    }
}
