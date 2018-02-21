/*
 * File: IVoiceWrapper.cs
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
using JustAnotherVoiceChat.Server.Wrapper.Elements.Models;
using JustAnotherVoiceChat.Server.Wrapper.Math;

namespace JustAnotherVoiceChat.Server.Wrapper.Interfaces
{
    public interface IVoiceWrapper
    {
        void CreateNativeServer(VoiceServerConfiguration configuration);
        void DestroyNativeServer();
        bool StartNativeServer();
        void StopNativeServer();

        bool RemoveClient(IVoiceClient client);
        
        void Set3DSettings(float distanceFactor, float rolloffFactor);

        void TestCallClientTalkingChangedCallback(ushort handle, bool newStatus);
        void TestCallClientConnectedCallback(ushort handle);
        void TestCallClientDisconnectedCallback(ushort handle);

        void UnregisterClientConnectedCallback();
        void UnregisterClientDisconnectedCallback();
        
        void UnregisterClientTalkingChangedCallback();
        void UnregisterClientSpeakersMuteChangedCallback();
        void UnregisterClientMicrophoneMuteChangedCallback();
        void UnregisterLogMessageCallback();

        void RegisterClientConnectedCallback(NativeDelegates.ClientConnectCallback callback);
        void RegisterClientDisconnectedCallback(NativeDelegates.ClientCallback callback);
        
        void RegisterClientTalkingChangedCallback(NativeDelegates.ClientStatusCallback callback);
        void RegisterClientSpeakersMuteChangedCallback(NativeDelegates.ClientStatusCallback callback);
        void RegisterClientMicrophoneMuteChangedCallback(NativeDelegates.ClientStatusCallback callback);
        
        void RegisterLogMessageCallback(NativeDelegates.LogMessageCallback callback);
        
        void SetClientVoiceRange(IVoiceClient client, float voiceRange);
        bool SetListenerPosition(IVoiceClient listener, Vector3 position, float rotation);
        bool SetRelativeSpeakerPositionForListener(IVoiceClient listener, IVoiceClient speaker, Vector3 position);
        bool ResetRelativeSpeakerPositionForListener(IVoiceClient listener, IVoiceClient speaker);
        bool ResetAllRelativePositionsForListener(IVoiceClient listener);
    }
}
