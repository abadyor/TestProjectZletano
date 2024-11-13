using AutoMapper;
using MediatR;
using TestProject.Core.Fetures.Vendors.Queries.Models;
using TestProject.Core.Fetures.Vendors.Queries.Response;
using TestProject.Service.Abstract;
using TestProjectZletano.Core.Base;

namespace TestProject.Core.Fetures.Vendors.Queries.Handles
{
    public class VendorHandlerQuery : ResponseHandler,
                                    IRequestHandler<GetVendorListQuery, Response<List<GetVendorListResponse>>>,
                                    IRequestHandler<GetVendorByIdQuery, Response<GetVendorByIdResponse>>,
                                    IRequestHandler<GetMarket, Response<List<GetMarketResponse>>>,
                                    IRequestHandler<GetRegion, Response<List<GetRegionResponse>>>,
                                    IRequestHandler<GetCities, Response<List<GetCitiesResponse>>>,
                                    IRequestHandler<CheckUserByLogQuery, Response<CheckUserByLogResponse>>,
                                    IRequestHandler<LoginUserQuery, Response<LoginUserResponse>>
    /*IRequestHandler<CheckUserByUsernameQuery, Response<CheckUserByUsernameResponse>>*/

    {
        #region
        private readonly IVendorService _vendorService;

        private readonly IMapper _mapper;
        #endregion
        #region Constractor

        public VendorHandlerQuery(IVendorService vendorService, IMapper mapper)
        {
            _vendorService = vendorService;
            _mapper = mapper;
        }
        #endregion
        public async Task<Response<List<GetVendorListResponse>>> Handle(GetVendorListQuery request, CancellationToken cancellationToken)
        {
            var x = await _vendorService.GetDataAll();
            var xListMapper = _mapper.Map<List<GetVendorListResponse>>(x);
            return Success(xListMapper);
        }

        public async Task<Response<GetVendorByIdResponse>> Handle(GetVendorByIdQuery request, CancellationToken cancellationToken)
        {

            var x = await _vendorService.GetByid(request.id);
            if (x == null)
            {
                return NotFound<GetVendorByIdResponse>("Id Not Exist");
            }



            // إنشاء كائن GetVendorByIdResponse يدويًا
            var response = new GetVendorByIdResponse
            {
                Id = x.Id, // تأكد من أن لديك الخاصية Id في كائن x
                GivenNames = x.GivenNames,
                Nickname = x.Nickname,
                Gender = x.Gender,
                DocId = x.DocId,
                DocType = x.DocType,
                EmailAddress = x.EmailAddress,
                Mobile = x.Mobile,




                Username = x.Username,
                Password = x.Password,
                Timestamp_create = x.Timestamp_create
                // أضف أي خصائص أخرى تحتاجها من x
            };

            return Success(response);
            /* var x = await _vendorService.GetByid(request.id);
             if (x == null)
             {
                 return NotFound<GetVendorByIdResponse>("Id Not Exist");
             }
             var xByidMapper = _mapper.Map<GetVendorByIdResponse>(x);
             return Success(xByidMapper);*/
        }

        public Task<Response<List<GetMarketResponse>>> Handle(GetMarket request, CancellationToken cancellationToken)
        {
            var marketData = _vendorService.ReadMarketCodesFromFile(@"F:\TestProjectZletano\file.txt");

            // تحويل البيانات إلى قائمة من GetMarketResponse
            var marketResponses = new List<GetMarketResponse>();

            foreach (var market in marketData)
            {
                marketResponses.Add(new GetMarketResponse
                {
                    Name = market.Name,
                    Code = market.Code
                });
            }

            var response = new Response<List<GetMarketResponse>>
            {
                Data = marketResponses,
                Succeeded = true,
                Message = "تم جلب البيانات بنجاح"
            };

            return Task.FromResult(response); // إرجاع الاستجابة


        }

        public Task<Response<List<GetCitiesResponse>>> Handle(GetCities request, CancellationToken cancellationToken)
        {
            var ReginData = _vendorService.ReadCityCodesFromFile(@"F:\TestProjectZletano\file3.txt");

            // تحويل البيانات إلى قائمة من GetMarketResponse
            var ReginResponses = new List<GetCitiesResponse>();

            foreach (var market in ReginData)
            {
                ReginResponses.Add(new GetCitiesResponse
                {
                    Name = market.Name,
                    Code = market.Code
                });
            }

            var response = new Response<List<GetCitiesResponse>>
            {
                Data = ReginResponses,
                Succeeded = true,
                Message = "تم جلب البيانات بنجاح"
            };

            return Task.FromResult(response); // إرجاع الاستجابة
        }
        public Task<Response<List<GetRegionResponse>>> Handle(GetRegion request, CancellationToken cancellationToken)
        {
            var ReginData = _vendorService.ReadCityCodesFromFile(@"F:\TestProjectZletano\file2.txt");

            // تحويل البيانات إلى قائمة من GetMarketResponse
            var ReginResponses = new List<GetRegionResponse>();

            foreach (var market in ReginData)
            {
                ReginResponses.Add(new GetRegionResponse
                {
                    Name = market.Name,
                    Code = market.Code
                });
            }

            var response = new Response<List<GetRegionResponse>>
            {
                Data = ReginResponses,
                Succeeded = true,
                Message = "تم جلب البيانات بنجاح"
            };

            return Task.FromResult(response); // إرجاع الاستجابة
        }


        public async Task<Response<CheckUserByLogResponse>> Handle(CheckUserByLogQuery request, CancellationToken cancellationToken)
        {
            var x = await _vendorService.GetVendorByUserNamePasswordLogUser(request.username, request.password, request.loguser);
            if (x == null)
            {
                return NotFound<CheckUserByLogResponse>("Id Not Exist");
            }



            // إنشاء كائن GetVendorByIdResponse يدويًا
            var response = new CheckUserByLogResponse
            {
                Id = x.Id, // تأكد من أن لديك الخاصية Id في كائن x
                GivenNames = x.GivenNames,
                Nickname = x.Nickname,
                Gender = x.Gender,
                DocId = x.DocId,
                DocType = x.DocType,
                EmailAddress = x.EmailAddress,
                Mobile = x.Mobile,

                Username = x.Username,
                Password = x.Password,
                Timestamp_create = x.Timestamp_create
                // أضف أي خصائص أخرى تحتاجها من x
            };

            return Success(response);
        }

        public async Task<Response<LoginUserResponse>> Handle(LoginUserQuery request, CancellationToken cancellationToken)
        {
            var x = await _vendorService.LoginVendor(request.username, request.password);
            if (x == null)
            {
                return NotFound<LoginUserResponse>("Id Not Exist");
            }



            // إنشاء كائن GetVendorByIdResponse يدويًا
            var response = new LoginUserResponse
            {
                Id = x.Id, // تأكد من أن لديك الخاصية Id في كائن x
                GivenNames = x.GivenNames,
                Nickname = x.Nickname,
                Gender = x.Gender,
                DocId = x.DocId,
                DocType = x.DocType,
                EmailAddress = x.EmailAddress,
                Mobile = x.Mobile,

                Username = x.Username,
                Password = x.Password,
                Timestamp_create = x.Timestamp_create
                // أضف أي خصائص أخرى تحتاجها من x
            };

            return Success(response);
        }



        /* public async Task<Response<CheckUserByUsernameResponse>> Handle(CheckUserByUsernameQuery request, CancellationToken cancellationToken)
         {
             var x = await _vendorService.CheckUserAndSendEmail(request.Username, request.Password);
             if (x == null)
             {
                 return NotFound<CheckUserByUsernameResponse>("هدا المستخدم غير موجود");
             }

             return Success(Success(x))
         }*/
    }
}
