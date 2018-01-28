using System;
using System.Collections.Generic;
using AlternateVoice.Server.Wrapper.Structs;

namespace AlternateVoice.Server.Wrapper.Interfaces
{
    public interface IVoiceServer : IDisposable
    {
        event Delegates.EmptyEvent OnServerStarted;
        event Delegates.EmptyEvent OnServerStopping;
        
        event Delegates.ClientEvent OnClientConnecting;
        event Delegates.ClientDisconnected OnClientDisconnected;

        event Delegates.ClientEvent OnClientStartsTalking;
        event Delegates.ClientEvent OnClientStopsTalking;

        string Hostname { get; }
        ushort Port { get; }
        int ChannelId { get; }
        bool Started { get; }
        
        void Start();
        void Stop();

        void TestLipSyncActiveForClient(IVoiceClient client);
        void TestLipSyncInactiveForClient(IVoiceClient client);

        IVoiceClient CreateClient();
        bool RemoveClient(IVoiceClient client);

        IVoiceGroup CreateGroup();
        IEnumerable<IVoiceGroup> GetAllGroups();
        void DestroyGroup(IVoiceGroup voiceGroup);

        IVoiceClient GetClientByHandle(VoiceHandle handle);
        IVoiceClient GetClientByHandle(ushort handle);

        void TriggerClientConnectedEvent(ushort handle);
        void TriggerClientDisconnectedEvent(ushort handle);

    }
}
