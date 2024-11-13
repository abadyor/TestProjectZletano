using MediatR;
using Microsoft.AspNetCore.Mvc;
using TestProject.Core.Fetures.T11.Command.Models;
using TestProject.Core.Fetures.T11.Queries.Models;

namespace TestProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class T1Controller : ControllerBase
    {
        private readonly IMediator _mediator;

        public T1Controller(IMediator mediator)
        {
            _mediator = mediator;
        }




        [HttpGet("/T1/Getall")]
        public async Task<IActionResult> Getall()
        {
            var response = await _mediator.Send(new GetT1ListQueries());
            return Ok(response);
        }


        [HttpGet("/T1/GetDataWhereRecord")]
        public async Task<IActionResult> GetDataWhereRecord(int num)
        {


            if (num <= 0)
            {
                return BadRequest(new { Message = "رقم غير صحيح. يجب أن يكون أكبر من صفر." });
            }

            try
            {
                var query = new GetDataTimeWhereRecordQuery() { number = num };
                var (records, elapsedMilliseconds) = await _mediator.Send(query);
                return Ok(new { Records = records, ElapsedMilliseconds = elapsedMilliseconds });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "حدث خطأ أثناء معالجة الطلب.", Error = ex.Message });
            }




            /*   try
               {
                   var query = new GetDataTimeWhereRecordQuery(num);
                   var (records, elapsedMilliseconds) = await _mediator.Send(query);
                   return Ok(new { Records = records, ElapsedMilliseconds = elapsedMilliseconds });
               }
               catch (Exception ex)
               {
                   // يمكنك تسجيل الخطأ هنا
                   return StatusCode(500, new { Message = "حدث خطأ أثناء معالجة الطلب.", Error = ex.Message });
               }*/

            /*var query = new GetDataTimeWhereRecordQuery(num);
            var (records, elapsedMilliseconds) = await _mediator.Send(query);
            return Ok(new { Records = records, ElapsedMilliseconds = elapsedMilliseconds });
*/

            /*  var query = new GetDataTimeWhereRecordQuery(num);
              var records = await _mediator.Send(query);
              return Ok(records);*/
        }


        [HttpGet("/T1/GetTimeall")]
        public async Task<IActionResult> GetTimeAll()
        {
            var query = new GetT1ListTimeQueries();
            var (records, elapsedMilliseconds) = await _mediator.Send(query);
            return Ok(new { Records = records, ElapsedMilliseconds = elapsedMilliseconds });
        }

        [HttpGet("/T1/GetByid/{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var response = await _mediator.Send(new GetT1ByIdQuery() { id = id });
            return Ok(response);
        }

        [HttpPost("/T1/Create")]
        public async Task<IActionResult> Create([FromBody] AddT1Command addT1Command)
        {
            var response = await _mediator.Send(addT1Command);
            return Ok(response);
        }

        [HttpPost("/T1/CreateTableRunTime")]
        public async Task<IActionResult> CreateTableRunTime([FromBody] AddT1CreateRunTimeCommand addT1Command)
        {
            var response = await _mediator.Send(addT1Command);
            return Ok(response);
        }


        [HttpPut("/T1/Edite")]
        public async Task<IActionResult> Edite([FromBody] EditeT1Command editeT1Command)
        {
            var response = await _mediator.Send(editeT1Command);
            return Ok(response);
        }

        [HttpPut("/T1/Edite/record1")]
        public async Task<IActionResult> EditeRecord([FromBody] EditeT1Command editeT1Command)
        {
            var response = await _mediator.Send(editeT1Command);
            return Ok(response);
        }

        [HttpDelete("/T1/Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteT1Command() { id = id };
            var response = await _mediator.Send(command);
            if (!response.Succeeded)
            {
                return NotFound(response.Message);
            }
            return Ok(response);
        }

        [HttpPost("create-multiple")]
        public async Task<IActionResult> CreateMultipleRecords([FromBody] CreateMultipleRecordsCommand command)
        {
            // استدعاء الـ Handler عبر Mediator
            var response = await _mediator.Send(command);

            // التحقق من نجاح العملية
            if (!response.Succeeded)
            {
                // إرجاع استجابة خطأ مع الحالة المناسبة
                return StatusCode((int)response.StatusCode, response); // أو BadRequest(response)
            }

            // إرجاع استجابة ناجحة مع البيانات
            return Ok(response);
        }

        [HttpPut("/T1/EditeAll")]
        public async Task<IActionResult> EditRecords([FromBody] EditeT1MultyCommand command)
        {

            var response = await _mediator.Send(command);

            if (!response.Succeeded)
            {
                return BadRequest(response.Message);
            }

            return Ok(response);

            /*var command = new EditeT1MultyCommand();
            var response = await _mediator.Send(command);

            if (!response.Succeeded)
            {
                return BadRequest(response.Message);
            }

            return Ok(response);*/
        }






    }
}
