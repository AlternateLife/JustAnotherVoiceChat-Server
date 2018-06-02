/*
 * File: VoiceServer.Events.Dispatcher.cs
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

using JustAnotherVoiceChat.Server.Wrapper.Elements.Models;
using JustAnotherVoiceChat.Server.Wrapper.Enums;
using JustAnotherVoiceChat.Server.Wrapper.Interfaces;

namespace JustAnotherVoiceChat.Server.Wrapper.Elements.Server
{
    public partial class VoiceServer<TClient, TIdentifier> where TClient : IVoiceClient
    {
        private bool OnClientConnectingFromVoice(ushort handle, string teamspeakId)
        {
            Log(LogLevel.Trace, $"OnClientConnectingFromVoice({handle}, {teamspeakId})");
            return RunWhenClientValid(handle, client =>
            {
                var eventArgs = new ClientConnectingEventArgs();
                InvokeProtectedEvent(() => OnClientConnecting?.Invoke(client, teamspeakId, eventArgs));
                
                return !eventArgs.Reject;
            });
        }

        private async void OnClientConnectedFromVoice(ushort handle)
        {
            Log(LogLevel.Trace, $"OnClientConnectedFromVoice({handle})");
            await RunWhenClientValidAsync(handle, async client =>
            {
                await InvokeProtectedEventAsync(() => OnClientConnected?.Invoke(client));
            });
        }
        
        private async void OnClientRejectedFromVoice(ushort handle, int statusCode)
        {
            Log(LogLevel.Trace, $"OnClientRejectedFromVoice({handle}, {statusCode})");
            await RunWhenClientValidAsync(handle, async client =>
            {                
                await InvokeProtectedEventAsync(() => OnClientRejected?.Invoke(client, (StatusCode) statusCode));
            });
        }

        private async void OnClientDisconnectedFromVoice(ushort handle)
        {
            Log(LogLevel.Trace, $"OnClientDisconnectedFromVoice({handle})");
            await RunWhenClientValidAsync(handle, async client =>
            {
                await InvokeProtectedEventAsync(() => OnClientDisconnected?.Invoke(client));
            });
        }

        private async void OnClientTalkingStatusChangedFromVoice(ushort handle, bool newStatus)
        {
            Log(LogLevel.Trace, $"OnClientTalkingStatusChangedFromVoice({handle}, {newStatus})");
            await RunWhenClientValidAsync(handle, async client =>
            {
                await InvokeProtectedEventAsync(() => OnClientTalkingChanged?.Invoke(client, newStatus));
            });
        }

        private async void OnClientSpeakersMuteChangedFromVoice(ushort handle, bool newStatus)
        {
            Log(LogLevel.Trace, $"OnClientSpeakersMuteChangedFromVoice({handle}, {newStatus})");
            await RunWhenClientValidAsync(handle, async client =>
            {
                await InvokeProtectedEventAsync(() => OnClientSpeakersMuteChanged?.Invoke(client, newStatus));
            });
        }

        private async void OnClientMicrophoneMuteChangedFromVoice(ushort handle, bool newStatus)
        {
            Log(LogLevel.Trace, $"OnClientMicrophoneMuteChangedFromVoice({handle}, {newStatus})");
            await RunWhenClientValidAsync(handle, async client =>
            {
                await InvokeProtectedEventAsync(() => OnClientMicrophoneMuteChanged?.Invoke(client, newStatus));
            });
        }

        private async void OnLogMessageFromVoice(string message, int loglevel)
        {
            await InvokeProtectedEventAsync(() => Log((LogLevel) loglevel, message));
        }
    }
}
