/*
 * File: VoiceWrapper.Clients.cs
 * Date: 15.2.2018,
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
        public bool RemoveClient(IVoiceClient client)
        {
            return NativeLibary.JV_RemoveClient(client.Handle.Identifer);
        }
        
        public bool SetClientNickname(IVoiceClient client, string nickname)
        {
            return NativeLibary.JV_SetClientNickname(client.Handle.Identifer, nickname);
        }

        public void SetClientVoiceRange(IVoiceClient client, float voiceRange)
        {
            NativeLibary.JV_SetClientVoiceRange(client.Handle.Identifer, voiceRange);
        }
    }
}
