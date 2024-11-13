using AutoMapper;
using MediatR;
using TestProject.Core.Fetures.PeapleBusnises.Commads.Models;
using TestProject.Data.Entity;
using TestProject.Service.Abstract;
using TestProjectZletano.Core.Base;

namespace TestProject.Core.Fetures.PeapleBusnises.Commads.Handlers
{
    public class PeapleBusniseCommandHandler : ResponseHandler,
        IRequestHandler<CreatePeapleBusniseCommand, Response<string>>,
            IRequestHandler<UpdatePeapleBusniseCommand, Response<string>>


    {
        private readonly IPeapleBusniseService _peapleBusniseService;

        private readonly IMapper _mapper;
        public PeapleBusniseCommandHandler(IPeapleBusniseService PeapleBusniseService, IMapper mapper)
        {
            _peapleBusniseService = PeapleBusniseService;
            _mapper = mapper;

        }

        public async Task<Response<string>> Handle(CreatePeapleBusniseCommand request, CancellationToken cancellationToken)
        {
            var peapleBusnise = _mapper.Map<PeapleBusnise>(request);
            await _peapleBusniseService.AddAsync(peapleBusnise);
            return Created("ok");
            /*    return new Response<string> { Data = "تم إنشاء السجل بنجاح", Success = true };*/
        }

        public async Task<Response<string>> Handle(UpdatePeapleBusniseCommand request, CancellationToken cancellationToken)
        {

            var peapleBusnise = _mapper.Map<PeapleBusnise>(request);
            await _peapleBusniseService.UpdateAsync(peapleBusnise);

            return Success("ok updata");
        }

        /*  public async Task<Response<GetPeapleBusnisByIdResponse>> Handle(GetPeapleBusniseByIdQuery request, CancellationToken cancellationToken)
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
          }*/

        /*   public async Task<List<PeapleBusnise>> Handle(GetListPeapleBusniseQueries request, CancellationToken cancellationToken)
           {
               var peapleBusnises = await _peapleBusniseService.GetAllAsync();
               return new Response<List<PeapleBusnise>>;
           }*/
    }
}
