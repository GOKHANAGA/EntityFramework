using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LINQ
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            List<string> names = new List<string>();
            names.Add("Ahmet");
            names.Add("Mehmet");
            names.Add("Ali");
            names.Add("Ayşe");
            names.Add("Fatma");

            string fName = string.Empty;

            List<string> foundNames = new List<string>();
            //for (int i = 0; i < names.Count;i++ )
            //{
            //    if (names[i].Contains("Ahmet"))
            //    {
            //        foundNames.Add(names[i]);
            //    }
            //}
            #region firstLastSingle
            ///*----ilk ahmet değerini getir----*/
            //fName = (from n in names
            //         where n == "Ahmet"
            //         select n).First();

            ///*---varsa ilkini yoksa o değerin default değerini(string için null,int için 0)----*/
            //fName = (from n in names
            //         where n == "Ahmet"
            //         select n).FirstOrDefault();

            ///*---Tek cevap dönecekse----*/
            //fName = (from n in names
            //         where n == "Ahmet"
            //         select n).Single();

            ///*----Tek cevap dönerse yakala bulamazsan o değerin default değerini kullan----*/
            //fName = (from n in names
            //         where n == "Ahmet"
            //         select n).SingleOrDefault();


            ///*----ToList()-->     okuduğunuz değerleri,o değerin tipinde liste olarak dönderir----*/
            ///*----ToArray()-->    okuduğunuz değerleri,o değerin tipinde array olarak dönderir----*/
            ///*----.Count-->       sorgudaki okuduğunuz değerlerin sayısını dönderir----*/
            ///*----Sum()           Gelen değerlerin toplamını dönderir----*/
            ///*----First()         Sorguya uyan ilk kaydı getirir. Eğer kayıt yoksa hata verir----*/
            ///*----Last()          Sorguya uyan son kaydı getirir.Eğer kayıt yoksa hata verir----*/
            ///*----Single()        Sorguya uyan tek kayıt varsa getirir.Birden fazla kayıt varsa ya da hiç kayıt yoksa hata verir----*/
            ///*----LastOrDefault() Sorguya uyan son kaydı getirir. Eğer kayıt yoksa hata verir----*/
            ///*----FirstOrDefault() Sorguya uyan ilk kaydı getirir.o değerin default değerini(string için null,int için 0) getirir----*/

            //foundNames = (from n in names
            //              where n == "Ahmet"
            //              select n).ToList();
            #endregion
            fName = names.Where(n => n.Contains("Ahmet")).FirstOrDefault();
            var numericResults = names.Where(c => c.Contains("Ahmet")).Count();

            GridView1.DataSource = names;
            GridView1.DataBind();
        }

        public object from { get; set; }
    }
}