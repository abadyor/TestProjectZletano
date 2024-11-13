using TestProject.Data.Entity;

namespace TestProject.Service.Abstract
{
    public interface IPeapleBusniseService
    {
        Task<PeapleBusnise> GetByIdAsync(int id);
        Task<IEnumerable<PeapleBusnise>> GetAllAsync();
        Task AddAsync(PeapleBusnise peapleBusnise);
        Task UpdateAsync(PeapleBusnise peapleBusnise);
        Task DeleteAsync(int id);
    }
}
