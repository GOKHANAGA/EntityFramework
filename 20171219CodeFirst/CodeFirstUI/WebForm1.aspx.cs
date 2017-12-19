using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CodeFirstUI
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           CodeFirstDbContext db = new CodeFirstDbContext();

           //db.Kategori.Add(new Category()
           //{
           //    CategoryName = "İçecek2",
           //    Description = "Açıklama2"
           //});
           //db.SaveChanges();
           //Product su = new Product();
           //su.CategoryID = 2;
           //su.ProductName = "Suasdsadsadsada";
           //su.ExpiredDate = DateTime.Now;
           //su.Cesit = "aaa";
           //su.UruneOzelKolon = 5;
           //db.Urun.Add(su);
           //db.SaveChanges();

           var result=db.Urun.FirstOrDefault();
           var result2 = db.Kategori.ToList();
                        
        }
    }
}