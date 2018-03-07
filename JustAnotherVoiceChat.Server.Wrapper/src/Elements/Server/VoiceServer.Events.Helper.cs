/*
 * File: VoiceServer.Events.Helper.cs
 * Date: 7.3.2018,
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
using System.Threading.Tasks;
using JustAnotherVoiceChat.Server.Wrapper.Interfaces;

namespace JustAnotherVoiceChat.Server.Wrapper.Elements.Server
{
    public partial class VoiceServer<TClient, TIdentifier> where TClient : IVoiceClient
    {
        internal T RunWhenClientValid<T>(ushort handle, Func<TClient, T> callback)
        {
            var client = GetVoiceClient(handle);

            if (client == null)
            {
                return default(T);
            }

            return callback(client);
        }

        internal void RunWhenClientValid(ushort handle, Action<TClient> callback)
        {
            var client = GetVoiceClient(handle);

            if (client == null)
            {
                return;
            }

            callback(client);
        }

        internal async Task<T> RunWhenClientValidAsync<T>(ushort handle, Func<TClient, Task<T>> callback)
        {
            var client = GetVoiceClient(handle);

            if (client == null)
            {
                return default(T);
            }

            return await callback(client);
        }

        internal async Task RunWhenClientValidAsync(ushort handle, Func<TClient, Task> callback)
        {
            var client = GetVoiceClient(handle);

            if (client == null)
            {
                return;
            }

            await callback(client);
        }


        internal Task<T> RunWhenClientConnectedAsync<T>(ushort handle, Func<TClient, Task<T>> callback)
        {
            return RunWhenClientValidAsync(handle, async client =>
            {
                if (!client.Connected)
                {
                    return default(T);
                }

                return await callback(client);
            });
        }

        internal Task RunWhenClientConnectedAsync(ushort handle, Func<TClient, Task> callback)
        {
            return RunWhenClientValidAsync(handle, async client =>
            {
                if (!client.Connected)
                {
                    return;
                }

                await callback(client);
            });
        }

        internal T RunWhenClientConnected<T>(ushort handle, Func<TClient, T> callback)
        {
            return RunWhenClientValid(handle, client =>
            {
                if (!client.Connected)
                {
                    return default(T);
                }

                return callback(client);
            });
        }

        internal void RunWhenClientConnected(ushort handle, Action<TClient> callback)
        {
            RunWhenClientValid(handle, client =>
            {
                if (!client.Connected)
                {
                    return;
                }

                callback(client);
            });
        }
    }
}
