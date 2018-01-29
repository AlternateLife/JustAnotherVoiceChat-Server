using GrandTheftMultiplayer.Server.Elements;
using GrandTheftMultiplayer.Server.Managers;

namespace AlternateVoice.Server.GTMP.Resource
{
    public partial class VoiceScript
    {
        
        [Command("lip", "Usage: /lip [true/false]")]
        public void SetLipSync(Client sender, bool active)
        {
            if (active)
            {
                _voiceServer.TestLipSyncActiveForPlayer(sender);
            }
            else
            {
                _voiceServer.TestLipSyncInactiveForPlayer(sender);
            }
        }
        
        [Command("connect")]
        public void VoicePlayerConnect(Client sender, ushort handle, bool connected)
        {
            if (connected)
            {
                _voiceServer.TriggerOnClientConnectedEvent(handle);
            }
            else
            {
                _voiceServer.TriggerOnClientDisonnectedEvent(handle);
            }
        }
        
    }
}
