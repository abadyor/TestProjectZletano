using AutoMapper;
using MediatR;
using TestProject.Core.Fetures.ControlTables.Qureries.Models;
using TestProject.Core.Fetures.ControlTables.Qureries.Response;
using TestProject.Service.Abstract;
using TestProjectZletano.Core.Base;

namespace TestProject.Core.Fetures.ControlTables.Qureries.Handles
{
    public class ControlQueryHandler : ResponseHandler,
                    IRequestHandler<GetControlByVendorQuery, Response<List<GetControlByVendorResponse>>>
    {
        private readonly IControlService _controlService;

        private readonly IMapper _mapper;



        public ControlQueryHandler(IControlService controlService, IMapper mapper)
        {
            _controlService = controlService;
            _mapper = mapper;
        }
        public async Task<Response<List<GetControlByVendorResponse>>> Handle(GetControlByVendorQuery request, CancellationToken cancellationToken)
        {
            var records = await _controlService.GetByVendorIdAsync(request.VendorId);

            if (records == null || !records.Any())
            {
                return NotFound<List<GetControlByVendorResponse>>("Id Not Exist");
            }

            var response = records.Select(record => new GetControlByVendorResponse
            {
                Id = record.Id,
                id_vendor = record.id_vendor,
                M_Code = record.M_Code,
                Last_sore = record.Last_sore,
                visitor = record.visitor,
                shopeName = record.shopeName,
                Address = record.Address,
                region = record.region,
                city = record.city,
                Street = record.Street,
                NerestPoint = record.NerestPoint
            }).ToList();

            return Success(response);
        }
    }
}
