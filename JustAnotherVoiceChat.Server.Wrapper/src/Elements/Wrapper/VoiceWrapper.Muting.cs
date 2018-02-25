/*
 * File: VoiceWrapper.Muting.cs
 * Date: 25.2.2018,
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

using JustAnotherVoiceChat.Server.Wrapper.Interfaces;

namespace JustAnotherVoiceChat.Server.Wrapper.Elements.Wrapper
{
    internal partial class VoiceWrapper
    {
        public bool MuteClientForAll(IVoiceClient client, bool muted)
        {
            return NativeLibary.JV_MuteClientForAll(client.Handle.Identifer, muted);
        }

        public bool IsClientMutedForAll(IVoiceClient client)
        {
            return NativeLibary.JV_IsClientMutedForAll(client.Handle.Identifer);
        }

        public bool MuteClientForClient(IVoiceClient speaker, IVoiceClient listener, bool muted)
        {
            return NativeLibary.JV_MuteClientForClient(speaker.Handle.Identifer, listener.Handle.Identifer, muted);
        }

        public bool IsClientMutedForClient(IVoiceClient speaker, IVoiceClient listener)
        {
            return NativeLibary.JV_IsClientMutedForClient(speaker.Handle.Identifer, listener.Handle.Identifer);
        }
    }
}
