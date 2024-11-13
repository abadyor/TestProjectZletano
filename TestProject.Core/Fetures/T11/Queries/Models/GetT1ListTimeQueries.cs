using MediatR;
using TestProject.Core.Fetures.T11.Queries.Response;

namespace TestProject.Core.Fetures.T11.Queries.Models
{
    public class GetT1ListTimeQueries : IRequest<(List<GetT1TimeResponse> records, long elapsedMilliseconds)>
    {
    }
}
