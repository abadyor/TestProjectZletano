using System.Net;
using System.Net.Mail;
using TestProject.Data.Entity;
using TestProject.Infrustrucure.Abstract;
using TestProject.Service.Abstract;

namespace TestProject.Service.Implemention
{
    public class BasketService : IBasketService
    {

        public string codegeny = "";
        private readonly IBasketRepository _basketRepository;
        public BasketService(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }
        public async Task<string> AddBasketAsync(Basket basket)
        {
            var x = await _basketRepository.AddBasketAsync(basket);
            if (x == "oK")
            {
                return "oK";
            }
            else
            {
                return "No";
            }

        }

        public async Task<string> UpdateBasketAsync(Basket basket)
        {
            var x = await _basketRepository.UpdateBasketAsync(basket);

            if (x == "oK")
            {
                return "oK";
            }
            else
            {
                return "No";
            }
        }

        public async Task<string> DeleteBasketAsync(int basketId)
        {
            var x = await _basketRepository.DeleteBasketAsync(basketId);

            if (x == "oK")
            {
                return "oK";
            }
            else
            {
                return "No";
            }

        }

        public async Task<IEnumerable<Basket>> GetAllAsync()
        {
            return await _basketRepository.GetAllAsync();
        }

        public async Task<Basket> GetByIdAsync(int id)
        {
            return await _basketRepository.GetByIdAsync(id);
        }

        public async Task<Basket> GetEndRowAsync()
        {
            var x = await _basketRepository.GetEndRowAsync();
            if (x == null)
            {
                return null;
            }
            else
            {
                return x;
            }
        }

        public async Task<Basket> GetByCustomerIdAsync(int customerid, bool closeBasket)
        {
            return await _basketRepository.GetByCustomerIdAsync(customerid, closeBasket);
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

        private string GenerateRandomCode()
        {
            // توليد كود عشوائي مكون من 8 أرقام (يمكنك تعديله حسب الحاجة)
            Random random = new Random();
            return random.Next(10000000, 99999999).ToString("D8"); // كود مكون من 8 أرقام
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

        public async Task<string> updateCloseBasket(int BasketId)
        {
            var updateone = await _basketRepository.updateCloseBasket(BasketId);
            if (updateone == "The Value Updated")
            {
                return "The Value Updated";
            }
            else
            {
                return "The Value No Updated";
            }
        }

        public async Task<string> updateonefild(int BasketId, string code)
        {
            var updateone = await _basketRepository.updateonefild(BasketId, code);
            if (updateone == "The Value Updated")
            {
                return "The Value Updated";
            }
            else
            {
                return "The Value No Updated";
            }
        }

        public async Task<Basket> GetBasketWhereCustoemrAndBasketIdAndLog(int BasketId, int customerId, string code)
        {
            var basket = await _basketRepository.GetBasketWhereCustoemrAndBasketIdAndLog(BasketId, customerId, code);
            if (basket != null)
            {


                return basket;
            }
            else
            {
                return null;
            }

        }
    }
}

