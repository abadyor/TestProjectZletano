using MediatR;
using TestProjectZletano.Core.Base;

namespace TestProject.Core.Fetures.Basket_ss.Commands.Models
{
    public class AddBasket_sCommand : IRequest<Response<string>>
    {

        public int basketId { get; set; }



        public int itemId { get; set; }

        public int quantity { get; set; }

        public decimal totoal { get; set; }

        public DateTime date { get; set; }
    }
}
