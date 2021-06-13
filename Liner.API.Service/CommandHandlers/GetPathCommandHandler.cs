using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Liner.API.Service.Commands;
using Liner.Core.Services;
using MediatR;
using Domain = Liner.Core.Domain;

namespace Liner.API.Service.CommandHandlers
{
    public class GetPathCommandHandler : IRequestHandler<GetPathCommand, Contracts.Responses.PathResponse>
    {
        public async Task<Contracts.Responses.PathResponse> Handle(GetPathCommand request, CancellationToken cancellationToken)
        {
            var start = new Domain.Point(request.Start.X, request.Start.Y);
            var end = new Domain.Point(request.End.X, request.End.Y);

            var linesWsad = request.ExistingLines.Select(line =>
                new Domain.Line(
                    new Domain.Point(request.Start.X, request.Start.Y),
                    new Domain.Point(request.End.X, request.End.Y)))
                .ToList();

            var existingLines = new Domain.ExistingLines(linesWsad);

            var boudaryPoint = new Domain.BoundaryPoint(request.Boundaries.MaxWidth, request.Boundaries.MaxHeight);

            var pathCreator = new PathCreatorService(start, end, existingLines, boudaryPoint);

            var path = pathCreator.Create();

            return new Contracts.Responses.PathResponse
            {
                Lines = path.Lines.Select(line => new Contracts.Common.Line()
                {
                    Start = new Contracts.Common.Point { X = line.Start.X, Y = line.Start.Y },
                    End = new Contracts.Common.Point { X = line.End.X, Y = line.End.Y }
                }).ToList().AsReadOnly()
            };
        }
    }
}
