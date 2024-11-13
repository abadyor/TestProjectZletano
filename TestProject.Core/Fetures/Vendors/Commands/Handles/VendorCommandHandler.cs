using AutoMapper;
using MediatR;
using TestProject.Core.Fetures.Vendors.Commands.Models;
using TestProject.Data.Entity;
using TestProject.Service.Abstract;
using TestProjectZletano.Core.Base;

namespace TestProject.Core.Fetures.Vendors.Commands.Handles
{
    public class VendorCommandHandler : ResponseHandler,
         IRequestHandler<AddVendorCommand, Response<string>>
    {

        private readonly IVendorService _vendorService;
        private readonly IMapper _mapper;

        public VendorCommandHandler(IVendorService vendorService, IMapper mapper)
        {
            _vendorService = vendorService;
            _mapper = mapper;
        }
        private async Task<string> GetUserInputForVerificationCode()
        {
            // منطق لجمع كود التحقق من واجهة المستخدم
            // يمكن أن يكون من خلال نموذج أو واجهة استخدام
            // هذه الدالة تعيد الكود المدخل بواسطة المستخدم
            return await Task.FromResult("الكود المدخل"); // استبدل هذا بمنطق جمع المدخلات الفعلي
        }
        public async Task<Response<string>> Handle(AddVendorCommand request, CancellationToken cancellationToken)
        {
            /*  var x = _mapper.Map<Vendor>(request);
              var result = await _vendorService.Create(x);
              if (result == "Success")
              {
                  return Created("Add Successfuly");
              }
              else
              {
                  return BadRequest<string>("لم تتم العملية من فضلك اعد المحاولة");
              }*/


            //دالة تاخد الرقم فقط
            /*public string ExtractNumber(string input)
            {
                // التحقق مما إذا كانت السلسلة تحتوي على السلسلة 's_'
                if (input.StartsWith("s_"))
                {
                    // إزالة 's_' من بداية السلسلة
                    input = input.Substring(2);
                }

                // إرجاع الرقم فقط
                return input; // قد ترغب في إضافة التحقق من الأرقام فقط إذا كان ذلك مطلوبًا
            }*/

            // var codeGenrated = await _vendorService.GenerateCodeAndSendEmailAndGetCode(request.EmailAddress);


            var lastStoreCode = await _vendorService.GetLastControlLastCode();

            // محاولة تحويل lastStoreCode إلى عدد صحيح


            // إنشاء store0 باستخدام m_code، vendor.City، و lastStoreCode

            var vendor = new Vendor
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
                Timestamp_create = DateTime.UtcNow, // تعيين التاريخ الحالي
                /*stor0 = request.MarketCode + request.City + lastStoreCode*/

            };

            /*  if (string.IsNullOrEmpty(request.MarketCode))
              {
                  return new Response<string>("يرجى توفير كود السوق.");
              }

              if (string.IsNullOrEmpty(request.RegionCode))
              {
                  return new Response<string>("يرجى توفير كود المنطقة.");
              }*/
            /*var marketCode = request.MarketCode;
            var regionCode = request.RegionCode;*/
            /*    var marketCode = await _vendorService.GetControlFirstId();
                if (marketCode == "No Data")
                {
                    return BadRequest<string>("No Data In ControlTables");
                }*/
            // var result = await _vendorService.Create2(vendor, marketCode, regionCode);
            var result = await _vendorService.Create(vendor);
            if (result == "ok")
            {

                //var testemail = _vendorService.SendEmailAsyncAnotherFunction("مرحبا ", "مرحبا فتحي كيف الحال");
                var email = await _vendorService.GenerateCodeAndSendEmail(request.EmailAddress);
                var CodeGenerate = await _vendorService.GetCodeGeny();
                var updateoneFields = await _vendorService.updateonefild(CodeGenerate);
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
                    return BadRequest<string>("NoSendEmail");
                }

            }
            else
            {
                return BadRequest<string>("لم تتم العملية من فضلك اعد المحاولة");
            }
        }
    }
}

