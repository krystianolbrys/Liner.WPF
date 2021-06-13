using Liner.API.Contracts.Responses;
using MediatR;

namespace Liner.API.Service.Commands
{
    public class GetPathCommand : IRequest<LinesResponse>
    {
        public Point Start { get; set; }
        public Point End { get; set; }
    }

    public class Point
    {
        public int X { get; set; }
        public int Y { get; set; }
    }
}
