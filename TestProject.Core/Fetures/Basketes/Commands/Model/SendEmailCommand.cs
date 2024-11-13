using MediatR;
using TestProjectZletano.Core.Base;

namespace TestProject.Core.Fetures.Basketes.Commands.Model
{
    public class SendEmailCommand : IRequest<Response<string>>
    {
        public int BasketId { get; set; }
        public int customerId { get; set; }
    }
}
