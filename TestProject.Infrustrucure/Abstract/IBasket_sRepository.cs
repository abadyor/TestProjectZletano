using TestProject.Data.Entity;

namespace TestProject.Infrustrucure.Abstract
{
    public interface IBasket_sRepository
    {
        public Task<string> AddBasketAsync(Basket_s basket_S);
        public Task<string> UpdateBasketAsync(Basket_s basket_S);
        public Task<string> DeleteBasketAsync(int basketId);
        Task<Basket_s> GetByIdAsync(int id);
        Task<List<Basket_s>> GetByBasketIdAsync(int basketId);

        Task<IEnumerable<Basket_s>> GetAllAsync();
    }
}
