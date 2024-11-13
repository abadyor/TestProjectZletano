using TestProject.Data.Entity;
using TestProject.Infrustrucure.Abstract;
using TestProject.Service.Abstract;

namespace TestProject.Service.Implemention
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<List<Customer>> GetAllCustomersAsync()
        {
            return await _customerRepository.GetDataAll();
        }

        public async Task<Customer> GetCustomerByIdAsync(int id)
        {
            return await _customerRepository.GetByIdAsync(id);
        }

        public async Task<string> CreateCustomerAsync(Customer customer)
        {
            var x = await _customerRepository.Create(customer);
            if (x == "ok")
            {
                return "ok";
            }
            return "no";
        }

        public async Task<string> UpdateCustomerAsync(Customer customer)
        {
            var x = await _customerRepository.Update(customer);
            if (x == "ok")
            {
                return "ok";
            }
            return "no";
        }

        public async Task<string> DeleteCustomerAsync(int id)
        {
            var x = await _customerRepository.Delete(id);
            if (x == "ok")
            {
                return "ok";
            }
            return "no";
        }

        public async Task<Customer> LoginCustomer(string username, string password)
        {
            var x = await _customerRepository.LoginCustomer(username, password);
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
