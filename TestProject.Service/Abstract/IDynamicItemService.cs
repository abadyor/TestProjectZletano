namespace TestProject.Service.Abstract
{
    public interface IDynamicItemService
    {
        Task<string> GetItemNameById(string tableName, int itemId);
    }
}
