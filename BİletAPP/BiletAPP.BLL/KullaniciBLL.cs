using System;
using System.Data.SqlClient;
using BiletAPP.MODEL;
using DAL;

namespace BiletAPP.BLL
{
    public class KullaniciBLL
    {
        public bool KullaniciEkle(Kullanici kullanici)
        {
            SqlParameter[] pp = {
                             new SqlParameter("@@KoltukNumarası",kullanici.KoltukNumarası)
                         };

            Helper hlp = new Helper();
           
            int rowEffected = hlp.ExecuteNonQuery($"SELECT COUNT(*) FROM tblKullanicilar WHERE KoltukNumarası ={kullanici.KoltukNumarası}", pp);
            if (rowEffected<0)
            {
                SqlParameter[] p = {
                             new SqlParameter("@TC",kullanici.TC),
                             new SqlParameter("@Ad",kullanici.Ad),
                             new SqlParameter("@Soyad",kullanici.Soyad),
                             new SqlParameter("@Cinsiyet",kullanici.Cinsiyet),
                             new SqlParameter("@Universite",kullanici.Univeriste),
                             new SqlParameter("@Bolum",kullanici.Bolum),
                             new SqlParameter("@KoltukNumarası",kullanici.KoltukNumarası)
                         };

               
                return hlp.ExecuteNonQuery("Insert into tblKullanicilar values(@TC, @Ad, @Soyad, @Cinsiyet, @Universite, @Bolum, @KoltukNumarası)", p) > 0;
            }
            else
            {
                return false;
            }

        }
    }
}
