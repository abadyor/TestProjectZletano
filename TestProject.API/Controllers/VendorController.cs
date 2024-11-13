using MediatR;
using Microsoft.AspNetCore.Mvc;
using TestProject.Core.Fetures.Vendors.Commands.Models;
using TestProject.Core.Fetures.Vendors.Queries.Models;

namespace TestProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendorController : ControllerBase
    {
        private readonly IMediator _mediator;

        public VendorController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("/Vendor/Getall")]
        public async Task<IActionResult> Getall()
        {
            var response = await _mediator.Send(new GetVendorListQuery());
            return Ok(response);
        }


        [HttpGet("/Vendor/GetByid/{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var response = await _mediator.Send(new GetVendorByIdQuery() { id = id });
            return Ok(response);
        }


        /*   [HttpPost("/Vendor/Create")]
           public async Task<IActionResult> Create([FromBody] AddVendorCommand addVendorCommand)
           {
               var response = await _mediator.Send(addVendorCommand);
               return Ok(response);
           }*/


        [HttpPost("/Vendor/Create")]
        public async Task<IActionResult> Create([FromBody] AddVendorCommand addVendorCommand)
        {
            // التأكد من أن البيانات المرسلة مكتملة وصحيحة
            /*    if (addVendorCommand == null || (addVendorCommand.MarketCode == null || addVendorCommand.MarketCode == "") || (addVendorCommand.RegionCode == null || addVendorCommand.RegionCode == ""))
                {
                    return BadRequest("البيانات المرسلة غير مكتملة.");
                }*/

            // استدعاء الإجراء لإنشاء البائع باستخدام الأكواد المرسلة
            var response = await _mediator.Send(addVendorCommand);
            return Ok(response);
        }

        [HttpPost("/Vendor/cheklog")]
        public async Task<IActionResult> CheckLog([FromBody] CheckUserByLogQuery query)
        {
            var response = await _mediator.Send(query);
            if (response.Succeeded)
            {
                return Ok(response.Data); // إرجاع البيانات بنجاح
            }
            return BadRequest(response.Message); // إرجاع رسالة خطأ
        }

        [HttpPost("/Vendor/Login")]
        public async Task<IActionResult> Login([FromBody] LoginUserQuery query)
        {
            var response = await _mediator.Send(query);
            if (response.Succeeded)
            {
                return Ok(response.Data); // إرجاع البيانات بنجاح
            }
            return BadRequest(response.Message); // إرجاع رسالة خطأ
        }

        /*    [HttpPost("/Vendor/Create")]
            public async Task<IActionResult> Create([FromBody] AddVendorCommand addVendorCommand)
            {
                // استدعاء API للحصول على بيانات الأسواق
                var marketsResponse = await _mediator.Send(new GetMarket());
                if (!marketsResponse.Succeeded)
                {
                    return BadRequest("فشل في الحصول على بيانات الأسواق.");
                }

                // استخراج الأكواد فقط من بيانات الأسواق
                addVendorCommand.MarketCodes = marketsResponse.Data.Select(m => m.Code).ToList();

                // استدعاء API للحصول على بيانات المناطق
                var regionsResponse = await _mediator.Send(new GetRegion());
                if (!regionsResponse.Succeeded)
                {
                    return BadRequest("فشل في الحصول على بيانات المناطق.");
                }

                // استخراج الأكواد فقط من بيانات المناطق
                addVendorCommand.RegionCodes = regionsResponse.Data.Select(r => r.Code).ToList();

                // استدعاء الإجراء لإنشاء البائع
                var response = await _mediator.Send(addVendorCommand);
                return Ok(response);
            }*/

        /*   [HttpPost("/Vendor/Create")]
           public async Task<IActionResult> Create([FromBody] AddVendorCommand addVendorCommand)
           {
               // استدعاء API للحصول على بيانات الأسواق
               var marketsResponse = await _mediator.Send(new GetMarket());
               if (!marketsResponse.Succeeded)
               {
                   return BadRequest("فشل في الحصول على بيانات الأسواق.");
               }

               // استدعاء API للحصول على بيانات المناطق
               var regionsResponse = await _mediator.Send(new GetRegion());
               if (!regionsResponse.Succeeded)
               {
                   return BadRequest("فشل في الحصول على بيانات المناطق.");
               }

               // تعيين البيانات المسترجعة إلى addVendorCommand
               addVendorCommand.Markets = marketsResponse.Data; // البيانات المستلمة من GetMarket
               addVendorCommand.Regions = regionsResponse.Data; // البيانات المستلمة من GetRegion

               // استدعاء الإجراء لإنشاء البائع
               var response = await _mediator.Send(addVendorCommand);
               return Ok(response);
           }*/


        /* [HttpGet("markets")]
         public IActionResult GetMarketCodes()
         {
             var marketCodes = ReadMarketCodesFromFile();
             return Ok(marketCodes); // إرجاع القاموس مباشرة
         }*/

        [HttpGet("/Vendor/markets")]
        public async Task<IActionResult> GetMarkets()
        {
            var response = await _mediator.Send(new GetMarket());
            if (response.Succeeded)
            {
                return Ok(response.Data); // إرجاع البيانات بنجاح
            }
            return BadRequest(response.Message); // إرجاع رسالة خطأ
        }

        [HttpGet("/Vendor/Regin")]
        public async Task<IActionResult> GetRegion()
        {
            var response = await _mediator.Send(new GetRegion());
            if (response.Succeeded)
            {
                return Ok(response.Data); // إرجاع البيانات بنجاح
            }
            return BadRequest(response.Message); // إرجاع رسالة خطأ
        }

        [HttpGet("/Vendor/City")]
        public async Task<IActionResult> GetCity()
        {
            var response = await _mediator.Send(new GetCities());
            if (response.Succeeded)
            {
                return Ok(response.Data); // إرجاع البيانات بنجاح
            }
            return BadRequest(response.Message); // إرجاع رسالة خطأ
        }

    }
}
