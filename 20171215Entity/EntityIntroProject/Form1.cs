using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EntityIntroProject
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        NORTHWNDEntities dB = new NORTHWNDEntities();

        private void btnQuan_Stock_Click(object sender, EventArgs e)
        {
            lst.DataSource = dB.Products.Join(dB.Order_Details, p => p.ProductID, od => od.ProductID, (p, od) => new
            {
                /*21.Sipariş verilip de stoğumun yetersiz olduğu ürünler hangisidir? Bu ürünlerden kaç tane eksiğim vardır?*/
                product = p.ProductName,
                missingAmount = od.Quantity - p.UnitsInStock
            }).Where(a => a.missingAmount > 0).ToList();

        }

        private void btnShipDays_Click(object sender, EventArgs e)
        {
            /*20.Satışlarımı kaç günde teslim etmişim?*/
            lst.DataSource = dB.Orders.Select(o => new
            {
                order = o.OrderID,
                preparedDay = DbFunctions.DiffDays(o.OrderDate,o.ShippedDate)
            }).ToList();
        }

        private void btnAboveAverage_Click(object sender, EventArgs e)
        {
            /**19.Ortalama satış miktarının üzerine çıkan satışlarım hangisi?*/
            lst.DataSource = dB.Order_Details.Where(od => od.Quantity > dB.Order_Details.Average(avg => avg.Quantity)).Select(od => new 
            {
                quantity=od.Quantity,
                id=od.OrderID
            }).ToList();
        }

        private void btnLate_Click(object sender, EventArgs e)
        {
            /*18.Zamanında teslim edemediğim siparişlerim ID’leri nelerdir ve kaç gün geç göndermişim?*/
            lst.DataSource = dB.Order_Details.Where(o => DbFunctions.DiffDays(o.Orders.RequiredDate, o.Orders.ShippedDate) > 0).Select(p => new
            {
                productName = p.OrderID,
                delayedDays = DbFunctions.DiffDays(p.Orders.RequiredDate, p.Orders.ShippedDate)
            }).Distinct().ToList();
        }

        private void btnCountryOrders_Click(object sender, EventArgs e)
        {
            //SELECT c.Country, SUM(Quantity*UnitPrice) from [Order Details] od
            //                    INNER JOIN Orders o ON od.OrderID=o.OrderID
            //                    INNER JOIN Customers c ON o.CustomerID =c.CustomerID
            //                    GROUP BY c.Country

            /*17.Hangi ülkelere ne kadarlık satış yapmışım?*/

            lst.DataSource = dB.Order_Details.GroupBy(od => new
            {
                countries = od.Orders.Customers.Country,
            }).Select(od => new
            {
                country = od.Key.countries,
                amount = od.Sum(a => a.Quantity * a.UnitPrice)
            }).ToList();
        }

        private void btnBestBuyer_Click(object sender, EventArgs e)
        {
            //16.Hangi müşteriler para bazında en fazla hangi ürünü almışlar?
            lst.DataSource=dB.Order_Details.OrderBy(od => new
            {
                customerNames = od.Orders.Customers.CompanyName
            }).ToList();
        }

        private void btnvalCus_Click(object sender, EventArgs e)
        {
            /*15.En değerli müşterim hangisi? (en fazla satış yaptığım müşteri)*/
            /*
             * select top 1 c.CompanyName,count(o.OrderID) as sells from orders o
		        Inner join Customers c on o.CustomerID = c.CustomerID 
		        group by c.CompanyName
		        order by sells desc
             */

            lst.DataSource = dB.Orders.GroupBy(o => new
            {
                customerName = o.Customers.CompanyName
            }).OrderByDescending(o => new
            {
                amount = o.Count()
            }).Select(or => new
            {
                cName = or.Key.customerName,
                counted = or.Count()

            }).Take(1).ToList();
        }

        private void btnSupPro_Click(object sender, EventArgs e)
        {
            /*
             * 14.Hangi tedarikçiden aldığım ürünlerden ne kadar satmışım?
             * 
             * select s.CompanyName,sum(Quantity) from [Order Details] od
								inner join Products p on od.ProductID=p.ProductID
								inner join Suppliers s on p.SupplierID=s.SupplierID 
								group by s.CompanyName
             */

            lst.DataSource = dB.Order_Details.GroupBy(od => new
            {
                compName = od.Products.Suppliers.CompanyName
            }).Select(od => new
            {
                supName = od.Key.compName,
                sum = od.Sum(or => or.Quantity)
            }).ToList();
        }

        private void btnToast_Click(object sender, EventArgs e)
        {
            /*13.Tost yapmayı seven çalışanım hangisi? (Basit bir like sorgusu)
             * 
             * select FirstName from Employees Where Notes like '%Toast%' 
             */
            lst.DataSource = dB.Employees.Where(em => em.Notes.Contains("toast")).Select(emp => emp.FirstName + " " + emp.LastName).ToList();
        }

        private void btnSupPay_Click(object sender, EventArgs e)
        {
            /*12.Hangi kargo şirketine toplam ne kadar ödeme yapmışım?
             * 
             * select s.CompanyName,sum(o.Freight) from Orders o
			                inner join Shippers s on o.ShipVia=s.ShipperID
			                group by s.CompanyName
             */

            lst.DataSource = dB.Orders.GroupBy(or => new
            {
                shipVia = or.Shippers.CompanyName
            }
            ).Select(or => new
            {
                shipperName = or.Key.shipVia,
                sum = or.Sum(o => o.Freight)
            }).ToList();
        }

        private void btnEmpPro_Click(object sender, EventArgs e)
        {
            /*10.Çalışanlar ürün bazında ne kadarlık satış yapmışlar?
             * 
             * select e.FirstName,p.ProductName,sum(od.Quantity) from [Order Details] od
			            inner join Products p on p.ProductID=od.ProductID
			            inner join Orders o on o.OrderID=od.OrderID
			            inner join Employees e on o.EmployeeID = e.EmployeeID
			            group by  e.FirstName,p.ProductName
             */

            lst.DataSource = dB.Order_Details.GroupBy(od => new
            {
                empName = od.Orders.Employees.FirstName + " " + od.Orders.Employees.LastName,
                proName = od.Products.ProductName
            }).Select(od => new
            {
                employeeName = od.Key.empName,
                proName = od.Key.proName,
                totalSell = od.Sum(o => o.Quantity)
            }).ToList();
        }

        private void btnCatSel_Click(object sender, EventArgs e)
        {
            /*9.Ürün kategorilerine göre satışlarım nasıl? (sayı bazında)
             * 
             * select c.CategoryName,sum(od.Quantity) from [Order Details] od
			                inner join Products p on p.ProductID = od.ProductID
			                inner join Categories c on c.CategoryID = p.CategoryID
			                group by c.CategoryName
             * 
             */

            lst.DataSource = dB.Order_Details.GroupBy(od => new
            {
                category = od.Products.Categories.CategoryName
            }).Select(catOd => new
            {
                category = catOd.Key.category,
                totalSell = catOd.Sum(od => od.Quantity)
            }).ToList();
        }

        private void btnCatSelMoney_Click(object sender, EventArgs e)
        {
            /*8.Ürün kategorilerine göre satışlarım nasıl? (para bazında)
             * 
             * select c.CategoryName,sum(od.Quantity*od.UnitPrice) from [Order Details] od
			        inner join Products p on p.ProductID = od.ProductID
			        inner join Categories c on c.CategoryID = p.CategoryID
			        group by c.CategoryName
             */
            lst.DataSource = dB.Order_Details.GroupBy(od => new
            {
                category = od.Products.Categories.CategoryName
            }).Select(catOd => new
            {
                category = catOd.Key.category,
                totalSell = catOd.Sum(od => od.Quantity*od.UnitPrice)
            }).ToList();
        }

        private void btnProSell_Click(object sender, EventArgs e)
        {
            /*7.Ürünlere göre satışım nasıl?
             * 
             * select p.ProductName,sum(od.Quantity) from [Order Details] od
			                inner join Products p on p.ProductID = od.ProductID
			                inner join Categories c on c.CategoryID = p.CategoryID
			                group by p.ProductName
             * 
             */

            lst.DataSource = dB.Order_Details.GroupBy(od => new
            {
                productName = od.Products.ProductName
            }).Select(proOd => new
            {
                productName = proOd.Key.productName,
                totalSell = proOd.Sum(productOd => productOd.Quantity)
            }).ToList();
        }

        private void btnExpCountry_Click(object sender, EventArgs e)
        {
            /*6.Hangi ülkelere ihracat yapıyorum?
             * 
             * select DISTINCT c.Country from Orders o
			        inner join Customers c on o.CustomerID = c.CustomerID
             */

            lst.DataSource = dB.Orders.Select(or => or.Customers.Country).Distinct().ToList();
        }

        private void btnEmpSel_Click(object sender, EventArgs e)
        {
            //5.Çalışanlarım ne kadarlık satış yapmışlar?

            //select e.FirstName,sum(od.Quantity) from [Order Details] od
            //      inner join Orders o on od.OrderID = o.OrderID
            //      inner join Employees e on o.EmployeeID =e.EmployeeID
            //      group by e.FirstName

            lst.DataSource = dB.Order_Details.GroupBy(od => new
            {
                empName = od.Orders.Employees.FirstName + " " + od.Orders.Employees.LastName
            }).Select(empSel => new
            {
                empName = empSel.Key.empName,
                Sold_Quantity = empSel.Sum(em => em.Quantity)
            }).ToList();
        }

        private void btnEmpMan_Click(object sender, EventArgs e)
        {
            //4.Hangi çalışanım hangi çalışanıma bağlı?

            //select e.FirstName as Low,em.FirstName as Up from Employees e
            //          inner join Employees em on e.ReportsTo = em.EmployeeID

            lst.DataSource = dB.Employees.Select(emp => new
            {
                empName = emp.FirstName + " " + emp.LastName,
                empManager = emp.Employees2.FirstName + " " + emp.Employees2.LastName
            }).ToList();
        }

        private void btnEmpBirthDay_Click(object sender, EventArgs e)
        {
            //3.Bugün doğum günü olan çalışanlarım kimler?


            //select * from Employees
            //          where DAY(BirthDate) = DAY(getdate()) AND MONTH(BirthDate) = MONTH(getdate())

            lst.DataSource = dB.Employees.Where(emp => emp.BirthDate.Value.Day.Equals(DateTime.Now.Day) && emp.BirthDate.Value.Month.Equals(DateTime.Now.Month)).Select(em => em.FirstName +" "+ em.LastName).ToList();

        }

        private void btn1997_Click(object sender, EventArgs e)
        {
            //2.1997'de tüm cirom ne kadar?
    
            //select sum(od.Quantity*(od.UnitPrice-(od.UnitPrice*od.Discount))) from [Order Details] od
            //                inner join Orders o on od.OrderID = o.OrderID
            //                Where Year(o.OrderDate) = 1997

            var result = dB.Order_Details.Where(o => o.Orders.OrderDate.Value.Year == 1997).Sum(od => (od.Quantity * od.UnitPrice));
            lst.DataSource = null;
            lst.Items.Add(result);
        }

        private void btnTotal_Click(object sender, EventArgs e)
        {
            //1.Tüm cirom ne kadar?
            //select sum(od.Quantity*(od.UnitPrice-(od.UnitPrice*od.Discount))) from [Order Details] od
            //                inner join Orders o on od.OrderID = o.OrderID

            var result = dB.Order_Details.Sum(od => (od.Quantity * od.UnitPrice));
            lst.DataSource = null;
            lst.Items.Add(result);

        }
    }
}
