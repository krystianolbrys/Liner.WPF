using System.Collections.Generic;
using Liner.API.Contracts;
using Liner.API.Contracts.Requests;
using Liner.API.Contracts.Responses;

namespace Liner.API.Service
{
    public class ApiService : ILinerApiService
    {
        public LinesResponse GetPath(TwoPointsRequest request)
        {
            return new LinesResponse
            {
                Lines = new List<Line>().AsReadOnly()
            };
        }
    }
}
