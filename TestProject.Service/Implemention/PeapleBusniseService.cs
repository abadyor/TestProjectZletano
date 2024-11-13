using TestProject.Data.Entity;
using TestProject.Infrustrucure.Abstract;
using TestProject.Service.Abstract;

namespace TestProject.Service.Implemention
{
    public class PeapleBusniseService : IPeapleBusniseService
    {
        private readonly IPeapleBusniseRepository _repository;

        public PeapleBusniseService(IPeapleBusniseRepository repository)
        {
            _repository = repository;
        }

        public async Task<PeapleBusnise> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<PeapleBusnise>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task AddAsync(PeapleBusnise peapleBusnise)
        {
            peapleBusnise.region_shope = ReadRegionShopeFromFile();


            await _repository.AddAsync(peapleBusnise);
        }

        public async Task UpdateAsync(PeapleBusnise peapleBusnise)
        {
            peapleBusnise.region_shope = ReadRegionShopeFromFile();
            await _repository.UpdateAsync(peapleBusnise);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }



        private string ReadRegionShopeFromFile()
        {
            // تأكد من أن المسار صحيح
            string filePath = "F:\\TestProjectZletano\file.txt"; // يمكنك تعديل المسار حسب الحاجة
            if (File.Exists(filePath))
            {
                return File.ReadAllText(filePath).Trim(); // قراءة المحتوى وإزالة الفراغات
            }
            throw new FileNotFoundException("الملف غير موجود.", filePath);
        }
    }
}
