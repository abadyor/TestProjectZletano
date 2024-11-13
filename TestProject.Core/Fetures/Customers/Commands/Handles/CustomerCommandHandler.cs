using AutoMapper;
using MediatR;
using TestProject.Core.Fetures.Customers.Commands.Models;
using TestProject.Data.Entity;
using TestProject.Service.Abstract;
using TestProjectZletano.Core.Base;

namespace TestProject.Core.Fetures.Customers.Commands.Handles
{
    public class CustomerCommandHandler : ResponseHandler,
                                        IRequestHandler<AddCustomerCommand, Response<string>>,
                                        IRequestHandler<EditeCustomerCommand, Response<string>>

    {
        private readonly ICustomerService _customerService;

        private readonly IMapper _mapper;

        public CustomerCommandHandler(ICustomerService customerService, IMapper mapper)
        {
            _customerService = customerService;
            _mapper = mapper;

        }
        public async Task<Response<string>> Handle(AddCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = new Customer
            {
                GivenNames = request.GivenNames,
                Nickname = request.Nickname,

                Gender = request.Gender,
                DocId = request.DocId,
                DocType = request.DocType,
                EmailAddress = request.EmailAddress,
                Mobile = request.Mobile,

                Username = request.Username,
                Password = request.Password,
                Timestamp_create = DateTime.UtcNow,
            };

            var result = await _customerService.CreateCustomerAsync(customer);
            if (result == "ok")
            {
                return Success("ok add");
            }
            else
            {
                return BadRequest<string>("no add");
            }
        }

        public async Task<Response<string>> Handle(EditeCustomerCommand request, CancellationToken cancellationToken)
        {
            var existingCustomer = await _customerService.GetCustomerByIdAsync(request.Id);
            if (existingCustomer == null)
            {
                return BadRequest<string>("العميل غير موجود");
            }

            existingCustomer.GivenNames = request.GivenNames;
            existingCustomer.Nickname = request.Nickname;
            existingCustomer.Gender = request.Gender;
            existingCustomer.DocId = request.DocId;
            existingCustomer.DocType = request.DocType;
            existingCustomer.EmailAddress = request.EmailAddress;
            existingCustomer.Mobile = request.Mobile;
            existingCustomer.Username = request.Username;
            // تحديث بيانات العميل
            existingCustomer.Password = request.Password;

            // تنفيذ عملية التحديث
            var result = await _customerService.UpdateCustomerAsync(existingCustomer);
            if (result == "ok")
            {
                return Success("تم تحديث العميل بنجاح");
            }
            else
            {
                return BadRequest<string>("فشل في تحديث العميل");
            }
        }


    }
}
