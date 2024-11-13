using MediatR;
using TestProject.Core.Fetures.T11.Queries.Response;
using TestProjectZletano.Core.Base;

namespace TestProject.Core.Fetures.T11.Queries.Models
{
    public class GetT1ByIdQuery : IRequest<Response<GetT1ByIdResponse>>
    {
        public int id { get; set; }
    }
}
