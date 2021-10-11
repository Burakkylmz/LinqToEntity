using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.SqlServer;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LinqToEntity
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        NorthwindEntities db = new NorthwindEntities();

        private void btnGetData_Click(object sender, EventArgs e)
        {
            #region
            //Soru: Bütün kategorileri Listele
            dataGridView1.DataSource = db.Categories.ToList();
            #endregion

            #region
            //Soru: Çalışnaların tablosundan isim, soyisim, ünvan ve doğum tarihlerini listele
            dataGridView1.DataSource = db.Employees.Select(x => new
            {
                x.FirstName,
                x.LastName,
                x.Title,
                x.BirthDate
            }).ToList();
            #endregion

            #region
            //Çalışan id'si 2 ile 8 arasında olan çalışanları A-Z'ye olacak şekilde isimlerine göre sıralayınız
            dataGridView1.DataSource = db.Employees.Where(x => x.EmployeeID > 2 && x.EmployeeID <= 8)
                                                   .OrderBy(x => x.FirstName)
                                                   .ToList();
            #endregion

            #region
            //60 yaşından büyük olan çalışanları listeleyin
            dataGridView1.DataSource = db.Employees.Where(x => SqlFunctions.DateDiff("Year", x.BirthDate, DateTime.Now) > 60)
                                                   .Select(x => new
                                                   {
                                                       x.FirstName,
                                                       x.LastName,
                                                       x.Title,
                                                       x.BirthDate
                                                   }).ToList();
            #endregion

            #region
            //1960 yılında doğan çalışanları listeleyin
            dataGridView1.DataSource = db.Employees.Where(x => SqlFunctions.DatePart("Year", x.BirthDate) == 1960)
                                                   .Select(x => new
                                                   {
                                                       x.FirstName,
                                                       x.LastName,
                                                       x.Title,
                                                       x.BirthDate
                                                   }).ToList();
            #endregion

            #region,
            //1950 ve 1961 aralığında doğmuş çalışanların ismi ve soyismi doğum tarihleriyle listelenyin
            dataGridView1.DataSource = db.Employees.Where(x => SqlFunctions.DatePart("Year", x.BirthDate) >= 1950 && 
                                                               SqlFunctions.DatePart("Year", x.BirthDate) <= 1961)
                                                   .Select(x => new
                                                   {
                                                       x.FirstName,
                                                       x.LastName,
                                                       x.BirthDate
                                                   }).ToList();
            #endregion

            #region
            //Ünvanı Mr. olan ve yaşı 60'tan büyük olan çalışanları listeleyin
            dataGridView1.DataSource = db.Employees.Where(x => x.TitleOfCourtesy == "Mr." && SqlFunctions.DateDiff("Year", x.BirthDate, DateTime.Now) > 60)
                                                   .Select(x => new
                                                   {
                                                       x.FirstName,
                                                       x.LastName,
                                                       x.Title,
                                                       x.BirthDate
                                                   }).ToList();
            #endregion

            #region
            //Çalışanların firstname, lastname, titleofcourtesy ve age ekrana getirilsin. yaşa göre azalan şekilde sıralansın
            dataGridView1.DataSource = db.Employees.OrderBy(x => SqlFunctions.DateDiff("Year", x.BirthDate, DateTime.Now))
                                                   .Select(x => new
                                                   {
                                                       x.FirstName,
                                                       x.LastName,
                                                       x.Title,
                                                       Yas = SqlFunctions.DateDiff("Year", x.BirthDate, DateTime.Now)
                                                   }).ToList();
            #endregion

            #region
            //Doğum tarihi 1930 ile 1960 arasında olup da USA'da çalışanları listeleyeceğiz
            dataGridView1.DataSource = db.Employees.Where(x => SqlFunctions.DatePart("Year", x.BirthDate) >= 1930 && 
                                                               SqlFunctions.DatePart("Year", x.BirthDate) <= 1960 && 
                                                               x.Country == "USA")
                                                  .Select(x => new
                                                   {
                                                       x.FirstName,
                                                       x.LastName,
                                                       x.Title,
                                                       x.Country,
                                                       x.BirthDate
                                                   }).ToList();
            #endregion

            #region
            //Ünvanı mr veya dr olanların listlenmesi
            dataGridView1.DataSource = db.Employees.Where(x => x.TitleOfCourtesy == "Mr." || x.TitleOfCourtesy == "Dr.").Select(x => new
            {
                x.TitleOfCourtesy,
                x.FirstName,
                x.LastName
            }).ToList();
            #endregion

            #region
            //Ürünlerin  birim fiyatları 18,19 veya 25 olanları listeleyin
            dataGridView1.DataSource = db.Products.Where(x => x.UnitPrice == 18 || x.UnitPrice == 19 || x.UnitPrice == 25)
                                                  .OrderByDescending(x => x.UnitPrice)
                                                  .Select(x => new
                                                  {
                                                      x.ProductName,
                                                      x.UnitPrice
                                                  }).ToList();
            #endregion

            #region
            //Ismi içerisinde "A" harfi geçen çallışanları listeleyin
            dataGridView1.DataSource = db.Employees.Where(x => x.FirstName.Contains("A")).Select(x => new
            {
                x.FirstName,
                x.LastName,
                x.Title
            }).ToList();
            #endregion


            #region
            //Products tablosundan ProductID, ProductName, CategoryID, Categories tablosundan CategoryName, Description
            dataGridView1.DataSource = db.Products.Join(db.Categories, p => p.CategoryID, c => c.CategoryID, (p, c) => new
            {
                p.ProductID,
                p.ProductName,
                p.CategoryID,
                p.Category.CategoryName
            }).ToList();
            #endregion

            #region
            dataGridView1.DataSource = db.Customers
                .Join(db.Orders,
                   c => c.CustomerID,
                   o => o.CustomerID,
                   (c, o) => new { c, o })
                .Join(db.Employees,
                   or => or.o.EmployeeID,
                   em => em.EmployeeID,
                   (or, em) => new { or, em }).Select(z => new
                   {
                       z.or.o.OrderID,
                       z.or.o.OrderDate,
                       z.or.c.CompanyName,
                       z.em.FirstName,
                       z.em.LastName,
                       z.em.Title
                   }).ToList();
            #endregion

            #region
            //Kategorilerine göre toplam stok miktarını lsiteleyiniz
            dataGridView1.DataSource = db.Categories.Join(db.Products, p => p.CategoryID, c => c.CategoryID, (p, c) => new { p, c })
                                                    .GroupBy(x => x.p.CategoryName)
                                                    .Select(y => new
                                                    {
                                                        Name = y.Key,
                                                        Count = y.Sum(p => p.c.UnitsInStock)
                                                    }).ToList();
            #endregion

            #region
            //Çalışanlar ne kadarlık satış yaptığını listeleyin
            dataGridView1.DataSource = db.Employees
                .Join(db.Orders, emp => emp.EmployeeID, ord => ord.EmployeeID, (ord, emp) => new { ord, emp })
                .Join(db.Order_Details, od => od.emp.OrderID, de => de.OrderID, (od, de) => new { od, de })
                .GroupBy(x=> x.od.ord.FirstName)
                .Select(z => new
                {
                    Name=z.Key,
                    Count=z.Sum(y=> y.de.Quantity*y.de.UnitPrice)
                }).ToList();
            #endregion

            #region
            //Ürünlere göre satış miktarını listeleyiniz
            dataGridView1.DataSource = db.Products
                .Join(db.Order_Details, od => od.ProductID, p => p.ProductID, (od, p) => new { od, p })
                .GroupBy(x => x.od.ProductName)
                .OrderByDescending(q => q.Sum(y => y.p.Quantity * y.p.UnitPrice))
                .Select(z => new
                {
                    Urun_Adı = z.Key,
                    Miktar = z.Sum(y => y.p.Quantity),
                    Gelir = z.Sum(y => y.p.Quantity * y.p.UnitPrice)
                }).ToList();
            #endregion

            #region
            //Ürün kategorilerine göre satışların ne kadar olduğunu listeleyin
            dataGridView1.DataSource = db.Categories
                .Join(db.Products, c => c.CategoryID, p => p.CategoryID, (c, p) => new { c, p })
                .Join(db.Order_Details, od => od.p.ProductID, p => p.ProductID, (od, p) => new { od, p })
                .GroupBy(x => x.od.c.CategoryName)
                .OrderBy(y => y.Sum(q => q.p.Quantity * q.p.UnitPrice))
                .Select(z => new
                {
                    Category_Name = z.Key,
                    Outcome = z.Sum(q=> q.p.Quantity*q.p.UnitPrice)
                }).ToList();
            #endregion
        }
    }
}
