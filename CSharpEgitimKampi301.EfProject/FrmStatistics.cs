using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSharpEgitimKampi301.EfProject
{
    public partial class FrmStatistics : Form
    {
        public FrmStatistics()
        {
            InitializeComponent();
        }
        EgitimKampiEfTravelDbEntities db = new EgitimKampiEfTravelDbEntities();

        private void FrmStatistics_Load(object sender, EventArgs e)
        {
            //Lokasyon Sayısı
            lblLocationCount.Text = db.Location.Count().ToString();

            //Toplam Kapasite
            lblSumCapacity.Text = db.Location.Sum(x => x.Capacity).ToString();

            //Rehber Sayısı
            lblGuideCount.Text = db.Guide.Count().ToString();

            //Ortalama Kapasite
            lblAverageCapacity.Text = db.Location.Average(x => x.Capacity ?? 0).ToString("F2");

            //Ortalama Tur Fiyatı
            lblAverageTourCapacity.Text = db.Location.Average(x => x.Price ?? 0).ToString("N2");

            //Eklenen Son ülke 
            lblLastCountry.Text = db.Location.OrderByDescending(x => x.LocationId).Select(x => x.Country).FirstOrDefault();

            //Kapadokya Tur Kapasitesi
            lblCappadociaTourCapacity.Text = db.Location.Where(x => x.City == "Kapadokya").Select(y => y.Capacity).FirstOrDefault().ToString();

            //Türkiye Ortalama Tur KApasitesi
            lblTrAverageCapacity.Text = db.Location.Where(x => x.Country == "Turkiye").Average(y => y.Capacity ?? 0).ToString("F2");

            //Roma Gezi Rehberi
            lblRomaGuide.Text = (from location in db.Location
                                 join guide in db.Guide
                                 on location.GuideId equals guide.GuideId
                                 where location.City == "Roma"
                                 select guide.GuideName + " " + guide.GuideSurname).FirstOrDefault() ?? "Rehber Bulunamadı!";

            //En Yüksek Kapasiteli Tur
            lblHighestCapacity.Text = db.Location.Where(x => x.Capacity == db.Location.Max(y => y.Capacity)).Select(z => z.City).FirstOrDefault()?.ToString();


            //En Yüksek Pahalı Tur
            lblExpensiveTour.Text = db.Location.Where(x => x.Price == db.Location.Max(y => y.Price)).Select(z => z.City).FirstOrDefault()?.ToString();


            //Ali Çınar Tur Sayısı
            lblGuideTourCount.Text = (from location in db.Location
                                      join guide in db.Guide
                                      on location.GuideId equals guide.GuideId
                                      where guide.GuideName == "Ali" && guide.GuideSurname == "Çınar"
                                      select location).Count().ToString();










        }
    }
}
