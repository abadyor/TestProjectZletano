using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using TestProject.Data.Entity;
using TestProject.Infrustrucure.Abstract;
using TestProject.Infrustrucure.Data;

namespace TestProject.Infrustrucure.Reposetories
{
    public class ControlRepository : IControlRepository
    {
        private readonly ApplicationDBContext _dbcontext;

        public ControlRepository(ApplicationDBContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<IEnumerable<ControlTable>> GetAllAsync()
        {
            return await _dbcontext.controlTables.ToListAsync();
        }

        public async Task<ControlTable> GetByIdAsync(int id)
        {
            return await _dbcontext.controlTables.FindAsync(id);
        }

        public async Task AddAsync(ControlTable controlTable)
        {
            await _dbcontext.controlTables.AddAsync(controlTable);
            await _dbcontext.SaveChangesAsync();
        }

        public async Task<List<ControlTable>> GetByVendorIdAsync(int idVendor)
        {
            var x = await _dbcontext.controlTables
                                 .Where(ct => ct.id_vendor == idVendor)
                                 .ToListAsync();

            return x;

        }

        public async Task UpdateAsync(ControlTable controlTable)
        {
            _dbcontext.controlTables.Update(controlTable);
            await _dbcontext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var controlTable = await GetByIdAsync(id);
            if (controlTable != null)
            {
                _dbcontext.controlTables.Remove(controlTable);
                await _dbcontext.SaveChangesAsync();
            }
        }

        public async Task<string> GetLastControlLastCode()
        {
            var lastControl = await _dbcontext.controlTables
           .OrderByDescending(v => v.Id) // تأكد من استخدام حقل ترتيبي مثل Id
           .FirstOrDefaultAsync();

            // إذا كان هناك سجل، إرجاع اسم البائع، وإلا إرجاع null أو قيمة افتراضية
            return lastControl?.Last_sore ?? "No records found"; // يمكنك تغيير القيمة الافتراضية حسب الحاجة
        }
        public async Task<string> GetControlFirstId()
        {
            var firstRecord = await _dbcontext.controlTables.FirstOrDefaultAsync();
            if (firstRecord != null)
            {
                var firstMCode = firstRecord.M_Code;
                return firstMCode;
            }
            else
            {
                return "No Data";
            }
        }

        public async Task AddItemToDynamicTable(string tableName, Dictionary<string, object> data)
        {
            // تحقق من أن الاسم يبدأ بـ "s_" ويليه رقم
            if (!Regex.IsMatch(tableName, @"^s_\d+$"))
            {
                throw new ArgumentException("Invalid table name format.");
            }

            // التأكد من وجود الحقول المطلوبة
            var requiredFields = new List<string> { "Item", "Discription", "Price", "Quantity", "CountImage", "NameImage" };
            foreach (var field in requiredFields)
            {
                if (!data.ContainsKey(field))
                {
                    throw new ArgumentException($"Missing required field: {field}");
                }
            }

            var sql = $"INSERT INTO {tableName} ([Item], [Discription], [Price], [Quantity], [CountImage], [NameImage]) " +
                      "VALUES (@Item, @Discription, @Price, @Quantity, @CountImage, @NameImage)";

            var parameters = new[]
            {
            new SqlParameter("@Item", data["Item"] ?? DBNull.Value),
            new SqlParameter("@Discription", data["Discription"] ?? DBNull.Value),
            new SqlParameter("@Price", data["Price"] ?? DBNull.Value),
            new SqlParameter("@Quantity", data["Quantity"] ?? DBNull.Value),
            new SqlParameter("@CountImage", data["CountImage"] ?? DBNull.Value),
            new SqlParameter("@NameImage", data["NameImage"] ?? DBNull.Value)
        };

            await _dbcontext.Database.ExecuteSqlRawAsync(sql, parameters);
        }

        /* public async Task AddItemToDynamicTable(string tableName, Dictionary<string, object> data)
         {
             var sql = $"INSERT INTO {tableName} ([Item], [Discription], [Price], [Quantity], [CountImage], [NameImage]) " +
                       $"VALUES (@Item, @Discription, @Price, @Quantity, @CountImage, @NameImage)";

             var parameters = data.Select(kvp => new SqlParameter($"@{kvp.Key}", kvp.Value ?? DBNull.Value)).ToArray();

             await _dbcontext.Database.ExecuteSqlRawAsync(sql, parameters);
         }*/

        public async Task<string> CreateTablesAsync(string tableName)
        {
            string sql = $@"
            CREATE TABLE s_{tableName} (
                Id INT PRIMARY KEY IDENTITY,
                Item NVARCHAR(50),
                Discription NVARCHAR(100),
                Price numeric(18, 3),
                Quantity numeric(18, 3),
                CountImage INT ,
                NameImage NVARCHAR(100)
               
            );";

            try
            {
                await _dbcontext.Database.ExecuteSqlRawAsync(sql);
                return "Table created successfully.";
            }
            catch (Exception ex)
            {
                return $"Error creating table: {ex.Message}";
            }
        }
        public async Task<string> CreateTable(string tableName)
        {
            var createResult = await CreateTablesAsync(tableName);
            if (createResult == "Table created successfully.")
            {
                return "okTable";
                /*var insertResult = await InsertIntoTableAsync(tableName, "Default", tableName, tableName);
                return insertResult;*/
            }
            return createResult; // إرجاع رسالة الخطأ عند فشل إنشاء الجدول
        }
    }
}
