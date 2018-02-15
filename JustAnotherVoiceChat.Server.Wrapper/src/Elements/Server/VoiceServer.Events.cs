/*
 * File: VoiceServer.Events.cs
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

using JustAnotherVoiceChat.Server.Wrapper.Interfaces;

namespace JustAnotherVoiceChat.Server.Wrapper.Elements.Server
{
    internal partial class VoiceServer
    {
        public event Delegates.EmptyEvent OnServerStarted;
        public event Delegates.EmptyEvent OnServerStopping;
        
        public event Delegates.ClientEvent OnClientConnected;
        public event Delegates.ClientDisconnected OnClientDisconnected;

        public event Delegates.ClientStatusEvent OnClientTalkingChanged;
        public event Delegates.ClientStatusEvent OnClientSpeakersMuteChanged;
        public event Delegates.ClientStatusEvent OnClientMicrophoneMuteChanged;

        public event Delegates.ClientGroupEvent OnClientJoinedGroup;
        public event Delegates.ClientGroupEvent OnClientLeftGroup;
        
        private void DisposeEvents()
        {
            OnServerStarted = null;
            OnServerStopping = null;
            
            OnClientConnected = null;
            OnClientConnected = null;
            
            OnClientJoinedGroup = null;
            OnClientLeftGroup = null;
            
            DisposeNativeEvents();
        }

        internal void FireClientJoinedGroup(IVoiceClient client, IVoiceGroup group)
        {
            OnClientJoinedGroup?.Invoke(client, group);
        }

        internal void FireClientLeftGroup(IVoiceClient client, IVoiceGroup group)
        {
            OnClientLeftGroup?.Invoke(client, group);
        }
        
    }
}
