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

            var twoPointLines = request.ExistingLines.Select(line => // zmien nazwe
                new Domain.TwoPointLine(
                    new Domain.Point(line.Start.X, line.Start.Y),
                    new Domain.Point(line.End.X, line.End.Y)))
                .ToList().AsReadOnly();

            var existingLines = new Domain.ExistingLines(twoPointLines);
            var linesMargin = new Domain.PixelsMargin(request.Configuration.LineMarginInPixels);
            var configuration = new Domain.Configuration(request.Configuration.Width, request.Configuration.Height, linesMargin);
            var pathCreator = new PathCreatorService(start, end, existingLines, configuration);

            // asynchronous non-blocking call to long running process
            Domain.Path path = null;

            await Task.Run(() =>
            {
                path = pathCreator.Create();
            });

            if (path == null)
            {
                return Contracts.Responses.PathResponse.EmptyResponse;
            }

            return new Contracts.Responses.PathResponse
            {
                IsPathFound = path.IsPathFound,
                TwoPointLines = path.Lines.Select(line => new Contracts.Common.TwoPointLine()
                {
                    Start = new Contracts.Common.Point { X = line.Start.X, Y = line.Start.Y },
                    End = new Contracts.Common.Point { X = line.End.X, Y = line.End.Y }
                }).ToList().AsReadOnly()
            };
        }
    }
}
