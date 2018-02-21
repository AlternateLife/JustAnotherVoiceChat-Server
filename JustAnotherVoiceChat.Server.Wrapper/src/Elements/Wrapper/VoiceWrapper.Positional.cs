using JustAnotherVoiceChat.Server.Wrapper.Interfaces;
using JustAnotherVoiceChat.Server.Wrapper.Math;

namespace JustAnotherVoiceChat.Server.Wrapper.Elements.Wrapper
{
    internal partial class VoiceWrapper
    {
        
        public bool ResetAllRelativePositionsForListener(IVoiceClient listener)
        {
            return NativeLibary.JV_ResetAllRelativePositions(listener.Handle.Identifer);
        }

        public bool ResetRelativeSpeakerPositionForListener(IVoiceClient listener, IVoiceClient speaker)
        {
            return NativeLibary.JV_ResetRelativePositionForClient(listener.Handle.Identifer, speaker.Handle.Identifer);
        }

        public bool SetListenerPosition(IVoiceClient listener, Vector3 position, float rotation)
        {
            return NativeLibary.JV_SetClientPosition(listener.Handle.Identifer, position.X, position.Y, position.Z, rotation);
        }

        public bool SetRelativeSpeakerPositionForListener(IVoiceClient listener, IVoiceClient speaker, Vector3 position)
        {
            return NativeLibary.JV_SetRelativePositionForClient(listener.Handle.Identifer, speaker.Handle.Identifer, position.X, position.Y, position.Z);
        }
    }
}
