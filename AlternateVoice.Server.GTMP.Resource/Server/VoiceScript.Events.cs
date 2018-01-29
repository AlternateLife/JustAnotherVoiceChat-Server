/*
 * File: VoiceScript.Events.cs
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
using GrandTheftMultiplayer.Server.Constant;
using GrandTheftMultiplayer.Shared.Gta.Tasks;

namespace AlternateVoice.Server.GTMP.Resource
{
    public partial class VoiceScript
    {

        private void AttachServerEvents(IGtmpVoiceServer server)
        {
            server.OnServerStarted += () =>
            {
                API.consoleOutput(LogCat.Info, $"GtmpVoiceServer started, listening on {server.Hostname}:{server.Port}");
            };
            
            server.OnServerStopping += () =>
            {
                API.consoleOutput(LogCat.Info, "GtmpVoiceServer stopping!");
            };

            server.OnClientPrepared += OnHandshakeShouldResend;
            
            server.OnClientConnected += OnClientConnected;
            server.OnClientDisconnected += OnHandshakeShouldResend;

            server.OnPlayerStartsTalking += OnPlayerStartsTalking;
            server.OnPlayerStopsTalking += OnPlayerStopsTalking;
        }

        private void OnClientConnected(IGtmpVoiceClient client)
        {
            client.Player.triggerEvent("VOICE_SET_HANDSHAKE", false);
        }

        private void OnHandshakeShouldResend(IGtmpVoiceClient client)
        {
            client.Player.triggerEvent("VOICE_SET_HANDSHAKE", true, client.HandshakeUrl);
        }
        
        private void OnPlayerStartsTalking(IGtmpVoiceClient speakingClient)
        {
            API.playPlayerAnimation(speakingClient.Player, (int)(AnimationFlag.Loop | AnimationFlag.AllowRotation), "mp_facial", "mic_chatter");
            // Needs further investigation of animation handling 
            //API.sendNativeToPlayersInRange(speakingClient.Player.position, 300f, Hash.TASK_PLAY_ANIM, speakingClient.Player.handle, "mp_facial", "mic_chatter",
            //  8f, -4f, -1, (int)(AnimationFlag.Loop | AnimationFlag.AllowRotation), 0.0f, false, false, false);
        }

        private void OnPlayerStopsTalking(IGtmpVoiceClient speakingClient)
        {
            // Stopping all animations?
            API.stopPlayerAnimation(speakingClient.Player);
            // Needs further investigation of animation handling 
            // API.sendNativeToPlayersInRange(speakingClient.Player.position, 300f, Hash.TASK_PLAY_ANIM, speakingClient.Player.handle, "mp_facial", "mic_chatter1",
            //    8f, -4f, -1, (int)(AnimationFlag.Loop | AnimationFlag.AllowRotation), 0.0f, false, false, false);
        }
        
    }
}
