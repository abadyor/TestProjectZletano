using MediatR;
using TestProjectZletano.Core.Base;

namespace TestProject.Core.Fetures.Basketes.Commands.Model
{
    public class CreateBasketCommand : IRequest<Response<string>>
    {
        public string TableName { get; set; }
        public int customerId { get; set; }






    }
}
