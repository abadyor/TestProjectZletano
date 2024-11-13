using MediatR;
using TestProject.Core.Fetures.Basketes.Commands.Model;
using TestProject.Data.Entity;
using TestProject.Service.Abstract;
using TestProjectZletano.Core.Base;

namespace TestProject.Core.Fetures.Basketes.Commands.Handler
{
    public class BasketCommandHandler : ResponseHandler,
                                        IRequestHandler<CreateBasketCommand, Response<string>>,
                                        IRequestHandler<SendEmailCommand, Response<string>>,
                                        IRequestHandler<DeleteBasketCommand, Response<string>>

    {
        private readonly IBasketService _basketService;
        private readonly ICustomerService _customerService;
        public BasketCommandHandler(IBasketService basketService, ICustomerService customerService)
        {
            _basketService = basketService;
            _customerService = customerService;
        }
        public async Task<Response<string>> Handle(CreateBasketCommand request, CancellationToken cancellationToken)
        {





            var BasketTable = new Basket
            {
                customerId = request.customerId,
                nameIdShope = request.TableName,
                Date = DateTime.Now,
                tootal = 0,
                countElementBasket = 0,
                codeBasket = request.TableName + request.customerId,

            };

            var x = await _basketService.AddBasketAsync(BasketTable);
            if (x == "oK")
            {
                return Success("Ok Add Succefully");
            }
            else
            {
                return BadRequest<string>("No Add Plese Try Agin");
            }

        }

        public async Task<Response<string>> Handle(SendEmailCommand request, CancellationToken cancellationToken)
        {
            if (request.BasketId != 0 && request.customerId != 0)
            {
                var x = await _customerService.GetCustomerByIdAsync(request.customerId);
                if (x == null)
                {
                    return NotFound<string>("Id Not Exist");
                }


                var customer = new Customer
                {
                    Id = x.Id,
                    GivenNames = x.GivenNames,
                    Nickname = x.Nickname,

                    Gender = x.Gender,
                    DocId = x.DocId,
                    DocType = x.DocType,
                    EmailAddress = x.EmailAddress,
                    Mobile = x.Mobile,

                    Username = x.Username,
                    Password = x.Password,
                    Timestamp_create = DateTime.UtcNow,

                    // أضف أي خصائص أخرى تحتاجها من x
                };

                var email = await _basketService.GenerateCodeAndSendEmail(customer.EmailAddress);
                var CodeGenerate = await _basketService.GetCodeGeny();
                var updateoneFields = await _basketService.updateonefild(request.BasketId, CodeGenerate);
                if (email == "OkSend")
                {
                    if (updateoneFields == "The Value Updated")
                    {


                        return Created("Add Successfuly");
                    }
                    else
                    {
                        return BadRequest<string>("No Update Field");
                    }
                }
                else
                {
                    return BadRequest<string>("The Value Not Comming");
                }



            }
            else
            {
                return BadRequest<string>("NoSendEmail");
            }

        }

        public async Task<Response<string>> Handle(DeleteBasketCommand request, CancellationToken cancellationToken)
        {
            var x = await _basketService.DeleteBasketAsync(request.Id);
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
