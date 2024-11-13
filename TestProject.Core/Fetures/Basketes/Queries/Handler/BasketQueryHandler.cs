using MediatR;
using TestProject.Core.Fetures.Basketes.Queries.Model;
using TestProject.Core.Fetures.Basketes.Queries.Response;
using TestProject.Service.Abstract;
using TestProjectZletano.Core.Base;

namespace TestProject.Core.Fetures.Basketes.Queries.Handler
{
    public class BasketQueryHandler : ResponseHandler,
                                    IRequestHandler<GetEndRowQuery, Response<GetEndRowResponse>>,
                                    IRequestHandler<GetBasketByCustomerIdQuery, Response<GetBasketByidResponse>>,
                                    IRequestHandler<GetBasketByCustomerId2Query, Response<GetBasketByidResponse>>,
                                    IRequestHandler<GetBasketByCustomerAndBasketIdAndLogQuery, Response<GetBasketByCustomerAndBasketIdAndLogResponse>>
    {
        private readonly IBasketService _basketService;
        public BasketQueryHandler(IBasketService basketService)
        {
            _basketService = basketService;
        }
        public async Task<Response<GetEndRowResponse>> Handle(GetEndRowQuery request, CancellationToken cancellationToken)
        {
            var x = await _basketService.GetEndRowAsync();
            if (x == null)
            {
                return NotFound<GetEndRowResponse>("Id Not Exist");
            }



            // إنشاء كائن GetVendorByIdResponse يدويًا
            var response = new GetEndRowResponse
            {
                Id = x.Id, // تأكد من أن لديك الخاصية Id في كائن x
                customerId = x.customerId,
                nameIdShope = x.nameIdShope,
                Date = x.Date,
                tootal = x.tootal,
                countElementBasket = x.countElementBasket,
                codeBasket = x.codeBasket,

                // أضف أي خصائص أخرى تحتاجها من x
            };

            return Success(response);
        }

        public async Task<Response<GetBasketByidResponse>> Handle(GetBasketByCustomerIdQuery request, CancellationToken cancellationToken)
        {
            var x = await _basketService.GetByCustomerIdAsync(request.CustomerId, false);
            if (x == null)
            {
                return NotFound<GetBasketByidResponse>("Id Not Exist");
            }

            var response = new GetBasketByidResponse
            {
                Id = x.Id, // تأكد من أن لديك الخاصية Id في كائن x
                customerId = x.customerId,
                nameIdShope = x.nameIdShope,
                Date = x.Date,
                tootal = x.tootal,
                countElementBasket = x.countElementBasket,
                codeBasket = x.codeBasket,

                // أضف أي خصائص أخرى تحتاجها من x
            };

            return Success(response);

        }

        public Task<Response<GetBasketByidResponse>> Handle(GetBasketByCustomerId2Query request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<Response<GetBasketByCustomerAndBasketIdAndLogResponse>> Handle(GetBasketByCustomerAndBasketIdAndLogQuery request, CancellationToken cancellationToken)
        {
            var x = await _basketService.GetBasketWhereCustoemrAndBasketIdAndLog(request.BasketId, request.customerId, request.loguser);
            if (x == null)
            {
                return NotFound<GetBasketByCustomerAndBasketIdAndLogResponse>("Id Not Exist");
            }

            var response = new GetBasketByCustomerAndBasketIdAndLogResponse
            {
                Id = x.Id, // تأكد من أن لديك الخاصية Id في كائن x
                customerId = x.customerId,
                nameIdShope = x.nameIdShope,
                Date = x.Date,
                tootal = x.tootal,
                countElementBasket = x.countElementBasket,
                codeBasket = x.codeBasket,
                closeBasket = x.closeBasket,
                loguser = x.loguser,

                // أضف أي خصائص أخرى تحتاجها من x
            };
            var w = await _basketService.updateCloseBasket(request.BasketId);
            if (w == "The Value Updated")
            {
                return Success(response);
            }
            else
            {
                return NotFound<GetBasketByCustomerAndBasketIdAndLogResponse>("The Value Not Updated");
            }


        }
    }
}


