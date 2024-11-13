using MediatR;
using TestProjectZletano.Core.Base;

namespace TestProject.Core.Fetures.T11.Command.Models
{
    public class AddT1CreateRunTimeCommand : IRequest<Response<string>>
    {
        public string Table_Name { get; set; }
    }
}
