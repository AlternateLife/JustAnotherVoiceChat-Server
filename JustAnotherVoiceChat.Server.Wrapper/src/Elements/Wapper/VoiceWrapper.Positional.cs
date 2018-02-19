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

        public void SetListenerPosition(TClient listener)
        {
            NativeLibary.JV_SetClientPosition(listener.Handle.Identifer, listener.Position.X, listener.Position.Y, listener.Position.Z, listener.CameraRotation);
        }

        public void SetRelativeSpeakerPositionForListener(TClient listener, TClient speaker)
        {
            NativeLibary.JV_SetRelativePositionForClient(listener.Handle.Identifer, speaker.Handle.Identifer, speaker.Position.X, speaker.Position.Y, speaker.Position.Z);
        }
    }
}
