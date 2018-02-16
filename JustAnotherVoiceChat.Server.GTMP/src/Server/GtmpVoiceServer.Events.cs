/*
 * File: GtmpVoiceServer.Events.cs
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

using GrandTheftMultiplayer.Server.Constant;
using GrandTheftMultiplayer.Server.Elements;
using JustAnotherVoiceChat.Server.GTMP.Interfaces;
using JustAnotherVoiceChat.Server.Wrapper;
using JustAnotherVoiceChat.Server.Wrapper.Enums;
using JustAnotherVoiceChat.Server.Wrapper.Interfaces;

namespace JustAnotherVoiceChat.Server.GTMP.Server
{
    internal partial class GtmpVoiceServer
    {

        public new event Delegates.EmptyEvent OnServerStarted;
        public new event Delegates.EmptyEvent OnServerStopping;
        
        public new event GtmpVoiceDelegates.GtmpVoiceClientEvent OnClientPrepared;
        
        public new event GtmpVoiceDelegates.GtmpVoiceClientEvent OnClientConnected;
        public new event GtmpVoiceDelegates.GtmpVoiceClientEvent OnClientDisconnected;

        public new event GtmpVoiceDelegates.GtmpVoiceClientStatusEvent OnClientTalkingChanged;

        private void AttachToEvents()
        {
            _api.onPlayerFinishedDownload += OnPlayerConnect;
            _api.onPlayerDisconnected += OnPlayerDisconnect;

            OnServerStarted += OnVoiceServerStarted;
            OnServerStopping += OnVoiceServerStopping;
            
            OnClientConnected += OnVoiceClientConnected;
            OnClientDisconnected += OnVoiceClientDisconnected;

            OnClientTalkingChanged += OnVoiceClientTalkingChanged;
        }

        private void OnVoiceServerStarted()
        {
            foreach (var player in _api.getAllPlayers())
            {
                RegisterPlayer(player);
            }
            
            OnServerStarted?.Invoke();
        }

        private void OnVoiceServerStopping()
        {
            foreach (var client in GetClients<IGtmpVoiceClient>())
            {
                UnregisterPlayer(client.Player);
            }
            
            OnServerStopping?.Invoke();
        }

        private void OnPlayerConnect(Client player)
        {
            if (RegisterPlayer(player) != null)
            {
                return;
            }

            player.kick("Failed to start VoiceClient!");
            _api.consoleOutput(LogCat.Error, $"Failed to start VoiceClient for Client {player.name}");
        }

        private void OnPlayerDisconnect(Client player, string reason)
        {
            UnregisterPlayer(player);
        }
        
        private void OnVoiceClientConnected(IVoiceClient client)
        {
            var voiceClient = client as IGtmpVoiceClient;
            if (voiceClient == null)
            {
                return;
            }
            
            
            OnClientConnected?.Invoke(voiceClient);
        }

        private void OnVoiceClientDisconnected(IVoiceClient client)
        {
            var voiceClient = client as IGtmpVoiceClient;
            if (voiceClient == null)
            {
                return;
            }
            
            OnClientDisconnected?.Invoke(voiceClient);
        }

        private void OnVoiceClientTalkingChanged(IVoiceClient client, bool newStatus)
        {
            var voiceClient = client as IGtmpVoiceClient;
            if (voiceClient == null)
            {
                return;
            }
            
            OnClientTalkingChanged?.Invoke(voiceClient, newStatus);
        }

        public void TriggerOnClientConnectedEvent(ushort handle)
        {
            FireClientConnected(handle);
        }

        public void TriggerOnClientDisonnectedEvent(ushort handle)
        {
            FireClientDisconnected(handle);
        }

        public void TriggerTalkingChangeEvent(ushort handle, bool newStatus)
        {
            FireClientTalkingChange(handle, newStatus);
        }
    }
}
