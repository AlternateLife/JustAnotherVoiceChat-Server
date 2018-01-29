/*
 * File: VoiceClient.cs
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
using System.Collections.Generic;
using System.Linq;
using AlternateVoice.Server.Wrapper.Interfaces;
using AlternateVoice.Server.Wrapper.Math;
using AlternateVoice.Server.Wrapper.Structs;

namespace AlternateVoice.Server.Wrapper.Elements.Client
{
    public abstract class VoiceClient : IVoiceClient
    {
        private readonly IVoiceServer _server;

        public VoiceHandle Handle { get; }

        public bool Microphone { get; set; }
        public bool Headphones { get; set; }

        public abstract Vector3 Position { get; }
        public float CameraRotation { get; set; }
        
        public string HandshakeUrl { get; }

        public bool Connected { get; internal set; }

        public IEnumerable<IVoiceGroup> Groups
        {
            get
            {
                return _server.GetAllGroups().Where(c => c.HasClient(this));
            }
        }

        protected VoiceClient(IVoiceServer server, VoiceHandle handle)
        {
            _server = server;

            Handle = handle;
            
            var handshakePayload = Uri.EscapeUriString($"{_server.Hostname}:{_server.Port}:{Handle.Identifer}");
            HandshakeUrl = $"http://localhost:23333/handshake/{handshakePayload}";
        }

        public void JoinGroup(IVoiceGroup group)
        {
            if (group == null)
            {
                throw new ArgumentNullException(nameof(group));
            }
            
            group.AddClient(this);
        }

        public void LeaveGroup(IVoiceGroup group)
        {
            if (group == null)
            {
                throw new ArgumentNullException(nameof(group));
            }
            
            group.RemoveClient(this);
        }

        public virtual void Dispose()
        {
            _server.RemoveClient(this);
        }
    }
}
