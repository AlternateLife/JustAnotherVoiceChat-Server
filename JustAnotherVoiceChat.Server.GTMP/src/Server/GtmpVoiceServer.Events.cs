/*
 * File: GtmpVoiceServer.Events.cs
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

using AlternateVoice.Server.GTMP.Interfaces;
using AlternateVoice.Server.Wrapper;
using AlternateVoice.Server.Wrapper.Enums;
using AlternateVoice.Server.Wrapper.Interfaces;
using GrandTheftMultiplayer.Server.Constant;
using GrandTheftMultiplayer.Server.Elements;

namespace AlternateVoice.Server.GTMP.Server
{
    internal partial class GtmpVoiceServer
    {

        public event Delegates.EmptyEvent OnServerStarted;
        public event Delegates.EmptyEvent OnServerStopping;
        
        public event GtmpVoiceDelegates.GtmpVoiceClientEvent OnClientPrepared;
        
        public event GtmpVoiceDelegates.GtmpVoiceClientEvent OnClientConnected;
        public event GtmpVoiceDelegates.GtmpVoiceClientEvent OnClientDisconnected;

        public event GtmpVoiceDelegates.GtmpVoiceClientStatusEvent OnClientTalkingChanged;

        private void AttachToEvents()
        {
            _api.onPlayerFinishedDownload += OnPlayerConnect;
            _api.onPlayerDisconnected += OnPlayerDisconnect;

            _server.OnServerStarted += OnVoiceServerStarted;
            _server.OnServerStopping += OnVoiceServerStopping;
            
            _server.OnClientConnected += OnVoiceClientConnected;
            _server.OnClientDisconnected += OnVoiceClientDisconnected;

            _server.OnClientTalkingChanged += OnVoiceClientTalkingChanged;
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
            foreach (var client in _server.GetClients<IGtmpVoiceClient>())
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

        private void OnVoiceClientDisconnected(IVoiceClient client, DisconnectReason reason)
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
            _server.FireClientConnected(handle);
        }

        public void TriggerOnClientDisonnectedEvent(ushort handle)
        {
            _server.FireClientDisconnected(handle);
        }

        public void TriggerTalkingChangeEvent(ushort handle, bool newStatus)
        {
            _server.FireClientTalkingChange(handle, newStatus);
        }
    }
}
