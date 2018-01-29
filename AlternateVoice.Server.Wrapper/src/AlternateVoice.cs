﻿#region copyright 
/*
 * File: AlternateVoice.cs
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
#endregion
using AlternateVoice.Server.Wrapper.Elements.Server;
using AlternateVoice.Server.Wrapper.Interfaces;

namespace AlternateVoice.Server.Wrapper
{
    public static class AlternateVoice
    {

        public static IVoiceServer Server { get; private set; }
        
        public static IVoiceServer MakeAndStoreServer(string hostname, ushort port, int channelId)
        {
            if (Server == null)
            {
                Server = MakeServer(hostname, port, channelId);
            }

            return Server;
        }

        public static IVoiceServer MakeServer(string hostname, ushort port, int channelId)
        {
            return new VoiceServer(hostname, port, channelId);
        }


    }
}
