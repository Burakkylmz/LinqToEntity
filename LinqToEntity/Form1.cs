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
            //dataGridView1.DataSource = db.Categories.ToList();
            #endregion

            #region
            //Soru: Çalışnaların tablosundan isim, soyisim, ünvan ve doğum tarihlerini listele
            //dataGridView1.DataSource = db.Employees.Select(x => new
            //{
            //    x.FirstName,
            //    x.LastName,
            //    x.Title,
            //    x.BirthDate
            //}).ToList();
            #endregion

            #region
            //Çalışan id'si 2 ile 8 arasında olan çalışanları A-Z'ye olacak şekilde isimlerine göre sıralayınız
            dataGridView1.DataSource = db.Employees.Where(x => x.EmployeeID > 2 && x.EmployeeID <= 8).OrderBy(x => x.FirstName).ToList();
            #endregion

            #region
            //60 yaşından büyük olan çalışanları listeleyin
            //dataGridView1.DataSource = db.Employees.Where(x=> SqlFunctions.DateDiff("Year",x.BirthDate,DateTime.Now)>60).Select(x => new
            //{
            //    x.FirstName,
            //    x.LastName,
            //    x.Title,
            //    x.BirthDate
            //}).ToList();
            #endregion

            #region
            //1960 yılında doğan çalışanları listeleyin
            //dataGridView1.DataSource = db.Employees.Where(x=> SqlFunctions.DatePart("Year",x.BirthDate)==1960).Select(x => new
            //{
            //    x.FirstName,
            //    x.LastName,
            //    x.Title,
            //    x.BirthDate
            //}).ToList();
            #endregion

            #region,
            //1950 ve 1961 aralığında doğmuş çalışanların ismi ve soyismi doğum tarihleriyle listelenyin
            //dataGridView1.DataSource = db.Employees.Where(x => SqlFunctions.DatePart("Year", x.BirthDate) >= 1950 && SqlFunctions.DatePart("Year", x.BirthDate) <= 1961).Select(x => new
            //{
            //    x.FirstName,
            //    x.LastName,
            //    x.BirthDate
            //}).ToList();
            #endregion

            #region
            //Ünvanı Mr. olan ve yaşı 60'tan büyük olan çalışanları listeleyin
            //dataGridView1.DataSource = db.Employees.Where(x => x.TitleOfCourtesy=="Mr." && SqlFunctions.DateDiff("Year", x.BirthDate, DateTime.Now) > 60).Select(x => new
            //{
            //    x.FirstName,
            //    x.LastName,
            //    x.Title,
            //    x.BirthDate
            //}).ToList();
            #endregion

            #region
            //Çalışanların firstname, lastname, titleofcourtesy ve age ekrana getirilsin. yaşa göre azalan şekilde sıralansın
            dataGridView1.DataSource = db.Employees.OrderBy(x => SqlFunctions.DateDiff("Year", x.BirthDate, DateTime.Now)).Select(x => new
            {
                x.FirstName,
                x.LastName,
                x.Title,
                Yas= SqlFunctions.DateDiff("Year", x.BirthDate, DateTime.Now)
            }).ToList();
            #endregion
        }
    }
}
