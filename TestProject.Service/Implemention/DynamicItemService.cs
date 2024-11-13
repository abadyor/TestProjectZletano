using TestProject.Infrustrucure.Abstract;
using TestProject.Service.Abstract;

namespace TestProject.Service.Implemention
{
    public class DynamicItemService : IDynamicItemService
    {
        private readonly IDynamicItemRepository _dynamicItemRepository;
        public DynamicItemService(IDynamicItemRepository dynamicItemRepository)
        {
            _dynamicItemRepository = dynamicItemRepository;
        }

        public async Task<string> GetItemNameById(string tableName, int itemId)
        {
            var x = await _dynamicItemRepository.GetItemNameById(tableName, itemId);
            if (x != "Item not found")
            {
                return x;
            }

            else
            {
                return null;
            }
        }
    }
}
