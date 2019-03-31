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
            //1950 ve 1961 aralığında doğmuş çalışanların ismi ve soyismi doğum tarihleriyle listelensin.
            //dataGridView1.DataSource = db.Employees.Where(x => SqlFunctions.DatePart("Year", x.BirthDate) >= 1950 && SqlFunctions.DatePart("Year", x.BirthDate) <= 1961).Select(x => new
            //{
            //    x.FirstName,
            //    x.LastName,
            //    x.BirthDate
            //}).ToList();
            #endregion
        }
    }
}
