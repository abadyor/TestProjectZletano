using TestProject.Data.Entity;

namespace TestProject.Service.Abstract
{
    public interface ICustomerService
    {
        Task<List<Customer>> GetAllCustomersAsync();
        Task<Customer> GetCustomerByIdAsync(int id);
        Task<string> CreateCustomerAsync(Customer customer);
        Task<string> UpdateCustomerAsync(Customer customer);
        Task<string> DeleteCustomerAsync(int id);

        public Task<Customer> LoginCustomer(string username, string password);
    }
}
