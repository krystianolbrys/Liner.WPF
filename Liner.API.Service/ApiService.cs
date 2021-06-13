using System;
using System.Threading.Tasks;
using Liner.API.Contracts;
using Liner.API.Contracts.Requests;
using Liner.API.Contracts.Responses;
using Liner.API.Service.Commands;
using MediatR;

namespace Liner.API.Service
{
    public class ApiService : ILinerApiService
    {
        private readonly IMediator _mediator;

        public ApiService(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task<LinesResponse> GetPath(TwoPointsRequest request)
        {
            var command = new GetPathCommand
            {
                Start = new Commands.Point { X = request.Start.X, Y = request.Start.Y },
                End = new Commands.Point { X = request.End.X, Y = request.End.Y }
            };

            return await _mediator.Send(command);
        }
    }
}
