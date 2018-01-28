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
        
        string Hostname { get; }
        ushort Port { get; }
        int ChannelId { get; }
        
        bool Started { get; }
        
        void Start();
        void Stop();

        IGtmpVoiceClient GetVoiceClientOfPlayer(Client player);

        void SetCameraRotationOfPlayer(Client player, float cameraRotation);

    }
}
