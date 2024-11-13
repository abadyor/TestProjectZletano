using MediatR;
using TestProject.Core.Fetures.T11.Queries.Response;
using TestProjectZletano.Core.Base;

namespace TestProject.Core.Fetures.T11.Queries.Models
{
    public class GetT1ListQueries : IRequest<Response<List<GetT1Response>>>
    {
    }
}
