using MediatR;
using TestProject.Core.Fetures.Basket_ss.Queries.Response;
using TestProjectZletano.Core.Base;

namespace TestProject.Core.Fetures.Basket_ss.Queries.Models
{
    public class GetBasket_sByBasketIdQuery : IRequest<Response<List<GetBasket_sByBasketIdResponse>>>
    {
        public int basketId { get; set; }
    }
}
