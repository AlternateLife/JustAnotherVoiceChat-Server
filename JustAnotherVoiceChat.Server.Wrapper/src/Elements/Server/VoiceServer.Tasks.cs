/*
 * File: VoiceServer.Tasks.cs
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
using System.Collections.Concurrent;
using System.Collections.Generic;
using JustAnotherVoiceChat.Server.Wrapper.Interfaces;

namespace JustAnotherVoiceChat.Server.Wrapper.Elements.Server
{
    public partial class VoiceServer
    {
        private ConcurrentBag<IVoiceTask> _voiceTasks = new ConcurrentBag<IVoiceTask>();

        private void AttachTasksToStartAndStopEvent()
        {
            OnServerStarted += StartTasks;
            OnServerStopping += StopTasks;
        }

        private void StartTasks()
        {
            foreach(var voiceTask in _voiceTasks)
            {
                voiceTask.RunVoiceTask();
            }
        }

        private void StopTasks()
        {
            foreach (var voiceTask in _voiceTasks)
            {
                voiceTask.CancelVoiceTask();
            }
        }

        public void AddTask(IVoiceTask voiceTask)
        { 
            if (voiceTask == null)
            {
                throw new ArgumentNullException(nameof(voiceTask));
            }

            _voiceTasks.Add(voiceTask);
        }

        public void AddTasks(IEnumerable<IVoiceTask> voiceTasks)
        {
            foreach (var voiceTask in voiceTasks)
            {
                AddTask(voiceTask);
            }
        }

        private void DisposeTasks()
        {
            foreach (var voiceTask in _voiceTasks)
            {
                voiceTask.Dispose();
            }
        }
    }
}
