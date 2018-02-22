/*
 * File: VoiceTaskExecutor.cs
 * Date: 21.2.2018,
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
using System.Threading;
using System.Threading.Tasks;
using JustAnotherVoiceChat.Server.Wrapper.Enums;
using JustAnotherVoiceChat.Server.Wrapper.Interfaces;

namespace JustAnotherVoiceChat.Server.Wrapper.Elements.Server.Helpers
{
    internal class VoiceTaskExecutor<TClient> : IVoiceTaskExecutor where TClient : IVoiceClient
    {

        private readonly IVoiceTask<TClient> _voiceTask;
        private readonly IVoiceServer<TClient> _voiceServer;

        private CancellationTokenSource _cancellationTokenSource;

        private Task _task;

        private bool _isRunning;

        internal VoiceTaskExecutor(IVoiceTask<TClient> voiceTask, IVoiceServer<TClient> voiceServer)
        {
            _voiceTask = voiceTask;
            _voiceServer = voiceServer;
        }

        public void Start()
        {
            if (_isRunning)
            {
                throw new InvalidOperationException("The task is already running!");
            }
            
            if (_cancellationTokenSource == null)
            {
                _cancellationTokenSource = new CancellationTokenSource();
            }

            _isRunning = true;
            
            var token = _cancellationTokenSource.Token;
            _task = Task.Run(async () =>
            {
                while (true)
                {
                    token.ThrowIfCancellationRequested();

                    int waitTime;
                    try
                    {
                        waitTime = _voiceTask.RunVoiceTask(_voiceServer);
                    }
                    catch (Exception e)
                    {
                        waitTime = 1000;
                        _voiceServer.Log(LogLevel.Error, "Exception in VoiceTask: " + e);
                    }

                    await Task.Delay(waitTime, token);
                }
            }, token);
        }

        public void Stop()
        {
            if (!_isRunning)
            {
                throw new InvalidOperationException("The task is currently not running!");
            }
            
            try
            {
                _cancellationTokenSource.Cancel();
                _cancellationTokenSource.Dispose();

                _cancellationTokenSource = null;
            }
            catch (AggregateException e)
            {
                _voiceServer.Log(LogLevel.Error, "The following exceptions were thrown in task: " + e);
            }
        }

        public void Dispose()
        {
            if (_cancellationTokenSource != null)
            {
                _cancellationTokenSource.Dispose();
                _cancellationTokenSource = null;
            }

            _task = null;
        }
    }
}
