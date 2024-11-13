using MediatR;
using TestProject.Core.Fetures.Vendors.Queries.Response;
using TestProjectZletano.Core.Base;

namespace TestProject.Core.Fetures.Vendors.Queries.Models
{
    public class CheckUserByUsernameQuery : IRequest<Response<CheckUserByUsernameResponse>>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
