using JustAnotherVoiceChat.Server.Wrapper.Interfaces;

namespace JustAnotherVoiceChat.Server.Wrapper.Elements.Wrapper
{
    internal partial class VoiceWrapper
    {
        public bool MuteClientForAll(IVoiceClient client, bool muted)
        {
            return NativeLibary.JV_MuteClientForAll(client.Handle.Identifer, muted);
        }

        public bool IsClientMutedForAll(IVoiceClient client)
        {
            return NativeLibary.JV_IsClientMutedForAll(client.Handle.Identifer);
        }

        public bool MuteClientForClient(IVoiceClient speaker, IVoiceClient listener, bool muted)
        {
            return NativeLibary.JV_MuteClientForClient(speaker.Handle.Identifer, listener.Handle.Identifer, muted);
        }

        public bool IsClientMutedForClient(IVoiceClient speaker, IVoiceClient listener)
        {
            return NativeLibary.JV_IsClientMutedForClient(speaker.Handle.Identifer, listener.Handle.Identifer);
        }
    }
}
