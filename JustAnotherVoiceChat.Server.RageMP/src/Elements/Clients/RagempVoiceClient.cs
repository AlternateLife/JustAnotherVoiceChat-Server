using GTANetworkAPI;
using JustAnotherVoiceChat.Server.RageMP.Interfaces;
using JustAnotherVoiceChat.Server.Wrapper.Elements.Client;
using JustAnotherVoiceChat.Server.Wrapper.Interfaces;
using JustAnotherVoiceChat.Server.Wrapper.Structs;
using Vector3 = JustAnotherVoiceChat.Server.Wrapper.Math.Vector3;

namespace JustAnotherVoiceChat.Server.RageMP.Elements.Clients
{
    public class RagempVoiceClient : VoiceClient<IRagempVoiceClient>, IRagempVoiceClient
    {
        public Client Player { get; }
        private new IRagempVoiceServer Server => (IRagempVoiceServer) base.Server;

        private readonly Vector3 _position = new Vector3();
        public override Vector3 Position
        {
            get
            {
                var clientPosition = Player.Position;

                _position.X = clientPosition.X;
                _position.Y = clientPosition.Y;
                _position.Z = clientPosition.Z;

                return _position;
            }
        }

        public RagempVoiceClient(Client client, IVoiceServer<IRagempVoiceClient> server, VoiceHandle handle) : base(server, handle)
        {
            Player = client;
        }
        
        public bool SetRelativeSpeakerPosition(Client speaker, GTANetworkAPI.Vector3 position)
        {
            return SetRelativeSpeakerPosition(Server.GetVoiceClient(speaker), new Vector3(position.X, position.Y, position.Z));
        }

        public bool ResetRelativeSpeakerPosition(Client speaker)
        {
            return ResetRelativeSpeakerPosition(Server.GetVoiceClient(speaker));
        }

        public bool MuteSpeaker(Client speaker, bool muted)
        {
            return MuteSpeaker(Server.GetVoiceClient(speaker), muted);
        }

        public bool IsSpeakerMuted(Client speaker)
        {
            return IsSpeakerMuted(Server.GetVoiceClient(speaker));
        }

        public ClientPosition MakeClientPosition(GTANetworkAPI.Vector3 position, float rotation)
        {
            return MakeClientPosition(new Vector3(position.X, position.Y, position.Z), rotation);
        }
    }
}
