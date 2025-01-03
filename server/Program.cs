﻿using Greet;
using Grpc.Core;
using System;
using System.IO;

namespace server
{
    internal class Program
    {
        const int port = 50051;
        static void Main(string[] args)
        {
            Server s = null;
            try
            {
                s = new Server()
                {
                    Services = { GreetingService.BindService(new GreetingServiceImp()) },
                    Ports = { new ServerPort("localhost", port, ServerCredentials.Insecure) }
                };
                s.Start();
                Console.WriteLine("lesting on " + port);
                Console.ReadKey();
            }
            catch (IOException e)
            {
                Console.WriteLine($"{e.Message}");
                throw;
            }
            finally
            {
                s?.ShutdownAsync().Wait();
            }
        }
    }
}
