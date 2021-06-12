using System.Threading;
using System.Threading.Tasks;
using Liner.Service.Queries;
using MediatR;

namespace Liner.Service.QueryHandler
{
    public class SampleRequestHandler : IRequestHandler<SampleRequest, int>
    {
        public async Task<int> Handle(SampleRequest request, CancellationToken cancellationToken)
        {
            // non blocking
            await Task.Delay(2000);
            return request.Value * 2;
        }
    }
}
