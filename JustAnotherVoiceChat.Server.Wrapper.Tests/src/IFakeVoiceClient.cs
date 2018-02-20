using JustAnotherVoiceChat.Server.Wrapper.Interfaces;

namespace JustAnotherVoiceChat.Server.Wrapper.Tests
{
    public interface IFakeVoiceClient : IVoiceClient
    {
        byte Identifer { get; }
    }
}
