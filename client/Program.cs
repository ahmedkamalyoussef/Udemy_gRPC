using Dummy;
using Grpc.Core;
using System;
using System.Threading.Tasks;

namespace client
{
    internal class Program
    {
        const string target = "127.0.0.1:50051";
        static void Main(string[] args)
        {
            Channel channel = new Channel(target, ChannelCredentials.Insecure);
            channel.ConnectAsync().ContinueWith((task) =>
            {
                if (task.Status == TaskStatus.RanToCompletion)
                    Console.WriteLine("connected gdfgdfgfd");
            });
            var client = new DummyService.DummyServiceClient(channel);
            channel.ShutdownAsync().Wait();

            Console.ReadKey();
        }
    }
}
