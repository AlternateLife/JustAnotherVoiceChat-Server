using JustAnotherVoiceChat.Server.Wrapper.Interfaces;
using JustAnotherVoiceChat.Server.Wrapper.Math;

namespace JustAnotherVoiceChat.Server.Wrapper.Elements.Client
{
    public partial class VoiceClient<TClient> where TClient : IVoiceClient
    {

        public void SetListeningPositionToCurrentPosition()
        {
            SetListeningPosition(Position, CameraRotation);
        }
        
        public void SetListeningPosition(Vector3 position, float rotation)
        {
            Server.NativeWrapper.SetListenerPosition(this, position, rotation);
        }
        
        public void SetRelativeSpeakerPosition(IVoiceClient speaker, Vector3 position)
        {
            Server.NativeWrapper.SetRelativeSpeakerPositionForListener(this, speaker, position);
        }
        
        public void ResetRelativeSpeakerPosition(IVoiceClient speaker)
        {
            Server.NativeWrapper.ResetRelativeSpeakerPositionForListener(this, speaker);
        }

        public void ResetAllRelativeSpeakerPositions()
        {
            Server.NativeWrapper.ResetAllRelativePositionsForListener(this);
        }
        
    }
}
