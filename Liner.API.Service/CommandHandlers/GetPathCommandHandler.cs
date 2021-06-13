using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Liner.API.Service.Commands;
using MediatR;

namespace Liner.API.Service.CommandHandlers
{
    public class GetPathCommandHandler : IRequestHandler<GetPathCommand, Contracts.Responses.PathResponse>
    {
        public async Task<Contracts.Responses.PathResponse> Handle(GetPathCommand request, CancellationToken cancellationToken)
        {
            return new Contracts.Responses.PathResponse
            {
                Lines = new List<Contracts.Common.Line>
                 {
                     new Contracts.Common.Line()
                     {
                         Start = new Contracts.Common.Point { X = request.Start.X, Y = request.Start.Y },
                         End = new Contracts.Common.Point { X = request.End.X, Y = request.End.Y }
                     },
                     new Contracts.Common.Line()
                     {
                         Start = new Contracts.Common.Point { X = request.End.X, Y = request.End.Y },
                         End = new Contracts.Common.Point { X = request.End.X, Y = request.End.Y + 10 }
                     }
                 }
            };
        }
    }
}
