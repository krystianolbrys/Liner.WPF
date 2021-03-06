using System.Threading.Tasks;
using Liner.API.Contracts.Requests;
using Liner.API.Contracts.Responses;

namespace Liner.API.Contracts
{
    public interface ILinerApiService
    {
        Task<PathResponse> GetPath(GetPathRequest request);
    }
}
