using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BiletAPP.BLL;
using BiletAPP.MODEL;

namespace BİletAPP
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private bool sayisalMi(string input)
        {
            return long.TryParse(input, out _);
        }



        private void btnKaydet_Click(object sender, EventArgs e)
        {
            try
            {
                var obl = new KullaniciBLL();

                if (txtTC.Text.Length != 11 || !sayisalMi(txtTC.Text))
                {
                    MessageBox.Show("TC alanına geçerli bir ifade giriniz!");
                    return;
                }

                if  (string.IsNullOrWhiteSpace(txtAd.Text) ||
                     string.IsNullOrWhiteSpace(txtSoyad.Text) ||
                     string.IsNullOrWhiteSpace(txtUniversite.Text) ||
                     string.IsNullOrWhiteSpace(txtBolum.Text))
                {
                    MessageBox.Show("Lütfen tüm alanları doldurunuz!");
                    return;
                }

                bool sonuc;
                int koltukNumarasi;

                bool cinsiyet = btnErkek.Checked;


                if (int.TryParse(txtKoltukNumarası.Text, out koltukNumarasi))
                {
                    if (koltukNumarasi < 0 || koltukNumarasi > 45)
                    {
                        MessageBox.Show("Koltuk numarası 0 ile 45 arasında olmalıdır!");
                        return;
                    }

                    sonuc = obl.KullaniciEkle(new Kullanici
                    {
                        Ad = txtAd.Text.Trim(),
                        Soyad = txtSoyad.Text.Trim(),
                        TC = long.Parse(txtTC.Text.Trim()),
                        Cinsiyet = cinsiyet,
                        Univeriste = txtUniversite.Text.Trim(),
                        Bolum = txtBolum.Text.Trim(),
                        KoltukNumarası = koltukNumarasi
                    });
                    MessageBox.Show(sonuc ? "Ekleme Başarılı!" : "Ekleme Başarısız!!");
                }
                else
                {
                    MessageBox.Show("Geçersiz koltuk numarası girişi!");
                }
            }
            catch (SqlException ex)
            {
                switch (ex.Number)
                {
                    case 2627:
                        MessageBox.Show("Bu numaralı koltuk daha önce alınmış!");
                        break;
                    default:
                        MessageBox.Show("Veritabanı hatası : " + ex.Number + " ***************** " + "\n\n" + ex.Message + " ***************** " + "\n\n" + ex.StackTrace);
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Veritabanı hatası" + " *****************" + "\n\n" + ex.Message + " *****************" + "\n\n" + ex.StackTrace);
            }
        }
    }
}
