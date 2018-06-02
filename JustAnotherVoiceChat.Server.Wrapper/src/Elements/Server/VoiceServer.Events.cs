/*
 * File: VoiceServer.Events.cs
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

using System;
using System.Threading.Tasks;
using JustAnotherVoiceChat.Server.Wrapper.Delegates;
using JustAnotherVoiceChat.Server.Wrapper.Enums;
using JustAnotherVoiceChat.Server.Wrapper.Interfaces;

namespace JustAnotherVoiceChat.Server.Wrapper.Elements.Server
{
    public partial class VoiceServer<TClient, TIdentifier> where TClient : IVoiceClient
    {
        public event Delegates<TClient>.EmptyEvent OnServerStarted;
        public event Delegates<TClient>.EmptyEvent OnServerStopping;
        
        public event Delegates<TClient>.ClientEvent OnClientConnected;
        public event Delegates<TClient>.ClientConnectingEvent OnClientConnecting;
        public event Delegates<TClient>.ClientRejectedEvent OnClientRejected;
        public event Delegates<TClient>.ClientEvent OnClientDisconnected;

        public event Delegates<TClient>.ClientStatusEvent OnClientTalkingChanged;
        public event Delegates<TClient>.ClientMuteStatusEvent OnClientSpeakersMuteChanged;
        public event Delegates<TClient>.ClientMuteStatusEvent OnClientMicrophoneMuteChanged;

        public event Delegates<TClient>.LogMessageEvent OnLogMessage; 
        
        private void DisposeEvents()
        {
            OnServerStarted = null;
            OnServerStopping = null;
            
            OnClientConnected = null;
            OnClientConnecting = null;
            OnClientDisconnected = null;
            OnClientRejected = null;

            OnClientTalkingChanged = null;
            OnClientSpeakersMuteChanged = null;
            OnClientMicrophoneMuteChanged = null;
            
            OnLogMessage = null;

            DisposeNativeEvents();
        }

        internal void InvokeProtectedEvent(Action callback)
        {
            try
            {
                callback();
            }
            catch (Exception e)
            {
                Log(LogLevel.Error, $"An error occured inside eventhandler: {e}");
            }
        }

        internal async Task InvokeProtectedEventAsync(Action callback)
        {
            try
            {
                await Task.Run(callback).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                Log(LogLevel.Error, $"An error occured inside eventhandler: {e}");
            }
        }
    }
}
