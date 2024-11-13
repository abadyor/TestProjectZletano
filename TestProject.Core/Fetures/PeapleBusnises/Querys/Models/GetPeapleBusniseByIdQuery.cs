using MediatR;
using TestProject.Core.Fetures.PeapleBusnises.Querys.Response;
using TestProjectZletano.Core.Base;

namespace TestProject.Core.Fetures.PeapleBusnises.Querys.Models
{
    public class GetPeapleBusniseByIdQuery : IRequest<Response<GetPeapleBusnisByIdResponse>>
    {
        public int Id { get; set; }
    }
}
