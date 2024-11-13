using Microsoft.EntityFrameworkCore;
using TestProject.Data.Entity;
using TestProject.Infrustrucure.Abstract;
using TestProject.Infrustrucure.Data;

namespace TestProject.Infrustrucure.Reposetories
{
    public class CustomerRepository : ICustomerRepository
    {

        private readonly ApplicationDBContext _dBContext;

        public CustomerRepository(ApplicationDBContext dBContext)
        {
            _dBContext = dBContext;
        }

        public async Task<List<Customer>> GetDataAll()
        {
            return await _dBContext.customers.ToListAsync();
        }

        public async Task<Customer> GetByIdAsync(int id)
        {
            return await _dBContext.customers.FindAsync(id);
        }

        public async Task<string> Create(Customer customer)
        {
            await _dBContext.customers.AddAsync(customer);
            await _dBContext.SaveChangesAsync();
            return "ok";
        }

        public async Task<string> Update(Customer customer)
        {
            _dBContext.customers.Update(customer);
            await _dBContext.SaveChangesAsync();
            return "ok";
        }

        public async Task<string> Delete(int id)
        {
            var customer = await _dBContext.customers.FindAsync(id);
            if (customer != null)
            {
                _dBContext.customers.Remove(customer);
                await _dBContext.SaveChangesAsync();
                return "ok";
            }
            return "Customer not found.";
        }

        public async Task<Customer> LoginCustomer(string username, string password)
        {
            var customer = await _dBContext.customers
                     .FirstOrDefaultAsync(v => v.Username == username &&
                       v.Password == password);

            return customer;
        }

        public Task<string> updateonefild(int id_cutormer, string code)
        {
            throw new NotImplementedException();
        }
    }
}
