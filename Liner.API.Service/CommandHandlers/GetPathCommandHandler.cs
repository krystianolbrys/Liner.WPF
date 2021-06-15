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
                new Domain.TwoPointLine(
                    new Domain.Point(line.Start.X, line.Start.Y),
                    new Domain.Point(line.End.X, line.End.Y)))
                .ToList();

            var existingLines = new Domain.ExistingLines(linesWsad);

            var boudaryPoint = new Domain.BoundaryPoint(request.Boundaries.MaxWidth, request.Boundaries.MaxHeight);

            var linesMargin = new Domain.PixelsMargin(3);

            var configuration = new Domain.Configuration(request.Boundaries.MaxWidth, request.Boundaries.MaxHeight, linesMargin);

            var pathCreator = new PathCreatorService(start, end, existingLines, configuration);

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
