using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Liner.API.Service.Commands;
using MediatR;
using Responses = Liner.API.Contracts.Responses;

namespace Liner.API.Service.CommandHandlers
{
    public class GetPathCommandHandler : IRequestHandler<GetPathCommand, Responses.LinesResponse>
    {
        public async Task<Responses.LinesResponse> Handle(GetPathCommand request, CancellationToken cancellationToken)
        {
            return new Responses.LinesResponse
            {
                Lines = new List<Responses.Line>
                 {
                     new Responses.Line()
                     {
                         Start = new Responses.Point { X = request.Start.X, Y = request.Start.Y },
                         End = new Responses.Point { X = request.End.X, Y = request.End.Y }
                     },
                     new Responses.Line()
                     {
                         Start = new Responses.Point { X = request.End.X, Y = request.End.Y },
                         End = new Responses.Point { X = request.End.X, Y = request.End.Y + 10 }
                     }
                 }
            };
        }
    }
}
