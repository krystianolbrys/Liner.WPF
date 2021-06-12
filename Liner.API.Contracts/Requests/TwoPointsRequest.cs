namespace Liner.API.Contracts.Requests
{
    public class TwoPointsRequest
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
