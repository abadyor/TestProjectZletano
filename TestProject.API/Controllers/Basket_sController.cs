using MediatR;
using Microsoft.AspNetCore.Mvc;
using TestProject.Core.Fetures.Basket_ss.Commands.Models;
using TestProject.Core.Fetures.Basket_ss.Queries.Models;
using TestProject.Core.Fetures.Basketes.Queries.Model;

namespace TestProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Basket_sController : ControllerBase
    {
        private readonly IMediator _mediator;

        public Basket_sController(IMediator mediator)
        {
            _mediator = mediator;
        }




        [HttpGet("/Basket_s/Getall")]
        public async Task<IActionResult> Getall()
        {
            var response = await _mediator.Send(new GetListBasket_sQuery());
            return Ok(response);
        }




        [HttpGet("/Basket_s/GetByid/{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var response = await _mediator.Send(new GetBaske_sByIdQuery() { id = id });
            return Ok(response);
        }



        [HttpPost("/Basket_s/Create")]
        public async Task<IActionResult> Create([FromBody] AddBasket_sCommand addBasket_sCommand)
        {
            var response = await _mediator.Send(addBasket_sCommand);
            return Ok(response);
        }

        [HttpGet("/Basket_s/GetBasketByCustomerId")]
        public async Task<IActionResult> GetBasketByCustomerId(int customerId)
        {
            if (customerId == 0)
            {
                return BadRequest("الـ customerId غير صالح.");
            }
            var response = await _mediator.Send(new GetBasketByCustomerIdQuery() { CustomerId = customerId });
            if (response.Data != null)
            {
                int basketId = response.Data.Id;

                var response2 = await _mediator.Send(new GetBasket_sByBasketIdQuery() { basketId = basketId });
                if (response2 != null)

                    return Ok(response2);
                else
                    return BadRequest("لم يتم جلب بيانات Basket_s");


            }
            else
            {
                return NotFound();
            }



        }



        [HttpPut("/Basket_s/Edite")]
        public async Task<IActionResult> Edite([FromBody] UpdateBasket_sCommand editeBasket_sCommand)
        {
            var response = await _mediator.Send(editeBasket_sCommand);
            if (!response.Succeeded)
            {
                return BadRequest(response.Message);
            }
            return Ok(response);
        }


        [HttpDelete("/Basket_s/Delete")]
        public async Task<IActionResult> Delete([FromBody] DeleteBasket_sCommand deleteBasket_SCommand)
        {
            if (deleteBasket_SCommand.Id == 0)
            {
                return BadRequest("Invalid input data.");
            }
            var response = await _mediator.Send(deleteBasket_SCommand);
            if (!response.Succeeded)
            {
                return BadRequest(response.Message);
            }
            return Ok(response);
        }






    }
}
