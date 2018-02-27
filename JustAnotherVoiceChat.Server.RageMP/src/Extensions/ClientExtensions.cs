using GTANetworkAPI;
using JustAnotherVoiceChat.Server.RageMP.Exceptions;
using JustAnotherVoiceChat.Server.RageMP.Factories;
using JustAnotherVoiceChat.Server.RageMP.Interfaces;

namespace JustAnotherVoiceChat.Server.RageMP.Extensions
{
    public static class ClientExtensions
    {
        private static IRagempVoiceServer Server => RagempVoice.Shared;

        public static IRagempVoiceClient GetVoiceClient(this Client client)
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

        public static bool MuteForAll(this Client listener, bool muted)
        {
            return listener.GetVoiceClient()?.MuteForAll(muted) ?? false;
        }
        
        public static bool IsMutedForAll(this Client listener)
        {
            return listener.GetVoiceClient()?.IsMutedForAll() ?? false;
        }

        public static bool MuteSpeaker(this Client listener, Client speaker, bool muted)
        {
            return listener.GetVoiceClient()?.MuteSpeaker(speaker, muted) ?? false;
        }

        public static bool IsSpeakerMuted(this Client listener, Client speaker)
        {
            return listener.GetVoiceClient()?.IsSpeakerMuted(speaker) ?? false;
        }
    }
}
