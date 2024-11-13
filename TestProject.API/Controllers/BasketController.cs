using MediatR;
using Microsoft.AspNetCore.Mvc;
using TestProject.Core.Fetures.Basketes.Commands.Model;
using TestProject.Core.Fetures.Basketes.Queries.Model;

namespace TestProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BasketController(IMediator mediator)
        {
            _mediator = mediator;
        }




        /* [HttpGet("/Basket/Getall")]
         public async Task<IActionResult> Getall()
         {
             var response = await _mediator.Send(new ());
             return Ok(response);
         }*/




        /*   [HttpGet("/Basket/GetByid/{id}")]
           public async Task<IActionResult> GetById([FromRoute] int id)
           {
               var response = await _mediator.Send(new GetBaske_sByIdQuery() { id = id });
               return Ok(response);
           }*/

        [HttpPost("/Basket/Create")]
        public async Task<IActionResult> Create([FromBody] CreateBasketCommand createBasketCommand)
        {
            var response = await _mediator.Send(createBasketCommand);
            return Ok(response);
        }

        [HttpGet("/Basket/GetEndRow")]
        public async Task<IActionResult> GetEndRow()
        {
            var response = await _mediator.Send(new GetEndRowQuery());
            return Ok(response);
        }


        [HttpPost("/Basket/GetBasketWhereLog")]
        public async Task<IActionResult> GetBasketWhereLog([FromBody] GetBasketByCustomerAndBasketIdAndLogQuery query)
        {
            if (query == null || query.BasketId == 0 || query.customerId == 0 || string.IsNullOrEmpty(query.loguser))
            {
                return BadRequest("Invalid input data.");
            }

            var response = await _mediator.Send(query);
            return Ok(response);
        }

        [HttpGet("/Basket/CheckBasketForInsert")]
        public async Task<IActionResult> CheckBasketForInsert(int customerId)
        {
            var response = await _mediator.Send(new GetBasketByCustomerIdQuery() { CustomerId = customerId });
            return Ok(response);
        }

        [HttpPost("/Basket/SendEmail")]
        public async Task<IActionResult> SendEmail([FromBody] SendEmailCommand sendEmailCommand)
        {



            var response = await _mediator.Send(sendEmailCommand);
            return Ok(response);
        }

        [HttpDelete("/Basket/Delete")]
        public async Task<IActionResult> Delete([FromBody] DeleteBasketCommand deleteBasketCommand)
        {
            if (deleteBasketCommand.Id == 0)
            {
                return BadRequest("Invalid input data.");
            }
            var response = await _mediator.Send(deleteBasketCommand);
            if (!response.Succeeded)
            {
                return BadRequest(response.Message);
            }
            return Ok(response);
        }




        /*  [HttpPut("/Basket/Edite")]
          public async Task<IActionResult> Edite([FromBody] UpdateBasketCommand editeBasketCommand)
          {
              var response = await _mediator.Send(editeBasketCommand);
              if (!response.Succeeded)
              {
                  return BadRequest(response.Message);
              }
              return Ok(response);
          }*/

    }
}
