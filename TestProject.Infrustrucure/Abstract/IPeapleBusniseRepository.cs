using TestProject.Data.Entity;

namespace TestProject.Infrustrucure.Abstract
{
    public interface IPeapleBusniseRepository
    {
        Task<PeapleBusnise> GetByIdAsync(int id);
        Task<IEnumerable<PeapleBusnise>> GetAllAsync();
        Task AddAsync(PeapleBusnise peapleBusnise);
        Task UpdateAsync(PeapleBusnise peapleBusnise);
        Task DeleteAsync(int id);
    }
}
