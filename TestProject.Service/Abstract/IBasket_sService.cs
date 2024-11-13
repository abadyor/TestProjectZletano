using TestProject.Data.Entity;

namespace TestProject.Service.Abstract
{
    public interface IBasket_sService
    {
        public Task<string> AddBasketAsync(Basket_s basket_S);
        public Task<string> UpdateBasketAsync(Basket_s basket_S);
        public Task<string> DeleteBasketAsync(int basketId);

        Task<IEnumerable<Basket_s>> GetAllAsync();

        Task<Basket_s> GetByIdAsync(int id);

        public Task<List<Basket_s>> GetByBasketIdAsync(int basketId);




    }
}
