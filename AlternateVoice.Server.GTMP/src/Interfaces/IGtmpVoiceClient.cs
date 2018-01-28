using AlternateVoice.Server.Wrapper.Interfaces;
using GrandTheftMultiplayer.Server.Elements;

namespace AlternateVoice.Server.GTMP.Interfaces
{
    public interface IGtmpVoiceClient
    {

        IVoiceClient VoiceClient { get; }
        Client Player { get; }

        bool Headphones { get; }
        bool Microphone { get; }

        float CameraRotation { get; }
        
        string HandshakeUrl { get; }

        void SetCameraRotation(float cameraRotation);

    }
}
