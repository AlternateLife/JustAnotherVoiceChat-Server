#region copyright 
/*
 * File: GtmpVoiceServer.Players.cs
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
        
        private readonly ConcurrentDictionary<NetHandle, IGtmpVoiceClient> _clients = new ConcurrentDictionary<NetHandle, IGtmpVoiceClient>();
        
        public IGtmpVoiceClient GetVoiceClientOfPlayer(Client player)
        {
            IGtmpVoiceClient result;
            if (_clients.TryGetValue(player.handle, out result))
            {
                return result;
            }

            return null;
        }
        
        private IGtmpVoiceClient GetVoiceClient(IVoiceClient client)
        {
            return _clients.Values.ToArray().FirstOrDefault(voiceClient => ReferenceEquals(voiceClient.VoiceClient, client));
        }

        private IGtmpVoiceClient RegisterPlayer(Client player)
        {
            var voiceClient = _server.CreateClient();
            if (voiceClient == null)
            {
                return null;
            }
            
            var client = new GtmpVoiceClient(player, voiceClient);
            if (!_clients.TryAdd(player.handle, client))
            {
                return null;
            }
            
            OnClientPrepared?.Invoke(client);
            
            return client;
        }

        private void UnregisterPlayer(Client player)
        {
            if (player == null)
            {
                throw new ArgumentNullException(nameof(player));
            }

            IGtmpVoiceClient client;
            if (!_clients.TryGetValue(player.handle, out client))
            {
                return;
            }
            
            OnClientDisconnected?.Invoke(client);

            if (!_clients.TryRemove(player.handle, out client))
            {
                return;
            }

            var voiceClient = client as GtmpVoiceClient;
            voiceClient?.Dispose();
        }
    }
}
