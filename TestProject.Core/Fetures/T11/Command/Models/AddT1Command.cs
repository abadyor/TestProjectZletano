using MediatR;
using TestProjectZletano.Core.Base;

namespace TestProject.Core.Fetures.T11.Command.Models
{
    public class AddT1Command : IRequest<Response<string>>
    {
        public string name { get; set; }

        public string des { get; set; }
    }
}
