using AlternateVoice.Server.Wrapper.Structs;

namespace AlternateVoice.Server.Wrapper.Interfaces
{
    public interface IVoiceClientRepository
    {

        IVoiceClient MakeClient(IVoiceServer server, VoiceHandle handle, params object[] arguments);

    }
}
