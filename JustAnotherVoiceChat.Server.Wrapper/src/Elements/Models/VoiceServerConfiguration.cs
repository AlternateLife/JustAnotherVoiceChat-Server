/*
 * File: VoiceServerConfiguration.cs
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
using JustAnotherVoiceChat.Server.Wrapper.Exceptions;

namespace JustAnotherVoiceChat.Server.Wrapper.Elements.Models
{
    public class VoiceServerConfiguration
    {
        
        public string Hostname { get; }
        public ushort Port { get; }
        
        public string TeamspeakServerId { get; }
        public ulong TeamspeakChannelId { get; }
        public string TeamspeakChannelPassword { get; }
        
        public float GlobalRollOffScale { get; }
        public float GlobalDistanceFactor { get; }
        public double GlobalMaxDistance { get; }

        public VoiceServerConfiguration(string hostname, ushort port, string teamspeakServerId, ulong teamspeakChannelId, string teamspeakChannelPassword)
            : this(hostname, port, teamspeakServerId, teamspeakChannelId, teamspeakChannelPassword, 1, 1, 10f)
        {
        }

        public VoiceServerConfiguration(string hostname, ushort port, string teamspeakServerId, ulong teamspeakChannelId, string teamspeakChannelPassword, float globalRollOffScale, float globalDistanceFactor, double globalMaxDistance)
        {
            if (string.IsNullOrWhiteSpace(hostname) || Uri.CheckHostName(hostname) == UriHostNameType.Unknown)
            {
                throw new ArgumentException($"The provided hostname \"{hostname}\" is invalid!");
            }

            if (string.IsNullOrWhiteSpace(teamspeakServerId))
            {
                throw new ArgumentException($"The provided teamspeakServerId \"{teamspeakServerId}\" is invalid!");
            }
            
            Hostname = hostname;
            Port = port;

            TeamspeakServerId = teamspeakServerId;
            TeamspeakChannelId = teamspeakChannelId;
            TeamspeakChannelPassword = teamspeakChannelPassword;

            GlobalRollOffScale = globalRollOffScale;
            GlobalDistanceFactor = globalDistanceFactor;
            GlobalMaxDistance = globalMaxDistance;
        }
        
    }
}
