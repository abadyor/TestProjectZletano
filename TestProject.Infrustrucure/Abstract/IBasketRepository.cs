using TestProject.Data.Entity;

namespace TestProject.Infrustrucure.Abstract
{
    public interface IBasketRepository
    {
        public Task<string> AddBasketAsync(Basket basket);
        public Task<string> UpdateBasketAsync(Basket basket);
        public Task<string> DeleteBasketAsync(int basketId);
        Task<Basket> GetByIdAsync(int id);
        Task<Basket> GetByCustomerIdAsync(int customerid, bool closeBasket);
        Task<Basket> GetEndRowAsync();

        Task<IEnumerable<Basket>> GetAllAsync();

        public Task<string> updateonefild(int BasketId, string code);
        public Task<string> updateCloseBasket(int BasketId);

        public Task<Basket> GetBasketWhereCustoemrAndBasketIdAndLog(int BasketId, int customerId, string code);

    }
}
