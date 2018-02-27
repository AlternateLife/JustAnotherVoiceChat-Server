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

    }
}
