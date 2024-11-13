using TestProject.Data.Entity;

namespace TestProject.Service.Abstract
{
    public interface IBasketService
    {
        public Task<string> AddBasketAsync(Basket basket);
        public Task<string> UpdateBasketAsync(Basket basket);
        public Task<string> DeleteBasketAsync(int basketId);

        Task<IEnumerable<Basket>> GetAllAsync();
        Task<Basket> GetByIdAsync(int id);

        public Task<Basket> GetByCustomerIdAsync(int customerid, bool closeBasket);
        Task<Basket> GetEndRowAsync();

        public Task<string> GenerateCodeAndSendEmail(string recipientEmail);

        public Task<string> GetCodeGeny();

        public Task<string> updateonefild(int BasketId, string code);

        public Task<Basket> GetBasketWhereCustoemrAndBasketIdAndLog(int BasketId, int customerId, string code);

        public Task<string> updateCloseBasket(int BasketId);


    }
}
