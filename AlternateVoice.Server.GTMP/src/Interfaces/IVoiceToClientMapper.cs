using AlternateVoice.Server.Wrapper.Interfaces;
using GrandTheftMultiplayer.Server.Elements;

namespace AlternateVoice.Server.GTMP.Interfaces
{
    public interface IVoiceToClientMapper : IGtmpVoiceElement
    {

        Client GetGtmpClient(IVoiceClient voiceClient);
        IVoiceClient GetVoiceClient(Client client);

    }
}
