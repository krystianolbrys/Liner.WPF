using System;
using System.Linq;
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

        public async Task<PathResponse> GetPath(GetPathRequest request)
        {
            var command = new GetPathCommand
            {
                Start = new Commands.Point { X = request.Start.X, Y = request.Start.Y },
                End = new Commands.Point { X = request.End.X, Y = request.End.Y },
                ExistingLines = request.ExistingLines.Select(line => new TwoPointLine
                {
                    Start = new Point { X = line.Start.X, Y = line.Start.Y },
                    End = new Point { X = line.End.X, Y = line.End.Y }
                }).ToList().AsReadOnly(),
                Configuration = new Commands.Configuration
                {
                    Width = request.Configuration.Width,
                    Height = request.Configuration.Height,
                    LineMarginInPixels = request.Configuration.LineMarginInPixels
                }
            };

            return await _mediator.Send(command);
        }
    }
}
