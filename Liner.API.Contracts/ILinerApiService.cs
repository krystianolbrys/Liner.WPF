using Liner.API.Contracts.Requests;
using Liner.API.Contracts.Responses;

namespace Liner.API.Contracts
{
    public interface ILinerApiService
    {
        LinesResponse GetPath(TwoPointsRequest request);
    }
}
