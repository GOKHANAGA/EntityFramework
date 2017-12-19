using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityManuelMigration
{
    public class Urun
    {
        public int UrunID { get; set; }
        public string UrunAdi { get; set; }

        public int KategoriID { get; set; }

        public byte[] Picture { get; set; }
        public Kategori Kategori { get; set; }
    }
}
