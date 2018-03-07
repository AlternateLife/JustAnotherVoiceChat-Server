![JAVIC logo](docs/images/JAVIC_Wide_250px.png)

[![Build status](https://ci.appveyor.com/api/projects/status/6fsulqaq7wccjtm7/branch/master?svg=true)](https://ci.appveyor.com/project/Micky5991/justanothervoicechat-server/branch/master)
[![Build Status](https://travis-ci.org/AlternateLife/JustAnotherVoiceChat-Server.svg?branch=master)](https://travis-ci.org/AlternateLife/JustAnotherVoiceChat-Server)
[![NuGet](https://img.shields.io/nuget/dt/JustAnotherVoiceChat.Server.Wrapper.svg)](https://www.nuget.org/packages/JustAnotherVoiceChat.Server.Wrapper/)

JustAnotherVoiceChat-Server is the control unit of the [JustAnotherVoiceChat TeamSpeak3-Plugin](https://github.com/AlternateLife/JustAnotherVoiceChat). It uses a dedicated TeamSpeak3-Server to communicate with other players on any .NET based multiplayer-server. The initial version is built for [GT-MP](http://gt-mp.net) but it can be used for any other games or mods, too.

Feel free to join our Discord Server if you have any further questions.

[![JAVIC Discord](https://discordapp.com/api/guilds/401509555684769802/embed.png?style=banner2)](https://discord.gg/uAVhvBT)

## Projects

| Project                                   | Description                                                          |
| ----------------------------------------- | -------------------------------------------------------------------- |
| [JustAnotherVoiceChat.Server](JustAnotherVoiceChat.Server)               | Native C++ networking library base                                   | 
| [JustAnotherVoiceChat.Server.Wrapper](JustAnotherVoiceChat.Server.Wrapper)       | Abstract .NET Standard wrapper for the C++ library base              | 
| [JustAnotherVoiceChat.Server.GTMP](JustAnotherVoiceChat.Server.GTMP)          | GT-MP integration and event-handlers for an easy GT-MP resource base | 
| [JustAnotherVoiceChat.Server.GTMP.Resource](JustAnotherVoiceChat.Server.GTMP.Resource) | Example GT-MP resource and implementation with active 3D Voice       | 

## Requirements

 * Teamspeak 3 server
    - Dedicated password protected channel. *(Remember the channel ID + channel password)*
    - Virtual TeamSpeak3-Server instance unique indentity *(This way, the client picks the right connection)*
 * Custom game server to include the JustAnotherVoiceChat server

### Find the unique identity of my TeamSpeak 3 server

### I have access to the TeamSpeak 3 ServerQuery

To find your server unique identity, you need to log into the ServerQuery and enter the following commands:

```
use port=9987
serverinfo
```

The output should start with the `virtualserver_unique_identifier`. This is the needed value for the server-creation.

**Example-Output**
```
virtualserver_unique_identifier=FEymkeQpMFKW+uRsuoCWti5F3II= virtualserver_name=...
```

### I **do not have access** to the TeamSpeak 3 ServerQuery

*Coming soon...*

## Installation

### GT-MP

>We currently offer an example resource for an easy integration in the [JustAnotherVoiceChat.Server.GTMP.Resource](JustAnotherVoiceChat.Server.GTMP.Resource) project

**[Basic GT-MP installation manual](docs/installation-gtmp.md)**

## Upgrading

### 0.2 to 0.3

- Change Camera-Rotation update value to `((API.getGamePlayCamRot().Z * -1) * Math.PI) / 180` [See commit](https://github.com/AlternateLife/JustAnotherVoiceChat-Server/commit/869504a5932ccec5157048e55f991cc27fb7629f#diff-dc55f06e6193cb5efd184191f3962aecR90)

## Documentation

*Coming soon, but every needed interface can be found [here](JustAnotherVoiceChat.Server.Wrapper/src/Interfaces)!*

## Authors

- Micky5991  
- MarkAtk  
- Cryma  
- dr1zzle  

## Contributors

- Elbloody  

## MIT license
Copyright (c) 2018 JustAnotherVoiceChat

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
