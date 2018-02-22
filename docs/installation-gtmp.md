# Integration into a GT-MP server

This manual offers a very basic guide how to integrate the JustAnotherVoiceServer into your own GT-MP server. For a more detailed showcase, how to use the server and resource, look into the [JustAnotherVoiceChat.Server.GTMP.Resource](https://github.com/AlternateLife/JustAnotherVoiceChat-Server/tree/master/JustAnotherVoiceChat.Server.GTMP.Resource) project.

## Requirements

- Stable GT-MP Server
- TeamSpeak3-Server with a password-protected channel.
- You need to know the following information of the selected TeamSpeak3-Server:
    - Channel ID of the target channel, where the players will be moved
    - Password of that said channel
    - Unique server identity of your server *(Example: `FEymkeQpMFKW+uRsuoCWti5F3II=`)*
- NuGet Package for GT-MP *(JustAnotherVoiceChat.Server.GTMP)*

## Very basic manual installation

These steps guide you to a very basic JustAnotherVoiceChat server, that accepts connections and everyone should be able to hear you.

> You can also find an example resource in the [JustAnotherVoiceChat.Server.GTMP.Resource](https://github.com/AlternateLife/JustAnotherVoiceChat-Server/tree/master/JustAnotherVoiceChat.Server.GTMP.Resource) project.

1. Install needed dependencies with NuGet
```
PM> Install-Package JustAnotherVoiceChat.Server.GTMP
```

2. Create JustAnotherVoiceChat server instance on startup
```CSHARP
public class MyGamemode : Script
{

    private JustAnotherVoiceChat.Server.GTMP.IGtmpVoiceServer _voiceServer;

    public MyGamemode()
    {
        API.onResourceStop += OnResourceStop;
        API.onResourceStart += OnResourceStart;
    }

    private void OnResourceStart()
    {
        _voiceServer = JustAnotherVoiceChat.Server.GTMP.Factories.GtmpVoice.CreateServer(API, new VoiceServerConfiguration("game.myexampleserver.com", 23332, "TeamSpekaIdentiy==", 232, "verySecretPassword"));

        // This line enables the 3D positional sound
        _voiceServer.AddTask(new PositionalVoiceTask<IGtmpVoiceClient>());

        _voiceServer.OnClientPrepared += OnClientShouldReceiveHandshake;
        _voiceServer.OnClientDisconnected += OnClientShouldReceiveHandshake;
        _voiceServer.OnClientConnected += OnClientConnected;

        _voiceServer.Start();
    }

    private void OnResourceStop()
    {
        _voiceServer.Stop();
        _voiceServer.Dispose();
    }

    private void OnClientConnected(IGtmpVoiceClient client) 
    {
        client.Player.triggerClientEvent("VOICE_HANDSHAKE", false, "");
    }

    private void OnClientShouldReceiveHandshake(IGtmpVoiceClient client)
    {
        client.Player.triggerEvent("VOICE_HANDSHAKE", true, client.HandshakeUrl);
    }
}
```

3. Create a clientside script that sends a HTTP request to the local JustAnotherVoiceChat client **with a headless remote CEF browser**. This step is **necessary**, so the TeamSpeak-Client is able to connect to the JustAnotherVoiceChat server.

```JS
let browser = null;
let handshakeTimer = 0;

API.onResourceStart.connect(() => {
    browser = API.createCefBrowser(0, 0, false);
    API.waitUntilCefBrowserInit(browser);
    API.setCefBrowserHeadless(browser, true);
});

API.onServerEventTrigger.connect((eventName, args) => {
    if(eventName !== "VOICE_HANDSHAKE") {
        return;
    }

    const newStatus = args[0];
    const handshakeUrl = args[1];

    if(newStatus) {
        handshakeTimer = API.every(3000, "resendHandshake", handshakeUrl);
    } else {
        if(handshakeTimer != -1) {
            API.stop(handshakeUrl);
            handshakeTimer = -1;
        }
    }

});

function resendHandshake(url) {
    API.loadPageCefBrowser(browser, url);
}
```

4. Copy needed libraries to the resource-location
 ```
 JustAnotherVoiceChat.Server.dll
 JustAnotherVoiceChat.Server.Wrapper.dll
 JustAntoherVoiceChat.Server.GTMP.dll
 ```

 5. Add assemblies to the resource's meta.xml. 
 *The `JustAnotherVoiceChat.Server.dll` shouldn't be listed there, because it is a native C++ library.*
 ```
<assembly ref="JustAnotherVoiceChat.Server.Wrapper.dll" />
<assembly ref="JustAnotherVoiceChat.Server.GTMP.dll" />
 ```