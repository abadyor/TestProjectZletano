using TestProject.Data.Entity;

namespace TestProject.Infrustrucure.Abstract
{
    public interface IVendorRepository
    {
        public Task<List<Vendor>> GetDataAll();

        public Task<Vendor> GetByIdAsync(int id);



        public Task<string> Create(Vendor vendor);
        public Task<string> update(Vendor vendor);
        public Task<string> Delete(int id);

        public Task<string> CreateTable(string table_name);
        public Task<string> InsertIntoTableAsync(int id_vendor, string m_code, string last_store, int visitor, string shopeName, string address, string region, string city, string street, string nerestPoint);

        public Task<string> GetLastControlLastCode();

        public Task<string> updateonefild(string code);

        public Task<string> GetControlFirstId();
        public Task<Vendor> GetVendorByUserNamePasswordLogUser(string username, string password, string loguser);

        public Task<Vendor> LoginVendor(string username, string password);

        public Task<Vendor> CheckUserAndSendEmail(string username, string password);
    }
}
