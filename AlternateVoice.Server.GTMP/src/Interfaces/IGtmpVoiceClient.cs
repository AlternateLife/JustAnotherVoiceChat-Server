using System;
using AlternateVoice.Server.Wrapper.Interfaces;
using GrandTheftMultiplayer.Server.Elements;

namespace AlternateVoice.Server.GTMP.Interfaces
{
    public interface IGtmpVoiceClient : IDisposable
    {

        IVoiceClient VoiceClient { get; }
        Client Player { get; }

        bool Headphones { get; set; }
        bool Microphone { get; set; }
        
    }
}
