/*
 * File: Program.cs
 * Date: 11.2.2018,
 *
 * MIT License
 *
 * Copyright (c) 2018 Alternate-Life.de
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
using JustAnotherVoiceChat.Server.Wrapper.Exceptions;
using JustAnotherVoiceChat.Server.Wrapper.Interfaces;
using NLog;

namespace JustAnotherVoiceChat.Server.Dummy
{
    public class Program
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private static ServerHandler _serverHandler;

        private static IVoiceClient _lastClient;
          
        public static void Main(string[] arguments)
        {
            Console.CancelKeyPress += OnCancelKeyPress;

            Logger.Info(new string('=', 10));
            Logger.Info("JustAnotherVoiceChat DummyServer");
            Logger.Info(new string('=', 10));
            Logger.Info("Available Commands:");
            Logger.Info("start - Start a new JustAnotherVoiceChat Server");
            Logger.Info("client - Prepare a new client for the server");
            Logger.Info("connect - Connect the last prepared client to the server");
            Logger.Info("stress - Start a basic client-preparing and removing stresstest");
            Logger.Info("stop - Stop the JustAnotherVoiceChat Server");
            Logger.Info("dispose - Dispose the JustAnotherVoiceChat Server");
            Logger.Info("exit - Close the JustAnotherVoiceChat-Server application");

            while (true)
            {
                ProcessInputLine(Console.ReadLine());
            }
        }

        private static void ProcessInputLine(string input)
        {
            switch (input)
            {
                case "start":
                {
                    if (_serverHandler == null)
                    {
                        _serverHandler = new ServerHandler("localhost", 23332, 20);
                    }
                    
                    Logger.Info("Starting JustAnotherVoiceChat DummyServer...");

                    try
                    {
                        _serverHandler.StartServer();
                    }
                    catch (VoiceServerAlreadyStartedException)
                    {
                        Logger.Info("Server is already started!");
                        return;
                    }
            
                    Logger.Info("JustAnotherVoiceChat DummyServer started!");
                    break;
                }
                case "stop":
                {
                    StopServer();
                    break;
                }
                case "dispose":
                {
                    DisposeServer();
                    break;
                }
                case "stress":
                {
                    Logger.Info("Serverstress started");
                    
                    _serverHandler.StartStresstest();
                    break;
                }
                case "client":
                {
                    var client = _serverHandler.PrepareClient();

                    _lastClient = client;
                    
                    Logger.Info("Prepared new client-slot: " + client.Handle.Identifer);      
                    break;
                }
                case "connect":
                {
                    _serverHandler.TriggerClientConnect(_lastClient.Handle.Identifer);

                    Logger.Info("Event Triggered");
                    
                    break;
                }
                case "exit":
                {
                    ExitApplication();
                    break;
                }
            }
        }

        private static void OnCancelKeyPress(object sender, ConsoleCancelEventArgs e)
        {
            ExitApplication();
        }

        private static void ExitApplication()
        {
            if (_serverHandler != null)
            {
                StopServer();
                DisposeServer();
            }

            Environment.Exit(0);
        }

        private static void StopServer()
        {
            try
            {
                _serverHandler.StopServer();
            }
            catch (VoiceServerNotStartedException)
            {
                Logger.Info("Server has not been started");
            }
        }

        private static void DisposeServer()
        {
            _serverHandler.Dispose();
            _serverHandler = null;

            Logger.Info("Server has been disposed");
        }
    }
}
