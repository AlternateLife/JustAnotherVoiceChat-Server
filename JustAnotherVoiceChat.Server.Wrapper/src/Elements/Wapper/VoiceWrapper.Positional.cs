using JustAnotherVoiceChat.Server.Wrapper.Math;

namespace JustAnotherVoiceChat.Server.Wrapper.Elements.Wapper
{
    internal partial class VoiceWrapper<TClient>
    {
        
        public void ResetAllRelativePositionsForListener(TClient listener)
        {
            NativeLibary.JV_ResetAllRelativePositions(listener.Handle.Identifer);
        }

        public void ResetRelativeSpeakerPositionForListener(TClient listener, TClient speaker)
        {
            NativeLibary.JV_ResetRelativePositionForClient(listener.Handle.Identifer, speaker.Handle.Identifer);
        }

        public void SetListenerPosition(TClient listener, Vector3 position, float rotation)
        {
            NativeLibary.JV_SetClientPosition(listener.Handle.Identifer, position.X, position.Y, position.Z, rotation);
        }

        public void SetRelativeSpeakerPositionForListener(TClient listener, TClient speaker, Vector3 position)
        {
            NativeLibary.JV_SetRelativePositionForClient(listener.Handle.Identifer, speaker.Handle.Identifer, position.X, position.Y, position.Z);
        }
    }
}
