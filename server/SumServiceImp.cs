using Grpc.Core;
using Sum;
using System.Threading.Tasks;
using static Sum.SumService;

namespace server
{
    public class SumServiceImp : SumServiceBase
    {
        public override Task<SumResponse> Sum(SumRequest request, ServerCallContext context)
        {
            string result = string.Format("result is {0}", request.Args.FirstNum + request.Args.SecondNum);
            return Task.FromResult(new SumResponse() { Res = result });
        }
    }
}
