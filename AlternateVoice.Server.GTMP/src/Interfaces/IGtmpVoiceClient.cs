using AlternateVoice.Server.Wrapper.Interfaces;
using GrandTheftMultiplayer.Server.Elements;

namespace AlternateVoice.Server.GTMP.Interfaces
{
    public interface IGtmpVoiceClient
    {

        IVoiceClient VoiceClient { get; }
        Client Player { get; }

        bool Headphones { get; set; }
        bool Microphone { get; set; }
        
    }
}
