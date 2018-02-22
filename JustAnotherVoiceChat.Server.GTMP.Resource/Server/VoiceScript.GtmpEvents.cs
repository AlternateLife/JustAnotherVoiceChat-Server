/*
 * File: VoiceScript.GtmpEvents.cs
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

using GrandTheftMultiplayer.Server.Constant;
using GrandTheftMultiplayer.Server.Elements;
using JustAnotherVoiceChat.Server.GTMP.Extensions;

namespace JustAnotherVoiceChat.Server.GTMP.Resource
{
    public partial class VoiceScript
    {

        private void AttachToGtmpEvents(bool testingEvents)
        {
            API.onClientEventTrigger += OnClientEventTrigger;

            if (testingEvents)
            {
                API.onPlayerFinishedDownload += OnPlayerFinishedDownload;
            }
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
    }
}
