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

using GrandTheftMultiplayer.Server.API;
using GrandTheftMultiplayer.Server.Constant;
using GrandTheftMultiplayer.Server.Elements;
using JustAnotherVoiceChat.Server.GTMP.Extensions;
using JustAnotherVoiceChat.Server.GTMP.Factories;
using JustAnotherVoiceChat.Server.GTMP.Interfaces;
using JustAnotherVoiceChat.Server.Wrapper.Elements.Models;
using JustAnotherVoiceChat.Server.Wrapper.Elements.Tasks;

namespace JustAnotherVoiceChat.Server.GTMP.Resource
{
    public partial class VoiceScript : Script
    {

        private readonly IGtmpVoiceServer _voiceServer;

        public VoiceScript()
        {
            // Create a JustAnotherVoiceServer based GtmpVoice Server.
            _voiceServer = GtmpVoice.CreateServer(API, new VoiceServerConfiguration("localhost", 23332, "S1u8otSWS/L/V1luEkMnupTwgeA=", 130, "123"));
            
            // Enables 3D Voice
            _voiceServer.AddTask(new PositionalVoiceTask<IGtmpVoiceClient>());

            AttachServerEvents(_voiceServer);

            API.onClientEventTrigger += OnClientEventTrigger;
            API.onResourceStop += OnResourceStop;
            
            API.onPlayerFinishedDownload += OnPlayerFinishedDownload;

            // Startup VoiceServer
            _voiceServer.Start();
        }

        private void OnPlayerFinishedDownload(Client player)
        {
            player.setSkin(PedHash.FreemodeMale01);
        }

        private void OnClientEventTrigger(Client sender, string eventName, params object[] arguments)
        {
            if (eventName != "VOICE_ROTATION")
            {
                return;
            }

            sender.SetVoiceRotation((float) arguments[0]);
        }

        private void OnResourceStop()
        {
            _voiceServer.Stop();
            _voiceServer.Dispose();
        }
    }
}
