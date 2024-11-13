using MediatR;
using TestProjectZletano.Core.Base;

namespace TestProject.Core.Fetures.Customers.Commands.Models
{
    public class AddCustomerCommand : IRequest<Response<string>>
    {
        public string GivenNames { get; set; }


        public string Nickname { get; set; }



        public string Gender { get; set; }


        public string DocId { get; set; }


        public string DocType { get; set; }


        public string EmailAddress { get; set; } = string.Empty;

        public string Mobile { get; set; }


        public string Username { get; set; }


        public string Password { get; set; }
    }
}
