using MediatR;
using TestProjectZletano.Core.Base;

namespace TestProject.Core.Fetures.T11.Command.Models
{
    public class EditeT1Command : IRequest<Response<string>>
    {
        public int id { get; set; }
        public string name { get; set; }

        public string des { get; set; }
    }
}
