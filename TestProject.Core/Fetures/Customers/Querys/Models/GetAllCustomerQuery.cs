using MediatR;
using TestProject.Core.Fetures.Customers.Querys.Response;
using TestProjectZletano.Core.Base;

namespace TestProject.Core.Fetures.Customers.Querys.Models
{
    public class GetAllCustomerQuery : IRequest<Response<List<GetAllCustomerResponse>>>
    {
    }
}
