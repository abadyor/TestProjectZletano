using TestProject.Data.Entity;

namespace TestProject.Infrustrucure.Abstract
{
    public interface IT1Reposatory
    {
        public Task<List<T1>> GetDataAll();

        public Task<T1> GetByIdAsync(int id);
        public Task<string> CreateTable(string table_name);
        public Task<string> GetByNameAsync(string name);
        public Task<string> GetByNameAndByIdAsync(string name, int id);
        public Task<string> Create(T1 t1);
        public Task<string> update(T1 t1);
        public Task<string> Delete(int id);
        Task<string> CreateMulty(int v);
        Task<List<T1>> GetDataTimeWhereRecord(int number);
        public Task<string> UpdateMulty(string fieldName);

    }
}
