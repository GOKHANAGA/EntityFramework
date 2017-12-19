using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Entity
{
    public class OrderDetail
    {
        [Key]
        [Column(Order = 1)]
        public int OrderID { get; set; }

        [Key]
        [Column(Order=2)]
        public int ProductID { get; set; }

        public double? UnitPrice { get; set; }

        public int? Quantity { get; set; }

        public double Discount { get; set; }


        //Navigation Properties
        public Product OrderProduct { get; set; }
        public Order DetailedOrder { get; set; }
    }
}
