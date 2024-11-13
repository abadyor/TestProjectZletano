using MediatR;
using TestProject.Core.Fetures.Basketes.Queries.Response;
using TestProjectZletano.Core.Base;

namespace TestProject.Core.Fetures.Basketes.Queries.Model
{
    public class GetBasketByCustomerAndBasketIdAndLogQuery : IRequest<Response<GetBasketByCustomerAndBasketIdAndLogResponse>>
    {
        public int BasketId { get; set; }
        public int customerId { get; set; }
        public string loguser { get; set; }



    }
}
