using TestProject.Data.Entity;

namespace TestProject.Infrustrucure.Abstract
{
    public interface ICustomerRepository
    {
        public Task<List<Customer>> GetDataAll();

        public Task<Customer> GetByIdAsync(int id);

        public Task<string> Create(Customer customer);
        public Task<string> Update(Customer customer);
        public Task<string> Delete(int id);

        public Task<Customer> LoginCustomer(string username, string password);

        public Task<string> updateonefild(int id_cutormer, string code);


    }
}
