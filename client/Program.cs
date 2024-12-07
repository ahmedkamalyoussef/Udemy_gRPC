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
            var greetings = new[]
            {
                new Greeting()
                {
                    FirstName = "one",
                    LastName = "one"
                },
                new Greeting()
                {
                    FirstName = "two",
                    LastName = "two"
                },
                new Greeting()
                {
                    FirstName = "three",
                    LastName = "three"
                },
                new Greeting()
                {
                    FirstName = "four",
                    LastName = "four"
                },
                new Greeting()
                {
                    FirstName = "five",
                    LastName = "five"
                }
            };
            var g = new Greeting()
            {
                FirstName = "five",
                LastName = "five"
            };
            var g2 = new Greeting()
            {
                FirstName = "hhhhhhhhhhhhhhhhhhhhhhhhh",
                LastName = "hhhhhhhhhhhhhhhhhhhhhhhhhhh"
            };
            var streme = client.GreetEveryone();
            // foreach (var greeting in greetings)
            // {
            //     await streme.RequestStream.WriteAsync(new GreetingEveryoneRequest() { Greeting = greeting });
            // }
            for (int i = 0; i < 15; i++)
            {
                await streme.RequestStream.WriteAsync(new GreetingEveryoneRequest() { Greeting = g });
                
            }
            await streme.RequestStream.CompleteAsync();
            while (await streme.ResponseStream.MoveNext())
            {
                Console.WriteLine(streme.ResponseStream.Current.Result);
                await Task.Delay(1000);
            }
            // var request = new GreetingManyRequest() { Greeting = greeting };
            // var response = client.GreetManyTimes(request);
            // while (await response.ResponseStream.MoveNext())
            // {
            //     Console.WriteLine(response.ResponseStream.Current.Result);
            //     await Task.Delay(400);
            // }
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
