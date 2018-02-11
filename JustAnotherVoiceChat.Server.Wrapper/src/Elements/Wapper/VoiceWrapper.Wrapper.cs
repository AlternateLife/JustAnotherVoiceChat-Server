/*
 * File: VoiceWrapper.Wrapper.cs
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

using System.Runtime.InteropServices;

namespace JustAnotherVoiceChat.Server.Wrapper.Elements.Wapper
{
    internal partial class VoiceWrapper
    {

#if LINUX
        private const string JustAnotherVoiceChatLibrary = "libJustAnotherVoiceChat.Server.so";
#else
        private const string JustAnotherVoiceChatLibrary = "JustAnotherVoiceChat.Server.dll";
#endif

        [DllImport(JustAnotherVoiceChatLibrary)]
        private static extern void AV_StartServer(ushort port);
        
        [DllImport(JustAnotherVoiceChatLibrary)]
        private static extern void AV_StopServer();
        
        [DllImport(JustAnotherVoiceChatLibrary)]
        [return:MarshalAs(UnmanagedType.I1)]
        private static extern bool AV_IsServerRunning();
        
        [DllImport(JustAnotherVoiceChatLibrary)]
        private static extern int AV_GetNumberOfClients();
        
        [DllImport(JustAnotherVoiceChatLibrary)]
        private static extern unsafe void AV_GetClientIds(ushort* list, int maxlength);
        
        [DllImport(JustAnotherVoiceChatLibrary)]
        private static extern void AV_RemoveClient(ushort handle);
        
        [DllImport(JustAnotherVoiceChatLibrary)]
        private static extern void AV_RemoveAllClients();

        [DllImport(JustAnotherVoiceChatLibrary)]
        private static extern void AV_Set3DSettings(float distanceFactor, float rolloffFactor);
        
        /**
         * Events
         */

        // ClientConnected
        
        [DllImport(JustAnotherVoiceChatLibrary)]
        private static extern void AV_RegisterClientConnectedCallback([MarshalAs(UnmanagedType.FunctionPtr)] Delegates.ClientConnectCallback callback);
        
        [DllImport(JustAnotherVoiceChatLibrary)]
        private static extern void AV_UnregisterClientConnectedCallback();
        
        [DllImport(JustAnotherVoiceChatLibrary)]
        private static extern void AVTest_CallClientConnectedCallback(ushort clientId);
        
        // ClientDisconnected
        
        [DllImport(JustAnotherVoiceChatLibrary)]
        private static extern void AV_RegisterClientDisconnectedCallback([MarshalAs(UnmanagedType.FunctionPtr)] Delegates.ClientCallback callback);
        
        [DllImport(JustAnotherVoiceChatLibrary)]
        private static extern void AV_UnregisterClientDisconnectedCallback();
        
        [DllImport(JustAnotherVoiceChatLibrary)]
        private static extern void AVTest_CallClientDisconnectedCallback(ushort clientId);
        
        // ClientStartsTalking
        
        [DllImport(JustAnotherVoiceChatLibrary)]
        private static extern void AV_RegisterClientTalkingChangedCallback([MarshalAs(UnmanagedType.FunctionPtr)] Delegates.ClientStatusCallback callback);
        
        [DllImport(JustAnotherVoiceChatLibrary)]
        private static extern void AV_UnregisterClientTalkingChangedCallback();
        
        [DllImport(JustAnotherVoiceChatLibrary)]
        private static extern void AVTest_CallClientTalkingChangedCallback(ushort clientId, bool newStatus);
        
        // ClientSpeakersMuteChanged
        
        [DllImport(JustAnotherVoiceChatLibrary)]
        private static extern void AV_RegisterClientSpeakersMuteChangedCallback([MarshalAs(UnmanagedType.FunctionPtr)] Delegates.ClientStatusCallback callback);
        
        [DllImport(JustAnotherVoiceChatLibrary)]
        private static extern void AV_UnregisterClientSpeakersMuteChangedCallback();
        
        [DllImport(JustAnotherVoiceChatLibrary)]
        private static extern void AVTest_CallClientSpeakersMuteChangedCallback(ushort clientId, bool newStatus);
        
        // ClientMicrophoneMuteChanged
        
        [DllImport(JustAnotherVoiceChatLibrary)]
        private static extern void AV_RegisterClientMicrophoneMuteChangedCallback([MarshalAs(UnmanagedType.FunctionPtr)] Delegates.ClientStatusCallback callback);
        
        [DllImport(JustAnotherVoiceChatLibrary)]
        private static extern void AV_UnregisterClientMicrophoneMuteChangedCallback();
        
        [DllImport(JustAnotherVoiceChatLibrary)]
        private static extern void AVTest_CallClientMicrophoneMuteChangedCallback(ushort clientId, bool newStatus);

    }
}
