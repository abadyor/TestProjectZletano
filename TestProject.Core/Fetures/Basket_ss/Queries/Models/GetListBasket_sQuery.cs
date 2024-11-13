using MediatR;
using TestProject.Core.Fetures.Basket_ss.Queries.Response;
using TestProjectZletano.Core.Base;

namespace TestProject.Core.Fetures.Basket_ss.Queries.Models
{
    public class GetListBasket_sQuery : IRequest<Response<List<GetListBasket_sResponse>>>
    {
    }
}
