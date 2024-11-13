using MediatR;
using Microsoft.AspNetCore.Mvc;
using TestProject.Core.Fetures.PeapleBusnises.Commads.Models;
using TestProject.Core.Fetures.PeapleBusnises.Querys.Models;

namespace TestProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeapleBusnisController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PeapleBusnisController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("/peaple/create")]
        public async Task<IActionResult> Create([FromBody] CreatePeapleBusniseCommand command)
        {
            var response = await _mediator.Send(command);
            if (!response.Succeeded)
            {
                return BadRequest(response.Message);
            }
            return CreatedAtAction(nameof(GetpeapleById), new { id = response.Data }, response);
        }

        [HttpPut("/peaple/update")]
        public async Task<IActionResult> Update([FromBody] UpdatePeapleBusniseCommand command)
        {
            var response = await _mediator.Send(command);
            if (!response.Succeeded)
            {
                return BadRequest(response.Message);
            }
            return Ok(response);
        }

        [HttpGet("/peaple/{id}")]
        public async Task<IActionResult> GetpeapleById(int id)
        {
            var query = new GetPeapleBusniseByIdQuery { Id = id };
            var response = await _mediator.Send(query);
            if (!response.Succeeded)
            {
                return NotFound(response.Message);
            }
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetListPeapleBusniseQueries();
            var response = await _mediator.Send(query);
            if (!response.Succeeded)
            {
                return BadRequest(response.Message);
            }
            return Ok(response);
        }

        /*   [HttpGet("markets")]
           public IActionResult GetMarketCodes()
           {
               var marketCodes = ReadMarketCodesFromFile();
               return Ok(marketCodes);
           }*/

        [HttpGet("markets")]
        public IActionResult GetMarketCodes()
        {
            var marketCodes = ReadMarketCodesFromFile();
            return Ok(marketCodes); // إرجاع القاموس مباشرة
        }

        [HttpGet("City")]
        public IActionResult GetCityCodes()
        {
            var marketCodes = ReadCityCodesFromFile();
            return Ok(marketCodes); // إرجاع القاموس مباشرة
        }
        private List<Market> ReadMarketCodesFromFile()
        {
            string filePath = @"F:\TestProjectZletano\file.txt"; // استخدام المسار المطلق
            List<Market> markets = new List<Market>();

            if (System.IO.File.Exists(filePath)) // استخدام System.IO.File
            {
                var content = System.IO.File.ReadAllText(filePath).Trim(); // استخدام System.IO.File
                var marketEntries = content.Split(',');

                foreach (var entry in marketEntries)
                {
                    var parts = entry.Split(':');
                    if (parts.Length == 2)
                    {
                        markets.Add(new Market { Name = parts[0], Code = parts[1] }); // إضافة كائن Market إلى القائمة
                    }
                }
            }
            else
            {
                throw new FileNotFoundException("الملف غير موجود.", filePath);
            }

            return markets; // إرجاع القائمة
        }

        private List<City> ReadCityCodesFromFile()
        {
            string filePath = @"F:\TestProjectZletano\file2.txt"; // استخدام المسار المطلق
            List<City> Cities = new List<City>();

            if (System.IO.File.Exists(filePath)) // استخدام System.IO.File
            {
                var content = System.IO.File.ReadAllText(filePath).Trim(); // استخدام System.IO.File
                var marketEntries = content.Split(',');

                foreach (var entry in marketEntries)
                {
                    var parts = entry.Split(':');
                    if (parts.Length == 2)
                    {
                        Cities.Add(new City { Name = parts[0], Code = parts[1] }); // إضافة كائن Market إلى القائمة
                    }
                }
            }
            else
            {
                throw new FileNotFoundException("الملف غير موجود.", filePath);
            }

            return Cities; // إرجاع القائمة
        }

        /*     private Dictionary<string, string> ReadMarketCodesFromFile()
             {
                 string filePath = @"F:\TestProjectZletano\file.txt"; // استخدام المسار المطلق
                 Dictionary<string, string> markets = new Dictionary<string, string>();

                 if (System.IO.File.Exists(filePath)) // استخدام System.IO.File
                 {
                     var content = System.IO.File.ReadAllText(filePath).Trim(); // استخدام System.IO.File
                     var marketEntries = content.Split(',');

                     foreach (var entry in marketEntries)
                     {
                         var parts = entry.Split(':');
                         if (parts.Length == 2)
                         {
                             markets[parts[0]] = parts[1]; // إضافة إلى القاموس
                         }
                     }
                 }
                 else
                 {
                     throw new FileNotFoundException("الملف غير موجود.", filePath);
                 }

                 return markets; // إرجاع القاموس
             }*/
        /*  private List<Market> ReadMarketCodesFromFile()
          {
              *//* string filePath = Path.Combine(Directory.GetCurrentDirectory(), "F:\\TestProjectZletano\file.txt");*//*
              string filePath = @"F:\TestProjectZletano\file.txt"; // استخدام المسار المطلق
              Dictionary<string, string> markets = new Dictionary<string, string>();

              if (System.IO.File.Exists(filePath)) // استخدام System.IO.File
              {
                  var content = System.IO.File.ReadAllText(filePath).Trim(); // استخدام System.IO.File
                  var marketEntries = content.Split(',');

                  foreach (var entry in marketEntries)
                  {
                      var parts = entry.Split(':');
                      if (parts.Length == 2)
                      {
                          markets[parts[0]] = parts[1]; // إضافة إلى القاموس
                      }
                  }
              }
              else
              {
                  throw new FileNotFoundException("الملف غير موجود.", filePath);
              }

              return markets; // إرجاع القاموس
          }

          // باقي الدوال كما هي...
      }*/

        // نموذج السوق
        public class Market
        {
            public string Name { get; set; }
            public string Code { get; set; }
        }

        public class City
        {
            public string Name { get; set; }
            public string Code { get; set; }
        }

        /*        private List<string> ReadMarketCodesFromFile()
                {
                    string filePath = Path.Combine(Directory.GetCurrentDirectory(), "file.txt"); // استخدام المسار الكامل
                    if (File.Exists(filePath))
                    {
                        var content = File.ReadAllText(filePath).Trim();
                        var marketEntries = content.Split(',');

                        // استخراج الأرقام فقط
                        return marketEntries.Select(entry => entry.Split(':')[1]).ToList();
                    }
                    throw new FileNotFoundException("الملف غير موجود.", filePath);
                }
        */
    }
}
