/*
 * File: GtmpVoiceServer.Players.cs
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
using System.Collections.Concurrent;
using System.Linq;
using AlternateVoice.Server.GTMP.Clients;
using AlternateVoice.Server.GTMP.Interfaces;
using AlternateVoice.Server.Wrapper.Interfaces;
using GrandTheftMultiplayer.Server.Elements;
using GrandTheftMultiplayer.Shared;

namespace AlternateVoice.Server.GTMP.Server
{
    internal partial class GtmpVoiceServer
    {
        public IGtmpVoiceClient GetVoiceClientOfPlayer(Client player)
        {
            return _server.FindClient<IGtmpVoiceClient>(c => c.Player == player);
        }

        private IGtmpVoiceClient RegisterPlayer(Client player)
        {
            var voiceClient = _server.CreateClient(player) as IGtmpVoiceClient;
            if (voiceClient == null)
            {
                return null;
            }
            
            OnClientPrepared?.Invoke(voiceClient);
            
            return voiceClient;
        }

        private void UnregisterPlayer(Client player)
        {
            if (player == null)
            {
                throw new ArgumentNullException(nameof(player));
            }

            GetVoiceClientOfPlayer(player)?.Dispose();
        }
    }
}
