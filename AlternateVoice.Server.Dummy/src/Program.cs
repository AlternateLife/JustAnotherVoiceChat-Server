using System;
using System.Threading;
using AlternateVoice.Server.Wrapper.Exceptions;
using NLog;

namespace AlternateVoice.Server.Dummy
{
    public class Program
    {

        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private static ServerHandler _serverHandler;
        
        
        public static void Main(string[] arguments)
        {
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
                    try
                    {
                        _serverHandler.StopServer();
                    }
                    catch (VoiceServerNotStartedException)
                    {
                        Logger.Info("Server has not been started");                        
                    }
                    break;
                }
                case "dispose":
                {
                    _serverHandler.Dispose();
                    _serverHandler = null;
                    
                    Logger.Info("Server has been disposed");      
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
    }
}
