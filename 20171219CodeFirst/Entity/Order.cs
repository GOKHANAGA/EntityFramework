using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Entity
{
    public class Order
    {
        public int OrderID { get; set; }

        public string CustomerID { get; set; }

        [ForeignKey("OrderEmployee")]
        public int? EmployeeID { get; set; }

        public DateTime? OrderDate { get; set; }
        public DateTime? RequiredDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        public int? ShipVia { get; set; }

        public double? Freight { get; set; }

        public string ShipCountry { get; set; }
        public string ShipName { get; set; }


        //NavigationProperty
        public  ICollection<OrderDetail> OrderDetail { get; set; }
        public Employee OrderEmployee { get; set; }
    }
}
