using Microsoft.EntityFrameworkCore;
using TestProject.Data.Entity;
using TestProject.Infrustrucure.Abstract;
using TestProject.Infrustrucure.Data;

namespace TestProject.Infrustrucure.Reposetories
{
    public class PeapleBusniseRepository : IPeapleBusniseRepository
    {
        private readonly ApplicationDBContext _context;

        public PeapleBusniseRepository(ApplicationDBContext dBcontext)
        {
            _context = dBcontext;
        }

        public async Task<PeapleBusnise> GetByIdAsync(int id)
        {
            return await _context.peapleBusnise.FindAsync(id);

        }

        public async Task<IEnumerable<PeapleBusnise>> GetAllAsync()
        {
            return await _context.peapleBusnise.ToListAsync();
        }

        public async Task AddAsync(PeapleBusnise peapleBusnise)
        {
            await _context.peapleBusnise.AddAsync(peapleBusnise);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(PeapleBusnise peapleBusnise)
        {
            _context.peapleBusnise.Update(peapleBusnise);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var peapleBusnise = await GetByIdAsync(id);
            if (peapleBusnise != null)
            {
                _context.peapleBusnise.Remove(peapleBusnise);
                await _context.SaveChangesAsync();
            }
        }
    }
}
