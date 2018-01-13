using AlternateVoice.Server.GTMP.Interfaces;
using AlternateVoice.Server.Wrapper.Interfaces;
using GrandTheftMultiplayer.Server.Elements;

namespace AlternateVoice.Server.GTMP.Elements
{
    public class VoiceToClientMapper : IVoiceToClientMapper
    {

        internal VoiceToClientMapper()
        {
            
        }
        
        public Client GetGtmpClient(IVoiceClient voiceClient)
        {
            throw new System.NotImplementedException();
        }

        public IVoiceClient GetVoiceClient(Client client)
        {
            throw new System.NotImplementedException();
        }
    }
}
