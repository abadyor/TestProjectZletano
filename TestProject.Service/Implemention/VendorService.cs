using System.Net;
using System.Net.Mail;
using TestProject.Data.Entity;
using TestProject.Infrustrucure.Abstract;
using TestProject.Service.Abstract;

namespace TestProject.Service.Implemention
{
    public class VendorService : IVendorService
    {
        private readonly IVendorRepository _vendorRepository;
        #region Field

        public string codegeny = "";
        #endregion

        #region Constractor
        public VendorService(IVendorRepository vendorRepository)
        {
            _vendorRepository = vendorRepository;
        }

        #endregion


        public async Task<string> updateonefild(string code)
        {
            var updateone = await _vendorRepository.updateonefild(code);
            if (updateone == "The Value Updated")
            {
                return "The Value Updated";
            }
            else
            {
                return "The Value No Updated";
            }
        }

        public async Task<string> GetLastControlLastCode()
        {
            var lastStoreCode = await _vendorRepository.GetLastControlLastCode();

            // محاولة تحويل lastStoreCode إلى عدد صحيح
            if (int.TryParse(lastStoreCode, out int lastCode))
            {
                lastCode += 1; // إضافة 1 إلى القيمة
                return lastCode.ToString("D3"); // إرجاع القيمة كنص مع 3 أرقام (مثل 001، 002، إلخ.)
            }
            else
            {
                return "000"; // إرجاع قيمة افتراضية مع أصفار إذا لم تكن القيمة صالحة
            }
        }

        public async Task<string> GetControlFirstId()
        {
            var marketCode = await _vendorRepository.GetControlFirstId();
            if (marketCode != "No Data")
            {
                return marketCode;
            }
            else
            {
                return "No Data";
            }
        }



        /* public async Task<int> GetLastControlLastCode()
         {
             var lastStoreCode = await _vendorRepository.GetLastControlLastCode();

             // محاولة تحويل lastStoreCode إلى عدد صحيح
             if (int.TryParse(lastStoreCode, out int lastCode))
             {
                 lastCode += 1; // إضافة 1 إلى القيمة
                 lastStoreCode = lastCode.ToString("D3");
                 return Convert.ToInt32(lastStoreCode);// تحويل القيمة إلى نص مع 3 أرقام (مثل 001، 002، إلخ.)
             }
             else
             {
                 return 0; // معالجة حالة عدم صلاحية lastStoreCode
             }
         }*/

        /*   public async Task<string> GenerateCodeAndSendEmail(string recipientEmail)
           {
               // توليد كود عشوائي
               string code = GenerateRandomCode();

               // إرسال البريد الإلكتروني
               await SendEmailAsync(recipientEmail, code);

               return code; // يمكن إرجاع الكود إذا لزم الأمر
           }

           private string GenerateRandomCode()
           {
               // توليد كود عشوائي مكون من 8 أرقام (يمكنك تعديله حسب الحاجة)
               Random random = new Random();
               return random.Next(10000000, 99999999).ToString("D8"); // كود مكون من 8 أرقام
           }

           private async Task SendEmailAsync(string recipientEmail, string code)
           {
               // إعدادات البريد الإلكتروني
               var fromAddress = new MailAddress("your-email@example.com", "Your Name");
               var toAddress = new MailAddress(recipientEmail);
               const string fromPassword = "your-email-password";
               const string subject = "Your Verification Code";
               string body = $"Your verification code is: {code}";

               using (var smtp = new SmtpClient
               {
                   Host = "smtp.example.com", // SMTP server address
                   Port = 587, // أو 465 حسب مزود الخدمة
                   EnableSsl = true,
                   DeliveryMethod = SmtpDeliveryMethod.Network,
                   UseDefaultCredentials = false,
                   Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
               })
               {
                   using (var message = new MailMessage(fromAddress, toAddress)
                   {
                       Subject = subject,
                       Body = body,
                       IsBodyHtml = false // إذا كنت تريد إرسال HTML، ضع true
                   })
                   {
                       await smtp.SendMailAsync(message);
                   }
               }
           }*/

