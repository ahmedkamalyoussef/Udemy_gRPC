using Greet;
using Grpc.Core;
using System;
using System.Linq;
using System.Threading.Tasks;
using static Greet.GreetingService;

namespace server
{
    public class GreetingServiceImp : GreetingServiceBase
    {
        public override Task<GreetingResponse> Greet(GreetingRequest request, ServerCallContext context)
        {
            string result = string.Format("hello {0} {1}", request.Greeting.FirstName, request.Greeting.LastName);
            return Task.FromResult(new GreetingResponse() { Result = result });
        }
        public override async Task GreetManyTimes(GreetingManyRequest request, IServerStreamWriter<GreetingManyResponse> responseStream, ServerCallContext context)
        {
            Console.WriteLine("received the request");
            Console.WriteLine(request.ToString());
            string result = string.Format("hello {0} {1}", request.Greeting.FirstName, request.Greeting.LastName);
            foreach (var i in Enumerable.Range(1, 10))
                await responseStream.WriteAsync(new GreetingManyResponse() { Result = result });
        }
    }
}
