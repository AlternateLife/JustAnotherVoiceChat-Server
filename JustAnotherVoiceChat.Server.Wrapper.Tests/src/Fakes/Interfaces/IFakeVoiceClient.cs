using JustAnotherVoiceChat.Server.Wrapper.Interfaces;

namespace JustAnotherVoiceChat.Server.Wrapper.Tests.Fakes.Interfaces
{
    public interface IFakeVoiceClient : IVoiceClient
    {
        byte Identifer { get; }
    }
}
