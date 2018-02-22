# JustAnotherVoiceChat

JustAnotherVoiceChat-Server is the control unit of the [JustAnotherVoiceChat TeamSpeak3-Plugin](https://github.com/AlternateLife/JustAnotherVoiceChat). It uses a dedicated TeamSpeak3-Server to communicate with other players on any .NET based multiplayer-server. The initial version is built for [GT-MP](http://gt-mp.net) but it can be used for any other games or mods, too.

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

## Installation

### GT-MP

>We currently offer an example resource for an easy integration in the [JustAnotherVoiceChat.Server.GTMP.Resource](JustAnotherVoiceChat.Server.GTMP.Resource) project

**[Basic GT-MP installation manual](docs/installation-gtmp.md)**

## Documentation

*Coming soon, but every needed interface can be found [here](JustAnotherVoiceChat.Server.Wrapper/src/Interfaces)!*

## Authors

- Micky5991  
- MarkAtk  
- Cryma  
- dr1zzle  


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
