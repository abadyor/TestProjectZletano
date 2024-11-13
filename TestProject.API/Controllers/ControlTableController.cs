using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using TestProject.Core.Fetures.ControlTables.Commands.Models;
using TestProject.Core.Fetures.ControlTables.Qureries.Models;
using TestProject.Core.Fetures.ControlTables.Qureries.Response;
using TestProject.Infrustrucure.Data;
using TestProjectZletano.Core.Base;
namespace TestProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ControlTableController : ControllerBase
    {
        private readonly IWebHostEnvironment _environment;
        private readonly ApplicationDBContext _applicationDBContext;

        private readonly IMediator _mediator; // استخدام Mediator إذا كنت تستخدمه
        private readonly IConfiguration _configuration;


        public ControlTableController(IMediator mediator, IWebHostEnvironment environment, ApplicationDBContext applicationDBContext, IConfiguration configuration)
        {
            _mediator = mediator;
            _environment = environment;
            _applicationDBContext = applicationDBContext;
            _configuration = configuration;
        }




        [HttpGet("/Control/GetByVendorId/{vendorId}")]
        public async Task<IActionResult> GetByVendorId(int vendorId)
        {
            var query = new GetControlByVendorQuery { VendorId = vendorId };
            var response = await _mediator.Send(query);

            if (response.Succeeded)
            {
                return Ok(response.Data);
            }
            else
            {
                return NotFound(response.Message);
            }
        }


        /*    [HttpPut("/Control/UpdateImage")]
            public async Task<IActionResult> UpdateImage(string tableName, [FromBody] UpdateImageRequestCommand request)
            {
                if (string.IsNullOrEmpty(tableName) || request == null)
                {
                    return BadRequest("Table name and update data are required.");
                }

                try
                {
                    // تحقق من صحة البيانات
                    if (request.Price < 0 || request.Quantity < 0)
                    {
                        return BadRequest("Price and Quantity cannot be negative.");
                    }

                    // إعداد استعلام SQL للتحديث، مع إضافة الحقل Discription
                    var query = $"UPDATE {tableName} SET Price = @Price, Quantity = @Quantity, NameImage = @NameImage, Discription = @Discription WHERE Item = @Item";

                    // الاتصال بقاعدة البيانات
                    using (var connection = _applicationDBContext.Database.GetDbConnection())
                    {
                        await connection.OpenAsync();

                        using (var command = connection.CreateCommand())
                        {
                            command.CommandText = query;

                            // إضافة المعاملات إلى الاستعلام
                            command.Parameters.Add(new SqlParameter("@Price", request.Price));
                            command.Parameters.Add(new SqlParameter("@Quantity", request.Quantity));
                            command.Parameters.Add(new SqlParameter("@NameImage", request.ImageName));
                            command.Parameters.Add(new SqlParameter("@Discription", request.Description));
                            command.Parameters.Add(new SqlParameter("@Item", request.Item));

                            // تنفيذ التحديث
                            var rowsAffected = await command.ExecuteNonQueryAsync();

                            if (rowsAffected > 0)
                            {
                                return Ok("Update successful.");
                            }
                            else
                            {
                                return NotFound("Item not found.");
                            }
                        }
                    }
                }
                catch (SqlException sqlEx)
                {
                    // معالجة الأخطاء المتعلقة بقاعدة البيانات
                    return StatusCode(500, $"Database error: {sqlEx.Message}");
                }
                catch (Exception ex)
                {
                    // معالجة الأخطاء العامة
                    return StatusCode(500, $"Internal server error: {ex.Message}");
                }
            }*/

        /*   [HttpPost("/Control/UploadImage")]
           public async Task<IActionResult> UploadImage()
           {
               var file = HttpContext.Request.Form.Files.FirstOrDefault();
               if (file == null || file.Length == 0)
               {
                   return BadRequest("No file uploaded.");
               }

               try
               {
                   var tableName = *//* الحصول على اسم الجدول المناسب *//*;
                   var fileName = Path.GetFileName(file.FileName);
                   var uploadPath = Path.Combine("uploads", tableName, fileName);

                   // تأكد من أن الدليل موجود
                   if (!Directory.Exists(Path.Combine("uploads", tableName)))
                   {
                       Directory.CreateDirectory(Path.Combine("uploads", tableName));
                   }

                   using (var stream = new FileStream(uploadPath, FileMode.Create))
                   {
                       await file.CopyToAsync(stream);
                   }

                   // ارجع المسار إلى المستخدم
                   return Ok(uploadPath.TrimStart('/')); // تأكد من أن المسار يعود بشكل صحيح
               }
               catch (Exception ex)
               {
                   return StatusCode(500, $"Internal server error: {ex.Message}");
               }
           }*/

        /*  [HttpPost("/Control/UploadImage")]
          public async Task<IActionResult> UploadImage([FromForm] UpdateImageRequestCommand imageRecord)
          {
              var file = HttpContext.Request.Form.Files.FirstOrDefault();
              if (file == null || file.Length == 0)
              {
                  return BadRequest("No file uploaded.");
              }

              try
              {
                  var fileName = Path.GetFileName(file.FileName);
                  var uploadPath = Path.Combine("uploads", imageRecord.TableName, fileName);

                  // تأكد من أن الدليل موجود
                  if (!Directory.Exists(Path.Combine("uploads", imageRecord.TableName)))
                  {
                      Directory.CreateDirectory(Path.Combine("uploads", imageRecord.TableName));
                  }

                  using (var stream = new FileStream(uploadPath, FileMode.Create))
                  {
                      await file.CopyToAsync(stream);
                  }

                  // تحديث السجل في قاعدة البيانات
                  using (var connection = _applicationDBContext.Database.GetDbConnection())
                  {
                      await connection.OpenAsync();
                      using (var command = connection.CreateCommand())
                      {
                          command.CommandText = "UPDATE Images SET ImageName = @ImageName, Description = @Description, Price = @Price, Quantity = @Quantity WHERE Id = @Id";

                          command.Parameters.Add(new SqlParameter("@ImageName", fileName));
                          command.Parameters.Add(new SqlParameter("@Description", imageRecord.Description));
                          command.Parameters.Add(new SqlParameter("@Price", imageRecord.Price));
                          command.Parameters.Add(new SqlParameter("@Quantity", imageRecord.Quantity));
                          command.Parameters.Add(new SqlParameter("@Id", imageRecord.Id)); // تأكد من أن لديك حقل Id لتحديد السجل المراد تحديثه

                          var rowsAffected = await command.ExecuteNonQueryAsync();
                          if (rowsAffected == 0)
                          {
                              return NotFound("Image record not found.");
                          }
                      }
                  }

                  // ارجع المسار إلى المستخدم
                  return Ok(uploadPath.TrimStart('/')); // تأكد من أن المسار يعود بشكل صحيح
              }
              catch (Exception ex)
              {
                  return StatusCode(500, $"Internal server error: {ex.Message}");
              }
          }
  */

        /* [HttpPut("/Control/UpdateImage")]
         public async Task<IActionResult> UpdateImage([FromBody] UpdateImageRequestCommand request)
         {
             if (request == null || string.IsNullOrEmpty(request.TableName))
             {
                 return BadRequest("Table name and update data are required.");
             }

             try
             {
                 // تحقق من صحة البيانات
                 if (request.Price < 0 || request.Quantity < 0)
                 {
                     return BadRequest("Price and Quantity cannot be negative.");
                 }

                 // إعداد استعلام SQL للتحديث، مع إضافة الحقل Description
                 var query = $"UPDATE {request.TableName} SET Price = @Price, Quantity = @Quantity, NameImage = @NameImage, Discription = @Discription WHERE Item = @Item";

                 // الاتصال بقاعدة البيانات
                 using (var connection = _applicationDBContext.Database.GetDbConnection())
                 {
                     await connection.OpenAsync();

                     using (var command = connection.CreateCommand())
                     {
                         command.CommandText = query;

                         // إضافة المعاملات إلى الاستعلام
                         command.Parameters.Add(new SqlParameter("@Price", request.Price));
                         command.Parameters.Add(new SqlParameter("@Quantity", request.Quantity));
                         command.Parameters.Add(new SqlParameter("@NameImage", request.ImageName));
                         command.Parameters.Add(new SqlParameter("@Discription", request.Description));
                         command.Parameters.Add(new SqlParameter("@Item", request.Item));

                         // تنفيذ التحديث
                         var rowsAffected = await command.ExecuteNonQueryAsync();

                         if (rowsAffected > 0)
                         {
                             return Ok("Update successful.");
                         }
                         else
                         {
                             return NotFound("Item not found.");
                         }
                     }
                 }
             }
             catch (SqlException sqlEx)
             {
                 // معالجة الأخطاء المتعلقة بقاعدة البيانات
                 return StatusCode(500, $"Database error: {sqlEx.Message}");
             }
             catch (Exception ex)
             {
                 // معالجة الأخطاء العامة
                 return StatusCode(500, $"Internal server error: {ex.Message}");
             }
         }*/

        /*
                [HttpPut("/Control/UpdateImage")]
                public async Task<IActionResult> UpdateImage([FromQuery] string tableName, [FromBody] UpdateImageRequestCommand request)
                {
                    if (string.IsNullOrEmpty(tableName) || request == null)
                    {
                        return BadRequest("Table name and update data are required.");
                    }

                    try
                    {
                        if (request.Price < 0 || request.Quantity < 0)
                        {
                            return BadRequest("Price and Quantity cannot be negative.");
                        }

                        var query = $"UPDATE {tableName} SET Price = @Price, Quantity = @Quantity, NameImage = @NameImage, Discription = @Discription WHERE Item = @Item";

                        using (var connection = _applicationDBContext.Database.GetDbConnection())
                        {
                            await connection.OpenAsync();

                            using (var command = connection.CreateCommand())
                            {
                                command.CommandText = query;
                                command.Parameters.Add(new SqlParameter("@Price", request.Price));
                                command.Parameters.Add(new SqlParameter("@Quantity", request.Quantity));
                                command.Parameters.Add(new SqlParameter("@NameImage", request.ImageName));
                                command.Parameters.Add(new SqlParameter("@Discription", request.Description));
                                command.Parameters.Add(new SqlParameter("@Item", request.Item));

                                var rowsAffected = await command.ExecuteNonQueryAsync();

                                if (rowsAffected > 0)
                                {
                                    return Ok("Update successful.");
                                }
                                else
                                {
                                    return NotFound("Item not found.");
                                }
                            }
                        }
                    }
                    catch (SqlException sqlEx)
                    {
                        return StatusCode(500, $"Database error: {sqlEx.Message}");
                    }
                    catch (Exception ex)
                    {
                        return StatusCode(500, $"Internal server error: {ex.Message}");
                    }
                }*/

        /*[HttpPut("/Control/UpdateImage")]
        public async Task<IActionResult> UpdateImage(string tableName, [FromBody] UpdateImageRequestCommand request)
        {
            if (string.IsNullOrEmpty(tableName) || request == null)
            {
                return BadRequest("Table name and update data are required.");
            }

            try
            {
                // تحقق من صحة البيانات
                if (request.Price < 0 || request.Quantity < 0)
                {
                    return BadRequest("Price and Quantity cannot be negative.");
                }

                // إعداد استعلام SQL للتحديث، مع إضافة الحقل Discription
                var query = $"UPDATE {tableName} SET Price = @Price, Quantity = @Quantity, Discription = @Discription WHERE Item = @Item";

                // الاتصال بقاعدة البيانات
                using (var connection = _applicationDBContext.Database.GetDbConnection())
                {
                    await connection.OpenAsync();

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = query;

                        // إضافة المعاملات إلى الاستعلام
                        command.Parameters.Add(new SqlParameter("@Price", request.Price));
                        command.Parameters.Add(new SqlParameter("@Quantity", request.Quantity));
                        command.Parameters.Add(new SqlParameter("@Discription", request.Description));
                        command.Parameters.Add(new SqlParameter("@Item", request.Item));

                        // تنفيذ التحديث
                        var rowsAffected = await command.ExecuteNonQueryAsync();

                        if (rowsAffected > 0)
                        {
                            return Ok("Update successful.");
                        }
                        else
                        {
                            return NotFound("Item not found.");
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                // معالجة الأخطاء المتعلقة بقاعدة البيانات
                return StatusCode(500, $"Database error: {sqlEx.Message}");
            }
            catch (Exception ex)
            {
                // معالجة الأخطاء العامة
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }*/

        /*  [HttpPut("/Control/UpdateImage")]
          public async Task<IActionResult> UpdateImage(string tableName, [FromBody] UpdateImageRequestCommand request)
          {
              if (string.IsNullOrEmpty(tableName) || request == null)
              {
                  return BadRequest("Table name and update data are required.");
              }

              try
              {
                  // تحقق من صحة البيانات
                  if (request.Price < 0 || request.Quantity < 0)
                  {
                      return BadRequest("Price and Quantity cannot be negative.");
                  }

                  // إعداد استعلام SQL للتحديث، مع إضافة الحقل Discription واسم الصورة
                  var query = $"UPDATE {tableName} SET Price = @Price, Quantity = @Quantity, Discription = @Discription, NameImage = @ImageName WHERE Item = @Item";

                  // الاتصال بقاعدة البيانات
                  using (var connection = _applicationDBContext.Database.GetDbConnection())
                  {
                      await connection.OpenAsync();

                      using (var command = connection.CreateCommand())
                      {
                          command.CommandText = query;

                          // إضافة المعاملات إلى الاستعلام
                          command.Parameters.Add(new SqlParameter("@Price", request.Price));
                          command.Parameters.Add(new SqlParameter("@Quantity", request.Quantity));
                          command.Parameters.Add(new SqlParameter("@Discription", request.Description));
                          command.Parameters.Add(new SqlParameter("@Item", request.Item));
                          command.Parameters.Add(new SqlParameter("@ImageName", request.ImageName)); // إضافة اسم الصورة

                          // تنفيذ التحديث
                          var rowsAffected = await command.ExecuteNonQueryAsync();

                          if (rowsAffected > 0)
                          {
                              return Ok("Update successful.");
                          }
                          else
                          {
                              return NotFound("Item not found.");
                          }
                      }
                  }
              }
              catch (SqlException sqlEx)
              {
                  // معالجة الأخطاء المتعلقة بقاعدة البيانات
                  return StatusCode(500, $"Database error: {sqlEx.Message}");
              }
              catch (Exception ex)
              {
                  // معالجة الأخطاء العامة
                  return StatusCode(500, $"Internal server error: {ex.Message}");
              }
          }*/



        /*   [HttpPut("/Control/UpdateImage")]
           public async Task<IActionResult> UpdateImage(string tableName, [FromBody] UpdateImageRequestCommand request)
           {
               if (string.IsNullOrEmpty(tableName) || request == null)
               {
                   return BadRequest("Table name and update data are required.");
               }

               try
               {
                   // تحقق من صحة البيانات
                   if (request.Price < 0 || request.Quantity < 0)
                   {
                       return BadRequest("Price and Quantity cannot be negative.");
                   }

                   // إعداد استعلام SQL للتحديث، مع إضافة الحقل Discription واسم الصورة
                   var query = $"UPDATE {tableName} SET Price = @Price, Quantity = @Quantity, Discription = @Discription, NameImage = @ImageName WHERE Item = @Item";

                   // الاتصال بقاعدة البيانات
                   using (var connection = _applicationDBContext.Database.GetDbConnection())
                   {
                       await connection.OpenAsync();

                       using (var command = connection.CreateCommand())
                       {
                           command.CommandText = query;

                           // إضافة المعاملات إلى الاستعلام
                           command.Parameters.Add(new SqlParameter("@Price", request.Price));
                           command.Parameters.Add(new SqlParameter("@Quantity", request.Quantity));
                           command.Parameters.Add(new SqlParameter("@Discription", request.Description));
                           command.Parameters.Add(new SqlParameter("@Item", request.Item));
                           command.Parameters.Add(new SqlParameter("@ImageName", request.ImageName)); // إضافة اسم الصورة

                           // تنفيذ التحديث
                           var rowsAffected = await command.ExecuteNonQueryAsync();

                           if (rowsAffected > 0)
                           {
                               return Ok("Update successful.");
                           }
                           else
                           {
                               return NotFound("Item not found.");
                           }
                       }
                   }
               }
               catch (SqlException sqlEx)
               {
                   // معالجة الأخطاء المتعلقة بقاعدة البيانات
                   return StatusCode(500, $"Database error: {sqlEx.Message}");
               }
               catch (Exception ex)
               {
                   // معالجة الأخطاء العامة
                   return StatusCode(500, $"Internal server error: {ex.Message}");
               }
           }*/


        [HttpPut("/Control/UpdateImage")]
        public async Task<IActionResult> UpdateImage([FromBody] UpdateImageRequestCommand request)
        {
            if (string.IsNullOrEmpty(request.TableName) || request == null)
            {
                return BadRequest("Table name and update data are required.");
            }

            try
            {
                // تحقق من صحة البيانات
                if (request.Price < 0 || request.Quantity < 0)
                {
                    return BadRequest("Price and Quantity cannot be negative.");
                }

                // إعداد المسار الأساسي
                var uploadBasePath = "F:\\TestProjectZletano\\TestProjectZletano\\TestProject.API\\wwwroot"; // المسار الأساسي

                // استخراج اسم الصورة من oldImageName
                var oldImageName = Path.GetFileName(request.OldImageName); // سيأخذ فقط اسم الملف بدون المسار
                var oldImagePath = Path.Combine(uploadBasePath, "uploads", request.TableName, oldImageName); // بناء المسار للصورة القديمة

                // حذف الصورة القديمة إذا كانت موجودة
                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }
                else
                {
                    Console.WriteLine($"Old image not found: {oldImagePath}"); // اطبع رسالة إذا لم يتم العثور على الصورة
                }

                // إعداد استعلام SQL للتحديث، مع إضافة الحقل Description واسم الصورة
                var query = $"UPDATE {request.TableName} SET Price = @Price, Quantity = @Quantity, Discription = @Description, NameImage = @ImageName WHERE Item = @Item";

                // الاتصال بقاعدة البيانات
                using (var connection = _applicationDBContext.Database.GetDbConnection())
                {
                    await connection.OpenAsync();

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = query;

                        // إضافة المعاملات إلى الاستعلام
                        command.Parameters.Add(new SqlParameter("@Price", request.Price));
                        command.Parameters.Add(new SqlParameter("@Quantity", request.Quantity));
                        command.Parameters.Add(new SqlParameter("@Description", request.Description));
                        command.Parameters.Add(new SqlParameter("@Item", request.Item));
                        command.Parameters.Add(new SqlParameter("@ImageName", request.ImageName)); // إضافة اسم الصورة

                        // تنفيذ التحديث
                        var rowsAffected = await command.ExecuteNonQueryAsync();

                        if (rowsAffected > 0)
                        {
                            return Ok("Update successful.");
                        }
                        else
                        {
                            return NotFound("Item not found.");
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                // معالجة الأخطاء المتعلقة بقاعدة البيانات
                return StatusCode(500, $"Database error: {sqlEx.Message}");
            }
            catch (Exception ex)
            {
                // معالجة الأخطاء العامة
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        /*
         * هدا الكود شغال 1000000000000000000000000000000000000000         * 
         * [HttpPut("/Control/UpdateImage")]
        public async Task<IActionResult> UpdateImage([FromBody] UpdateImageRequestCommand request)
        {
            if (string.IsNullOrEmpty(request.TableName) || request == null)
            {
                return BadRequest("Table name and update data are required.");
            }

            try
            {
                // تحقق من صحة البيانات
                if (request.Price < 0 || request.Quantity < 0)
                {
                    return BadRequest("Price and Quantity cannot be negative.");
                }

                // إعداد استعلام SQL للتحديث، مع إضافة الحقل Description واسم الصورة
                var query = $"UPDATE {request.TableName} SET Price = @Price, Quantity = @Quantity, Discription = @Description, NameImage = @ImageName WHERE Item = @Item";

                // الاتصال بقاعدة البيانات
                using (var connection = _applicationDBContext.Database.GetDbConnection())
                {
                    await connection.OpenAsync();

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = query;

                        // إضافة المعاملات إلى الاستعلام
                        command.Parameters.Add(new SqlParameter("@Price", request.Price));
                        command.Parameters.Add(new SqlParameter("@Quantity", request.Quantity));
                        command.Parameters.Add(new SqlParameter("@Description", request.Description));
                        command.Parameters.Add(new SqlParameter("@Item", request.Item));
                        command.Parameters.Add(new SqlParameter("@ImageName", request.ImageName)); // إضافة اسم الصورة

                        // تنفيذ التحديث
                        var rowsAffected = await command.ExecuteNonQueryAsync();

                        if (rowsAffected > 0)
                        {
                            return Ok("Update successful.");
                        }
                        else
                        {
                            return NotFound("Item not found.");
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                // معالجة الأخطاء المتعلقة بقاعدة البيانات
                return StatusCode(500, $"Database error: {sqlEx.Message}");
            }
            catch (Exception ex)
            {
                // معالجة الأخطاء العامة
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }*/

        /*  [HttpDelete("/Control/DeleteImage")]
          public async Task<IActionResult> DeleteImage([FromQuery] string fileName, [FromQuery] string folder, [FromQuery] string tableName, [FromQuery] string item)
          {
              if (string.IsNullOrEmpty(fileName) || string.IsNullOrEmpty(folder) || string.IsNullOrEmpty(tableName) || string.IsNullOrEmpty(item))
              {
                  return BadRequest("File name, folder, table name, and item are required.");
              }

              try
              {
                  // المسار الأساسي لمجلد "uploads" على الخادم
                  var uploadsBasePath = Path.Combine(Directory.GetCurrentDirectory(), "uploads");

                  // بناء المسار الكامل للملف
                  var filePath = Path.Combine(uploadsBasePath, folder, fileName);
                  Console.WriteLine($"Full file path: {filePath}"); // طباعة المسار للتأكد

                  // التحقق من وجود الملف
                  if (System.IO.File.Exists(filePath))
                  {
                      // حذف الملف
                      System.IO.File.Delete(filePath);

                      // حذف مسار الصورة من قاعدة البيانات
                      using (var connection = _applicationDBContext.Database.GetDbConnection())
                      {
                          await connection.OpenAsync();

                          var query = $"UPDATE {tableName} SET NameImage = NULL WHERE Item = @Item";
                          using (var command = connection.CreateCommand())
                          {
                              command.CommandText = query;
                              command.Parameters.Add(new SqlParameter("@Item", item));

                              // تنفيذ التحديث
                              var rowsAffected = await command.ExecuteNonQueryAsync();

                              if (rowsAffected > 0)
                              {
                                  return Ok("Image deleted successfully and path removed from database.");
                              }
                              else
                              {
                                  return NotFound("Image path not found in database.");
                              }
                          }
                      }
                  }
                  else
                  {
                      return NotFound("Image not found in uploads.");
                  }
              }
              catch (Exception ex)
              {
                  return StatusCode(500, $"Internal server error: {ex.Message}");
              }
          }*/

        /*   [HttpDelete("/Control/DeleteImage")]
           public async Task<IActionResult> DeleteImage([FromQuery] string fileName, [FromQuery] string folder, [FromQuery] string tableName, [FromQuery] string item)
           {
               // تحقق من أن القيم ليست فارغة
               if (string.IsNullOrEmpty(fileName) || string.IsNullOrEmpty(folder) || string.IsNullOrEmpty(tableName) || string.IsNullOrEmpty(item))
               {
                   return BadRequest("File name, folder, table name, and item are required.");
               }

               try
               {
                   // قم بإزالة "/uploads" من اسم الملف عند بناء المسار
                   var actualFileName = Path.GetFileName(fileName); // الحصول على اسم الملف فقط
                   var uploadBasePath = Path.Combine("F:\\TestProjectZletano\\TestProjectZletano\\TestProject.API\\uploads", folder);
                   var filePath = Path.Combine(uploadBasePath, actualFileName);

                   // تحقق من وجود الملف
                   if (System.IO.File.Exists(filePath))
                   {
                       // حذف الملف
                       System.IO.File.Delete(filePath);

                       // حذف مسار الصورة من قاعدة البيانات
                       using (var connection = _applicationDBContext.Database.GetDbConnection())
                       {
                           await connection.OpenAsync();

                           var query = $"UPDATE {tableName} SET NameImage = NULL WHERE Item = @Item";
                           using (var command = connection.CreateCommand())
                           {
                               command.CommandText = query;
                               command.Parameters.Add(new SqlParameter("@Item", item));

                               // تنفيذ التحديث
                               var rowsAffected = await command.ExecuteNonQueryAsync();

                               if (rowsAffected > 0)
                               {
                                   return Ok("Image deleted successfully and path removed from database.");
                               }
                               else
                               {
                                   return NotFound("Image path not found in database.");
                               }
                           }
                       }
                   }
                   else
                   {
                       return NotFound("Image not found in uploads.");
                   }
               }
               catch (Exception ex)
               {
                   return StatusCode(500, $"Internal server error: {ex.Message}");
               }
           }*/

        [HttpDelete("/Control/DeleteImage")]
        public async Task<IActionResult> DeleteImage(string fileName, string folder, string item)
        {
            if (string.IsNullOrEmpty(fileName) || string.IsNullOrEmpty(folder) || string.IsNullOrEmpty(item))
            {
                return BadRequest("File name, folder, and item are required.");
            }

            // بناء مسار الصورة الكامل
            var uploadBasePath = "F:\\TestProjectZletano\\TestProjectZletano\\TestProject.API\\wwwroot"; // المسار الأساسي
            var filePath = Path.Combine(uploadBasePath, "uploads", folder, Path.GetFileName(fileName)); // بناء المسار الكامل

            // تحقق من وجود الملف
            if (!System.IO.File.Exists(filePath))
            {
                return NotFound($"Image not found at {filePath}.");
            }

            try
            {
                // حذف الملف من النظام
                System.IO.File.Delete(filePath);

                // حذف مسار الصورة من قاعدة البيانات
                using (var connection = _applicationDBContext.Database.GetDbConnection())
                {
                    await connection.OpenAsync();

                    var query = $"UPDATE {folder} SET NameImage = NULL WHERE Item = @Item"; // استخدام folder بدلاً من tableName
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = query;
                        command.Parameters.Add(new SqlParameter("@Item", item));

                        // تنفيذ التحديث
                        var rowsAffected = await command.ExecuteNonQueryAsync();

                        if (rowsAffected > 0)
                        {
                            return Ok("Image deleted successfully and path removed from database.");
                        }
                        else
                        {
                            return NotFound("Item not found in database.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }






        /*  [HttpDelete("/Control/DeleteImage")]
          public IActionResult DeleteImage([FromQuery] string fileName, [FromQuery] string folder)
          {
              if (string.IsNullOrEmpty(fileName) || string.IsNullOrEmpty(folder))
              {
                  return BadRequest("File name and folder are required.");
              }

              try
              {
                  // المسار الكامل للملف
                  var filePath = Path.Combine("uploads", folder, fileName);

                  // التحقق من وجود الملف
                  if (System.IO.File.Exists(filePath))
                  {
                      // حذف الملف
                      System.IO.File.Delete(filePath);
                      return Ok("Image deleted successfully.");
                  }
                  else
                  {
                      return NotFound("Image not found.");
                  }
              }
              catch (Exception ex)
              {
                  return StatusCode(500, $"Internal server error: {ex.Message}");
              }
          }*/


        [HttpGet("/Control/GetAllShopsWithImages")]
        public async Task<IActionResult> GetAllShopsWithImages()
        {
            var baseImageUrl = _configuration["BaseImageUrl"];
            var shopsWithImages = new List<object>();

            try
            {
                var shops = await _applicationDBContext.controlTables.ToListAsync();
                bool skipFirst = true;

                foreach (var shop in shops)
                {
                    if (skipFirst)
                    {
                        skipFirst = false;
                        continue;
                    }

                    string tableName = "s_" + shop.M_Code + shop.city + shop.Last_sore;
                    var query = $"SELECT Item, Discription, Price, Quantity, NameImage FROM {tableName}";

                    // استخدام اتصال جديد بدلاً من الاتصال داخل DbContext
                    using (var connection = new SqlConnection(_applicationDBContext.Database.GetDbConnection().ConnectionString))
                    {
                        await connection.OpenAsync();

                        using (var command = connection.CreateCommand())
                        {
                            command.CommandText = query;

                            using (var result = await command.ExecuteReaderAsync())
                            {
                                var imageDataList = new List<GetImageResponse>();

                                while (await result.ReadAsync())
                                {
                                    var imageData = new GetImageResponse
                                    {
                                        Item = result["Item"].ToString(),
                                        Description = result["Discription"].ToString(),
                                        Price = result["Price"] != DBNull.Value ? Convert.ToDecimal(result["Price"]) : 0,
                                        Quantity = result["Quantity"] != DBNull.Value ? Convert.ToInt32(result["Quantity"]) : 0,
                                        ImageName = $"{baseImageUrl}/{tableName}/{tableName}_{result["NameImage"]}"
                                    };
                                    imageDataList.Add(imageData);
                                }

                                shopsWithImages.Add(new
                                {
                                    tableName = tableName,
                                    ShopName = shop.shopeName,

                                    Address = shop.Address,
                                    NerestPoint = shop.NerestPoint,
                                    Images = imageDataList
                                });
                            }
                        }
                    }
                }

                return Ok(shopsWithImages);
            }
            catch (SqlException sqlEx)
            {
                return StatusCode(500, $"Database error: {sqlEx.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [HttpGet("/Control/SearchCustomer")]
        public async Task<IActionResult> SearchCustomer(int Search)
        {
            var baseImageUrl = _configuration["BaseImageUrl"];
            var shopsWithImages = new List<object>();

            try
            {
                var shops = await _applicationDBContext.controlTables.ToListAsync();
                bool skipFirst = true;

                foreach (var shop in shops)
                {
                    if (skipFirst)
                    {
                        skipFirst = false;
                        continue;
                    }

                    string tableName = "s_" + shop.M_Code + shop.city + shop.Last_sore;
                    var query = $"SELECT Id, Item, Discription, Price, Quantity, NameImage FROM {tableName} WHERE Item={Search}";

                    // استخدام اتصال جديد بدلاً من الاتصال داخل DbContext
                    using (var connection = new SqlConnection(_applicationDBContext.Database.GetDbConnection().ConnectionString))
                    {
                        await connection.OpenAsync();

                        using (var command = connection.CreateCommand())
                        {
                            command.CommandText = query;

                            using (var result = await command.ExecuteReaderAsync())
                            {
                                var imageDataList = new List<GetImageResponse>();

                                while (await result.ReadAsync())
                                {
                                    var imageData = new GetImageResponse
                                    {
                                        Id = result["Id"] != DBNull.Value ? Convert.ToInt32(result["Id"]) : 0,
                                        Item = result["Item"].ToString(),
                                        Description = result["Discription"].ToString(),
                                        Price = result["Price"] != DBNull.Value ? Convert.ToDecimal(result["Price"]) : 0,
                                        Quantity = result["Quantity"] != DBNull.Value ? Convert.ToInt32(result["Quantity"]) : 0,
                                        ImageName = $"{baseImageUrl}/{tableName}/{tableName}_{result["NameImage"]}"
                                    };
                                    imageDataList.Add(imageData);
                                }

                                shopsWithImages.Add(new
                                {
                                    tableName = tableName,
                                    ShopName = shop.shopeName,

                                    Address = shop.Address,
                                    NerestPoint = shop.NerestPoint,
                                    Images = imageDataList
                                });
                            }
                        }
                    }
                }

                return Ok(shopsWithImages);
            }
            catch (SqlException sqlEx)
            {
                return StatusCode(500, $"Database error: {sqlEx.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }



        [HttpGet("/Control/GetImages")]
        public async Task<IActionResult> GetImages(string tableName)
        {
            if (string.IsNullOrEmpty(tableName))
            {
                return BadRequest("Table name is required.");
            }

            try
            {
                // Prepare the SQL query to select all records from the dynamic table
                var query = $"SELECT Id, Item, Discription, Price, Quantity, NameImage FROM {tableName}";

                // Establish a connection to the database
                using (var connection = _applicationDBContext.Database.GetDbConnection())
                {
                    await connection.OpenAsync(); // Open the connection

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = query;

                        using (var result = await command.ExecuteReaderAsync())
                        {
                            var imageDataList = new List<GetImageResponse>();

                            while (await result.ReadAsync())
                            {
                                // Read data from the result set
                                var imageData = new GetImageResponse
                                {
                                    Id = result["Id"] != DBNull.Value ? Convert.ToInt32(result["Id"]) : 0,
                                    Item = result["Item"].ToString(),
                                    Description = result["Discription"].ToString(),
                                    Price = result["Price"] != DBNull.Value ? Convert.ToDecimal(result["Price"]) : 0,
                                    Quantity = result["Quantity"] != DBNull.Value ? Convert.ToInt32(result["Quantity"]) : 0,
                                    ImageName = $"/uploads/{tableName}/{tableName}_{result["NameImage"]}"
                                };
                                imageDataList.Add(imageData);
                            }
                            return Ok(imageDataList); // Return the list of images
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                // Log SQL errors or return a specific message
                return StatusCode(500, $"Database error: {sqlEx.Message}");
            }
            catch (Exception ex)
            {
                // Log general errors
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        /*
                [HttpGet("/Control/GetImages")]
                public async Task<IActionResult> GetImages(string tableName)
                {
                    if (string.IsNullOrEmpty(tableName))
                    {
                        return BadRequest("Table name is required.");
                    }

                    try
                    {
                        var query = $"SELECT Item, Discription, Price, Quantity, ImageName FROM {tableName}";
                        var items = await _applicationDBContext.Set<GetImageResponse>()
                                                    .FromSqlRaw(query)
                                                    .ToListAsync();

                        if (items == null || !items.Any())
                        {
                            return NotFound("No data found for the specified table.");
                        }

                        var imageDataList = items.Select(item => new GetImageResponse
                        {
                            Item = item.Item,
                            Description = item.Description,
                            Price = item.Price,
                            Quantity = item.Quantity,
                            ImageName = $"/uploads/{tableName}/{item.ImageName}"
                        }).ToList();

                        return Ok(imageDataList);
                    }
                    catch (Exception ex)
                    {
                        // سجل الخطأ وأعد رسالة توضيحية للعميل
                        return StatusCode(500, $"Internal server error: {ex.Message}");
                    }
                }
        */





        /*  [HttpGet("GetImages")]
          public IActionResult GetImages(string tableName)
          {
              if (string.IsNullOrEmpty(tableName))
              {
                  return BadRequest("Table name is required.");
              }

              // مسار المجلد للجدول المحدد
              var folderPath = Path.Combine(_environment.WebRootPath, "uploads", tableName);

              if (!Directory.Exists(folderPath))
              {
                  return NotFound("No images found for the specified table.");
              }

              // قراءة جميع الصور وبياناتها من قاعدة البيانات أو ملف معين
              var imageDataList = new List<ImageDataModel>();

              // إضافة الصور الموجودة في المجلد
              var imageFiles = Directory.GetFiles(folderPath);

              foreach (var imagePath in imageFiles)
              {
                  var imageName = Path.GetFileName(imagePath);

                  // تعيين بيانات افتراضية أو جلبها من قاعدة البيانات
                  imageDataList.Add(new ImageDataModel
                  {
                      Item = "اسم العنصر",
                      Description = "وصف العنصر",
                      Price = 100.0m,
                      Quantity = 10,
                      ImageUrl = $"/uploads/{tableName}/{imageName}" // تعيين المسار النسبي للصورة
                  });
              }

              return Ok(imageDataList);
          }*/

        /*[HttpGet("/Control/GetImages")]
        public IActionResult GetImages(string tableName)
        {
            if (string.IsNullOrEmpty(tableName))
            {
                return BadRequest("Table name is required.");
            }

            // مسار المجلد للجدول المحدد
            var folderPath = Path.Combine(_environment.WebRootPath, "uploads", tableName);

            if (!Directory.Exists(folderPath))
            {
                return NotFound("No images found for the specified table.");
            }

            // إنشاء قائمة لنتائج الصور باستخدام `GetImageResponse`
            var imageDataList = new List<GetImageResponse>();

            // إضافة الصور الموجودة في المجلد
            var imageFiles = Directory.GetFiles(folderPath);

            foreach (var imagePath in imageFiles)
            {
                var imageName = Path.GetFileName(imagePath);

                // تعيين بيانات افتراضية أو جلبها من قاعدة البيانات
                imageDataList.Add(new GetImageResponse
                {
                    Item = "اسم العنصر", // تخصيص قيمة مناسبة هنا
                    Description = "وصف العنصر", // تخصيص قيمة مناسبة هنا
                    Price = 000, // تخصيص قيمة مناسبة هنا
                    Quantity = 00, // تخصيص قيمة مناسبة هنا
                    ImageUrl = $"/uploads/{tableName}/{imageName}" // تعيين المسار النسبي للصورة
                });
            }

            return Ok(imageDataList);
        }*/

        [HttpPost("/Control/AddDynamic")]
        public async Task<IActionResult> AddDynamic([FromBody] AddDynamicTableCommand addDynamicTableCommand)
        {
            if (addDynamicTableCommand == null)
                return BadRequest("Invalid request.");

            var result = await _mediator.Send(addDynamicTableCommand);

            if (result.Succeeded)
                return Ok(result.Data);

            return BadRequest(result.Errors);
        }




        [HttpPost("/Control/UploadImage")]
        public async Task<IActionResult> UploadImage([FromForm] IFormFile file, [FromQuery] string folder)
        {
            if (file == null || file.Length == 0)
                return BadRequest("Invalid file.");

            // تحديد المسار للمجلد الذي سيتم حفظ الصورة فيه
            var uploadsPath = Path.Combine("wwwroot", "uploads", folder);
            if (!Directory.Exists(uploadsPath))
            {
                Directory.CreateDirectory(uploadsPath);
            }

            // اسم الصورة
            var fileName = $"{folder}_{file.FileName}";
            var filePath = Path.Combine(uploadsPath, fileName);

            // تحميل الصورة وضغطها
            using (var image = await Image.LoadAsync(file.OpenReadStream()))
            {
                // تطبيق الضغط عن طريق تقليل الجودة
                var encoder = new JpegEncoder { Quality = 1 }; // يمكنك تعديل نسبة الجودة

                // حفظ الصورة المضغوطة في المسار المحدد
                await image.SaveAsync(filePath, encoder);
            }

            return Ok(new { fileName = file.FileName });
        }


        /* 
         * هدا الكود شغال 1000000000000000000000000000000
         * 
         * [HttpPost("/Control/UploadImage")]
          public async Task<IActionResult> UploadImage([FromForm] IFormFile file, [FromQuery] string folder)
          {
              if (file == null || file.Length == 0)
                  return BadRequest("Invalid file.");

              // تحديد المسار للمجلد الذي سيتم حفظ الصورة فيه
              var uploadsPath = Path.Combine("wwwroot", "uploads", folder);
              if (!Directory.Exists(uploadsPath))
              {
                  Directory.CreateDirectory(uploadsPath);
              }

              // اسم الصورة
              var filePath = Path.Combine(uploadsPath, $"{folder}_{file.FileName}");

              using (var stream = new FileStream(filePath, FileMode.Create))
              {
                  await file.CopyToAsync(stream);
              }

              return Ok(new { fileName = file.FileName });
          }*/


        [HttpPost("/ControlTable/Create")]
        public async Task<ActionResult<Response<string>>> AddControlTable([FromBody] AddControlCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                return BadRequest("Invalid request.");
            }

            var result = await _mediator.Send(request, cancellationToken);

            if (result.Succeeded)
            {
                return Ok(result); // 200 OK مع الرسالة
            }
            else
            {
                return BadRequest(result); // 400 Bad Request مع الرسالة
            }
        }
    }
}
