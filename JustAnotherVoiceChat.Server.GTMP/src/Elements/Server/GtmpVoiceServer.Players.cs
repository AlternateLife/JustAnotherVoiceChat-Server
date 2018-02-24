/*
 * File: GtmpVoiceServer.Players.cs
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
using GrandTheftMultiplayer.Server.Elements;
using JustAnotherVoiceChat.Server.GTMP.Interfaces;

namespace JustAnotherVoiceChat.Server.GTMP.Elements.Server
{
    public partial class GtmpVoiceServer
    {
        private const string PlayerDataKey = "JV_HANDLE";
        
        public IGtmpVoiceClient GetVoiceClient(Client player)
        {
            if (player.hasData(PlayerDataKey))
            {
                return (IGtmpVoiceClient) player.getData(PlayerDataKey);
            }
            
            return FindClient(c => c.Player == player);
        }

        private IGtmpVoiceClient RegisterPlayer(Client player)
        {
            var voiceClient = PrepareClient(player);
            if (voiceClient == null)
            {
                return null;
            }
            
            player.setData(PlayerDataKey, voiceClient);
            
            OnClientPrepared?.Invoke(voiceClient);
            
            return voiceClient;
        }

        private void UnregisterPlayer(Client player)
        {
            if (player == null)
            {
                throw new ArgumentNullException(nameof(player));
            }

            var client = GetVoiceClient(player);
            if (client != null)
            {
                RemoveClient(client);
                client.Player.resetData(PlayerDataKey);
            }
        }
    }
}
