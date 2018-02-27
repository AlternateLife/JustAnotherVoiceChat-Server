using GTANetworkAPI;
using JustAnotherVoiceChat.Server.RageMP.Extensions;
using JustAnotherVoiceChat.Server.RageMP.Factories;
using JustAnotherVoiceChat.Server.RageMP.Interfaces;
using JustAnotherVoiceChat.Server.Wrapper.Elements.Models;
using JustAnotherVoiceChat.Server.Wrapper.Elements.Tasks;

namespace JustAnotherVoiceChat.Server.RageMP.Resource
{
    public partial class VoiceScript : Script
    {

        private IRagempVoiceServer _voiceServer;
        
        public VoiceScript()
        {
            
        }

        [ServerEvent(Event.ResourceStart)]
        public void ResourceStartEvent()
        {
            // Create a JustAnotherVoiceServer based GtmpVoice Server!
            var hostname = NAPI.Resource.GetResourceSetting<string>("justanothervoicechat", "voice_host");
            var port = NAPI.Resource.GetResourceSetting<ushort>("justanothervoicechat", "voice_port");
            
            var teamspeakServerId = NAPI.Resource.GetResourceSetting<string>("justanothervoicechat", "voice_teamspeak_serverid");
            var teamspeakChannelId = NAPI.Resource.GetResourceSetting<ulong>("justanothervoicechat", "voice_teamspeak_channelid");
            var teamspeakChannelPassword = NAPI.Resource.GetResourceSetting<string>("justanothervoicechat", "voice_teamspeak_channelpassword");
            
            _voiceServer = RagempVoice.CreateServer(new VoiceServerConfiguration(hostname, port, teamspeakServerId, teamspeakChannelId, teamspeakChannelPassword));
            
            // Enables 3D Voice
            _voiceServer.AddTask(new PositionalVoiceTask<IRagempVoiceClient>());

            // Attach to some IVoiceServer events to react to certain them with certain actions.
            AttachToVoiceServerEvents();

            // Startup VoiceServer, so players are able to connect.
            _voiceServer.Start();
        }

        [ServerEvent(Event.PlayerConnected)]
        public void PlayerConnected(Client client)
        {
            _voiceServer.BridgeOnClientConnectedEvent(client);
        }

        [ServerEvent(Event.PlayerDisconnected)]
        public void PlayerDisconnected(Client client)
        {
            _voiceServer.BridgeOnClientDisconnectedEvent(client);
        }

        [RemoteEvent("VOICE_ROTATION")]
        public void UpdateVoiceRotation(Client sender, float rotation)
        {
            sender.SetVoiceRotation(rotation);
        }
        
    }
}
