using System.Diagnostics;
using TestProject.Data.Entity;
using TestProject.Infrustrucure.Abstract;
using TestProject.Service.Abstract;

namespace TestProject.Service.Implemention
{
    public class T1Service : IT1Service
    {
        private readonly IT1Reposatory _t1Reposatory;
        #region Field
        #endregion

        #region Constractor
        public T1Service(IT1Reposatory t1Reposatory)
        {
            _t1Reposatory = t1Reposatory;
        }


        #endregion


        #region Handle Fuction
        public async Task<List<T1>> GetDataAll()
        {
            var getall = await _t1Reposatory.GetDataAll();
            return getall;
        }


        public async Task<T1> GetByid(int id)
        {
            var x = await _t1Reposatory.GetByIdAsync(id);
            return x;
        }

        public async Task<string> Create(T1 t1)
        {

            var nameExist = await _t1Reposatory.GetByNameAsync(t1.name);

            if (nameExist == "ok not found")
            {

                var x = await _t1Reposatory.Create(t1);
                if (x == "ok")
                {
                    return "Success";
                }
                else
                {
                    return "No Insert Data";
                }
            }
            else
            {
                return "The Name Exist";
            }

        }



        public async Task<string> update(T1 t1)
        {
            return await _t1Reposatory.update(t1);
            var nameExist = await _t1Reposatory.GetByNameAsync(t1.name);


            if (nameExist == "ok not found")
            {

                var x = await _t1Reposatory.update(t1);
                if (x == "ok")
                {
                    return "Success";
                }
                else
                {
                    var nameAndIdExist = await _t1Reposatory.GetByNameAndByIdAsync(t1.name, t1.id);
                    if (nameAndIdExist == "ok not found")
                    {

                        var up = await _t1Reposatory.update(t1);
                        if (up == "ok")
                        {
                            return "Success";
                        }
                    }

                    return "Field";

                }
            }
            else
            {
                return "The Name Exist";
            }
        }

        public async Task<string> Delete(int id)
        {

            var result = await _t1Reposatory.Delete(id);
            return result; // إرجاع نتيجة الحذف مباشرة



            /* var result = await _t1Reposatory.Delete(id);

             if (result == "Entity not found")
             {
                 return "Entity not found in repository"; // توضيح الرسالة
             }

             return "Success"; // رسالة النجاح*/



            /*var x = await _t1Reposatory.Delete(id);
            if (x != "Success")
            {
                return "not found Repository";
            }
            return "Success";*/
        }

        public async Task<string> CreateMUlty(int v)
        {
            return await _t1Reposatory.CreateMulty(v);
        }


        public async Task<string> UpdateMulty(string fieldName)
        {


            /* foreach (var record in records)
             {
                 await _t1Reposatory.UpdateMulty(record);
             }*/
            return await _t1Reposatory.UpdateMulty(fieldName);

        }

        public async Task<(List<T1> records, long elapsedMilliseconds)> GetDataAllTime()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var records = await _t1Reposatory.GetDataAll();

            stopwatch.Stop();
            var elapsedMilliseconds = stopwatch.ElapsedMilliseconds;

            return (records, elapsedMilliseconds);
        }

        public async Task<(List<T1> records, long elapsedMilliseconds)> GetDataTimeWhereRecord(int number)
        {
            if (number <= 0)
            {
                return (new List<T1>(), 0); // إرجاع قائمة فارغة ووقت استجابة 0
            }
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            // استدعاء الدالة من repository
            var records = await _t1Reposatory.GetDataTimeWhereRecord(number);

            stopwatch.Stop();
            var elapsedMilliseconds = stopwatch.ElapsedMilliseconds;

            // إرجاع السجلات ووقت الاستجابة
            return (records, elapsedMilliseconds);
        }

        public async Task<string> CreateTable(string table_name)
        {

            var e = await _t1Reposatory.CreateTable(table_name);
            if (e == "Table added successfully.")
            {
                return "okok";
            }
            return "nono";

        }






        #endregion

    }
}
