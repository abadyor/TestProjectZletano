using MediatR;
using TestProject.Core.Fetures.Basket_ss.Queries.Response;
using TestProjectZletano.Core.Base;

namespace TestProject.Core.Fetures.Basket_ss.Queries.Models
{
    public class GetBaske_sByIdQuery : IRequest<Response<GetBasket_sResponse>>
    {
        public int id { get; set; }
    }
}
