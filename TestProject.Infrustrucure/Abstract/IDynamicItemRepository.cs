namespace TestProject.Infrustrucure.Abstract
{
    public interface IDynamicItemRepository
    {
        Task<string> GetItemNameById(string tableName, int itemId);
    }
}
