using Greet;
using Grpc.Core;
using System;
using System.Threading.Tasks;

namespace client
{
    internal class Program
    {
        const string target = "127.0.0.1:50051";
        static async Task Main(string[] args)
        {
            Channel channel = new Channel(target, ChannelCredentials.Insecure);
            await channel.ConnectAsync().ContinueWith((task) =>
            {
                if (task.Status == TaskStatus.RanToCompletion)
                    Console.WriteLine("connected");
            });
            var client = new GreetingService.GreetingServiceClient(channel);
            var greeting = new Greeting()
            {
                FirstName = "ahmed",
                LastName = "kamal"
            };
            var request = new GreetingManyRequest() { Greeting = greeting };
            var response = client.GreetManyTimes(request);
            while (await response.ResponseStream.MoveNext())
            {
                Console.WriteLine(response.ResponseStream.Current.Result);
                await Task.Delay(400);
            }
            #region sum task
            //var client = new SumService.SumServiceClient(channel);
            //var arguments = new Arguments()
            //{
            //    FirstNum = 7,
            //    SecondNum = 5
            //};
            //var req = new SumRequest() { Args = arguments };
            //var response = client.Sum(req);
            //Console.WriteLine(response);
            #endregion

            Console.ReadKey();
            channel.ShutdownAsync().Wait();

        }
    }
}
