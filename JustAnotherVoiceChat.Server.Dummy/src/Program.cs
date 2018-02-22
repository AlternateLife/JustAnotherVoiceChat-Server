/*
 * File: Program.cs
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
using JustAnotherVoiceChat.Server.Dummy.Client;
using JustAnotherVoiceChat.Server.Dummy.Repositories;
using JustAnotherVoiceChat.Server.Wrapper.Elements.Models;
using JustAnotherVoiceChat.Server.Wrapper.Exceptions;
using JustAnotherVoiceChat.Server.Wrapper.Interfaces;
using NLog;

namespace JustAnotherVoiceChat.Server.Dummy
{
    public class Program
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private static ServerHandler  _server;

        private static DummyClient _lastClient;
          
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
                    if (_server == null)
                    {
                        _server = new ServerHandler(new DummyClientFactory(), new VoiceServerConfiguration("localhost", 23332, "S1u8otSWS/L/V1luEkMnupTwgeA=", 130, "123"));
                    }
                    
                    Logger.Info("Starting JustAnotherVoiceChat DummyServer...");

                    try
                    {
                        _server.Start();
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
                    
                    _server.StartStresstest();
                    break;
                }
                case "client":
                {
                    var client = _server.PrepareClient();

                    _lastClient = client;
                    
                    Logger.Info("Prepared new client-slot: " + client.Handle.Identifer);      
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
            if (_server != null)
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
                _server.Stop();;
            }
            catch (VoiceServerNotStartedException)
            {
                Logger.Info("Server has not been started");
            }
        }

        private static void DisposeServer()
        {
            _server.Dispose();
            _server = null;

            Logger.Info("Server has been disposed");
        }
    }
}