        public async Task<string> GetCodeGeny()
        {
            if (codegeny != "")
            {
                var code = codegeny;
                return code;
            }
            else
            {
                return "NONONO";
            }


        }
        public async Task<string> GenerateCodeAndSendEmail(string recipientEmail)
        {
            // توليد كود عشوائي
            string code = GenerateRandomCode();

            // إرسال البريد الإلكتروني
            await SendEmailAsync(recipientEmail, code);
            codegeny = code;

            return "OkSend"; // يمكن إرجاع الكود إذا لزم الأمر
        }

        public async Task<string> GenerateCodeAndSendEmailAndGetCode(string recipientEmail)
        {
            // توليد كود عشوائي
            string code = GenerateRandomCode();

            // إرسال البريد الإلكتروني
            await SendEmailAsync(recipientEmail, code);


            return code; // يمكن إرجاع الكود إذا لزم الأمر
        }

        private string GenerateRandomCode()
        {
            // توليد كود عشوائي مكون من 8 أرقام (يمكنك تعديله حسب الحاجة)
            Random random = new Random();
            return random.Next(10000000, 99999999).ToString("D8"); // كود مكون من 8 أرقام
        }

        public async Task<string> CheckUserAndSendEmail(string username, string password)
        {
            var x = await _vendorRepository.CheckUserAndSendEmail(username, password);
            if (x != null)
            {
                var w = await GenerateCodeAndSendEmail(x.EmailAddress);
                if (w == "OkSend")
                {
                    return "OkOkOk";
                }
                else
                {
                    return "NoNoNo";
                }
            }
            else
            {
                return "No Login";
            }
        }

        /*   private async Task SendEmailAsync(string recipientEmail, string code)
           {
               var fromAddress = new MailAddress("your-email@example.com", "Your Name");
               var toAddress = new MailAddress(recipientEmail);
               const string subject = "Your Verification Code";
               string body = $"Your verification code is: {code}";

               using (var smtp = new SmtpClient
               {
                   Host = "localhost:7252", // استخدم localhost
                   Port = 25, // المنفذ الافتراضي لـ SMTP محلي
                   EnableSsl = false, // تأكد من أن SSL غير مفعل
                   UseDefaultCredentials = true // استخدم الاعتمادات الافتراضية
               })
               {
                   using (var message = new MailMessage(fromAddress, toAddress)
                   {
                       Subject = subject,
                       Body = body,
                       IsBodyHtml = false
                   })
                   {
                       await smtp.SendMailAsync(message);
                   }
               }
           }*/


        public async Task SendEmailAsyncAnotherFunction(string title, string body)
        {
            /*   var fromAddress = new MailAddress("yourEmail@gmail.com", "Your Name");
               var toAddress = new MailAddress(recipientEmail);
               const string subject = "Your Verification Code";
               string body = $"Your verification code is: {code}";*/

            using (var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com", // استخدم localhost فقط
                Port = 587, // تأكد من أن هذا هو المنفذ الصحيح
                EnableSsl = true, // تأكد من أن SSL غير مفعل
                                  // UseDefaultCredentials = false, // استخدم false إذا كان لديك اعتمادات محددة
                Credentials = new NetworkCredential("aben78905@gmail.com", "sendemail123")// استخدم هذه إذا كانت مطلوبة
            })
            {
                await smtp.SendMailAsync("aben78905@gmail.com", "hero.alsawlgan@gmail.com", title, body);
                /*using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = false
                })
                {
                    await smtp.SendMailAsync(message);
                }*/
            }
        }


