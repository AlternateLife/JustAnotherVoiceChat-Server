using AlternateVoice.Server.GTMP.Interfaces;
using AlternateVoice.Server.GTMP.Server;
using GrandTheftMultiplayer.Server.API;

namespace AlternateVoice.Server.GTMP.Factories
{
    public static class GtmpVoice
    {

        public static IGtmpVoiceServer CreateServer(API api, string hostname, ushort port, int channelId)
        {
            return new GtmpVoiceServer(api, hostname, port, channelId);
        }
        
    }
}
