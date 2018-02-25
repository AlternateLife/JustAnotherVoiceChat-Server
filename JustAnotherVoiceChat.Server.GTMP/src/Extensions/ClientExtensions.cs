/*
 * File: ClientExtensions.cs
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

using GrandTheftMultiplayer.Server.Elements;
using GrandTheftMultiplayer.Shared.Math;
using JustAnotherVoiceChat.Server.GTMP.Exceptions;
using JustAnotherVoiceChat.Server.GTMP.Factories;
using JustAnotherVoiceChat.Server.GTMP.Interfaces;

namespace JustAnotherVoiceChat.Server.GTMP.Extensions
{
    public static class ClientExtensions
    {

        private static IGtmpVoiceServer Server => GtmpVoice.Shared;

        public static IGtmpVoiceClient GetVoiceClient(this Client client)
        {
            if (Server == null)
            {
                throw new ServerNotSharedException();
            }

            return Server.GetVoiceClient(client);
        }

        public static void SetVoiceRotation(this Client client, float rotation)
        {
            var voiceClient = client.GetVoiceClient();
            if (voiceClient != null)
            {
                voiceClient.CameraRotation = rotation;
            }
        }

        public static void SetVoiceRange(this Client listener, float range)
        {
            listener.GetVoiceClient()?.SetVoiceRange(range);
        }

        public static bool SetListeningPosition(this Client listener, Vector3 position, float rotation)
        {
            return listener.GetVoiceClient()?.SetListeningPosition(new Wrapper.Math.Vector3(position.X, position.Y, position.Z), rotation) ?? false;
        }

        public static bool SetRelativeSpeakerPosition(this Client listener, Client speaker, Vector3 position)
        {
            return listener.GetVoiceClient()?.SetRelativeSpeakerPosition(speaker, position) ?? false;
        }

        public static bool ResetRelativeSpeakerPosition(this Client listener, Client speaker)
        {
            return listener.GetVoiceClient()?.ResetRelativeSpeakerPosition(speaker) ?? false;
        }

        public static bool ResetAllRelativeSpeakerPositions(this Client listener)
        {
            return listener.GetVoiceClient()?.ResetAllRelativeSpeakerPositions() ?? false;
        }

        public static bool MuteForAll(this Client listener, bool muted)
        {
            return listener.GetVoiceClient()?.MuteForAll(muted) ?? false;
        }
        
        public static bool IsMutedForAll(this Client listener)
        {
            return listener.GetVoiceClient()?.IsMutedForAll() ?? false;
        }

        public static bool MuteSpeaker(this Client listener, Client speaker, bool muted)
        {
            return listener.GetVoiceClient()?.MuteSpeaker(speaker, muted) ?? false;
        }

        public static bool IsSpeakerMuted(this Client listener, Client speaker)
        {
            return listener.GetVoiceClient()?.IsSpeakerMuted(speaker) ?? false;
        }
    }
}
