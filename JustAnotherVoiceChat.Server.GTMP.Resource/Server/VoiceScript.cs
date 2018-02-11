/*
 * File: VoiceScript.cs
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

using AlternateVoice.Server.GTMP.Factories;
using AlternateVoice.Server.GTMP.Interfaces;
using GrandTheftMultiplayer.Server.API;
using GrandTheftMultiplayer.Server.Elements;

namespace AlternateVoice.Server.GTMP.Resource
{
    public partial class VoiceScript : Script
    {

        private readonly IGtmpVoiceServer _voiceServer;

        public VoiceScript()
        {
            _voiceServer = GtmpVoice.CreateServer(API, "game.alternate-life.de", 23332, 23);

            AttachServerEvents(_voiceServer);

            API.onClientEventTrigger += OnClientEventTrigger;
            API.onResourceStop += OnResourceStop;

            _voiceServer.Start();
        }

        private void OnClientEventTrigger(Client sender, string eventName, params object[] arguments)
        {
            if (eventName != "VOICE_ROTATION")
            {
                return;
            }

            var rotation = (float)arguments[0];
            var voiceClient = _voiceServer.GetVoiceClientOfPlayer(sender);

            if (voiceClient == null)
            {
                return;
            }

            voiceClient.CameraRotation = rotation;
        }

        private void OnResourceStop()
        {
            _voiceServer.Stop();
            _voiceServer.Dispose();
        }
    }
}
