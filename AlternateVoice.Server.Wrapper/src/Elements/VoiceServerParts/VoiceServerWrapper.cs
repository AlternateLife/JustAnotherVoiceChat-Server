using System.Runtime.InteropServices;

namespace AlternateVoice.Server.Wrapper.Elements.VoiceServerParts
{
    internal partial class VoiceServer
    {

        private delegate void ClientCallback(ushort handle);

        [DllImport("AlternateVoice.dll")]
        private static extern void AL_StartServer(string hostname, ushort port, int channelId);
        
        [DllImport("AlternateVoice.dll")]
        private static extern void AL_StopServer();
        
        [DllImport("AlternateVoice.dll")]
        [return:MarshalAs(UnmanagedType.I1)]
        private static extern bool AL_IsServerRunning();
        
        [DllImport("AlternateVoice.dll")]
        private static extern int AL_GetNumberOfClients();
        
        [DllImport("AlternateVoice.dll")]
        private static extern unsafe void AL_GetClientIds(ushort* list, int maxlength);
        
        [DllImport("AlternateVoice.dll")]
        private static extern void AL_RemoveClient(ushort handle);
        
        [DllImport("AlternateVoice.dll")]
        private static extern void AL_MuteClientFor(ushort listenerId, ushort clientId, bool muted);
        
        [DllImport("AlternateVoice.dll")]
        private static extern void AL_RegisterNewClientCallback([MarshalAs(UnmanagedType.FunctionPtr)] ClientCallback callback);
        
        [DllImport("AlternateVoice.dll")]
        private static extern void AL_UnregisterNewClientCallback();
        
        [DllImport("AlternateVoice.dll")]
        private static extern void ALTest_CallNewClientCallback();

    }
}
