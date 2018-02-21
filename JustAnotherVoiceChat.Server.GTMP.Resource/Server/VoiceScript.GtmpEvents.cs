using GrandTheftMultiplayer.Server.Constant;
using GrandTheftMultiplayer.Server.Elements;
using JustAnotherVoiceChat.Server.GTMP.Extensions;

namespace JustAnotherVoiceChat.Server.GTMP.Resource
{
    public partial class VoiceScript
    {

        private void AttachToGtmpEvents(bool testingEvents)
        {
            API.onClientEventTrigger += OnClientEventTrigger;

            if (testingEvents)
            {
                API.onPlayerFinishedDownload += OnPlayerFinishedDownload;
            }
        }
        
        private void OnPlayerFinishedDownload(Client player)
        {
            player.setSkin(PedHash.FreemodeMale01);
        }

        private void OnClientEventTrigger(Client sender, string eventName, params object[] arguments)
        {
            if (eventName != "VOICE_ROTATION")
            {
                return;
            }

            sender.SetVoiceRotation((float) arguments[0]);
        }
    }
}
