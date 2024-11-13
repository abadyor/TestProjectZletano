using MediatR;
using TestProjectZletano.Core.Base;

namespace TestProject.Core.Fetures.Basket_ss.Commands.Models
{
    public class UpdateBasket_sCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }



        public int quantity { get; set; }


    }
}
