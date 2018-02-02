/*
 * File: VoiceServer.cs
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
using AlternateVoice.Server.Wrapper.Exceptions;
using AlternateVoice.Server.Wrapper.Interfaces;

namespace AlternateVoice.Server.Wrapper.Elements.Server
{
    internal partial class VoiceServer : IVoiceServer, IVoiceTaskServer
    {
        private readonly IVoiceClientRepository _repository;

        public string Hostname { get; }
        public ushort Port { get; }
        public int ChannelId { get; }
        public bool Started { get; private set; }

        public double GlobalMaxDistance { get; }
        public float GlobalDistanceFactor { get; }
        public float GlobalRollOffScale { get; }

        internal VoiceServer(IVoiceClientRepository repository, string hostname, ushort port, int channelId, float globalRollOffScale = 1.0f,
            float globalDistanceFactor = 1.0f, double globalMaxDistance = 6.0)
        {
            if (repository == null)
            {
                throw new ArgumentNullException(nameof(repository));
            }
            
            _repository = repository;
            
            var result = Uri.CheckHostName(hostname);
            if (result == UriHostNameType.Unknown)
            {
                throw new InvalidHostnameException(hostname);
            }
            
            Hostname = hostname;
            Port = port;
            ChannelId = channelId;

            GlobalMaxDistance = globalMaxDistance;
            GlobalDistanceFactor = globalDistanceFactor;
            GlobalRollOffScale = globalRollOffScale;

            AttachToNativeEvents();
            CreateAndAttachTasks();
        }

        public void Start()
        {
            if (Started)
            {
                throw new VoiceServerAlreadyStartedException();
            }
            
            AV_StartServer(Port);
            
            Started = true;
            OnServerStarted?.Invoke();
        }

        public void Stop()
        {
            if (!Started)
            {
                throw new VoiceServerNotStartedException();
            }
            
            AV_StopServer();
            
            OnServerStopping?.Invoke();
            Started = false;
        }

        public void Dispose()
        {
            DisposeGroups();

            DisposeTasks();

            DisposeEvents();
        }
    }
}
