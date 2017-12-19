using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class CodeFirstDbContext:DbContext
    {
       //// 1
       // public CodeFirstDbContext():base("conn")
       // {

       // }
       // //2
       // public CodeFirstDbContext():base("name=conn")
       // {

       // }
        //3

        public CodeFirstDbContext() : base("Server=.;Database=MyCodeFirstDB;Integrated Security=True;")
        {

        }

        //DbSet içine yazdığım veritabanında oluşacak tablo adı, Aynı tabloya karşılık gelen, code behind tarafında kullanacağım ise property adı

        //Default olarak tablo adlarına s takısı ekliyor.

        public DbSet<Product> Urun { get; set; }
        public DbSet<Category> Kategori { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<OrderDetail> OrderDetail { get; set; }       

        //s takısını silmek için base classta bulunan OnModelCreating metodunu override ettik. ve Metot içerisinde s takısını ekleyen kuralı sildik.
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
