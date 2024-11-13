using MediatR;
using TestProject.Core.Fetures.Basket_ss.Queries.Models;
using TestProject.Core.Fetures.Basket_ss.Queries.Response;
using TestProject.Service.Abstract;
using TestProjectZletano.Core.Base;

namespace TestProject.Core.Fetures.Basket_ss.Queries.Handler
{
    public class Basket_QueryHandler : ResponseHandler,
                                        IRequestHandler<GetListBasket_sQuery, Response<List<GetListBasket_sResponse>>>,
                                        IRequestHandler<GetBaske_sByIdQuery, Response<GetBasket_sResponse>>,
                                        IRequestHandler<GetBasket_sByBasketIdQuery, Response<List<GetBasket_sByBasketIdResponse>>>
    {
        private readonly IBasket_sService _basket_SService;
        private readonly IBasketService _basketService;
        private readonly IDynamicItemService _dynamicItemService;



        public Basket_QueryHandler(IBasket_sService basket_SService, IBasketService basketService, IDynamicItemService dynamicItemService)
        {
            _basket_SService = basket_SService;
            _basketService = basketService;
            _dynamicItemService = dynamicItemService;
        }
        public async Task<Response<List<GetListBasket_sResponse>>> Handle(GetListBasket_sQuery request, CancellationToken cancellationToken)
        {
            var basket_s = await _basket_SService.GetAllAsync();



            // تحويل العملاء إلى استجابة
            var response = basket_s.Select(x => new GetListBasket_sResponse
            {
                Id = x.Id, // تأكد من أن لديك الخاصية Id في كائن x
                basketId = x.basketId,
                itemId = x.itemId,
                quantity = x.quantity,
                totoal = x.totoal,
                date = x.date,

                // لا تقم بإرجاع كلمة المرور لأسباب تتعلق بالأمان
                // Password = x.Password, // تأكد من عدم إرجاع كلمة المرور
                // أضف أي خصائص أخرى تحتاجها من x
            }).ToList();
            return Success(response);
        }

        public async Task<Response<GetBasket_sResponse>> Handle(GetBaske_sByIdQuery request, CancellationToken cancellationToken)
        {
            var x = await _basket_SService.GetByIdAsync(request.id);
            if (x == null)
            {
                return NotFound<GetBasket_sResponse>("Id Not Exist");
            }



            // إنشاء كائن GetVendorByIdResponse يدويًا
            var response = new GetBasket_sResponse
            {
                Id = x.Id, // تأكد من أن لديك الخاصية Id في كائن x
                basketId = x.basketId,
                itemId = x.itemId,
                quantity = x.quantity,
                totoal = x.totoal,
                date = x.date,

                // أضف أي خصائص أخرى تحتاجها من x
            };

            return Success(response);
        }

        public async Task<Response<List<GetBasket_sByBasketIdResponse>>> Handle(GetBasket_sByBasketIdQuery request, CancellationToken cancellationToken)
        {
            /*  var x = await _basket_SService.GetByBasketIdAsync(request.basketId);
              if (x == null)
              {
                  return NotFound<List<GetBasket_sByBasketIdResponse>>("Id Not Exist");

              }

              var responseList = x.Select(b => new GetBasket_sByBasketIdResponse
              {
                  Id = b.Id,
                  basketId = b.basketId,
                  itemId = b.itemId,
                  quantity = b.quantity,
                  totoal = b.totoal,
                  date = b.date
                  // أضف أي خصائص أخرى تحتاجها من b
              }).ToList();

              return Success(responseList);*/

            var basket = await _basketService.GetByIdAsync(request.basketId);
            if (basket == null)
            {
                return NotFound<List<GetBasket_sByBasketIdResponse>>("Basket Id Not Exist");
            }

            // التأكد من أن `tablename` يحتوي على اسم الجدول
            string tableName = basket.nameIdShope;
            if (string.IsNullOrEmpty(tableName))
            {
                return NotFound<List<GetBasket_sByBasketIdResponse>>("Table name not specified in Basket");
            }

            // جلب جميع العناصر المرتبطة بالسلة من `basket_s`
            var basketItems = await _basket_SService.GetByBasketIdAsync(request.basketId);
            if (basketItems == null || !basketItems.Any())
            {
                return NotFound<List<GetBasket_sByBasketIdResponse>>("No items found for this Basket Id");
            }

            // قائمة لإضافة النتائج النهائية
            var responseList = new List<GetBasket_sByBasketIdResponse>();

            foreach (var item in basketItems)
            {
                // استعلام ديناميكي لجلب اسم `item` من الجدول المحدد بواسطة `tableName`
                var itemName = await _dynamicItemService.GetItemNameById(tableName, item.itemId);

                // إعداد كائن الاستجابة
                var response = new GetBasket_sByBasketIdResponse
                {
                    Id = item.Id,
                    basketId = item.basketId,
                    itemId = item.itemId,
                    quantity = item.quantity,
                    totoal = item.totoal,
                    date = item.date,
                    itemName = itemName // تعيين اسم العنصر
                };
                responseList.Add(response);
            }

            return Success(responseList);


        }
    }
}
