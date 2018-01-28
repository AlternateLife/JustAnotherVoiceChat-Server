using System;
using AlternateVoice.Server.Wrapper;
using GrandTheftMultiplayer.Server.Elements;

namespace AlternateVoice.Server.GTMP.Interfaces
{
    public interface IGtmpVoiceServer : IDisposable
    {
        event Delegates.EmptyEvent OnServerStarted;
        event Delegates.EmptyEvent OnServerStopping;
        
        event GtmpVoiceDelegates.GtmpVoiceClientEvent OnClientPrepared;

        event GtmpVoiceDelegates.GtmpVoiceClientEvent OnClientConnected;
        event GtmpVoiceDelegates.GtmpVoiceClientEvent OnClientDisconnected;
        
        event GtmpVoiceDelegates.GtmpVoiceClientEvent OnPlayerStartsTalking;
        event GtmpVoiceDelegates.GtmpVoiceClientEvent OnPlayerStopsTalking;
        
        string Hostname { get; }
        ushort Port { get; }
        int ChannelId { get; }
        
        bool Started { get; }
        
        void Start();
        void Stop();

        void TestLipSyncActiveForPlayer(Client player);
        void TestLipSyncInactiveForPlayer(Client player);

        IGtmpVoiceClient GetVoiceClientOfPlayer(Client player);

    }
}
