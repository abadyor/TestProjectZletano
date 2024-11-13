using TestProject.Data.Entity;
using TestProject.Service.Implemention;

namespace TestProject.Service.Abstract
{
    public interface IVendorService
    {
        public Task<List<Vendor>> GetDataAll();
        public Task<Vendor> GetByid(int id);
        List<VendorService.Regin> ReadCityCodesFromFile(string filePath); // إضافة دالة قراءة المدن
        List<VendorService.Market> ReadMarketCodesFromFile(string filePath); // إضافة دالة قراءة الأ
        List<VendorService.City> ReadCitiesCodesFromFile(string filePath); // إضافة دالة قراءة الأ
        public Task<string> Create(Vendor vendor);
        public Task<string> update(Vendor vendor);


        public Task<string> Delete(int id);

        public Task<string> Create2(Vendor vendor, string m_code, string region);
        public Task<string> GetLastControlLastCode();
        public Task<string> GetCodeGeny();

        public Task<string> GetControlFirstId();
        public Task<string> updateonefild(string code);
        public Task<string> GenerateCodeAndSendEmail(string recipientEmail);
        public Task SendEmailAsyncAnotherFunction(string title, string body);

        public Task<string> CheckUserAndSendEmail(string username, string password);

        public Task<string> GenerateCodeAndSendEmailAndGetCode(string recipientEmail);
        public Task<Vendor> GetVendorByUserNamePasswordLogUser(string username, string password, string loguser);

        public Task<Vendor> LoginVendor(string username, string password);

    }
}
