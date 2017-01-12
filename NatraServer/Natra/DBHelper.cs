using MySql.Data.MySqlClient;
using NatraServer.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NatraServer.Natra
{
    public class DBHelper
    {
        private static DBHelper _instance;

        public static DBHelper Instance
        {
            get
            {
                if (_instance == null) _instance = new DBHelper();
                return _instance;
            }
        }

        public bool openConnection()
        {
            try
            {
                connection = new MySql.Data.MySqlClient.MySqlConnection();
                connection.ConnectionString = myConnectionString;
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                return false;
            }
        }

        public bool closeConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                return false;
            }
        }

        private MySqlConnection connection;

        public int updateCtr = 0;

        string myConnectionString = "server=localhost; port=3306; uid=root;" + "pwd=n@tra; database=star; charset=utf8;";
        //string myConnectionString = ConfigurationSettings.AppSettings["mysqlConnectionString"];

        public List<Stok> getStoksFromDB()
        {
            if (connection == null) openConnection();
            List<Stok> stoks = new List<Stok>();
            //string sqlQuery = "SELECT group_concat(distinct uruncinsi) as modeller,group_concat(distinct marka) as markalar,stok_h.*,sum(if(coalesce(depo.calismatipi,'Alfa') in ('Alfa','Beta'),COALESCE(stok_d.girenmiktar,0),0)) as giren,sum(if(coalesce(depo.calismatipi,'Alfa') in ('Alfa','Beta'),COALESCE(stok_d.cikanmiktar,0),0)) as cikan,sum(if(coalesce(depo.calismatipi,'Alfa') in ('Alfa','Beta'),COALESCE(stok_d.girenmiktar,0)-COALESCE(stok_d.cikanmiktar,0),0)) as bakiye,0*sum(COALESCE(stok_d.cikanmiktar,0)) as bakiye4,0*sum(COALESCE(stok_d.cikanmiktar,0)) as bakiye5,0*sum(COALESCE(stok_d.cikanmiktar,0)) as bakiye2,0*sum(COALESCE(stok_d.cikanmiktar,0)) as bakiye3,sum(if(coalesce(depo.calismatipi,'Alfa') in ('Alfa'),COALESCE(stok_d.girenmiktar,0)-COALESCE(stok_d.cikanmiktar,0),0))+sum(if(coalesce(depo.calismatipi,'Alfa') in ('Omega'),COALESCE(stok_d.girenmiktar,0)-COALESCE(stok_d.cikanmiktar,0),0)) as ozelbakiye,sign(if(coalesce(stok_h.asgarimiktar,0)>0 and coalesce(stok_h.asgarimiktar,0)>sum(if(coalesce(depo.calismatipi,'Alfa') in ('Alfa','Beta'),COALESCE(stok_d.girenmiktar,0)-COALESCE(stok_d.cikanmiktar,0),0)),1,0)) as asgari  FROM stok_h LEFT JOIN stok_d ON stok_d.stokkodu=stok_h.stokkodu left join depo on depo.depokodu=stok_d.depokodu left join sube on sube.subekodu=depo.subekodu  WHERE stok_h.tipi = 'S'  and stok_h.durumu='Kullanımda' and  stok_h.yetkiseviyesi<=5 GROUP BY stok_h.StokKodu  order by stok_h.StokKodu  limit 1000";
            string sqlQuery = "SELECT group_concat(distinct uruncinsi) as modeller,group_concat(distinct marka) as markalar,stok_h.*,sum(if(coalesce(depo.calismatipi,'Alfa') in ('Alfa','Beta'),COALESCE(stok_d.girenmiktar,0),0)) as giren,sum(if(coalesce(depo.calismatipi,'Alfa') in ('Alfa','Beta'),COALESCE(stok_d.cikanmiktar,0),0)) as cikan,sum(if(coalesce(depo.calismatipi,'Alfa') in ('Alfa','Beta'),COALESCE(stok_d.girenmiktar,0)-COALESCE(stok_d.cikanmiktar,0),0)) as bakiye,0*sum(COALESCE(stok_d.cikanmiktar,0)) as bakiye4,0*sum(COALESCE(stok_d.cikanmiktar,0)) as bakiye5,0*sum(COALESCE(stok_d.cikanmiktar,0)) as bakiye2,0*sum(COALESCE(stok_d.cikanmiktar,0)) as bakiye3,sum(if(coalesce(depo.calismatipi,'Alfa') in ('Alfa'),COALESCE(stok_d.girenmiktar,0)-COALESCE(stok_d.cikanmiktar,0),0))+sum(if(coalesce(depo.calismatipi,'Alfa') in ('Omega'),COALESCE(stok_d.girenmiktar,0)-COALESCE(stok_d.cikanmiktar,0),0)) as ozelbakiye,sign(if(coalesce(stok_h.asgarimiktar,0)>0 and coalesce(stok_h.asgarimiktar,0)>sum(if(coalesce(depo.calismatipi,'Alfa') in ('Alfa','Beta'),COALESCE(stok_d.girenmiktar,0)-COALESCE(stok_d.cikanmiktar,0),0)),1,0)) as asgari  FROM stok_h LEFT JOIN stok_d ON stok_d.stokkodu=stok_h.stokkodu left join depo on depo.depokodu=stok_d.depokodu left join sube on sube.subekodu=depo.subekodu  WHERE (stok_h.tipi = 'S'  AND stok_h.durumu LIKE '%mda' AND  stok_h.yetkiseviyesi<=5) GROUP BY stok_h.StokKodu order by stok_h.StokKodu limit 1000";
            //string sqlQuery = "SELECT group_concat(distinct uruncinsi) as modeller,group_concat(distinct marka) as markalar,stok_h.*,sum(if(coalesce(depo.calismatipi,'Alfa') in ('Alfa','Beta'),COALESCE(stok_d.girenmiktar,0),0)) as giren,sum(if(coalesce(depo.calismatipi,'Alfa') in ('Alfa','Beta'),COALESCE(stok_d.cikanmiktar,0),0)) as cikan,sum(if(coalesce(depo.calismatipi,'Alfa') in ('Alfa','Beta'),COALESCE(stok_d.girenmiktar,0)-COALESCE(stok_d.cikanmiktar,0),0)) as bakiye,0*sum(COALESCE(stok_d.cikanmiktar,0)) as bakiye4,0*sum(COALESCE(stok_d.cikanmiktar,0)) as bakiye5,0*sum(COALESCE(stok_d.cikanmiktar,0)) as bakiye2,0*sum(COALESCE(stok_d.cikanmiktar,0)) as bakiye3,sum(if(coalesce(depo.calismatipi,'Alfa') in ('Alfa'),COALESCE(stok_d.girenmiktar,0)-COALESCE(stok_d.cikanmiktar,0),0))+sum(if(coalesce(depo.calismatipi,'Alfa') in ('Omega'),COALESCE(stok_d.girenmiktar,0)-COALESCE(stok_d.cikanmiktar,0),0)) as ozelbakiye,sign(if(coalesce(stok_h.asgarimiktar,0)>0 and coalesce(stok_h.asgarimiktar,0)>sum(if(coalesce(depo.calismatipi,'Alfa') in ('Alfa','Beta'),COALESCE(stok_d.girenmiktar,0)-COALESCE(stok_d.cikanmiktar,0),0)),1,0)) as asgari  FROM stok_h LEFT JOIN stok_d ON stok_d.stokkodu=stok_h.stokkodu left join depo on depo.depokodu=stok_d.depokodu left join sube on sube.subekodu=depo.subekodu  WHERE stok_h.tipi = 'S' and stok_h.durumu='Kullanımda' and  stok_h.yetkiseviyesi<=5 ";
            //string sqlQuery = "SELECT * FROM stok_d WHERE StokKodu='denemeStok'";
            //string sqlQuery = "select * from stok_h LEFT JOIN stok_d ON stok_d.stokkodu=stok_h.stokkodu group by stok_h.stokkodu";
            //MySql.Data.MySqlClient.MySqlHelper.EscapeString(sqlQuery);

            MySqlCommand cmd = new MySqlCommand(sqlQuery, connection);
            //cmd.Parameters.AddWithValue("@kullanim", "Kullanımda");
            try
            {
                using (MySqlDataReader Reader = cmd.ExecuteReader())
                {
                    if (Reader.HasRows)
                    {

                    }
                    while (Reader.Read())
                    {
                        Stok newStok = new Stok();
                        newStok.StokKodu = (Reader.IsDBNull(Reader.GetOrdinal("StokKodu"))) ? null : Reader.GetString("StokKodu");
                        newStok.OlcuBirimi1 = (Reader.IsDBNull(Reader.GetOrdinal("OlcuBirimi1"))) ? null : Reader.GetString("OlcuBirimi1");
                        newStok.OlcuBirimi2 = (Reader.IsDBNull(Reader.GetOrdinal("OlcuBirimi2"))) ? null : Reader.GetString("OlcuBirimi2");
                        newStok.SatisFiyati1 = (Reader.IsDBNull(Reader.GetOrdinal("SatisFiyati1"))) ? -1 : Int32.Parse(Reader.GetString("SatisFiyati1"));
                        newStok.bakiye = (Reader.IsDBNull(Reader.GetOrdinal("bakiye"))) ? -1 : Int32.Parse(Reader.GetString("bakiye"));
                        newStok.KDV = (Reader.IsDBNull(Reader.GetOrdinal("SatisKDV"))) ? -1 : Int32.Parse(Reader.GetString("SatisKDV"));
                        newStok.Carpan = (Reader.IsDBNull(Reader.GetOrdinal("Carpan"))) ? -1 : Int32.Parse(Reader.GetString("Carpan"));
                        newStok.StokAciklamasi = (Reader.IsDBNull(Reader.GetOrdinal("StokAciklamasi"))) ? null : Reader.GetString("StokAciklamasi");
                        stoks.Add(newStok);
                        string json = JsonConvert.SerializeObject(newStok);
                        var obj=JsonConvert.DeserializeObject<Stok>(json);
                        //Console.WriteLine(newStok.StokKodu);
                    }
                }
            }
            catch (Exception e)
            {

            }

            

            //Console.WriteLine(stoks.Count);
            printStoks(stoks);
            return stoks;

        }

        public void printStoks(List<Stok> stoks)
        {
            for (int i = 0; i < stoks.Count; i++)
            {
                Console.WriteLine(i + " :");
                var s = stoks[i];
                Console.WriteLine("stok kodu: " + s.StokKodu + " bakiye: " + s.bakiye);
                Console.WriteLine("-------------");
            }
        }

        public SiparisTemp getSiparisData()
        {
            if (connection == null) openConnection();
            string evraknoSql = "select max(right(evrakno,7)) as enbuyuk from siparis_h where left(evrakno,3)='SSP'";
            string BelgeNoSql = "SELECT MAX(BelgeNo) AS BelgeNoMax FROM siparis_no where left(evrakno,3) in ('SSP','AIS') and Seri='A' and etkin=1";

            int evrakno = -1;
            int belgeNo = -1;

            MySqlCommand cmd = null;

            cmd = new MySqlCommand(evraknoSql, connection);
            //cmd.Parameters.AddWithValue("@kullanim", "Kullanımda");
            try
            {
                using (MySqlDataReader Reader = cmd.ExecuteReader())
                {
                    while (Reader.Read())
                    {
                        evrakno = (Reader.IsDBNull(Reader.GetOrdinal("enbuyuk"))) ? -1 : Int32.Parse(Reader.GetString("enbuyuk"));
                    }
                }
            }
            catch (Exception e)
            {

            }

            cmd = new MySqlCommand(BelgeNoSql, connection);
            try
            {
                using (MySqlDataReader Reader = cmd.ExecuteReader())
                {
                    while (Reader.Read())
                    {
                        belgeNo = (Reader.IsDBNull(Reader.GetOrdinal("BelgeNoMax"))) ? -1 : Int32.Parse(Reader.GetString("BelgeNoMax"));
                    }
                }
            }
            catch (Exception e)
            {

            }
            //Console.WriteLine("siparis:"+ evrakno);
            //Console.WriteLine("belge:"+ belgeNo);
            //Console.WriteLine(stoks.Count);
            int i = 4;
            var evraknoString = "SSP";
            evraknoString += (evrakno + 1).ToString("D7");
            return new SiparisTemp() { EvrakNo = evraknoString, BelgeNo = (belgeNo + 1) };
        }

        public void sendSiparis(Siparis_hWrapper siparis_h,User user)
        {
            if (connection == null) openConnection();

            MySqlCommand command = connection.CreateCommand();

            List<Siparis_dWrapper> siparis_dList = siparis_h.siparis_dWrapperList;

            //string sqlInsertIntoHatakontrol= "insert into hatakontrol(EvrakNo, Tarih, Deger, USER) values(@EvrakNo, @Tarih, 1, @admin)";

            //command.Parameters.Add(new MySqlParameter("admin", UserData.id));
            //command.Parameters.Add(new MySqlParameter("EvrakNo", siparis.EvrakNo));
            //command.Parameters.Add(new MySqlParameter("Tarih", siparis.RECDATE));

            //command.CommandText = sqlInsertIntoHatakontrol;

            //try
            //{
            //    command.ExecuteNonQuery();
            //}
            //catch (Exception e)
            //{

            //}


            //command = connection.CreateCommand();

            //string sqlDeleteInsertSiparisNo = "delete from siparis_no where evrakno = @EvrakNo; insert into siparis_no (evrakno, belgeno, seri, etkin) values(@EvrakNo, 4, 'A', @BelgeNo);";

            //command.Parameters.Add(new MySqlParameter("EvrakNo", siparis.EvrakNo));
            //command.Parameters.Add(new MySqlParameter("BelgeNo", siparis.BelgeNo));

            //command.CommandText = sqlDeleteInsertSiparisNo;

            //try
            //{
            //    command.ExecuteNonQuery();
            //}
            //catch (Exception e)
            //{

            //}







            command = connection.CreateCommand();
            
            string insertToSiparis_H = "INSERT INTO siparis_h" +
  "(EvrakNo, HesapKodu, Seri, BelgeNo, BelgeTarihi, BelgeTuru, KDV, DovizKodu, DovizKuru, OzelNotlar, Aciklama1, Aciklama2, Aciklama3, Aciklama4, OzelKod1, OzelKod2, BrutTutar, GenelIskonto, KDVToplam, GenelToplam, IskontoOrani1, IskontoOrani2, IskontoTutar1, IskontoTutar2, KDVToplam1, KDVToplam2, Tipi, YedekParcaToplam, IscilikToplam, Depokodu, VadeTarihi, VadeGunu, Aciklama, TeslimatTarihi, TeslimatAdresi, HesapAciklamasi, Adres, VergiNo, VergiDairesi, Il, Ilce, MusteriTemsilcisi, YetkiSeviyesi, Kargo, IskontoKodu, Durumu, SiparisNotlari, Yazdir, Inceleme, ProjeKodu, TelKod1, Telefon1, TelKod4, GSM, Resim, DepoKoduKaynak, PesinOdeme, KrediKartiOdeme, KalanTutar, EPosta, SasNo, KargoOdemeTipi, OdemeKodu, BelgeSaati, Uretim, Okuma, OkumaTarihi, ToplamHacim, ToplamAgirlik, SevkTarihi, BayiNo, MusteriNo, EvrakNoTalep, USER, MUSER, MRECDATE, RECDATE, BirlestirilmisKayit, Kontakt)"
 //+ "VALUES (@EvrakNo, @HesapKodu, @Seri, @BelgeNo, @BelgeTarihi, 'Hariç', '', 1, NULL, @OzelNotlar, @Aciklama1, '', '','', '', '', @BrutTutar, 0, 0, @GenelToplam, 0, 0, 0, 0,NULL, NULL, 'S', @YedekParcaToplam, 0, @Depokodu, @VadeTarihi,NULL, @Aciklama, @TeslimatTarihi, '', @HesapAciklamasi, '', '', '', '', '', '', 5, '', '', '','', 0, 0, '', '', '','', '', '', NULL, NULL, NULL, NULL, '', '',0, '', @BelgeSaati, 0, NULL,NULL, 0, 0, @SevkTarihi, '', '', NULL, @USER, NULL, NULL, @RECDATE, NULL, NULL)";
 + "VALUES (@EvrakNo, @HesapKodu, @Seri, @BelgeNo, @BelgeTarihi, 'Açık',  'Hariç','', '1', @OzelNotlar, @Aciklama1, '', '','', '', '', @BrutTutar, 0, 0, @GenelToplam, 0, 0, 0, 0,NULL, NULL, 'S', @YedekParcaToplam, 0, @Depokodu, @VadeTarihi,NULL, @Aciklama, @TeslimatTarihi, '', @HesapAciklamasi, '', '', '', '', '', '', 5, '', '', '0',@SiparisNotlari, 0, 0, '', '', '','', '', '', NULL, NULL, NULL, NULL, '', '',0, '', @BelgeSaati, 0, NULL,NULL, 0, 0, @SevkTarihi, '', '', NULL, @USER, NULL, NULL, @RECDATE, NULL, NULL)";
            //+ "VALUES (@EvrakNo, '@HesapKodu', 'A', 12, '2016-12-28', 'Açık', 'Hariç', '', 1, NULL, 'aç sekme 1', 'aç sekme 2', '', '', '', '', 330, 0, 0, 330, 0, 0, 0, 0, NULL, NULL, 'S', 330, 0, '00', '2016-12-28', NULL, 'Siparişiniz', '2016-12-28 00:00:00', '', 'alperen', '', '', '', '', '', '', 5, '', '', '0', '', 0, 0, '', '', '', '', '', '', NULL, NULL, NULL, NULL, '', '', 0, '', '14:06:29.476', 0, NULL, NULL, 0, 0, '2016-12-28', '', '', NULL, 'admin', NULL, NULL, '2016-12-28 14:07:12.701', NULL, NULL)";

            command.Parameters.Add(new MySqlParameter("EvrakNo", siparis_h.EvrakNo));
            command.Parameters.Add(new MySqlParameter("HesapKodu", user.username));
            command.Parameters.Add(new MySqlParameter("Seri", "A"));
            command.Parameters.Add(new MySqlParameter("BelgeNo", siparis_h.BelgeNo));
            command.Parameters.Add(new MySqlParameter("BelgeTarihi", siparis_h.BelgeTarihi));
            command.Parameters.Add(new MySqlParameter("OzelNotlar", ""));
            command.Parameters.Add(new MySqlParameter("Aciklama1", ""));
            command.Parameters.Add(new MySqlParameter("SiparisNotlari", siparis_h.SiparisNotlari));
            command.Parameters.Add(new MySqlParameter("BrutTutar", siparis_h.BrutTutar));
            command.Parameters.Add(new MySqlParameter("GenelToplam", siparis_h.GenelToplam));
            command.Parameters.Add(new MySqlParameter("YedekParcaToplam", siparis_h.YedekParcaToplam));
            command.Parameters.Add(new MySqlParameter("DepoKodu", siparis_h.Depokodu));
            command.Parameters.Add(new MySqlParameter("VadeTarihi", siparis_h.VadeTarihi));
            command.Parameters.Add(new MySqlParameter("Aciklama", siparis_h.Aciklama));
            command.Parameters.Add(new MySqlParameter("TeslimatTarihi", siparis_h.TeslimatTarihi));
            command.Parameters.Add(new MySqlParameter("HesapAciklamasi", siparis_h.HesapAciklamasi));
            command.Parameters.Add(new MySqlParameter("BelgeSaati", siparis_h.BelgeSaati));
            command.Parameters.Add(new MySqlParameter("SevkTarihi", siparis_h.SevkTarihi));
            command.Parameters.Add(new MySqlParameter("USER", siparis_h.USER));
            command.Parameters.Add(new MySqlParameter("RECDATE", siparis_h.RECDATE));

            command.CommandText = insertToSiparis_H;

            try
            {
                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {

            }

            command = connection.CreateCommand();

            //          string insertToSiparis_d = "INSERT INTO siparis_h" +
            //"(EvrakNo, HesapKodu, Seri, BelgeNo, BelgeTarihi, BelgeTuru, KDV, DovizKodu, DovizKuru, OzelNotlar, Aciklama1, Aciklama2, Aciklama3, Aciklama4, OzelKod1, OzelKod2, BrutTutar, GenelIskonto, KDVToplam, GenelToplam, IskontoOrani1, IskontoOrani2, IskontoTutar1, IskontoTutar2, KDVToplam1, KDVToplam2, Tipi, YedekParcaToplam, IscilikToplam, Depokodu, VadeTarihi, VadeGunu, Aciklama, TeslimatTarihi, TeslimatAdresi, HesapAciklamasi, Adres, VergiNo, VergiDairesi, Il, Ilce, MusteriTemsilcisi, YetkiSeviyesi, Kargo, IskontoKodu, Durumu, SiparisNotlari, Yazdir, Inceleme, ProjeKodu, TelKod1, Telefon1, TelKod4, GSM, Resim, DepoKoduKaynak, PesinOdeme, KrediKartiOdeme, KalanTutar, EPosta, SasNo, KargoOdemeTipi, OdemeKodu, BelgeSaati, Uretim, Okuma, OkumaTarihi, ToplamHacim, ToplamAgirlik, SevkTarihi, BayiNo, MusteriNo, EvrakNoTalep, USER, MUSER, MRECDATE, RECDATE, BirlestirilmisKayit, Kontakt)"
            //+ "VALUES (@EvrakNo, @HesapKodu, @Seri, @BelgeNo, @BelgeTarihi, 'Hariç', '', 1, NULL, @OzelNotlar, @Aciklama1, '', '','', '', '', @BrutTutar, 0, 0, @GenelToplam, 0, 0, 0, 0,NULL, NULL, 'S', @YedekParcaToplam, 0, @Depokodu, @VadeTarihi,NULL, @Aciklama, @TeslimatTarihi, '', @HesapAciklamasi, '', '', '', '', '', '', 5, '', '', '0','', 0, 0, '', '', '','', '', '', NULL, NULL, NULL, NULL, '', '',0, '', @BelgeSaati, 0, NULL,NULL, 0, 0, @SevkTarihi, '', '', NULL, @USER, NULL, NULL, @RECDATE, NULL, NULL)";
            //          //+ "VALUES (@EvrakNo, '@HesapKodu', 'A', 12, '2016-12-28', 'Açık', 'Hariç', '', 1, NULL, 'aç sekme 1', 'aç sekme 2', '', '', '', '', 330, 0, 0, 330, 0, 0, 0, 0, NULL, NULL, 'S', 330, 0, '00', '2016-12-28', NULL, 'Siparişiniz', '2016-12-28 00:00:00', '', 'alperen', '', '', '', '', '', '', 5, '', '', '0', '', 0, 0, '', '', '', '', '', '', NULL, NULL, NULL, NULL, '', '', 0, '', '14:06:29.476', 0, NULL, NULL, 0, 0, '2016-12-28', '', '', NULL, 'admin', NULL, NULL, '2016-12-28 14:07:12.701', NULL, NULL)";

            string insertToSiparis_d = "INSERT INTO siparis_d"
+ "(SiparisD_ID, EvrakNo, HesapKodu, StokKodu, StokAciklamasi, KalemTipi, Miktar, OlcuBirimi, BirimFiyat, BrutTutar, DovizKodu, DovizKuru, KDV, ServisElemani, NetTutar, IskontoOrani1, IskontoOrani2, DvzBirimFiyat, DvzBrutFiyat, TeslimEden, EvrakNoFatura, EvrakNoIrsaliye, EvrakNoIsEmri, Kalan, SiraNo, Carpan, OlcuBirimi1, TerminTarihi, NetBirimFiyat, OzelKod1, OzelKod2, OzelKod3, OzelKod4, RenkKodu, BedenKodu, SiparisTipi, IskontoOrani3, IskontoOrani4, StokKodu2, StokAciklamasi2, FiyatDegisim, SonAlisFiyati, GecisID, EvrakNoUretimPlani, Boy, En, Adet, UretimPlani, EvrakNoTekilCikis, SiparisDurumu, Uretim, SevkMiktar, PaketKalan, SevkHazirlikOnay, SevkiyatOnay, IrsaliyeHazir, ComputerName, SasNo2, TeslimatNoktasi, USER, MUSER, RECDATE, MRECDATE)"
+ "VALUES (SiparisD_ID, @EvrakNo, @HesapKodu, @StokKodu, @StokAciklamasi, 'Stok', @Miktar, @OlcuBirimi, @BirimFiyat, @BrutTutar, '', 1, 0, NULL, @NetTutar, 0, 0, @DvzBirimFiyat, @DvzBrutFiyat, NULL, NULL, NULL, NULL, @Kalan, 1, 1, @OlcuBirimi1, @TerminTarihi, @NetBirimFiyat, '', '', '', '', NULL, NULL, '', 0, 0, '', '', 1, NULL,  NUll, NULL, 0, 0, 0, NULL, NULL, 1, 0, 0, 0, 0, 0, 0, NULL, '', '', NULL, NULL, NULL, NULL)";
            // (NULL, 'SSP0000052', 'm001', 'demoStok', 'demoStok Açıklaması (stok oluşturulurken girildi)', 'Stok', 30, 'kg', 11, 330, '', 1, 0, NULL, 330, 0, 0, 11, 330, NULL, NULL, NULL, NULL, 30, 1, 1, 'kg', '2016-12-28', 11, '', '', '',                  '', NULL, NULL, '', 0, 0, '', '', 1, 33, NULL, NULL, 0, 0, 0, NULL, NULL, 1, 0, 0, 0, 0, 0, 0, NULL, '', '', NULL, NULL, NULL, NULL)

            foreach (var siparis_d in siparis_dList)
            {
                command.Parameters.Add(new MySqlParameter("EvrakNo", siparis_d.EvrakNo));
                command.Parameters.Add(new MySqlParameter("HesapKodu", siparis_d.HesapKodu));
                command.Parameters.Add(new MySqlParameter("StokKodu", siparis_d.stok.StokKodu));
                command.Parameters.Add(new MySqlParameter("StokAciklamasi", siparis_d.stok.StokAciklamasi));
                command.Parameters.Add(new MySqlParameter("Miktar", siparis_d.Miktar));
                command.Parameters.Add(new MySqlParameter("OlcuBirimi", siparis_d.OlcuBirimi));
                command.Parameters.Add(new MySqlParameter("BirimFiyat", siparis_d.BirimFiyat));
                command.Parameters.Add(new MySqlParameter("BrutTutar", siparis_d.BrutTutar));
                command.Parameters.Add(new MySqlParameter("NetTutar", siparis_d.NetTutar));
                command.Parameters.Add(new MySqlParameter("DvzBirimFiyat", siparis_d.BirimFiyat)); // !
                command.Parameters.Add(new MySqlParameter("DvzBrutFiyat", siparis_d.BrutTutar)); // !
                command.Parameters.Add(new MySqlParameter("Kalan", siparis_d.Kalan));
                //command.Parameters.Add(new MySqlParameter("Kalan", 10));
                command.Parameters.Add(new MySqlParameter("OlcuBirimi1", siparis_d.stok.OlcuBirimi1));
                command.Parameters.Add(new MySqlParameter("TerminTarihi", siparis_d.TerminTarihi));
                command.Parameters.Add(new MySqlParameter("NetBirimFiyat", siparis_d.NetBirimFiyat));
                //command.Parameters.Add(new MySqlParameter("SonAlisFiyati", siparis.SonAlisFiyati));  // ??


                command.CommandText = insertToSiparis_d;

                try
                {
                    command.ExecuteNonQuery();
                }
                catch (Exception e)
                {

                }

                
            }

            string sqlInsertToSiparis_no = "insert into siparis_no (evrakno, belgeno, seri, etkin) values(@evrakno, @belgeno, 'A', 1)";
            command = connection.CreateCommand();

            command.Parameters.Add(new MySqlParameter("evrakno", siparis_h.EvrakNo));
            command.Parameters.Add(new MySqlParameter("belgeno", siparis_h.BelgeNo));

            command.CommandText = sqlInsertToSiparis_no;

            try
            {
                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {

            }




            //string sqlDeletetIntoHatakontrol = "update hatakontrol set Deger=0 where evrakno=@EvrakNo";

            //command = connection.CreateCommand();

            //command.Parameters.Add(new MySqlParameter("EvrakNo", siparis.EvrakNo));

            //command.CommandText = sqlDeletetIntoHatakontrol;

            //try
            //{
            //    command.ExecuteNonQuery();
            //}
            //catch (Exception e)
            //{

            //}

            //string sqlDeleteFromKilit = "delete from kilit where Modul_ID = 6 and EvrakNo = @evrakno";
            //command = connection.CreateCommand();

            //command.Parameters.Add(new MySqlParameter("evrakno", siparis.EvrakNo));

            //command.CommandText = sqlDeleteFromKilit;

            //try
            //{
            //    command.ExecuteNonQuery();
            //}
            //catch (Exception e)
            //{

            //}




        }

        public bool authUser(User user)  
        {
            return true;
        }
    }
}