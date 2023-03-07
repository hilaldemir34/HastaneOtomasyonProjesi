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
    public partial class FrmHastaGiris : Form
    {
        public FrmHastaGiris()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl = new sqlbaglantisi();
        private void FrmHastaGiris_Load(object sender, EventArgs e)
        {

        }
        
        private void lnkUyeOl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmHastaKayıt fr = new FrmHastaKayıt();
            fr.Show();
            this.Hide();
        }

        private void btnGiris_Click(object sender, EventArgs e)
        {
            SqlCommand komut1 = new SqlCommand("Select * From Hastalar where HastaTC=@p1 and HastaSifre=@p2", bgl.baglanti());
            komut1.Parameters.AddWithValue("@p1", mskTC.Text);//değerlerimizi buraya atadık
            komut1.Parameters.AddWithValue("@p2", txtSifre.Text);
            SqlDataReader dr = komut1.ExecuteReader();//komuttan gelen değerle birlikte okuyucuyu çalıştır
            if(dr.Read()) //dr işlemini doğru bir şekilde okuma işlemi gerçekleşirse. Verilerin doğru olup olmadığını kontrol eder. while verileri okuyup yazdırır
            {
                FrmHastaDetay fr = new FrmHastaDetay();
                fr.tc = mskTC.Text;//burdaki tc kimlik numarasını hasta detaydaki tc kimlik numarasına atadım.ve hasta detaya yazdırdım
                fr.Show();
                fr.Hide(); 
            }
            else
            {
                MessageBox.Show("Hatalı TC&Sifre!");
            }
            bgl.baglanti().Close();
            

        }
    }
}
