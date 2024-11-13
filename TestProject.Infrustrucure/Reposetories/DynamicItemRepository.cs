using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using TestProject.Infrustrucure.Abstract;
using TestProject.Infrustrucure.Data;

namespace TestProject.Infrustrucure.Reposetories
{
    public class DynamicItemRepository : IDynamicItemRepository
    {
        private readonly ApplicationDBContext _applicationDBContext;

        public DynamicItemRepository(ApplicationDBContext applicationDBContext)
        {
            _applicationDBContext = applicationDBContext;
        }


        /*  public async Task<string> GetItemNameById(string tableName, int itemId)
          {
              // التحقق من أن tableName ليس فارغًا
              if (string.IsNullOrEmpty(tableName))
                  throw new ArgumentException("Table name cannot be null or empty.", nameof(tableName));

              // الحصول على DbSet بشكل ديناميكي باستخدام اسم الجدول
              var dbSetProperty = _applicationDBContext.GetType().GetProperty(tableName);
              if (dbSetProperty == null)
                  throw new ArgumentException($"Table '{tableName}' not found in the database context.");

              // تحويل DbSet إلى IQueryable ديناميكي
              var dbSet = dbSetProperty.GetValue(_applicationDBContext) as IQueryable<dynamic>;
              if (dbSet == null)
                  throw new InvalidOperationException($"DbSet '{tableName}' is not valid or not accessible.");

              // استخدام LINQ للبحث عن العنصر ديناميكيا باستخدام Reflection
              foreach (var item in dbSet)
              {
                  var idProperty = item.GetType().GetProperty("Id");
                  if (idProperty != null && (int)idProperty.GetValue(item) == itemId)
                  {
                      // الحصول على اسم العنصر
                      var nameProperty = item.GetType().GetProperty("Item");
                      return nameProperty != null ? nameProperty.GetValue(item)?.ToString() ?? "Unknown Item Name" : "Name property not found";
                  }
              }

              return "Item not found";
          }*/


        /*  public async Task<string> GetItemNameById(string tableName, int itemId)
          {
              // تأكد من أن tableName آمن قبل الإدراج في الاستعلام
              if (string.IsNullOrWhiteSpace(tableName))
              {
                  throw new ArgumentException("Table name is not provided.");
              }

              // صياغة استعلام SQL
              var query = $"SELECT Item FROM {tableName} WHERE Id = @itemId";

              // تنفيذ الاستعلام وإرجاع النتيجة
              var itemName = await _applicationDBContext
                  .Database
                  .SqlQueryRaw<string>(query, new SqlParameter("@itemId", itemId))
                  .FirstOrDefaultAsync();

              return itemName ?? "Unknown Item Name";
          }*/

        /*   public async Task<string> GetItemNameById(string tableName, int itemId)
           {
               // تأكد من أن tableName آمن قبل الإدراج في الاستعلام
               if (string.IsNullOrWhiteSpace(tableName))
               {
                   throw new ArgumentException("Table name is not provided.");
               }

               // صياغة استعلام SQL لاسترجاع اسم العنصر
               var query = $"SELECT Item FROM {tableName} WHERE Id = @itemId";

               // تنفيذ الاستعلام باستخدام EF Core مع SqlParameter
               var itemName = await _applicationDBContext
                   .Database
                   .SqlQuery<string>(query, new SqlParameter("@itemId", itemId))
                   .FirstOrDefaultAsync();

               return itemName ?? "Unknown Item Name";
           }*/

        /* public async Task<string> GetItemNameById(string tableName, int itemId)
         {
             // تأكد من أن tableName آمن قبل الإدراج في الاستعلام
             if (string.IsNullOrWhiteSpace(tableName))
             {
                 throw new ArgumentException("Table name is not provided.");
             }

             // صياغة استعلام SQL لاسترجاع اسم العنصر
             var query = $"SELECT Item FROM {tableName} WHERE Id = @itemId";

             // تنفيذ الاستعلام باستخدام FromSqlRaw مع SqlParameter
             var itemName = await _applicationDBContext
                 .Set<dynamic>() // يجب أن تستخدم DbSet<any type> أو dynamic
                 .FromSqlRaw(query, new SqlParameter("@itemId", itemId))
                 .FirstOrDefaultAsync();

             return itemName ?? "Unknown Item Name";
         }*/

        /*   public async Task<string> GetItemNameById(string tableName, int itemId)
           {
               // تأكد من أن tableName آمن قبل الإدراج في الاستعلام
               if (string.IsNullOrWhiteSpace(tableName))
               {
                   throw new ArgumentException("Table name is not provided.");
               }

               // صياغة استعلام SQL لاسترجاع اسم العنصر
               var query = $"SELECT Item FROM {tableName} WHERE Id = @itemId";

               // تنفيذ الاستعلام باستخدام EF Core مع SqlParameter
               var itemName = await _applicationDBContext
                   .Database
                   .SqlQueryRaw<string>(query, new SqlParameter("@itemId", itemId))
                   .FirstOrDefaultAsync();

               return itemName ?? "Unknown Item Name";
           }*/

        public class ItemResult
        {
            public string Item { get; set; }
        }

        public async Task<string> GetItemNameById(string tableName, int itemId)
        {
            // تحقق من أن tableName ليس فارغًا
            if (string.IsNullOrWhiteSpace(tableName))
            {
                throw new ArgumentException("Table name is not provided.");
            }

            // تحديد الاستعلام الذي سيتم تنفيذه
            var query = $"SELECT Item FROM {tableName} WHERE Id = @itemId";

            // استخدام SqlQueryRaw مع النوع المخصص
            var itemName = await _applicationDBContext
                .Database
                .SqlQueryRaw<ItemResult>(query, new SqlParameter("@itemId", itemId))
                .FirstOrDefaultAsync();  // استرجاع أول قيمة أو null إذا لم يتم العثور

            if (itemName == null)
            {
                return "Item not found";  // إرجاع رسالة إذا لم يتم العثور على العنصر
            }

            return itemName.Item ?? "Unknown Item Name";  // إرجاع اسم العنصر
        }











    }
}
