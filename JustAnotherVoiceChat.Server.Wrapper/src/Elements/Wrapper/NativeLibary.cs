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
using JustAnotherVoiceChat.Server.Wrapper.Delegates;

// ReSharper disable UnusedMember.Local
namespace JustAnotherVoiceChat.Server.Wrapper.Elements.Wrapper
{
    internal static class NativeLibary
    {

#if LINUX
        private const string JustAnotherVoiceChatLibrary = "libJustAnotherVoiceChat.Server.so";
#else
        private const string JustAnotherVoiceChatLibrary = "JustAnotherVoiceChat.Server.dll";
#endif

        [DllImport(JustAnotherVoiceChatLibrary)]
        internal static extern void JV_CreateServer(ushort port, string teamspeakServerId, ulong teamspeakChannelId, string teamspeakChannelPassword);

        [DllImport(JustAnotherVoiceChatLibrary)]
        [return: MarshalAs(UnmanagedType.I1)]
        internal static extern bool JV_StartServer();
            
        [DllImport(JustAnotherVoiceChatLibrary)]
        internal static extern void JV_StopServer();
        
        [DllImport(JustAnotherVoiceChatLibrary)]
        [return:MarshalAs(UnmanagedType.I1)]
        internal static extern bool JV_IsServerRunning();
        
        [DllImport(JustAnotherVoiceChatLibrary)]
        internal static extern int JV_GetNumberOfClients();
        
        [DllImport(JustAnotherVoiceChatLibrary)]
        internal static extern unsafe void JV_GetClientGameIds(ushort* list, int maxlength);
        
        [DllImport(JustAnotherVoiceChatLibrary)]
        internal static extern void JV_RemoveClient(ushort handle);
        
        [DllImport(JustAnotherVoiceChatLibrary)]
        internal static extern void JV_RemoveAllClients();

        [DllImport(JustAnotherVoiceChatLibrary)]
        internal static extern void JV_Set3DSettings(float distanceFactor, float rolloffFactor);
        
        /**
         * Events
         */

        // ClientConnected
        
        [DllImport(JustAnotherVoiceChatLibrary)]
        internal static extern void JV_RegisterClientConnectedCallback([MarshalAs(UnmanagedType.FunctionPtr)] NativeDelegates.ClientConnectCallback callback);
        
        [DllImport(JustAnotherVoiceChatLibrary)]
        internal static extern void JV_UnregisterClientConnectedCallback();
        
        [DllImport(JustAnotherVoiceChatLibrary)]
        internal static extern void JVTest_CallClientConnectedCallback(ushort clientId);
        
        // ClientDisconnected
        
        [DllImport(JustAnotherVoiceChatLibrary)]
        internal static extern void JV_RegisterClientDisconnectedCallback([MarshalAs(UnmanagedType.FunctionPtr)] NativeDelegates.ClientCallback callback);
        
        [DllImport(JustAnotherVoiceChatLibrary)]
        internal static extern void JV_UnregisterClientDisconnectedCallback();
        
        [DllImport(JustAnotherVoiceChatLibrary)]
        internal static extern void JVTest_CallClientDisconnectedCallback(ushort clientId);
        
        // ClientStartsTalking
        
        [DllImport(JustAnotherVoiceChatLibrary)]
        internal static extern void JV_RegisterClientTalkingChangedCallback([MarshalAs(UnmanagedType.FunctionPtr)] NativeDelegates.ClientStatusCallback callback);
        
        [DllImport(JustAnotherVoiceChatLibrary)]
        internal static extern void JV_UnregisterClientTalkingChangedCallback();
        
        [DllImport(JustAnotherVoiceChatLibrary)]
        internal static extern void JVTest_CallClientTalkingChangedCallback(ushort clientId, bool newStatus);
        
        // ClientSpeakersMuteChanged
        
        [DllImport(JustAnotherVoiceChatLibrary)]
        internal static extern void JV_RegisterClientSpeakersMuteChangedCallback([MarshalAs(UnmanagedType.FunctionPtr)] NativeDelegates.ClientStatusCallback callback);
        
        [DllImport(JustAnotherVoiceChatLibrary)]
        internal static extern void JV_UnregisterClientSpeakersMuteChangedCallback();
        
        [DllImport(JustAnotherVoiceChatLibrary)]
        internal static extern void JVTest_CallClientSpeakersMuteChangedCallback(ushort clientId, bool newStatus);
        
        // ClientMicrophoneMuteChanged
        
        [DllImport(JustAnotherVoiceChatLibrary)]
        internal static extern void JV_RegisterClientMicrophoneMuteChangedCallback([MarshalAs(UnmanagedType.FunctionPtr)] NativeDelegates.ClientStatusCallback callback);
        
        [DllImport(JustAnotherVoiceChatLibrary)]
        internal static extern void JV_UnregisterClientMicrophoneMuteChangedCallback();
        
        [DllImport(JustAnotherVoiceChatLibrary)]
        internal static extern void JVTest_CallClientMicrophoneMuteChangedCallback(ushort clientId, bool newStatus);
        
        // LogMessage
        
        [DllImport(JustAnotherVoiceChatLibrary)]
        internal static extern void JV_RegisterLogMessageCallback([MarshalAs(UnmanagedType.FunctionPtr)] NativeDelegates.LogMessageCallback callback);
        
        [DllImport(JustAnotherVoiceChatLibrary)]
        internal static extern void JV_UnregisterLogMessageCallback();
        
        
        /**
         * 3D Voice
         */

        [DllImport(JustAnotherVoiceChatLibrary)]
        internal static extern void JV_SetClientPosition(ushort clientId, float x, float y, float z, float rotation);

        [DllImport(JustAnotherVoiceChatLibrary)]
        internal static extern void JV_SetRelativePositionForClient(ushort listenerId, ushort speakerId, float x, float y, float z);

        [DllImport(JustAnotherVoiceChatLibrary)]
        internal static extern void JV_ResetRelativePositionForClient(ushort listenerId, ushort speakerId);

        [DllImport(JustAnotherVoiceChatLibrary)]
        internal static extern void JV_ResetAllRelativePositions(ushort clientId);

    }
}
