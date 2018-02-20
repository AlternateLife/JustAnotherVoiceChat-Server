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
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using JustAnotherVoiceChat.Server.Wrapper.Interfaces;

namespace JustAnotherVoiceChat.Server.Wrapper.Elements.Server
{
    public partial class VoiceServer<TClient, TIdentifier> where TClient : IVoiceClient
    {
        private readonly ConcurrentBag<IVoiceTask<TClient>> _voiceTasks = new ConcurrentBag<IVoiceTask<TClient>>();

        private Task _taskRunner;
        private CancellationTokenSource _cancellationToken;

        private void AttachTasksToStartAndStopEvent()
        {
            OnServerStarted += StartupTaskRunner;
            OnServerStopping += StopTaskRunner;
        }

        private void StartupTaskRunner()
        {
            if (_taskRunner != null)
            {
                return;
            }
            _cancellationToken = new CancellationTokenSource();

            var cancelToken = _cancellationToken.Token;
            
            _taskRunner = Task.Factory.StartNew(() =>
            {
                cancelToken.ThrowIfCancellationRequested();
                
                while (true)
                {
                    cancelToken.ThrowIfCancellationRequested();
                    
                    foreach (var voiceTask in _voiceTasks)
                    {
                        voiceTask.RunVoiceTask(this).Wait(cancelToken);
                    }
                }
            }, _cancellationToken.Token);
        }

        private void StopTaskRunner()
        {
            _cancellationToken.Cancel();
            try
            {
                _taskRunner.Wait();
            }
            catch (AggregateException e)
            {
                
            }
        }

        public void AddTask(IVoiceTask<TClient> voiceTask)
        { 
            if (voiceTask == null)
            {
                throw new ArgumentNullException(nameof(voiceTask));
            }

            _voiceTasks.Add(voiceTask);
        }

        public void AddTasks(IEnumerable<IVoiceTask<TClient>> voiceTasks)
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
            
            _cancellationToken.Dispose();
        }
    }
}
