/*
 * File: IVoiceServer.cs
 * Date: 15.2.2018,
 *
 * MIT License
 *
 * Copyright (c) 2018 JustAnotherVoiceChat
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 */

using System;
using System.Collections.Generic;
using JustAnotherVoiceChat.Server.Wrapper.Delegates;
using JustAnotherVoiceChat.Server.Wrapper.Elements.Models;
using JustAnotherVoiceChat.Server.Wrapper.Enums;
using JustAnotherVoiceChat.Server.Wrapper.Structs;

namespace JustAnotherVoiceChat.Server.Wrapper.Interfaces
{
    public interface IVoiceServer<TClient> : IDisposable where TClient : IVoiceClient
    {
        event Delegates<TClient>.EmptyEvent OnServerStarted;
        event Delegates<TClient>.EmptyEvent OnServerStopping;
        
        event Delegates<TClient>.ClientEvent OnClientConnected;
        event Delegates<TClient>.ClientConnectingEvent OnClientConnecting;
        event Delegates<TClient>.ClientRejectedEvent OnClientRejected;
        event Delegates<TClient>.ClientEvent OnClientDisconnected;

        event Delegates<TClient>.ClientStatusEvent OnClientTalkingChanged;
        event Delegates<TClient>.ClientStatusEvent OnClientMicrophoneMuteChanged;
        event Delegates<TClient>.ClientStatusEvent OnClientSpeakersMuteChanged;
        event Delegates<TClient>.LogMessageEvent OnLogMessage;

        bool Started { get; }
        
        VoiceServerConfiguration Configuration { get; }
        IVoiceWrapper NativeWrapper { get; }

        void Start();
        void Stop();

        void Log(LogLevel logLevel, string message);
        void Log(string message);

        IVoiceGroup<TClient> CreateGroup();
        IEnumerable<IVoiceGroup<TClient>> GetAllGroups();
        bool DestroyGroup(IVoiceGroup<TClient> voiceGroup);

        TClient GetVoiceClient(VoiceHandle handle);
        TClient GetVoiceClient(ushort handle);

        TClient FindClient(Func<TClient, bool> filter);

        IEnumerable<TClient> GetClients(Func<TClient, bool> filter);
        IEnumerable<TClient> GetClients();

        void AddTask(IVoiceTask<TClient> voiceTask);
        void AddTasks(IEnumerable<IVoiceTask<TClient>> voiceTasks);
    }
}
