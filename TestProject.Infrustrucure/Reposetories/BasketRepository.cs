using Microsoft.EntityFrameworkCore;
using TestProject.Data.Entity;
using TestProject.Infrustrucure.Abstract;
using TestProject.Infrustrucure.Data;

namespace TestProject.Infrustrucure.Reposetories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly ApplicationDBContext _applicationDBContext;

        public BasketRepository(ApplicationDBContext applicationDBContext)
        {
            _applicationDBContext = applicationDBContext;
        }
        public async Task<string> AddBasketAsync(Basket basket)
        {
            await _applicationDBContext.basket.AddAsync(basket);
            await _applicationDBContext.SaveChangesAsync();
            return "oK";
        }

        public async Task<string> UpdateBasketAsync(Basket basket)
        {
            _applicationDBContext.basket.Update(basket);
            await _applicationDBContext.SaveChangesAsync();
            return "oK";
        }

        public async Task<string> DeleteBasketAsync(int basketId)
        {
            var basket = await _applicationDBContext.basket.FindAsync(basketId);
            if (basket != null)
            {
                _applicationDBContext.basket.Remove(basket);
                await _applicationDBContext.SaveChangesAsync();
                return "oK";
            }
            else
            {
                return "No";
            }
        }

        public async Task<Basket> GetByIdAsync(int id)
        {
            return await _applicationDBContext.basket.FindAsync(id);
        }

        public async Task<IEnumerable<Basket>> GetAllAsync()
        {
            return await _applicationDBContext.basket.ToListAsync();
        }

        public async Task<Basket> GetEndRowAsync()
        {
            return await _applicationDBContext.basket
                  .OrderByDescending(v => v.Id) // تأكد من استخدام حقل ترتيبي مثل Id
                    .FirstOrDefaultAsync();


        }

        public async Task<Basket> GetByCustomerIdAsync(int customerid, bool closeBasket)
        {
            var basket = await _applicationDBContext.basket
                                   .FirstOrDefaultAsync(b => b.customerId == customerid && b.closeBasket == closeBasket);

            return basket;
        }
        public async Task<string> updateCloseBasket(int BasketId)
        {
            var entity = await _applicationDBContext.basket.FindAsync(BasketId);
            if (entity == null)
            {
                throw new Exception("Entity not found");
            }

            entity.closeBasket = true;
            await _applicationDBContext.SaveChangesAsync();

            return "The Value Updated";
        }
        public async Task<string> updateonefild(int BasketId, string code)
        {

            var entity = await _applicationDBContext.basket.FindAsync(BasketId);
            if (entity == null)
            {
                throw new Exception("Entity not found");
            }

            entity.loguser = code;
            await _applicationDBContext.SaveChangesAsync();

            return "The Value Updated";

        }

        public async Task<Basket> GetBasketWhereCustoemrAndBasketIdAndLog(int BasketId, int customerId, string code)
        {
            var basket = await _applicationDBContext.basket
                                  .FirstOrDefaultAsync(b => b.Id == BasketId && b.customerId == customerId && b.loguser == code);

            return basket;
        }
    }
}
