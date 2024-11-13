using MediatR;
using TestProjectZletano.Core.Base;

namespace TestProject.Core.Fetures.ControlTables.Commands.Models
{
    public class AddControlCommand : IRequest<Response<string>>
    {
        public int id_vendor { get; set; }

        public string? M_Code { get; set; }




        public string? shopeName { get; set; }

        public string? Address { get; set; }

        public string? region { get; set; }


        public string? city { get; set; }



        public string? Street { get; set; }

        public string? NerestPoint { get; set; }
    }
}
