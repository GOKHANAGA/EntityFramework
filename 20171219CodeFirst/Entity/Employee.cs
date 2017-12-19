using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class Employee
    {
        public int EmployeeID { get; set; }
        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public string TitleOfCourtesy { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime? HireDate { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        [ForeignKey("Reports")]
        public int? ReportsTo { get; set; }


        /*Navigation Property*/
        public Employee Reports { get; set; }
        public ICollection<Order> EmployeeOrders { get; set; } 

    }
}
