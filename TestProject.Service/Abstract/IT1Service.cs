using TestProject.Data.Entity;

namespace TestProject.Service.Abstract
{
    public interface IT1Service
    {
        public Task<List<T1>> GetDataAll();
        public Task<T1> GetByid(int id);
        public Task<string> Create(T1 t1);
        public Task<string> update(T1 t1);
        public Task<string> CreateTable(string table_name);

        public Task<string> Delete(int id);
        public Task<string> CreateMUlty(int v);

        public Task<string> UpdateMulty(string fieldName);
        /**/
        public Task<(List<T1> records, long elapsedMilliseconds)> GetDataAllTime();

        public Task<(List<T1> records, long elapsedMilliseconds)> GetDataTimeWhereRecord(int number);




    }
}
