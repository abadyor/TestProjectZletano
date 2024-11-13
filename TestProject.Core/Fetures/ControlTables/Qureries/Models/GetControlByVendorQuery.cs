using MediatR;
using TestProject.Core.Fetures.ControlTables.Qureries.Response;
using TestProjectZletano.Core.Base;

namespace TestProject.Core.Fetures.ControlTables.Qureries.Models
{
    public class GetControlByVendorQuery : IRequest<Response<List<GetControlByVendorResponse>>>
    {
        public int VendorId { get; set; }
    }
}
