using JustAnotherVoiceChat.Server.Wrapper.Interfaces;
using JustAnotherVoiceChat.Server.Wrapper.Math;

namespace JustAnotherVoiceChat.Server.Wrapper.Elements.Client
{
    public partial class VoiceClient<TClient> where TClient : IVoiceClient
    {

        public bool SetListeningPositionToCurrentPosition()
        {
            return SetListeningPosition(Position, CameraRotation);
        }
        
        public bool SetListeningPosition(Vector3 position, float rotation)
        {
            return Server.NativeWrapper.SetListenerPosition(this, position, rotation);
        }
        
        public bool SetRelativeSpeakerPosition(IVoiceClient speaker, Vector3 position)
        {
            return Server.NativeWrapper.SetRelativeSpeakerPositionForListener(this, speaker, position);
        }
        
        public bool ResetRelativeSpeakerPosition(IVoiceClient speaker)
        {
            return Server.NativeWrapper.ResetRelativeSpeakerPositionForListener(this, speaker);
        }

        public bool ResetAllRelativeSpeakerPositions()
        {
            return Server.NativeWrapper.ResetAllRelativePositionsForListener(this);
        }
        
        public void SetVoiceRange(float range)
        {
            Server.NativeWrapper.SetClientVoiceRange(this, range);
        }
        
    }
}
