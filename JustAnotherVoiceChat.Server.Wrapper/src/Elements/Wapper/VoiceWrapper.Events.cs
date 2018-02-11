/*
 * File: VoiceWrapper.Events.cs
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

namespace JustAnotherVoiceChat.Server.Wrapper.Elements.Wapper
{
    internal partial class VoiceWrapper
    {

        public void RegisterClientConnectedCallback(Delegates.ClientConnectCallback callback)
        {
            JV_RegisterClientConnectedCallback(callback);
        }

        public void RegisterClientDisconnectedCallback(Delegates.ClientCallback callback)
        {
            JV_RegisterClientDisconnectedCallback(callback);
        }

        public void RegisterClientTalkingChangedCallback(Delegates.ClientStatusCallback callback)
        {
            JV_RegisterClientTalkingChangedCallback(callback);
        }

        public void RegisterClientSpeakersMuteChangedCallback(Delegates.ClientStatusCallback callback)
        {
            JV_RegisterClientSpeakersMuteChangedCallback(callback);
        }

        public void RegisterClientMicrophoneMuteChangedCallback(Delegates.ClientStatusCallback callback)
        {
            JV_RegisterClientMicrophoneMuteChangedCallback(callback);
        }

        public void UnregisterClientConnectedCallback()
        {
            JV_UnregisterClientConnectedCallback();
        }

        public void UnregisterClientDisconnectedCallback()
        {
            JV_UnregisterClientDisconnectedCallback();
        }
        public void UnregisterClientTalkingChangedCallback()
        {
            JV_UnregisterClientTalkingChangedCallback();
        }

        public void UnregisterClientSpeakersMuteChangedCallback()
        {
            JV_UnregisterClientSpeakersMuteChangedCallback();
        }

        public void UnregisterClientMicrophoneMuteChangedCallback()
        {
            JV_UnregisterClientMicrophoneMuteChangedCallback();
        }
    }
}
