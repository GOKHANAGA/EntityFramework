using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    //Mapleme Yönetimi => Data Annotations
    //Attribute yazdığımda kendinden sonraki ilk satır için geçerli olur

    [Table("Urunler")]
    public class Product
    {
        //Primary Key ve Otomatik artan kolon
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int hilmi { get; set; }

        [MaxLength(50),Required,MinLength(10)]
        public string ProductName { get; set; }

        [ForeignKey("Kategori")]
        public int CategoryID { get; set; }

        //Bu property Veritabanında kolon olarak oluşmasın.
        [NotMapped]
        public Nullable<int> UruneOzelKolon { get; set; }


        //Sql de kolon adı Tip olsun ve veri tipi char olsun.
        [Column("Tip",TypeName="char"),MaxLength(50)]
        public string Cesit { get; set; }

        public DateTime ExpiredDate { get; set; }


        //Navigation Property
        public Category Kategori { get; set; }
    }
}
