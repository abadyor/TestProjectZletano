using TestProject.Data.Entity;

namespace TestProject.Infrustrucure.Abstract
{
    public interface IControlRepository
    {
        Task<IEnumerable<ControlTable>> GetAllAsync();
        Task<ControlTable> GetByIdAsync(int id);
        Task AddAsync(ControlTable controlTable);
        Task UpdateAsync(ControlTable controlTable);
        Task DeleteAsync(int id);
        public Task<string> GetLastControlLastCode();

        public Task<string> CreateTable(string table_name);
        public Task<List<ControlTable>> GetByVendorIdAsync(int idVendor);

        public Task<string> GetControlFirstId();

        Task AddItemToDynamicTable(string tableName, Dictionary<string, object> data);
    }
}
