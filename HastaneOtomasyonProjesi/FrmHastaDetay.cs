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
    public partial class FrmHastaDetay : Form
    {
        public FrmHastaDetay()
        {
            InitializeComponent();
        }
        public string tc;// burda tc değişkeni oluşturdum
        sqlbaglantisi bgl = new sqlbaglantisi();

        private void FrmHastaDetay_Load(object sender, EventArgs e)
        {
            //Ad soyad çekme
            LblTC.Text = tc;//tc değişkenini buraya yazdırdım.Bu tc değişkenini de hasta girişten taşıcam.hasta detay formunu açmadan önce
                            //tcsi girilen hastanın adını soyadını veren program
            SqlCommand komut = new SqlCommand("select HastaAd,HastaSoyad from Hastalar where HastaTC=@p1", bgl.baglanti());//hasta tc sine göre hasta ad soyadı getirir.
            komut.Parameters.AddWithValue("@p1", LblTC.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while(dr.Read())
            {
                LblAdSoyad.Text = dr[0] + " " + dr[1];

            }
            bgl.baglanti().Close();
            //Randevu geçmişi
            DataTable dt = new DataTable();//veri tablosu oluştur.
            SqlDataAdapter da = new SqlDataAdapter("select * from Hastalar where HastaTC=" + tc, bgl.baglanti());//verileri datagride aktarmak için kullanıyorum.data adapter da parametre kullanılmaz.
            da.Fill(dt);//data adapterın içini tablodan gelen değerle doldur.
            dataGridView1.DataSource = dt; //dataGridView1in veri kaynağı dt den gelen tablo

            //branş çek
            SqlCommand komut2 = new SqlCommand("select * from Branslar", bgl.baglanti());
            SqlDataReader dr2 = komut2.ExecuteReader();
            while(dr.Read())
            {
                cmbBrans.Items.Add(dr2[0]);
            }
            bgl.baglanti().Close();
        
        }

        private void cmbBrans_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbDoktor.Items.Clear();
            SqlCommand komut3 = new SqlCommand("select DoktorAd,DoktorSoyad from Doktorlar where DoktorBrans=@p1", bgl.baglanti());
            komut3.Parameters.AddWithValue("@p1", cmbBrans.Text);
            SqlDataReader dr3 = komut3.ExecuteReader();//Bir ya da birden fazla satırların sonuç olarak döneceği sorgularda
                                                       //SqlCommand' ın ExecuteReader özelliği kullanılmaktadır.
                                                       //ExecuteReader geriye SqlDataReader tipinde veri döndürmektedir.
            while (dr3.Read())
            {
                cmbDoktor.Items.Add(dr3[0] + " " + dr3[1]);

            }
            bgl.baglanti().Close();
        }

        private void cmbDoktor_SelectedIndexChanged(object sender, EventArgs e)
        {
            //DOKTORU SEÇTİĞİMDE RANDEVULAR GELECEK
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select * from Tbl_Randevular where RandevuBrans='" + cmbBrans.Text + "' +and RandevuDoktor='"+cmbDoktor.Text+"'", bgl.baglanti());
            da.Fill(dt);
            dataGridView2.DataSource = dt;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmBilgiDuzenle fr = new FrmBilgiDuzenle();
            fr.TCno = LblTC.Text;
            fr.Show();
            this.Hide();
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView2.SelectedCells[0].RowIndex;
            txtID.Text = dataGridView2.Rows[secilen].Cells[0].Value.ToString();
        }

        private void btnRandevuAl_Click(object sender, EventArgs e)
        {
            SqlCommand komut1 = new SqlCommand("Update Tbl_Randevular Set RandevuDurum=1,HastaTC=@p1,HastaSikayet=@p2 where Randevuid=@p3", bgl.baglanti());
            komut1.Parameters.AddWithValue("@p1", LblTC.Text);
            komut1.Parameters.AddWithValue("@p2", rchSikayet.Text);
            komut1.Parameters.AddWithValue("@p3", txtID.Text);
            komut1.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Randevu Alındı");

        }
    }
}
