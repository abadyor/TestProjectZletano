using System.Text.RegularExpressions;
using TestProject.Data.Entity;
using TestProject.Infrustrucure.Abstract;
using TestProject.Service.Abstract;

namespace TestProject.Service.Implemention
{
    public class ControlService : IControlService
    {
        private readonly IControlRepository _controlRepository;

        public ControlService(IControlRepository controlRepository)
        {
            _controlRepository = controlRepository;
        }

        public async Task<IEnumerable<ControlTable>> GetAllAsync()
        {
            return await _controlRepository.GetAllAsync();
        }

        public async Task<ControlTable> GetByIdAsync(int id)
        {
            return await _controlRepository.GetByIdAsync(id);
        }

        public async Task<bool> AddAsync(ControlTable controlTable)
        {

            try
            {
                await _controlRepository.AddAsync(controlTable);
                /* var tableResult = await _controlRepository.CreateTable(storeName);*/
                return true; // تشير إلى أن العملية تمت بنجاح
            }
            catch (Exception ex)
            {

                return false;
            }


        }

        public async Task<List<ControlTable>> GetByVendorIdAsync(int idVendor)
        {
            var x = await _controlRepository.GetByVendorIdAsync(idVendor);
            return x;
        }

        public async Task UpdateAsync(ControlTable controlTable)
        {
            await _controlRepository.UpdateAsync(controlTable);
        }

        public async Task DeleteAsync(int id)
        {
            await _controlRepository.DeleteAsync(id);
        }


        public async Task<string> GetLastControlLastCode()
        {
            var lastStoreCode = await _controlRepository.GetLastControlLastCode();

            // محاولة تحويل lastStoreCode إلى عدد صحيح
            if (int.TryParse(lastStoreCode, out int lastCode))
            {
                lastCode += 1; // إضافة 1 إلى القيمة
                return lastCode.ToString("D3"); // إرجاع القيمة كنص مع 3 أرقام (مثل 001، 002، إلخ.)
            }
            else
            {
                return "000"; // إرجاع قيمة افتراضية مع أصفار إذا لم تكن القيمة صالحة
            }
        }

        public async Task<string> GetControlFirstId()
        {
            var marketCode = await _controlRepository.GetControlFirstId();
            if (marketCode != "No Data")
            {
                return marketCode;
            }
            else
            {
                return "No Data";
            }
        }

        public async Task<string> CreateTable(string table_name)
        {
            try
            {

                var tableResult = await _controlRepository.CreateTable(table_name);
                return "Ok Create"; // تشير إلى أن العملية تمت بنجاح
            }
            catch (Exception ex)
            {

                return "No Create";
            }
        }

        public async Task AddItemToTable(string tableName, Dictionary<string, object> data)
        {
            // تحقق من أن الاسم يبدأ بـ "s_" ويليه رقم
            if (!Regex.IsMatch(tableName, @"^s_\d+$"))
            {
                throw new ArgumentException("Invalid table name format.");
            }

            await _controlRepository.AddItemToDynamicTable(tableName, data);
        }
    }
}
