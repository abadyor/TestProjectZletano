using MediatR;
using TestProjectZletano.Core.Base;

namespace TestProject.Core.Fetures.T11.Command.Models
{
    public class EditeT1MultyCommand : IRequest<Response<string>>
    {
        public string fieldName { get; set; } // اسم الحقل الذي نريد تحديثه
    }
}
