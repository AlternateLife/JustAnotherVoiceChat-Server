using System.Runtime.InteropServices;

namespace AlternateVoice.Server.Wrapper.Elements.Server
{
    internal partial class VoiceServer
    {

#if LINUX
        private const string AlternateVoiceLib = "AlternateVoice.so";
#else
        private const string AlternateVoiceLib = "AlternateVoice.dll";
#endif

        private delegate void ClientCallback(ushort handle);

        [DllImport(AlternateVoiceLib)]
        private static extern void AL_StartServer(string hostname, ushort port, int channelId);
        
        [DllImport(AlternateVoiceLib)]
        private static extern void AL_StopServer();
        
        [DllImport(AlternateVoiceLib)]
        [return:MarshalAs(UnmanagedType.I1)]
        private static extern bool AL_IsServerRunning();
        
        [DllImport(AlternateVoiceLib)]
        private static extern int AL_GetNumberOfClients();
        
        [DllImport(AlternateVoiceLib)]
        private static extern unsafe void AL_GetClientIds(ushort* list, int maxlength);
        
        [DllImport(AlternateVoiceLib)]
        private static extern void AL_RemoveClient(ushort handle);
        
        [DllImport(AlternateVoiceLib)]
        private static extern void AL_MuteClientFor(ushort listenerId, ushort clientId, bool muted);
        
        [DllImport(AlternateVoiceLib)]
        private static extern void AL_RegisterNewClientCallback([MarshalAs(UnmanagedType.FunctionPtr)] ClientCallback callback);
        
        [DllImport(AlternateVoiceLib)]
        private static extern void AL_UnregisterNewClientCallback();
        
        [DllImport(AlternateVoiceLib)]
        private static extern void ALTest_CallNewClientCallback();

    }
}
