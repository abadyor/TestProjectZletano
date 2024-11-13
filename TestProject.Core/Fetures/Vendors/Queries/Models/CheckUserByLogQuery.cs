using MediatR;
using TestProject.Core.Fetures.Vendors.Queries.Response;
using TestProjectZletano.Core.Base;

namespace TestProject.Core.Fetures.Vendors.Queries.Models
{
    public class CheckUserByLogQuery : IRequest<Response<CheckUserByLogResponse>>
    {
        public string username { get; set; }
        public string password { get; set; }
        public string loguser { get; set; }
    }
}
