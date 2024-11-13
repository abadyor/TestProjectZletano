using AutoMapper;
using MediatR;
using TestProject.Core.Fetures.Customers.Querys.Models;
using TestProject.Core.Fetures.Customers.Querys.Response;
using TestProject.Service.Abstract;
using TestProjectZletano.Core.Base;

namespace TestProject.Core.Fetures.Customers.Querys.Handles
{
    public class CustomerQueryHandler : ResponseHandler,
                                        IRequestHandler<GetCustomerByIdQuery, Response<GetCustomerByIdResponse>>,
                                        IRequestHandler<GetAllCustomerQuery, Response<List<GetAllCustomerResponse>>>,
                                        IRequestHandler<LoginCustomerQuery, Response<LoginCustomerResponse>>
    {
        private readonly ICustomerService _customerService;
        private readonly IMapper _mapper;

        public CustomerQueryHandler(ICustomerService customerService, IMapper mapper)
        {
            _customerService = customerService;
            _mapper = mapper;
        }
        public async Task<Response<GetCustomerByIdResponse>> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
        {
            var x = await _customerService.GetCustomerByIdAsync(request.Id);
            if (x == null)
            {
                return NotFound<GetCustomerByIdResponse>("Id Not Exist");
            }



            // إنشاء كائن GetVendorByIdResponse يدويًا
            var response = new GetCustomerByIdResponse
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
                Password = x.Password

                // أضف أي خصائص أخرى تحتاجها من x
            };

            return Success(response);
        }

        public async Task<Response<List<GetAllCustomerResponse>>> Handle(GetAllCustomerQuery request, CancellationToken cancellationToken)
        {
            // استرجاع جميع العملاء
            var customers = await _customerService.GetAllCustomersAsync();

            // تحويل العملاء إلى استجابة
            var response = customers.Select(x => new GetAllCustomerResponse
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
                // لا تقم بإرجاع كلمة المرور لأسباب تتعلق بالأمان
                // Password = x.Password, // تأكد من عدم إرجاع كلمة المرور
                // أضف أي خصائص أخرى تحتاجها من x
            }).ToList();
            return Success(response);
            /*
                        var x = await _customerService.GetAllCustomersAsync();
                        var response = new GetAllCustomerResponse
                        {
                            Id = x., // تأكد من أن لديك الخاصية Id في كائن x
                            GivenNames = x.GivenNames,
                            Nickname = x.Nickname,
                            Gender = x.Gender,
                            DocId = x.DocId,
                            DocType = x.DocType,
                            EmailAddress = x.EmailAddress,
                            Mobile = x.Mobile,

                            Username = x.Username,
                            Password = x.Password

                            // أضف أي خصائص أخرى تحتاجها من x
                        };
                        return Success(response);*/
        }

        public async Task<Response<LoginCustomerResponse>> Handle(LoginCustomerQuery request, CancellationToken cancellationToken)
        {
            var x = await _customerService.LoginCustomer(request.username, request.password);
            if (x == null)
            {
                return NotFound<LoginCustomerResponse>("Id Not Exist");
            }



            // إنشاء كائن GetVendorByIdResponse يدويًا
            var response = new LoginCustomerResponse
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
    }
}
