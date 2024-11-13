using TestProject.Data.Entity;

namespace TestProject.Service.Abstract
{
    public interface IControlService
    {
        Task<IEnumerable<ControlTable>> GetAllAsync();
        Task<ControlTable> GetByIdAsync(int id);
        Task<bool> AddAsync(ControlTable controlTable);
        Task UpdateAsync(ControlTable controlTable);
        Task DeleteAsync(int id);

        public Task<string> GetLastControlLastCode();
        public Task<string> CreateTable(string table_name);
        public Task<List<ControlTable>> GetByVendorIdAsync(int idVendor);
        public Task<string> GetControlFirstId();

        public Task AddItemToTable(string tableName, Dictionary<string, object> data);
    }
}
