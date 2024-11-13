using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using TestProject.Data.Entity;
using TestProject.Infrustrucure.Abstract;
using TestProject.Infrustrucure.Data;

namespace TestProject.Infrustrucure.Reposetories
{
    public class T1Reposetory : IT1Reposatory
    {

        #region Field
        private readonly ApplicationDBContext _dBContext;
        #endregion

        #region Constractor
        public T1Reposetory(ApplicationDBContext dBContext)
        {
            _dBContext = dBContext;
        }

        #endregion




        #region Handle Function
        public async Task<string> Create(T1 t1)
        {



            await _dBContext.t1.AddAsync(t1);
            await _dBContext.SaveChangesAsync();

            return "ok";


        }
        public async Task<string> CreateMulty(int numberOfRecords)
        {

            var records = new List<T1>();
            var stopwatch = new Stopwatch();

            stopwatch.Start();

            for (int i = 0; i < numberOfRecords; i++)
            {
                var t1 = new T1
                {
                    name = $"Name {i}",
                    des = $"description {i}"// تعيين اسم مختلف لكل كائن
                                            // إعداد الخصائص الأخرى لـ T1 حسب الحاجة
                };
                records.Add(t1);
                // await Task.Delay(3000);
            }

            await _dBContext.t1.AddRangeAsync(records); // إضافة جميع السجلات دفعة واحدة
            await _dBContext.SaveChangesAsync(); // حفظ التغييرات

            stopwatch.Stop(); // إيقاف القياس

            var elapsedMilliseconds = stopwatch.ElapsedMilliseconds; // الحصول على الوقت المستغرق بالمللي ثانية

            return $"Records added successfully in {elapsedMilliseconds} ms";


            /* await _dBContext.t1.AddAsync(t1);
             await _dBContext.SaveChangesAsync();

             return "ok";*/


        }

        public async Task<string> UpdateMulty(string fieldName)
        {
            // استرجاع جميع السجلات من قاعدة البيانات
            var records = await _dBContext.t1.ToListAsync();
            var stopwatch = new Stopwatch();

            stopwatch.Start();

            // تحديث الحقل المحدد في كل سجل
            foreach (var record in records)
            {
                // تحديث الحقل بناءً على اسم الحقل
                if (fieldName.Equals("name", StringComparison.OrdinalIgnoreCase))
                {
                    record.name = ReverseString(record.name); // قلب النص
                }
                else if (fieldName.Equals("des", StringComparison.OrdinalIgnoreCase))
                {
                    record.des = ReverseString(record.des); // قلب النص
                }
                // يمكنك إضافة المزيد من الحقول هنا إذا لزم الأمر
            }

            // تحديث جميع السجلات في قاعدة البيانات
            _dBContext.t1.UpdateRange(records);
            await _dBContext.SaveChangesAsync();


            stopwatch.Stop(); // إيقاف القياس

            var elapsedMilliseconds = stopwatch.ElapsedMilliseconds; // الحصول على الوقت المستغرق بالمللي ثانية

            return $"Records added successfully in {elapsedMilliseconds} ms";
        }

        public async Task<string> Delete(int id)
        {
            try
            {

                var record = await _dBContext.t1.FindAsync(id);
                if (record == null)
                {
                    return "null"; // السجل غير موجود
                }

                _dBContext.t1.Remove(record); // حذف السجل
                await _dBContext.SaveChangesAsync(); // حفظ التغييرات

                return "ok"; // تم الحذف بنجاح
            }
            catch
            {
                return "ErrrrrrrrooooooR";
            }
            /* var entity = await GetByIdAsync(id);
             if (entity == null)
             {
                 return "no"; // إذا لم يكن الكائن موجودًا
             }

             _dBContext.Set<T1>().Remove(entity);
             await _dBContext.SaveChangesAsync();
             return "Success"; // إرجاع رسالة النجاح */

            /*  var entity = await GetByIdAsync(id);
              if (entity != null)
              {
                  _dBContext.Set<T1>().Remove(entity);
                  await _dBContext.SaveChangesAsync();
                  return "Success";
              }
              return "Entity not found";*/
            /*  // البحث عن الكائن باستخدام id
              var entity = await _dBContext.t1.FindAsync(id);

              // التحقق مما إذا كان الكائن موجودًا
              if (entity == null)
              {
                  return "Entity not found"; 
              }

              // إزالة الكائن من DbSet
              _dBContext.t1.Remove(entity);

              // حفظ التغييرات في قاعدة البيانات
              await _dBContext.SaveChangesAsync();

              return "Success"; // إرجاع رسالة النجاح*/


        }

