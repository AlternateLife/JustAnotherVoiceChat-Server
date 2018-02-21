using GrandTheftMultiplayer.Server.Elements;
using GrandTheftMultiplayer.Shared.Math;
using JustAnotherVoiceChat.Server.GTMP.Exceptions;
using JustAnotherVoiceChat.Server.GTMP.Factories;
using JustAnotherVoiceChat.Server.GTMP.Interfaces;

namespace JustAnotherVoiceChat.Server.GTMP.Extensions
{
    public static class ClientExtensions
    {

        private static IGtmpVoiceServer Server
        {
            get
            {
                if (GtmpVoice.Shared == null)
                {
                    throw new ServerNotSharedException();
                }
                
                return GtmpVoice.Shared;
            }
        }

        public static IGtmpVoiceClient GetVoiceClient(this Client client)
        {
            return Server.GetVoiceClient(client);
        }

        public static void SetListeningPosition(this Client listener, Vector3 position, float rotation)
        {
            listener.GetVoiceClient().SetListeningPosition(new Wrapper.Math.Vector3(position.X, position.Y, position.Z), rotation);
        }

        public static void SetRelativeSpeakerPosition(this Client listener, Client speaker, Vector3 position)
        {
            listener.GetVoiceClient().SetRelativeSpeakerPosition(speaker, position);
        }

        public static void ResetRelativeSpeakerPosition(this Client listener, Client speaker)
        {
            listener.GetVoiceClient().ResetRelativeSpeakerPosition(speaker);
        }

        public static void ResetAllRelativeSpeakerPositions(this Client listener)
        {
            listener.GetVoiceClient().ResetAllRelativeSpeakerPositions();
        }
        
    }
}
