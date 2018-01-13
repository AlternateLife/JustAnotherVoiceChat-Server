# ALVoice communication

## Definitions

 * ALVoice Client: The Teamspeak 3 plugin which controls the local teamspeak
 * ALVoice Server: The server instance which controls the teamspeak plugin instances
 * Game Client: The actual game running on the users PC 
 * Game Server: The multiplayer server for the actual game

## General idea

The ALVoice teamspeak plugin is the *main component*. It un-/mutes teamspeak clients and changes their volume and positional audio. The needed information is send by the *ALVoice server* to each *ALVoice client* which is controlled by the *game server*.
The *game client* only talks on connection setup with the *ALVoice client* to give *ALVoice client* about the *ALVoice server* and unique client identifier.

![connection overview](images/general-communication.png)

## Connection setup

When the *game client* connects to the *game server* the server tells the client about the needed information (endpoint, client unique identifier) to connect to the *ALVoice server*. The *game client* then sends this information (via the local connection) to the *ALVoice client*. The *ALVoice client* now has all information needed to connect the specific *ALVoice server* which belongs to the *game server*. The *ALVoice client* connects to the *ALVoice server* and the server confirms the connection. When the connection was confirmed the client requests the client uid to teamspeak id mapping it needs to change teamspeak clients. With this information the connection setup is **completed**.

![connection setup](images/connection-setup.png)

## Continues updates

When the *ALVoice client* connection setup is completed it receives continues updates from the *ALVoice server* about changed players. This is the **main message** the complete communication is about.

![continues communication](images/continues-communication.png)

### updatePlayersMessage

The idea is to send as less information as possible but enough that the client can change the needed teamspeak clients. Information required to change a single clients is:

 * Player is in range to be heard
 * Range to the player to set volume modifier
 * Stereo position of the player to set audio pan
