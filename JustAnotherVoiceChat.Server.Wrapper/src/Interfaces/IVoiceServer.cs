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
using JustAnotherVoiceChat.Server.Wrapper.Structs;

namespace JustAnotherVoiceChat.Server.Wrapper.Interfaces
{
    public interface IVoiceServer : IDisposable
    {
        event Delegates.EmptyEvent OnServerStarted;
        event Delegates.EmptyEvent OnServerStopping;
        
        event Delegates.ClientEvent OnClientConnected;
        event Delegates.ClientDisconnected OnClientDisconnected;

        event Delegates.ClientStatusEvent OnClientTalkingChanged;
        event Delegates.ClientStatusEvent OnClientMicrophoneMuteChanged;
        event Delegates.ClientStatusEvent OnClientSpeakersMuteChanged;

        string Hostname { get; }
        ushort Port { get; }
        int ChannelId { get; }
        bool Started { get; }

        double GlobalMaxDistance { get; }
        float GlobalDistanceFactor { get; }
        float GlobalRollOffScale { get; }

        IVoiceWrapper3D VoiceWrapper3D { get; }

        void Start();
        void Stop();

        IVoiceClient CreateClient(params object[] arguments);
        bool RemoveClient(IVoiceClient client);

        IVoiceGroup CreateGroup();
        IEnumerable<IVoiceGroup> GetAllGroups();
        void DestroyGroup(IVoiceGroup voiceGroup);

        IVoiceClient GetClientByHandle(VoiceHandle handle);
        IVoiceClient GetClientByHandle(ushort handle);
        
        T FindClient<T>(Func<T, bool> filter) where T : IVoiceClient;

        IEnumerable<T> GetClients<T>(Func<T, bool> filter) where T : IVoiceClient;
        IEnumerable<T> GetClients<T>() where T : IVoiceClient;

        void AddTask(IVoiceTask voiceTask);
        void AddTasks(IEnumerable<IVoiceTask> voiceTasks);

    }
}