        private async Task SendEmailAsync(string recipientEmail, string code)
        {
            try
            {
                // **Security:** Use a more secure authentication method (recommended)
                // Consider using OAuth 2.0 or a third-party email service provider

                // **If using App Password (less secure):**
                var fromAddress = new MailAddress("abdosend873@gmail.com", "مرحبا بك شركة زليتانو"); // Replace with App Password
                var toAddress = new MailAddress(recipientEmail);
                const string subject = "كود التفعيل";
                string body = $"Your verification code is: {code}";

                using (var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    Credentials = new NetworkCredential(fromAddress.Address,
         "fduvduibgronztvz") // Use address for username
                })
                {
                    using (var message = new MailMessage(fromAddress, toAddress)
                    {
                        Subject = subject,
                        Body = body,
                        IsBodyHtml = false
                    })
                    {
                        await smtp.SendMailAsync(message);
                    }
                }
            }
            catch (Exception ex)
            {
                // **Improved Logging:** Use a logging library (e.g., NLog, Serilog)
                Console.WriteLine($"Error sending email: {ex.Message}"); // Basic logging for now
            }
        }
        /*    private async Task SendEmailAsync(string recipientEmail, string code)
            {
                try
                {
                    var fromAddress = new MailAddress("aben78905@gmail.com", "password");
                    var toAddress = new MailAddress(recipientEmail);
                    const string subject = "Your Verification Code";
                    string body = $"Your verification code is: {code}";

                    using (var smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        // UseDefaultCredentials = false,
                        Credentials = new NetworkCredential("aben78905@gmail.com", "password")
                    })
                    {
                        using (var message = new MailMessage(fromAddress, toAddress)
                        {
                            Subject = subject,
                            Body = body,
                            IsBodyHtml = false
                        })
                        {
                            await smtp.SendMailAsync(message);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // سجل الخطأ هنا
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
    */
        /*   private async Task SendEmailAsync(string recipientEmail, string code)
           {
               // إعدادات البريد الإلكتروني
               var fromAddress = new MailAddress("your-email@example.com", "Your Name");
               var toAddress = new MailAddress(recipientEmail); // هنا يتم استخدام بريد الزبون
               const string subject = "Your Verification Code";
               string body = $"Your verification code is: {code}";

               using (var smtp = new SmtpClient
               {
                   Host = "localhost", // إذا كنت تستخدم Gmail
                   Port = 25, // أو 465 إذا كنت تستخدم SSL
                   EnableSsl = false,
                   DeliveryMethod = SmtpDeliveryMethod.Network,
                   UseDefaultCredentials = true,
                   // Credentials = new NetworkCredential(fromAddress.Address, "your-email-password") // استخدم كلمة مرور التطبيق هنا
               })
               {
                   using (var message = new MailMessage(fromAddress, toAddress)
                   {
                       Subject = subject,
                       Body = body,
                       IsBodyHtml = false // إذا كنت تريد إرسال HTML، ضع true
                   })
                   {
                       await smtp.SendMailAsync(message);
                   }
               }
           }*/
        public async Task<string> Create2(Vendor vendor, string m_code, string region)
        {
            //


            //
            var lastStoreCode = await GetLastControlLastCode();
            var store0 = "";
            if (lastStoreCode != "000")
            {



                // إنشاء store0 باستخدام m_code، vendor.City، و lastStoreCode

                // var insertocontrol = await _vendorRepository.InsertIntoTableAsync(region, lastStoreCode.ToString(), 0, vendor.shopeName, vendor.Street);*/

            }
            var storeName = store0;




            if (string.IsNullOrWhiteSpace(storeName))
            {
                return "Store name cannot be empty.";
            }
            // فرضية: تنفيذ عملية إنشاء الـ Vendor هنا
            var createResult = await _vendorRepository.Create(vendor); // تأكد من أن هذا ينفذ العملية بشكل صحيح

            /*   var tableResult = await _vendorRepository.CreateTable(storeName);*/ // استخدم اسم المتجر كاسم الجدول
            if (createResult == "ok")
            {
                /* if (tableResult == "okTable")
                 {*/
                return "Success";
                /* }
                 else
                 {*/
                /*  return "No Insert ddddd Data";*/
                /*                }*/
                /* return tableResult == "Table created successfully."
                     ? "Success"
                     : $"Failed to add table: {tableResult}";*/
            }

            return $"Failed to create vendor: {createResult}";
        }

        public async Task<string> Create(Vendor vendor)
        {
            var result = await _vendorRepository.Create(vendor);
            if (result == "ok")
            {
                return "ok";
            }
            else
            {
                return "no";
            }
            /*  var lastStroe = await _vendorRepository.GetLastControlLastCode();

              var storeName = vendor.stor0;




              if (string.IsNullOrWhiteSpace(storeName))
              {
                  return "Store name cannot be empty.";
              }
              // فرضية: تنفيذ عملية إنشاء الـ Vendor هنا
              var createResult = await _vendorRepository.Create(vendor); // تأكد من أن هذا ينفذ العملية بشكل صحيح

              var tableResult = await _vendorRepository.CreateTable(storeName); // استخدم اسم المتجر كاسم الجدول
              if (createResult == "ok")
              {
                  if (tableResult == "Table added successfully.")
                  {
                      return "Success";
                  }
                  else
                  {
                      return "No Insert ddddd Data";
                  }
                  *//* return tableResult == "Table created successfully."
                       ? "Success"
                       : $"Failed to add table: {tableResult}";*//*
              }

              return $"Failed to create vendor: {createResult}";*/

        }

        /*  public async Task<string> Create(Vendor vendor)
          {
              // vendor.Region = ReadRegionShopeFromFile();




              var x = await _vendorRepository.Create(vendor);
              var codeTables = await _vendorRepository.CreateTable(vendor.stor0);
              if (x == "ok")
              {
                  if (codeTables == "Table added successfully.")
                  {
                      return "Success";
                  }
                  else
                  {
                      return "No Insert ddddd Data";
                  }
              }
              else
              {
                  return "No Insert Data";
              }



          }*/

        public async Task<string> Delete(int id)
        {
            var result = await _vendorRepository.Delete(id);
            return result; // إرجاع نتيجة الحذف مباشرة

        }

        public async Task<Vendor> GetByid(int id)
        {
            var x = await _vendorRepository.GetByIdAsync(id);
            return x;
        }

        public async Task<List<Vendor>> GetDataAll()
        {
            var getall = await _vendorRepository.GetDataAll();
            return getall;
        }

        public async Task<string> update(Vendor vendor)
        {
            return await _vendorRepository.update(vendor);
        }


        public class Regin
        {
            public string Name { get; set; }
            public string Code { get; set; }
        }

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

        public List<City> ReadCitiesCodesFromFile(string filePath)
        {
            List<City> cities = new List<City>();

            if (File.Exists(filePath))
            {
                var content = File.ReadAllText(filePath).Trim();
                var cityEntries = content.Split(',');

                foreach (var entry in cityEntries)
                {
                    var parts = entry.Split(':');
                    if (parts.Length == 2)
                    {
                        cities.Add(new City { Name = parts[0], Code = parts[1] }); // إضافة مدينة جديدة
                    }
                }
            }
            else
            {
                throw new FileNotFoundException("الملف غير موجود.", filePath);
            }

            return cities; // إرجاع قائمة المدن
        }
        public List<Regin> ReadCityCodesFromFile(string filePath)
        {
            List<Regin> cities = new List<Regin>();

            if (File.Exists(filePath))
            {
                var content = File.ReadAllText(filePath).Trim();
                var cityEntries = content.Split(',');

                foreach (var entry in cityEntries)
                {
                    var parts = entry.Split(':');
                    if (parts.Length == 2)
                    {
                        cities.Add(new Regin { Name = parts[0], Code = parts[1] }); // إضافة مدينة جديدة
                    }
                }
            }
            else
            {
                throw new FileNotFoundException("الملف غير موجود.", filePath);
            }

            return cities; // إرجاع قائمة المدن
        }

        // دالة قراءة بيانات الأسواق من ملف TXT
        public List<Market> ReadMarketCodesFromFile(string filePath)
        {
            List<Market> markets = new List<Market>();

            if (File.Exists(filePath))
            {
                var content = File.ReadAllText(filePath).Trim();
                var marketEntries = content.Split(',');

                foreach (var entry in marketEntries)
                {
                    var parts = entry.Split(':');
                    if (parts.Length == 2)
                    {
                        markets.Add(new Market { Name = parts[0], Code = parts[1] }); // إضافة سوق جديدة
                    }
                }
            }
            else
            {
                throw new FileNotFoundException("الملف غير موجود.", filePath);
            }

            return markets; // إرجاع قائمة الأسواق
        }

        public async Task<Vendor> GetVendorByUserNamePasswordLogUser(string username, string password, string loguser)
        {
            var x = await _vendorRepository.GetVendorByUserNamePasswordLogUser(username, password, loguser);
            if (x != null)
            {
                return x;
            }
            else
            {
                /*return x?? throw new NotImplementedException();*/
                return null;
            }

        }

        public async Task<Vendor> LoginVendor(string username, string password)
        {
            var x = await _vendorRepository.LoginVendor(username, password);
            if (x != null)
            {
                return x;
            }
            else
            {
                /*return x?? throw new NotImplementedException();*/
                return null;
            }

        }
    }

}

