using AutoMapper;
using MediatR;
using TestProject.Core.Fetures.PeapleBusnises.Querys.Models;
using TestProject.Core.Fetures.PeapleBusnises.Querys.Response;
using TestProject.Service.Abstract;
using TestProjectZletano.Core.Base;

namespace TestProject.Core.Fetures.PeapleBusnises.Querys.Handlers
{
    public class PeapleBusnistHandlerQueries : ResponseHandler,
        IRequestHandler<GetPeapleBusniseByIdQuery, Response<GetPeapleBusnisByIdResponse>>,
     IRequestHandler<GetListPeapleBusniseQueries, Response<List<GetPeapleBusnisResponse>>>
    {

        private readonly IPeapleBusniseService _peapleBusniseService;

        private readonly IMapper _mapper;
        public PeapleBusnistHandlerQueries(IPeapleBusniseService PeapleBusniseService, IMapper mapper)
        {
            _peapleBusniseService = PeapleBusniseService;
            _mapper = mapper;

        }
        public async Task<Response<GetPeapleBusnisByIdResponse>> Handle(GetPeapleBusniseByIdQuery request, CancellationToken cancellationToken)
        {
            var peapleBusnise = await _peapleBusniseService.GetByIdAsync(request.Id);
            if (peapleBusnise == null)
            {
                return BadRequest<GetPeapleBusnisByIdResponse>("غير موجود");
            }
            return new Response<GetPeapleBusnisByIdResponse>();
        }

        public async Task<Response<List<GetPeapleBusnisResponse>>> Handle(GetListPeapleBusniseQueries request, CancellationToken cancellationToken)
        {
            var peapleBusnises = await _peapleBusniseService.GetAllAsync();
            return new Response<List<GetPeapleBusnisResponse>>();
        }
    }
}
