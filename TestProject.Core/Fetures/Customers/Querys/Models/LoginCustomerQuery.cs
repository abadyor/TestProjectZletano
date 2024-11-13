using MediatR;
using TestProject.Core.Fetures.Customers.Querys.Response;
using TestProjectZletano.Core.Base;

namespace TestProject.Core.Fetures.Customers.Querys.Models
{
    public class LoginCustomerQuery : IRequest<Response<LoginCustomerResponse>>
    {
        public string username { get; set; }
        public string password { get; set; }
    }
}
