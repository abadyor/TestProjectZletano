using MediatR;
using TestProject.Core.Fetures.Basket_ss.Commands.Models;
using TestProject.Data.Entity;
using TestProject.Service.Abstract;
using TestProjectZletano.Core.Base;

namespace TestProject.Core.Fetures.Basket_ss.Commands.Handler
{
    public class Basket_sCommandHandler : ResponseHandler,
                                        IRequestHandler<AddBasket_sCommand, Response<string>>,
                                        IRequestHandler<UpdateBasket_sCommand, Response<string>>,
                                        IRequestHandler<DeleteBasket_sCommand, Response<string>>


    {

        private readonly IBasket_sService _basket_SService;


        public Basket_sCommandHandler(IBasket_sService basket_SService)
        {
            _basket_SService = basket_SService;

        }
        public async Task<Response<string>> Handle(AddBasket_sCommand request, CancellationToken cancellationToken)
        {


            var basket_s = new Basket_s
            {
                basketId = request.basketId,
                itemId = request.itemId,
                quantity = request.quantity,
                totoal = 0,
                date = DateTime.UtcNow,

            };

            var result = await _basket_SService.AddBasketAsync(basket_s);
            if (result == "oK")
            {
                return Success("ok add");
            }
            else
            {
                return BadRequest<string>("no add");
            }
        }

        public async Task<Response<string>> Handle(UpdateBasket_sCommand request, CancellationToken cancellationToken)
        {
            var existingCustomer = await _basket_SService.GetByIdAsync(request.Id);
            if (existingCustomer == null)
            {
                return BadRequest<string>("العميل غير موجود");
            }


            existingCustomer.quantity = request.quantity;




            // تنفيذ عملية التحديث
            var result = await _basket_SService.UpdateBasketAsync(existingCustomer);
            if (result == "ok")
            {
                return Success("تم تحديث العميل بنجاح");
            }
            else
            {
                return BadRequest<string>("فشل في تحديث العميل");
            }
        }

        public async Task<Response<string>> Handle(DeleteBasket_sCommand request, CancellationToken cancellationToken)
        {
            var x = await _basket_SService.DeleteBasketAsync(request.Id);
            if (x == "oK")
            {
                return Success("تم حدف العميل بنجاح");
            }
            else
            {
                return BadRequest<string>("فشل في حدف العميل");
            }
        }
    }
}
