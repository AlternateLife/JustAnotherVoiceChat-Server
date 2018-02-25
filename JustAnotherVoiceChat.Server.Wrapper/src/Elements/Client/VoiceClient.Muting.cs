using JustAnotherVoiceChat.Server.Wrapper.Interfaces;

namespace JustAnotherVoiceChat.Server.Wrapper.Elements.Client
{
    public partial class VoiceClient<TClient> where TClient : IVoiceClient
    {
        public bool MuteClientForAll(bool muted)
        {
            return Server.NativeWrapper.MuteClientForAll(this, muted);
        }

        public bool IsClientMutedForAll()
        {
            return Server.NativeWrapper.IsClientMutedForAll(this);
        }

        public bool MuteSpeakerForListener(IVoiceClient speaker, bool muted)
        {
            return Server.NativeWrapper.MuteClientForClient(speaker, this, muted);
        }

        public bool IsSpeakerMutedForListener(IVoiceClient speaker)
        {
            return Server.NativeWrapper.IsClientMutedForClient(speaker, this);
        }
    }
}
