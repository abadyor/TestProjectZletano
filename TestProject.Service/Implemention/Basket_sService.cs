using TestProject.Data.Entity;
using TestProject.Infrustrucure.Abstract;
using TestProject.Service.Abstract;

namespace TestProject.Service.Implemention
{
    public class Basket_sService : IBasket_sService
    {
        private readonly IBasket_sRepository _basket_SRepository;
        public Basket_sService(IBasket_sRepository basket_SRepository)
        {
            _basket_SRepository = basket_SRepository;
        }
        public async Task<string> AddBasketAsync(Basket_s basket_S)
        {
            var x = await _basket_SRepository.AddBasketAsync(basket_S);
            if (x == "oK")
            {
                return "oK";
            }
            else
            {
                return "No";
            }
        }

        public async Task<string> DeleteBasketAsync(int basketId)
        {
            var x = await _basket_SRepository.DeleteBasketAsync(basketId);

            if (x == "oK")
            {
                return "oK";
            }
            else
            {
                return "No";
            }
        }

        public async Task<IEnumerable<Basket_s>> GetAllAsync()
        {
            return await _basket_SRepository.GetAllAsync();
        }

        public async Task<List<Basket_s>> GetByBasketIdAsync(int basketId)
        {
            var x = await _basket_SRepository.GetByBasketIdAsync(basketId);
            if (x != null)
            {
                return x;
            }
            else
            {
                return null;
            }
        }

        public async Task<Basket_s> GetByIdAsync(int id)
        {
            return await _basket_SRepository.GetByIdAsync(id);
        }



        public async Task<string> UpdateBasketAsync(Basket_s basket_S)
        {
            var x = await _basket_SRepository.UpdateBasketAsync(basket_S);

            if (x == "oK")
            {
                return "oK";
            }
            else
            {
                return "No";
            }
        }
    }
}
