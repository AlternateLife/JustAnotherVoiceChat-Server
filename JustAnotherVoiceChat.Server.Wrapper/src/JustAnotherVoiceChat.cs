/*
 * File: JustAnotherVoiceChat.cs
 * Date: 11.2.2018,
 *
 * MIT License
 *
 * Copyright (c) 2018 Alternate-Life.de
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

using JustAnotherVoiceChat.Server.Wrapper.Elements.Server;
using JustAnotherVoiceChat.Server.Wrapper.Elements.Wapper;
using JustAnotherVoiceChat.Server.Wrapper.Elements.Wrapper3D;
using JustAnotherVoiceChat.Server.Wrapper.Interfaces;

namespace JustAnotherVoiceChat.Server.Wrapper
{
    public static class JustAnotherVoiceChat
    {
        public static IVoiceServer MakeServer(IVoiceClientRepository repository, string hostname, ushort port, int channelId)
        {
            return new VoiceServer(repository, new VoiceWrapper(), new VoiceWrapper3D(), hostname, port, channelId);
        }

        public static IVoiceServer MakeServer(IVoiceClientRepository repository, string hostname, ushort port, int channelId, float globalRollOffScale, float globalDistanceFactor, double globalMaxDistance)
        {
            return new VoiceServer(repository, new VoiceWrapper(), new VoiceWrapper3D(), hostname, port, channelId, globalRollOffScale, globalDistanceFactor, globalMaxDistance);
        }
    }
}
