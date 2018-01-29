#region copyright 
/*
 * File: GtmpVoiceClient.cs
 * Date: 29.29.2018,
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
#endregion
using System;
using AlternateVoice.Server.GTMP.Interfaces;
using AlternateVoice.Server.Wrapper.Interfaces;
using GrandTheftMultiplayer.Server.Elements;

namespace AlternateVoice.Server.GTMP.Clients
{
    internal class GtmpVoiceClient : IGtmpVoiceClient
    {
        public IVoiceClient VoiceClient { get; }
        public Client Player { get; }

        public bool Connected => VoiceClient.Connected;
        
        public bool Headphones => VoiceClient.Headphones;
        public bool Microphone => VoiceClient.Microphone;

        public float CameraRotation
        {
            get { return VoiceClient.CameraRotation; }
            set { VoiceClient.CameraRotation = value; }
        }

        public string HandshakeUrl => VoiceClient.HandshakeUrl;

        internal GtmpVoiceClient(Client player, IVoiceClient client)
        {
            Player = player;
            VoiceClient = client;
        }

        public void Dispose()
        {
            VoiceClient.Dispose();
            
            GC.SuppressFinalize(this);
        }
        
    }
}
