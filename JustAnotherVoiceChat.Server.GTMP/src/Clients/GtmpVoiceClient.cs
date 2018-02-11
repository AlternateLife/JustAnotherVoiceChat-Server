/*
 * File: GtmpVoiceClient.cs
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
using AlternateVoice.Server.GTMP.Interfaces;
using AlternateVoice.Server.Wrapper.Elements.Client;
using AlternateVoice.Server.Wrapper.Interfaces;
using AlternateVoice.Server.Wrapper.Math;
using AlternateVoice.Server.Wrapper.Structs;
using GrandTheftMultiplayer.Server.Elements;

namespace AlternateVoice.Server.GTMP.Clients
{
    internal class GtmpVoiceClient : VoiceClient, IGtmpVoiceClient
    {
        public Client Player { get; }

        public override Vector3 Position => new Vector3(Player.position.X, Player.position.Y, Player.position.Z);
        
        internal GtmpVoiceClient(Client player, IVoiceServer server, VoiceHandle handle) : base(server, server.VoiceWrapper3D, handle)
        {
            Player = player;
        }

        public override void Dispose()
        {
            base.Dispose();
            GC.SuppressFinalize(this);
        }
        
    }
}
