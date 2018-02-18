/*
 * File: VoiceWrapper.Wrapper.cs
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

using System.Runtime.InteropServices;

// ReSharper disable UnusedMember.Local
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
        private static extern void JV_StartServer(ushort port);
        
        [DllImport(JustAnotherVoiceChatLibrary)]
        private static extern void JV_StopServer();
        
        [DllImport(JustAnotherVoiceChatLibrary)]
        [return:MarshalAs(UnmanagedType.I1)]
        private static extern bool JV_IsServerRunning();
        
        [DllImport(JustAnotherVoiceChatLibrary)]
        private static extern int JV_GetNumberOfClients();
        
        [DllImport(JustAnotherVoiceChatLibrary)]
        private static extern unsafe void JV_GetClientIds(ushort* list, int maxlength);
        
        [DllImport(JustAnotherVoiceChatLibrary)]
        private static extern void JV_RemoveClient(ushort handle);
        
        [DllImport(JustAnotherVoiceChatLibrary)]
        private static extern void JV_RemoveAllClients();

        [DllImport(JustAnotherVoiceChatLibrary)]
        private static extern void JV_Set3DSettings(float distanceFactor, float rolloffFactor);
        
        /**
         * Events
         */

        // ClientConnected
        
        [DllImport(JustAnotherVoiceChatLibrary)]
        private static extern void JV_RegisterClientConnectedCallback([MarshalAs(UnmanagedType.FunctionPtr)] Delegates.ClientConnectCallback callback);
        
        [DllImport(JustAnotherVoiceChatLibrary)]
        private static extern void JV_UnregisterClientConnectedCallback();
        
        [DllImport(JustAnotherVoiceChatLibrary)]
        private static extern void JVTest_CallClientConnectedCallback(ushort clientId);
        
        // ClientDisconnected
        
        [DllImport(JustAnotherVoiceChatLibrary)]
        private static extern void JV_RegisterClientDisconnectedCallback([MarshalAs(UnmanagedType.FunctionPtr)] Delegates.ClientCallback callback);
        
        [DllImport(JustAnotherVoiceChatLibrary)]
        private static extern void JV_UnregisterClientDisconnectedCallback();
        
        [DllImport(JustAnotherVoiceChatLibrary)]
        private static extern void JVTest_CallClientDisconnectedCallback(ushort clientId);
        
        // ClientStartsTalking
        
        [DllImport(JustAnotherVoiceChatLibrary)]
        private static extern void JV_RegisterClientTalkingChangedCallback([MarshalAs(UnmanagedType.FunctionPtr)] Delegates.ClientStatusCallback callback);
        
        [DllImport(JustAnotherVoiceChatLibrary)]
        private static extern void JV_UnregisterClientTalkingChangedCallback();
        
        [DllImport(JustAnotherVoiceChatLibrary)]
        private static extern void JVTest_CallClientTalkingChangedCallback(ushort clientId, bool newStatus);
        
        // ClientSpeakersMuteChanged
        
        [DllImport(JustAnotherVoiceChatLibrary)]
        private static extern void JV_RegisterClientSpeakersMuteChangedCallback([MarshalAs(UnmanagedType.FunctionPtr)] Delegates.ClientStatusCallback callback);
        
        [DllImport(JustAnotherVoiceChatLibrary)]
        private static extern void JV_UnregisterClientSpeakersMuteChangedCallback();
        
        [DllImport(JustAnotherVoiceChatLibrary)]
        private static extern void JVTest_CallClientSpeakersMuteChangedCallback(ushort clientId, bool newStatus);
        
        // ClientMicrophoneMuteChanged
        
        [DllImport(JustAnotherVoiceChatLibrary)]
        private static extern void JV_RegisterClientMicrophoneMuteChangedCallback([MarshalAs(UnmanagedType.FunctionPtr)] Delegates.ClientStatusCallback callback);
        
        [DllImport(JustAnotherVoiceChatLibrary)]
        private static extern void JV_UnregisterClientMicrophoneMuteChangedCallback();
        
        [DllImport(JustAnotherVoiceChatLibrary)]
        private static extern void JVTest_CallClientMicrophoneMuteChangedCallback(ushort clientId, bool newStatus);

    }
}
