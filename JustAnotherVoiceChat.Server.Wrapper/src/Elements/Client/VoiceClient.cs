/*
 * File: VoiceClient.cs
 * Date: 21.2.2018,
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

using JustAnotherVoiceChat.Server.Wrapper.Exceptions;
using JustAnotherVoiceChat.Server.Wrapper.Interfaces;
using JustAnotherVoiceChat.Server.Wrapper.Math;
using JustAnotherVoiceChat.Server.Wrapper.Structs;
using System;

namespace JustAnotherVoiceChat.Server.Wrapper.Elements.Client
{
    public abstract partial class VoiceClient<TClient> : IVoiceClient where TClient : IVoiceClient
    {
        protected readonly IVoiceServer<TClient> Server;
        
        public VoiceHandle Handle { get; }

        public bool Microphone { get; private set; }
        public bool Speakers { get; private set; }

        public abstract Vector3 Position { get; }
        public float CameraRotation { get; set; }
        
        public string HandshakeUrl { get; }

        public bool Connected { get; private set; }

        protected VoiceClient(IVoiceServer<TClient> server, VoiceHandle handle)
        {
            Server = server;
            Handle = handle;

            Speakers = true;
            Microphone = true;

            var config = Server.Configuration;
            HandshakeUrl = $"http://localhost:23333/?host={config.Hostname}&port={config.Port}&uid={Handle.Identifer}";
            
            AttachToStatusChangeEvents();
        }
        
        public bool SetNickname(string nickname)
        {
            return Server.NativeWrapper.SetClientNickname(this, nickname);
        }

        private T RunWhileConnected<T>(Func<T> callback)
        {
            if (!Connected)
            {
                throw new ClientNotConnectedException(this.Handle);
            }

            return callback();
        }

        public void Dispose()
        {
            DetachFromStatusChangeEvents();
        }
    }
}
