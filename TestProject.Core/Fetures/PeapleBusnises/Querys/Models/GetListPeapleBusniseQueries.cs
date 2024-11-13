using MediatR;
using TestProject.Core.Fetures.PeapleBusnises.Querys.Response;
using TestProjectZletano.Core.Base;

namespace TestProject.Core.Fetures.PeapleBusnises.Querys.Models
{
    public class GetListPeapleBusniseQueries : IRequest<Response<List<GetPeapleBusnisResponse>>>
    {
    }
}
