/*
 * File: VoiceServer.Wrapper.cs
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

using System.Runtime.InteropServices;

namespace AlternateVoice.Server.Wrapper.Elements.Server
{
    internal partial class VoiceServer
    {

#if LINUX
        private const string AlternateVoiceLib = "libAlternateVoice.Server.so";
#else
        private const string AlternateVoiceLib = "AlternateVoice.Server.dll";
#endif

        private delegate bool ClientConnectCallback(ushort handle);
        private delegate void ClientCallback(ushort handle);
        private delegate void ClientStatusCallback(ushort handle, bool newStatus);

        [DllImport(AlternateVoiceLib)]
        private static extern void AV_StartServer(ushort port);
        
        [DllImport(AlternateVoiceLib)]
        private static extern void AV_StopServer();
        
        [DllImport(AlternateVoiceLib)]
        [return:MarshalAs(UnmanagedType.I1)]
        private static extern bool AV_IsServerRunning();
        
        [DllImport(AlternateVoiceLib)]
        private static extern int AV_GetNumberOfClients();
        
        [DllImport(AlternateVoiceLib)]
        private static extern unsafe void AV_GetClientIds(ushort* list, int maxlength);
        
        [DllImport(AlternateVoiceLib)]
        private static extern void AV_RemoveClient(ushort handle);
        
        [DllImport(AlternateVoiceLib)]
        private static extern void AV_RemoveAllClients();
        
        [DllImport(AlternateVoiceLib)]
        private static extern void AV_MuteClientForClient(ushort listenerId, ushort clientId, bool muted = true);
        
        [DllImport(AlternateVoiceLib)]
        private static extern void AV_SetClientPositionForClient(ushort listenerId, ushort clientId, float x, float y, float z);

        [DllImport(AlternateVoiceLib)]
        private static extern void AV_SetListenerDirection(ushort listenerId, float z);

        [DllImport(AlternateVoiceLib)]
        private static extern void AV_Set3DSettings(ushort listenerId, float distanceFactor, float rolloffScale);
        
        [DllImport(AlternateVoiceLib)]
        private static extern void AV_SetClientVolumeForClient(ushort listenerId, ushort clientId, float volume);
        
        /**
         * Events
         */
        
        // ClientConnected
        
        [DllImport(AlternateVoiceLib)]
        private static extern void AV_RegisterClientConnectedCallback([MarshalAs(UnmanagedType.FunctionPtr)] ClientConnectCallback callback);
        
        [DllImport(AlternateVoiceLib)]
        private static extern void AV_UnregisterClientConnectedCallback();
        
        [DllImport(AlternateVoiceLib)]
        private static extern void AVTest_CallClientConnectedCallback(ushort clientId);
        
        // ClientDisconnected
        
        [DllImport(AlternateVoiceLib)]
        private static extern void AV_RegisterClientDisconnectedCallback([MarshalAs(UnmanagedType.FunctionPtr)] ClientCallback callback);
        
        [DllImport(AlternateVoiceLib)]
        private static extern void AV_UnregisterClientDisconnectedCallback();
        
        [DllImport(AlternateVoiceLib)]
        private static extern void AVTest_CallClientDisconnectedCallback(ushort clientId);
        
        // ClientStartsTalking
        
        [DllImport(AlternateVoiceLib)]
        private static extern void AV_RegisterClientTalkingChangedCallback([MarshalAs(UnmanagedType.FunctionPtr)] ClientStatusCallback callback);
        
        [DllImport(AlternateVoiceLib)]
        private static extern void AV_UnregisterClientTalkingChangedCallback();
        
        [DllImport(AlternateVoiceLib)]
        private static extern void AVTest_CallClientTalkingChangedCallback(ushort clientId, bool newStatus);
        
        // ClientSpeakersMuteChanged
        
        [DllImport(AlternateVoiceLib)]
        private static extern void AV_RegisterClientSpeakersMuteChangedCallback([MarshalAs(UnmanagedType.FunctionPtr)] ClientStatusCallback callback);
        
        [DllImport(AlternateVoiceLib)]
        private static extern void AV_UnregisterClientSpeakersMuteChangedCallback();
        
        [DllImport(AlternateVoiceLib)]
        private static extern void AVTest_CallClientSpeakersMuteChangedCallback(ushort clientId, bool newStatus);
        
        // ClientMicrophoneMuteChanged
        
        [DllImport(AlternateVoiceLib)]
        private static extern void AV_RegisterClientMicrophoneMuteChangedCallback([MarshalAs(UnmanagedType.FunctionPtr)] ClientStatusCallback callback);
        
        [DllImport(AlternateVoiceLib)]
        private static extern void AV_UnregisterClientMicrophoneMuteChangedCallback();
        
        [DllImport(AlternateVoiceLib)]
        private static extern void AVTest_CallClientMicrophoneMuteChangedCallback(ushort clientId, bool newStatus);

    }
}
