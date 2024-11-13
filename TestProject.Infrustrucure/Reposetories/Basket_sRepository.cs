using Microsoft.EntityFrameworkCore;
using TestProject.Data.Entity;
using TestProject.Infrustrucure.Abstract;
using TestProject.Infrustrucure.Data;

namespace TestProject.Infrustrucure.Reposetories
{
    public class Basket_sRepository : IBasket_sRepository
    {
        private readonly ApplicationDBContext _applicationDBContext;

        public Basket_sRepository(ApplicationDBContext applicationDBContext)
        {
            _applicationDBContext = applicationDBContext;
        }
        public async Task<string> AddBasketAsync(Basket_s basket_S)
        {
            await _applicationDBContext.basket_s.AddAsync(basket_S);
            await _applicationDBContext.SaveChangesAsync();
            return "oK";
        }

        public async Task<string> DeleteBasketAsync(int basketId)
        {
            var basket_s = await _applicationDBContext.basket_s.FindAsync(basketId);
            if (basket_s != null)
            {
                _applicationDBContext.basket_s.Remove(basket_s);
                await _applicationDBContext.SaveChangesAsync();
                return "oK";
            }
            else
            {
                return "No";
            }
        }

        public async Task<IEnumerable<Basket_s>> GetAllAsync()
        {
            return await _applicationDBContext.basket_s.ToListAsync();
        }

        public async Task<List<Basket_s>> GetByBasketIdAsync(int basketId)
        {
            return await _applicationDBContext.basket_s
                  .Where(b => b.basketId == basketId)
                  .ToListAsync();

        }

        public async Task<Basket_s> GetByIdAsync(int id)
        {
            return await _applicationDBContext.basket_s.FindAsync(id);
        }

        public async Task<string> UpdateBasketAsync(Basket_s basket_S)
        {
            _applicationDBContext.basket_s.Update(basket_S);
            await _applicationDBContext.SaveChangesAsync();
            return "oK";
        }
    }
}
