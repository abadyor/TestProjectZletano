using MediatR;
using TestProject.Core.Fetures.Basketes.Queries.Response;
using TestProjectZletano.Core.Base;

namespace TestProject.Core.Fetures.Basketes.Queries.Model
{
    public class GetBasketByCustomerIdQuery : IRequest<Response<GetBasketByidResponse>>
    {
        public int CustomerId { get; set; }
    }
}
