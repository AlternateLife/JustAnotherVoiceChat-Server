/*
 * File: VoiceServer.cs
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
using JustAnotherVoiceChat.Server.Wrapper.Elements.Models;
using JustAnotherVoiceChat.Server.Wrapper.Enums;
using JustAnotherVoiceChat.Server.Wrapper.Exceptions;
using JustAnotherVoiceChat.Server.Wrapper.Interfaces;

namespace JustAnotherVoiceChat.Server.Wrapper.Elements.Server
{
    public partial class VoiceServer<TClient, TIdentifier> : IVoiceServer<TClient> where TClient : IVoiceClient
    {
        private readonly IVoiceClientFactory<TClient, TIdentifier> _factory;

        public VoiceServerConfiguration Configuration { get; }
        
        public bool Started { get; private set; }
        
        public IVoiceWrapper NativeWrapper { get; }

        protected VoiceServer(IVoiceClientFactory<TClient, TIdentifier> factory, VoiceServerConfiguration configuration) : this(factory, configuration, JustAnotherVoiceChat.GetVoiceWrapper())
        {
            
        }
        
        internal VoiceServer(IVoiceClientFactory<TClient, TIdentifier> factory, VoiceServerConfiguration configuration, IVoiceWrapper voiceWrapper)
        {
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
            NativeWrapper = voiceWrapper ?? throw new ArgumentNullException(nameof(voiceWrapper));

            var result = Uri.CheckHostName(configuration.Hostname);
            if (result == UriHostNameType.Unknown)
            {
                throw new InvalidHostnameException(configuration.Hostname);
            }
            
            Configuration = configuration;

            NativeWrapper.CreateNativeServer(configuration);

            AttachToNativeEvents();
            AttachTasksToStartAndStopEvent();
        }

        public void Start()
        {
            if (Started)
            {
                throw new VoiceServerAlreadyStartedException();
            }
            
            if (NativeWrapper.StartNativeServer() == false)
            {
                throw new VoiceServerNotStartedException();
            }
            
            Started = true;
            OnServerStarted?.Invoke();
        }

        public void Stop()
        {
            if (!Started)
            {
                throw new VoiceServerNotStartedException();
            }
            
            NativeWrapper.StopNativeServer();
            
            OnServerStopping?.Invoke();
            Started = false;
        }

        public void Log(LogLevel logLevel, string message)
        {
            OnLogMessage?.Invoke(message, logLevel);
        }

        public void Log(string message)
        {
            Log(LogLevel.Info, message);
        }

        public void Dispose()
        {
            DisposeGroups();

            DisposeTasks();

            DisposeEvents();

            NativeWrapper.DestroyNativeServer();
        }
    }
}
