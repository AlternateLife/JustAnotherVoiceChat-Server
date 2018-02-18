using JustAnotherVoiceChat.Server.Wrapper.Structs;

namespace JustAnotherVoiceChat.Server.Wrapper.Interfaces
{
    public interface IVoiceClientFactory<TClient, TIdentifer> where TClient : IVoiceClient
    {

        TClient MakeClient(TIdentifer identifer, IVoiceServer server, VoiceHandle handle);

    }
}
