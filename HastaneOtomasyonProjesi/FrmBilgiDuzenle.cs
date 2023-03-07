using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace HastaneOtomasyonProjesi
{
    public partial class FrmBilgiDuzenle : Form
    {
        public FrmBilgiDuzenle()
        {
            InitializeComponent();
        }

        private void btnBilgiGüncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut1 = new SqlCommand("update Hastalar set HastaAd=@p1,HastaSoyad=@p2,HastaTelefon=@p3,HastaSifre=@p4,HastaCinsiyet=@p5 where HastaTC=@p6 ", bgl.baglanti());
            komut1.Parameters.AddWithValue("@p1", txtAd.Text);
            komut1.Parameters.AddWithValue("@p2", txtSoyad.Text);
            komut1.Parameters.AddWithValue("@p3", mskTelefon.Text);
            komut1.Parameters.AddWithValue("@p4", txtSifre.Text);
            komut1.Parameters.AddWithValue("@p5", cmbCinsiyet.Text);
            komut1.ExecuteNonQuery();
            bgl.baglanti().Close();

           

        }
        //hasta detay formu yüklendiğinde buraya tc taşı
        public string TCno;
        sqlbaglantisi bgl = new sqlbaglantisi();//bağlantı sınıfımı çağırdım.
        private void FrmBilgiDuzenle_Load(object sender, EventArgs e)
        {
            //verileri bu bilgi düzenle formuna taşımam gerek.
            mskTcno.Text = TCno;
            SqlCommand komut = new SqlCommand("Select * From where HastalarTC=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", mskTcno);
            SqlDataReader dr = komut.ExecuteReader();
            while(dr.Read())
            {
                txtAd.Text = dr[1].ToString();
                txtSoyad.Text = dr[2].ToString();
                mskTelefon.Text = dr[3].ToString();
                txtSifre.Text = dr[4].ToString();
                cmbCinsiyet.Text = dr[5].ToString();
            }
            bgl.baglanti().Close();

        }
    }
}
