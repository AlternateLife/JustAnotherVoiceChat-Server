﻿/*
 * File: GtmpClientRepository.cs
 * Date: 17.2.2018,
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
using JustAnotherVoiceChat.Server.GTMP.Elements.Clients;
using JustAnotherVoiceChat.Server.GTMP.Elements.Server;
using JustAnotherVoiceChat.Server.GTMP.Interfaces;
using JustAnotherVoiceChat.Server.Wrapper.Interfaces;
using JustAnotherVoiceChat.Server.Wrapper.Structs;

namespace JustAnotherVoiceChat.Server.GTMP.Factories
{
    public class GtmpVoiceClientFactory : IGtmpVoiceClientFactory
    {
        public IGtmpVoiceClient MakeClient(Client player, IVoiceServer<IGtmpVoiceClient> server, VoiceHandle handle)
        {
            return new GtmpVoiceClient(player, (GtmpVoiceServer) server, handle);
        }
    }
}
