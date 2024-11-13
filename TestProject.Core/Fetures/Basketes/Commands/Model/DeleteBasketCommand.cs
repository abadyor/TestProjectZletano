using MediatR;
using TestProjectZletano.Core.Base;

namespace TestProject.Core.Fetures.Basketes.Commands.Model
{
    public class DeleteBasketCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }


    }
}
