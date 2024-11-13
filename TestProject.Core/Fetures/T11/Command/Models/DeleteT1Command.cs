using MediatR;
using TestProjectZletano.Core.Base;

namespace TestProject.Core.Fetures.T11.Command.Models
{
    public class DeleteT1Command : IRequest<Response<string>>
    {
        public int id { get; set; }


    }
}
