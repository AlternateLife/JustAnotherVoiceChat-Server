using GrandTheftMultiplayer.Server.Elements;
using GrandTheftMultiplayer.Shared.Math;
using JustAnotherVoiceChat.Server.GTMP.Exceptions;
using JustAnotherVoiceChat.Server.GTMP.Factories;
using JustAnotherVoiceChat.Server.GTMP.Interfaces;

namespace JustAnotherVoiceChat.Server.GTMP.Extensions
{
    public static class ClientExtensions
    {

        private static IGtmpVoiceServer Server => GtmpVoice.Shared;

        public static IGtmpVoiceClient GetVoiceClient(this Client client)
        {
            if (Server == null)
            {
                throw new ServerNotSharedException();
            }

            return Server.GetVoiceClient(client);
        }

        public static void SetVoiceRotation(this Client client, float rotation)
        {
            var voiceClient = client.GetVoiceClient();
            if (voiceClient != null)
            {
                voiceClient.CameraRotation = rotation;
            }
        }

        public static void SetVoiceRange(this Client listener, float range)
        {
            listener.GetVoiceClient()?.SetVoiceRange(range);
        }

        public static bool SetListeningPosition(this Client listener, Vector3 position, float rotation)
        {
            return listener.GetVoiceClient()?.SetListeningPosition(new Wrapper.Math.Vector3(position.X, position.Y, position.Z), rotation) ?? false;
        }

        public static bool SetRelativeSpeakerPosition(this Client listener, Client speaker, Vector3 position)
        {
            return listener.GetVoiceClient()?.SetRelativeSpeakerPosition(speaker, position) ?? false;
        }

        public static bool ResetRelativeSpeakerPosition(this Client listener, Client speaker)
        {
            return listener.GetVoiceClient()?.ResetRelativeSpeakerPosition(speaker) ?? false;
        }

        public static bool ResetAllRelativeSpeakerPositions(this Client listener)
        {
            return listener.GetVoiceClient()?.ResetAllRelativeSpeakerPositions() ?? false;
        }
        
    }
}
