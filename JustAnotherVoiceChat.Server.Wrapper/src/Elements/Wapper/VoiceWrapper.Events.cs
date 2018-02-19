/*
 * File: VoiceWrapper.Events.cs
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

using JustAnotherVoiceChat.Server.Wrapper.Delegates;
using JustAnotherVoiceChat.Server.Wrapper.Interfaces;

namespace JustAnotherVoiceChat.Server.Wrapper.Elements.Wapper
{
    internal partial class VoiceWrapper<TClient> where TClient : IVoiceClient<TClient>
    {

        public void RegisterClientConnectedCallback(NativeDelegates.ClientConnectCallback callback)
        {
            NativeLibary.JV_RegisterClientConnectedCallback(callback);
        }

        public void RegisterClientDisconnectedCallback(NativeDelegates.ClientCallback callback)
        {
            NativeLibary.JV_RegisterClientDisconnectedCallback(callback);
        }

        public void RegisterClientTalkingChangedCallback(NativeDelegates.ClientStatusCallback callback)
        {
            NativeLibary.JV_RegisterClientTalkingChangedCallback(callback);
        }

        public void RegisterClientSpeakersMuteChangedCallback(NativeDelegates.ClientStatusCallback callback)
        {
            NativeLibary.JV_RegisterClientSpeakersMuteChangedCallback(callback);
        }

        public void RegisterClientMicrophoneMuteChangedCallback(NativeDelegates.ClientStatusCallback callback)
        {
            NativeLibary.JV_RegisterClientMicrophoneMuteChangedCallback(callback);
        }

        public void UnregisterClientConnectedCallback()
        {
            NativeLibary.JV_UnregisterClientConnectedCallback();
        }

        public void UnregisterClientDisconnectedCallback()
        {
            NativeLibary.JV_UnregisterClientDisconnectedCallback();
        }
        public void UnregisterClientTalkingChangedCallback()
        {
            NativeLibary.JV_UnregisterClientTalkingChangedCallback();
        }

        public void UnregisterClientSpeakersMuteChangedCallback()
        {
            NativeLibary.JV_UnregisterClientSpeakersMuteChangedCallback();
        }

        public void UnregisterClientMicrophoneMuteChangedCallback()
        {
            NativeLibary.JV_UnregisterClientMicrophoneMuteChangedCallback();
        }
    }
}
