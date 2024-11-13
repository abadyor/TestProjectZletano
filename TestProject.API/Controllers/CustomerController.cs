using MediatR;
using Microsoft.AspNetCore.Mvc;
using TestProject.Core.Fetures.Customers.Commands.Models;
using TestProject.Core.Fetures.Customers.Querys.Models;

namespace TestProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomerController(IMediator mediator)
        {
            _mediator = mediator;
        }




        [HttpGet("/Customer/Getall")]
        public async Task<IActionResult> Getall()
        {
            var response = await _mediator.Send(new GetAllCustomerQuery());
            return Ok(response);
        }




        [HttpGet("/Customer/GetByid/{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var response = await _mediator.Send(new GetCustomerByIdQuery() { Id = id });
            return Ok(response);
        }

        [HttpPost("/Customer/Create")]
        public async Task<IActionResult> Create([FromBody] AddCustomerCommand addCustomerCommand)
        {
            var response = await _mediator.Send(addCustomerCommand);
            return Ok(response);
        }

        [HttpPost("/Customer/Login")]
        public async Task<IActionResult> Login([FromBody] LoginCustomerQuery query)
        {
            var response = await _mediator.Send(query);
            if (response.Succeeded)
            {
                return Ok(response.Data); // إرجاع البيانات بنجاح
            }
            return BadRequest(response.Message); // إرجاع رسالة خطأ
        }


        [HttpPut("Customer/Edit")]
        public async Task<IActionResult> Edit([FromBody] EditeCustomerCommand command)
        {


            if (command.Id <= 0)
            {
                return BadRequest("معرف العميل غير صحيح.");
            }

            // التحقق من الحقول المطلوبة
            if (string.IsNullOrEmpty(command.GivenNames) || string.IsNullOrEmpty(command.EmailAddress))
            {
                return BadRequest("الاسم والبريد الإلكتروني مطلوبان.");
            }


            var response = await _mediator.Send(command);

            if (response.Succeeded)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest("فشل في تحديث بيانات العميل.");
            }
        }
    }
}
