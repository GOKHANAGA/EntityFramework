using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityManuelMigration
{
    public class MyDbContext : DbContext
    {
        public MyDbContext():base ("Server=.;Database=MyDb;Trusted_Connection=true")
        {
            Database.SetInitializer<MyDbContext>(new DropCreateDatabaseIfModelChanges<MyDbContext>());
        }

        public DbSet<Kategori> Kategoriler { get; set; }
        public DbSet<Urun> Urunler { get; set; }



    }
}
