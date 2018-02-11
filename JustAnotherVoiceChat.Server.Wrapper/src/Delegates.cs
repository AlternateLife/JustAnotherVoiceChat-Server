/*
 * File: Delegates.cs
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

using JustAnotherVoiceChat.Server.Wrapper.Enums;
using JustAnotherVoiceChat.Server.Wrapper.Interfaces;

namespace JustAnotherVoiceChat.Server.Wrapper
{
    public class Delegates
    {
        public delegate void EmptyEvent();
        
        public delegate void ClientEvent(IVoiceClient client);
        public delegate void ClientDisconnected(IVoiceClient client, DisconnectReason reason);

        public delegate void ClientGroupEvent(IVoiceClient client, IVoiceGroup group);

        public delegate void ClientStatusEvent(IVoiceClient client, bool newStatus);

        public delegate bool ClientConnectCallback(ushort handle);
        public delegate void ClientCallback(ushort handle);
        public delegate void ClientStatusCallback(ushort handle, bool newStatus);
    }
}
