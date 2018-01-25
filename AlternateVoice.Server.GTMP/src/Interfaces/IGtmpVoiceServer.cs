using System;
using GrandTheftMultiplayer.Server.Elements;

namespace AlternateVoice.Server.GTMP.Interfaces
{
    public interface IGtmpVoiceServer : IDisposable
    {

        void Start();
        void Stop();

        IGtmpVoiceClient GetVoiceClientOfPlayer(Client player);

    }
}
