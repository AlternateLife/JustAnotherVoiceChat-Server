/*
 * File: VoiceScript.cs
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
using GrandTheftMultiplayer.Server.API;
using JustAnotherVoiceChat.Server.GTMP.Factories;
using JustAnotherVoiceChat.Server.GTMP.Interfaces;
using JustAnotherVoiceChat.Server.GTMP.Resource.Helpers;
using JustAnotherVoiceChat.Server.Wrapper.Elements.Models;
using JustAnotherVoiceChat.Server.Wrapper.Elements.Tasks;

namespace JustAnotherVoiceChat.Server.GTMP.Resource
{
    public partial class VoiceScript : Script
    {

        private IGtmpVoiceServer _voiceServer;

        private readonly TelephoneHandler _phoneHandler;

        public VoiceScript()
        {
            API.onResourceStop += OnResourceStop;
            API.onResourceStart += OnResourceStart;
        }

        private void OnResourceStart()
        {
            // Create a JustAnotherVoiceServer based GtmpVoice Server!
            var hostname = API.getResourceSetting<string>("justanothervoicechat", "voice_host");
            var port = API.getResourceSetting<ushort>("justanothervoicechat", "voice_port");
            
            var teamspeakServerId = API.getResourceSetting<string>("justanothervoicechat", "voice_teamspeak_serverid");
            var teamspeakChannelId = API.getResourceSetting<ulong>("justanothervoicechat", "voice_teamspeak_channelid");
            var teamspeakChannelPassword = API.getResourceSetting<string>("justanothervoicechat", "voice_teamspeak_channelpassword");
            
            _voiceServer = GtmpVoice.CreateServer(API, new VoiceServerConfiguration(hostname, port, teamspeakServerId, teamspeakChannelId, teamspeakChannelPassword));
            
            // Enables 3D Voice
            _voiceServer.AddTask(new PositionalVoiceTask<IGtmpVoiceClient>());

            // Attach to some IVoiceServer events to react to certain them with certain actions.
            AttachToVoiceServerEvents();
            
            // Attach to GTMP-Events, so the connection works and camera rotation updates.
            AttachToGtmpEvents(true);
            
            _phoneHandler = new TelephoneHandler(_voiceServer);

            // Startup VoiceServer, so players are able to connect.
            _voiceServer.Start();
        }

        private void OnResourceStop()
        {
            _voiceServer.Stop();
            _voiceServer.Dispose();
        }
    }
}
