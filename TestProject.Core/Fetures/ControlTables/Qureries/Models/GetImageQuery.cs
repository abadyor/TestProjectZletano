using MediatR;
using TestProject.Core.Fetures.ControlTables.Qureries.Response;
using TestProjectZletano.Core.Base;

namespace TestProject.Core.Fetures.ControlTables.Qureries.Models
{
    public class GetImageQuery : IRequest<Response<List<GetImageResponse>>>
    {
    }
}
