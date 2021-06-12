using MediatR;

namespace Liner.Service.Queries
{
    public class SampleRequest : IRequest<int>
    {
        public int Value { get; private set; }

        public SampleRequest(int value)
        {
            Value = value;
        }
    }
}
