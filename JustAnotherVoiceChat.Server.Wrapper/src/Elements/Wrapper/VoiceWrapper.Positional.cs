using JustAnotherVoiceChat.Server.Wrapper.Interfaces;
using JustAnotherVoiceChat.Server.Wrapper.Math;

namespace JustAnotherVoiceChat.Server.Wrapper.Elements.Wrapper
{
    internal partial class VoiceWrapper
    {
        
        public void ResetAllRelativePositionsForListener(IVoiceClient listener)
        {
            NativeLibary.JV_ResetAllRelativePositions(listener.Handle.Identifer);
        }

        public void ResetRelativeSpeakerPositionForListener(IVoiceClient listener, IVoiceClient speaker)
        {
            NativeLibary.JV_ResetRelativePositionForClient(listener.Handle.Identifer, speaker.Handle.Identifer);
        }

        public void SetListenerPosition(IVoiceClient listener, Vector3 position, float rotation)
        {
            NativeLibary.JV_SetClientPosition(listener.Handle.Identifer, position.X, position.Y, position.Z, rotation);
        }

        public void SetRelativeSpeakerPositionForListener(IVoiceClient listener, IVoiceClient speaker, Vector3 position)
        {
            NativeLibary.JV_SetRelativePositionForClient(listener.Handle.Identifer, speaker.Handle.Identifer, position.X, position.Y, position.Z);
        }
    }
}
