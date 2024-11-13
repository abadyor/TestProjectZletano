using Microsoft.EntityFrameworkCore;
using TestProject.Data.Entity;

namespace TestProject.Infrustrucure.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {

        }

        //public DbSet<T1> t1s { get; set; }
        public DbSet<T1> t1 { get; set; }
        public DbSet<T2> t2 { get; set; }
        public DbSet<T3> t3 { get; set; }

        public DbSet<PeapleBusnise> peapleBusnise { get; set; }

        public DbSet<Vendor> vendors { get; set; }

        public DbSet<ControlTable> controlTables { get; set; }

        public DbSet<Customer> customers { get; set; }

        public DbSet<Basket> basket { get; set; }

        public DbSet<Basket_s> basket_s { get; set; }




    }
}
