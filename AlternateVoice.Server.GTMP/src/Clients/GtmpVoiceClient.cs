using AlternateVoice.Server.GTMP.Interfaces;
using AlternateVoice.Server.Wrapper.Interfaces;
using GrandTheftMultiplayer.Server.Elements;

namespace AlternateVoice.Server.GTMP.Clients
{
    public class GtmpVoiceClient : IGtmpVoiceClient
    {
        public IVoiceClient VoiceClient { get; private set; }
        public Client Player { get; }
        public bool Headphones { get; set; }
        public bool Microphone { get; set; }

        public GtmpVoiceClient(Client player)
        {
            Player = player;
        }
        
    }
}