        public async Task<T1> GetByIdAsync(int id)
        {
            var x = await _dBContext.t1.FindAsync(id);
            return x;
        }

        public async Task<string> GetByNameAndByIdAsync(string name, int id)
        {
            var x = await _dBContext.t1.Where(x => x.name == name && x.id == id).FirstOrDefaultAsync();
            if (x != null)
            {
                return "no the name found";
            }
            else
            {
                return "ok not found";
            }
        }

        public async Task<string> GetByNameAsync(string name)
        {
            var x = await _dBContext.t1.Where(x => x.name == name).FirstOrDefaultAsync();
            if (x != null)
            {
                return "no the name found";
            }
            else
            {
                return "ok not found";
            }
        }

        public async Task<List<T1>> GetDataAll()
        {
            return await _dBContext.t1.ToListAsync();

        }

        public async Task<string> update(T1 t1)
        {
            var stopwatch = new Stopwatch();

            stopwatch.Start();
            for (int i = 0; i < 10000; i++)
            {
                _dBContext.t1.Update(t1);
                await _dBContext.SaveChangesAsync();
            }
            stopwatch.Stop(); // إيقاف القياس

            var elapsedMilliseconds = stopwatch.ElapsedMilliseconds; // الحصول على الوقت المستغرق بالمللي ثانية

            return $"Records added successfully in {elapsedMilliseconds} ms";
        }


        private string ReverseString(string input)
        {
            if (string.IsNullOrEmpty(input))
                return input; // إرجاع النص كما هو إذا كان فارغًا أو null

            char[] charArray = input.ToCharArray();
            Array.Reverse(charArray); // عكس المصفوفة
            return new string(charArray); // إرجاع النص المعكوس
        }

        public async Task<List<T1>> GetDataTimeWhereRecord(int number)
        {
            if (number <= 0)
            {
                return new List<T1>(); // إرجاع قائمة فارغة إذا كانت القيمة غير صحيحة
            }
            var records = await _dBContext.t1
           .Take(number)
           .ToListAsync();

            Console.WriteLine($"عدد السجلات المسترجعة: {records.Count}");

            return records;
        }


        public async Task<string> CreateTablesAsync(string table_name)
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
        }

        public async Task<string> CreateTable(string table_name)
        {
            var result = await CreateTablesAsync(table_name);
            if (result == "Table created successfully.")
                return "Table added successfully.";
            else
                return $"Failed to add table: {result}";
        }


        /* public async Task<string> CreateTablesAsync(string table_name)
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
         }*/

        /*   public async Task<string> CreateTablesAsync(string table_name)
           {
               string sql = $@"
         SELECT * INTO {table_name} FROM ramadan WHERE 1=0;";

               try
               {
                   _dBContext.Database.ExecuteSqlRaw(sql);
                   return "Table created successfully.";
               }
               catch (Exception ex)
               {
                   // يمكنك تسجيل الاستثناء هنا
                   return $"Error creating table: {ex.Message}";
               }
           }
           public async Task<string> CreateTable(string table_name)
           {
               var result = await CreateTablesAsync(table_name);
               if (result == "Table created successfully.")
                   return "Table added successfully.";
               else
                   return $"Failed to add table: {result}";
           }*/

        /*  public string CreateTables(string table_name)
          {
              string sql = $@"
               CREATE TABLE {table_name}(
                   Id INT PRIMARY KEY IDENTITY,
                   Name NVARCHAR(100),
                   Code NVARCHAR(50)
               )"
              ;

              _dBContext.Database.ExecuteSqlRawAsync(sql);
              return "Table created successfully.";
          }

          public async Task<string> CreateTable(string table_name)
          {
              var x = CreateTables(table_name);
              if (x == "Table created successfully.")
                  return "ok add successfully";
              else
                  return "no add why ttttt";
          }*/



        #endregion


    }
}
