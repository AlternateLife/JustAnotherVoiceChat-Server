using AlternateVoice.Server.GTMP.Interfaces;
using AlternateVoice.Server.Wrapper.Interfaces;
using GrandTheftMultiplayer.Server.Elements;

namespace AlternateVoice.Server.GTMP.Extensions
{
    public static class ClientExtensions
    {

        public static IVoiceClient GetVoiceClient(this Client client, IVoiceServer server)
        {
            return GtmpVoiceFactory.GetOrCreate<IVoiceToClientMapper>().GetVoiceClient(client);
        }
        
    }
}
