using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using TestProject.Data.Entity;
using TestProject.Infrustrucure.Abstract;
using TestProject.Infrustrucure.Data;

namespace TestProject.Infrustrucure.Reposetories
{
    public class VendorRepository : IVendorRepository
    {
        private readonly ApplicationDBContext _dBContext;
        public VendorRepository(ApplicationDBContext dBContext)
        {
            _dBContext = dBContext;
        }
        public async Task<string> Create(Vendor vendor)
        {
            await _dBContext.vendors.AddAsync(vendor);
            await _dBContext.SaveChangesAsync();

            return "ok";
        }


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
                await _dBContext.Database.ExecuteSqlRawAsync(sql);
                return "Table created successfully.";
            }
            catch (Exception ex)
            {
                return $"Error creating table: {ex.Message}";
            }
        }



        public async Task<string> InsertIntoTableAsync(int id_vendor, string m_code, string last_store, int visitor, string shopeName, string address, string region, string city, string street, string nerestPoint)
        {
            string sql = @"
            INSERT INTO controlTables (id_vendor,M_Code, Last_sore, visitor, shopeName,Address,region,city,Street,NerestPoint)
                               VALUES   (@id_vendor,@M_Code,@Last_sore,@visitor,@shopeName,@Address,@region,@city,@Street,@NerestPoint);";

            try
            {
                var parameters = new[]
                {
                     new SqlParameter("@id_vendor", id_vendor),
                new SqlParameter("@M_Code", m_code),
                new SqlParameter("@Last_sore", last_store),
                new SqlParameter("@visitor", visitor),
                  new SqlParameter("@shopeName", shopeName),
                  new SqlParameter("@Address", address),
                   new SqlParameter("@region", region),
                new SqlParameter("@city", city),
                new SqlParameter("@Street", street),
                  new SqlParameter("@NerestPoint", nerestPoint),
            };

                await _dBContext.Database.ExecuteSqlRawAsync(sql, parameters);
                return "Record inserted successfully.";
            }
            catch (Exception ex)
            {
                return $"Error inserting record: {ex.Message}";
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

        public async Task<string> GetControlFirstId()
        {
            var firstRecord = await _dBContext.controlTables.FirstOrDefaultAsync();
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
        private async Task<int> GetLastVendorLastId()
        {
            var lastId = await _dBContext.vendors
                .OrderByDescending(v => v.Id)
                .FirstOrDefaultAsync();
            return lastId?.Id ?? 0;
        }

        public async Task<string> updateonefild(string code)
        {
            var lastid = await GetLastVendorLastId();
            if (lastid == 0)
            {
                return "The Value 0";
            }
            else
            {
                var entity = await _dBContext.vendors.FindAsync(lastid);
                if (entity == null)
                {
                    throw new Exception("Entity not found");
                }

                entity.loguser = code;
                await _dBContext.SaveChangesAsync();

                return "The Value Updated";
            }
        }
        public async Task<string> GetLastControlLastCode()
        {
            var lastControl = await _dBContext.controlTables
           .OrderByDescending(v => v.Id) // تأكد من استخدام حقل ترتيبي مثل Id
           .FirstOrDefaultAsync();

            // إذا كان هناك سجل، إرجاع اسم البائع، وإلا إرجاع null أو قيمة افتراضية
            return lastControl?.Last_sore ?? "No records found"; // يمكنك تغيير القيمة الافتراضية حسب الحاجة
        }

        public async Task<Vendor> CheckUserAndSendEmail(string username, string password)
        {
            // تحقق من بيانات الاعتماد
            var user = await _dBContext.vendors
                .FirstOrDefaultAsync(u => u.Username == username && u.Password == password); // تأكد من تشفير كلمة المرور في التطبيق الحقيقي

            if (user != null)
            {
                // إذا كان المستخدم موجودًا، يمكنك إرسال بريد إلكتروني هنا

                return user;
            }
            else
            {
                return null;
            }
        }

        // code 
        /*  public async Task<string> CreateTablesAsync(string table_name)
          {
              string sql = $@"
                  CREATE TABLE {table_name} (
                      Id INT PRIMARY KEY IDENTITY,
                      Name NVARCHAR(100),
                      Code NVARCHAR(50)
                  );";

              try
              {
                  await _dBContext.Database.ExecuteSqlRawAsync(sql);
                  return "Table created successfully.";
              }
              catch (Exception ex)
              {
                  // يمكنك تسجيل الاستثناء هنا
                  return $"Error creating table: {ex.Message}";
              }
          }


          public async Task<string> InsertIntoTableAsync(string table_name, string name, string shopeCode, string endCode)
          {
              string sql = $@"
          INSERT INTO {table_name} (Name, Shope_Code, End_Code, count_persone)
          VALUES (@Name, @Shope_Code, @End_Code, 0);";

              try
              {
                  var parameters = new[]
                  {
              new SqlParameter("@Name", name),
              new SqlParameter("@Shope_Code", shopeCode),
              new SqlParameter("@End_Code", endCode)

          };

                  await _dBContext.Database.ExecuteSqlRawAsync(sql, parameters);
                  return "Record inserted successfully.";
              }
              catch (Exception ex)
              {
                  // يمكنك تسجيل الاستثناء هنا
                  return $"Error inserting record: {ex.Message}";
              }
          }
          public async Task<string> CreateTablesAsyncControle(string table_name)
          {
              string sql = $@"
                  CREATE TABLE {table_name} (
                      Id INT PRIMARY KEY IDENTITY,
                      Name NVARCHAR(100),
                      Shope_Code NVARCHAR(50),
                      End_Code NVARCHAR(50),
                      count_persone INT
                  );";

              try
              {
                  await _dBContext.Database.ExecuteSqlRawAsync(sql);
                  return "Table created successfully.";
              }
              catch (Exception ex)
              {
                  // يمكنك تسجيل الاستثناء هنا
                  return $"Error creating table: {ex.Message}";
              }
          }*/

        /*   public async Task<string> CreateTable(string table_name)
           {
               var result = await CreateTablesAsyncControle(table_name);

               if (result == "Table created successfully.")
               {


                   var add_table = await InsertIntoTableAsync(table_name, "Default", table_name, table_name);
                   if (add_table == "Record inserted successfully.")
                   {
                       var result2 = await CreateTablesAsync(table_name);
                       if (result2 == "Table created successfully.")

                           return "Table added successfully.";
                       else
                           return "No Created Table2";
                   }
                   else
                   {
                       return "No Add Data To Table1";
                   }
               }

               //return "Table added successfully.";
               else
                   return $"Failed to add table: {result}";
           }*/

        public async Task<string> Delete(int id)
        {
            var record = await _dBContext.vendors.FindAsync(id);
            if (record == null)
            {
                return "null"; // السجل غير موجود
            }

            _dBContext.vendors.Remove(record); // حذف السجل
            await _dBContext.SaveChangesAsync(); // حفظ التغييرات

            return "ok"; // تم الحذف بنجاح
        }

        public async Task<Vendor> GetByIdAsync(int id)
        {
            var x = await _dBContext.vendors.FindAsync(id);
            return x;
        }

        public async Task<List<Vendor>> GetDataAll()
        {
            return await _dBContext.vendors.ToListAsync();
        }

        public async Task<string> update(Vendor vendor)
        {
            var stopwatch = new Stopwatch();

            stopwatch.Start();
            for (int i = 0; i < 10000; i++)
            {
                _dBContext.vendors.Update(vendor);
                await _dBContext.SaveChangesAsync();
            }
            stopwatch.Stop(); // إيقاف القياس

            var elapsedMilliseconds = stopwatch.ElapsedMilliseconds; // الحصول على الوقت المستغرق بالمللي ثانية

            return $"Records added successfully in {elapsedMilliseconds} ms";
        }

        public async Task<Vendor> GetVendorByUserNamePasswordLogUser(string username, string password, string loguser)
        {
            var vendor = await _dBContext.vendors
                       .FirstOrDefaultAsync(v => v.Username == username &&
                         v.Password == password &&
                        v.loguser == loguser);

            return vendor;
        }
        public async Task<Vendor> LoginVendor(string username, string password)
        {
            var vendor = await _dBContext.vendors
                       .FirstOrDefaultAsync(v => v.Username == username &&
                         v.Password == password);

            return vendor;
        }
    }
}
