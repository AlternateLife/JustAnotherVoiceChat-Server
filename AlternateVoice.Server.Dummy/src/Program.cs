using System;

using NLog;

using AlternateVoice.Server.Dummy.src.Enums;
using AlternateVoice.Server.Wrapper.Exceptions;

namespace AlternateVoice.Server.Dummy
{
    public class Program
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private static ServerHandler _serverHandler;
          
        public static void Main(string[] arguments)
        {
            Console.CancelKeyPress += OnCancelKeyPress;

            Logger.Info(new string('=', 10));
            Logger.Info("AlternateVoice DummyServer");
            Logger.Info(new string('=', 10));
            Logger.Info("Available Commands:");
            Logger.Info("start - Start a new AlternateVoice Server");
            Logger.Info("client - Prepare a new client for the server");
            Logger.Info("stress - Start a basic client-preparing and removing stresstest");
            Logger.Info("stop - Stop the AlternateVoice Server");
            Logger.Info("dispose - Dispose the AlternateVoice Server");
            
            while (true)
            {
                ProcessInputLine(Console.ReadLine());
            }
        }

        private static void OnCancelKeyPress(object sender, ConsoleCancelEventArgs e)
        {
            if (_serverHandler != null)
            {
                StopServer();
                DisposeServer();
            }

            Environment.Exit(0);
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
                    
                    Logger.Info("Starting AlternateVoice DummyServer...");

                    try
                    {
                        _serverHandler.StartServer();
                    }
                    catch (VoiceServerAlreadyStartedException)
                    {
                        Logger.Info("Server is already started!");
                        return;
                    }
            
                    Logger.Info("AlternateVoice DummyServer started!");
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
                    
                    Logger.Info("Prepared new client-slot: " + client.Handle.Identifer);      
                    break;
                }
            }
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
