using NatraServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NatraServer.Natra
{
    public class Natra
    {

        string depokodu = "00"; // ?
        string dateString = DateTime.Now.ToString("yyyy-MM-dd"); // yyyy-mm-dd
        string dateStringDetail = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"); // yyyy-mm-dd hh:mm:ss
        string dateStringDetailMiliseconds = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"); // yyyy-mm-dd hh:mm:ss.mmm
        string belgeSaati = DateTime.Now.ToString("HH:mm:ss.fff"); // hh:mm:ss.mmm

        string hesapKodu;
        string hesapAciklamasi;
        SiparisTemp siparisTemp;

        public void addSiparises(Siparis_h siparis_h, User user)
        {

            hesapKodu = user.username;
            hesapAciklamasi = user.username + " hesabi";

            if (!checkStokAmounts(siparis_h.siparis_dList)) failAddSiparises();



            siparisTemp = DBHelper.Instance.getSiparisData();

            if (!(siparis_h.siparis_dList != null && siparis_h.siparis_dList[0] != null) || siparisTemp == null) failAddSiparises(); // siparises check first


            Siparis_hWrapper siparis_hWrapper = new Siparis_hWrapper(siparis_h);

            fillSiparis_hWrapperData(siparis_hWrapper);

            DBHelper.Instance.sendSiparis(siparis_hWrapper,user);

        }


        private void fillSiparis_hWrapperData(Siparis_hWrapper yeniSiparis)
        {

            yeniSiparis.EvrakNo = siparisTemp.EvrakNo;
            yeniSiparis.BelgeNo = siparisTemp.BelgeNo;
            yeniSiparis.YedekParcaToplam = yeniSiparis.GenelToplam;

            yeniSiparis.TerminTarihi = dateString;
            yeniSiparis.SiparisDurumu = "1";

            //siparis_d

            //siparis_h
            yeniSiparis.BelgeTarihi = dateString;
            yeniSiparis.Depokodu = depokodu;
            yeniSiparis.VadeTarihi = dateString;
            yeniSiparis.TeslimatTarihi = dateStringDetail;
            yeniSiparis.HesapAciklamasi = hesapAciklamasi;
            yeniSiparis.BelgeSaati = belgeSaati; //'10:47:38.257',
            yeniSiparis.SevkTarihi = dateString;
            yeniSiparis.USER = " "; // ???
            yeniSiparis.RECDATE = dateStringDetailMiliseconds; // '2016-12-27 10:48:28.54
                                                               //siparis_h
            foreach (Siparis_dWrapper var in yeniSiparis.siparis_dWrapperList)
            {
                fillSiparis_dWrapperData(var);
            }

        }

        private void fillSiparis_dWrapperData(Siparis_dWrapper yeniSiparis)
        {
            //yeniSiparis.YedekParcaToplam = yeniSiparis.BrutTutar;
            yeniSiparis.EvrakNo = siparisTemp.EvrakNo;
            yeniSiparis.TerminTarihi = dateString;
            yeniSiparis.BelgeTarihi = dateString;
            yeniSiparis.NetBirimFiyat = yeniSiparis.stok.SatisFiyati1;
            //yeniSiparis.SiparisDurumu = "1";
            //yeniSiparis.BirimFiyat = yeniSiparis.BirimFiyat * yeniSiparis.BirimFiyat;
            //yeniSiparis.BrutTutar = yeniSiparis.BirimFiyat * yeniSiparis.Miktar; // ?
            yeniSiparis.NetTutar = yeniSiparis.BrutTutar;  // ?
            yeniSiparis.Kalan =  yeniSiparis.Miktar;

            yeniSiparis.RECDATE = dateStringDetailMiliseconds;
            yeniSiparis.USER = ""; // ?

        }


        private bool checkStokAmounts(List<Siparis_d> siparises)
        {
            Dictionary<string, int> stokAmounts = new Dictionary<string, int>();

            foreach (var sip in siparises)
            {
                if (stokAmounts.ContainsKey(sip.stok.StokKodu))
                {
                    var curr = stokAmounts[sip.stok.StokKodu];
                    stokAmounts[sip.stok.StokKodu] = curr + sip.Miktar;
                }
                else
                {
                    stokAmounts[sip.stok.StokKodu] = sip.Miktar;
                }

            }

            var stoks=DBHelper.Instance.getStoksFromDB();

            foreach (var stok in stoks)
            {
                var stokMik = stok.bakiye;
                if (stokAmounts.ContainsKey(stok.StokKodu))
                {
                    var sipMik = stokAmounts[stok.StokKodu];
                    if (sipMik > stokMik) return false;  
                }
                else
                {
                    //stok kodu bulunamaması durumu halledilecek
                }
                
            }

            return true;

        }

        private void successAddSiparises()
        {

        }

        private void failAddSiparises()
        {

        }

        //string depokodu = "00"; // ?
        //string dateString = DateTime.Now.ToString("yyyy-MM-dd"); // yyyy-mm-dd
        //string dateStringDetail = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"); // yyyy-mm-dd hh:mm:ss
        //string dateStringDetailMiliseconds = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"); // yyyy-mm-dd hh:mm:ss.mmm
        //string belgeSaati = DateTime.Now.ToString("HH:mm:ss.fff"); // hh:mm:ss.mmm
        //string user = User.user;
        //string hesapKodu = "m001";
        //string hesapAciklamasi = "m001 hesabi";

        //Stok selectedStok = null;

        //public Natra()
        //{
        //    DBHelper.Instance.openConnection();
        //    //Siparis yeniSiparis=DBHelper.Instance.getSiparisData();

        //    //yeniSiparis=fillSiparisUserData(DBHelper.Instance.getStoksFromDB(), yeniSiparis);
        //    //fillSiparisDataConstants(DBHelper.Instance.getStoksFromDB(),yeniSiparis);

        //    Siparis yeniSiparis = null;

        //    while (true)
        //    {
        //        Console.WriteLine("1 to take all stoks , 5 to auto fill user data, 3 to add siparis");
        //        string s = Console.ReadLine();
        //        switch (s)
        //        {
        //            case "1":
        //                var stocks = DBHelper.Instance.getStoksFromDB();
        //                Console.WriteLine("select stock");

        //                yeniSiparis = DBHelper.Instance.getSiparisData();

        //                string choice = Console.ReadLine();
        //                selectedStok = stocks[Int32.Parse(choice)];
        //                fillSiparisDataConstants(yeniSiparis);
        //                break;
        //            case "5":
        //                fillSiparisUserData(selectedStok, yeniSiparis);
        //                editSiparisDataWithUserData(yeniSiparis);
        //                break;
        //            case "3":
        //                makeQueryAndExecute(yeniSiparis);
        //                break;
        //        }
        //    }

        //}

        //void fillSiparisUserData(Stok stok, Siparis yeniSiparis)
        //{
        //    yeniSiparis.StokAciklamasi = "User açıklama fillSiparisUserData";
        //    yeniSiparis.Miktar = 1;
        //    yeniSiparis.OlcuBirimi = "kg";
        //}

        //void editSiparisDataWithUserData(Siparis yeniSiparis)
        //{
        //    yeniSiparis.StokKodu = selectedStok.StokKodu;
        //    yeniSiparis.GenelToplam = yeniSiparis.Miktar * yeniSiparis.NetBirimFiyat;
        //    yeniSiparis.YedekParcaToplam = yeniSiparis.GenelToplam;
        //    //yeniSiparis.Kalan = selectedStok.bakiye - yeniSiparis.Miktar; //kalan stok-miktar mı olmalı ? program direkt miktarı yazıyor
        //    yeniSiparis.Kalan = yeniSiparis.Miktar;
        //    yeniSiparis.BrutTutar = selectedStok.SatisFiyati1 - yeniSiparis.Miktar;
        //    yeniSiparis.NetTutar = selectedStok.SatisFiyati1 - yeniSiparis.Miktar;
        //}

        //void fillSiparisDataConstants(Siparis yeniSiparis)
        //{
        //    yeniSiparis.TerminTarihi = dateString;
        //    yeniSiparis.NetBirimFiyat = selectedStok.SatisFiyati1;
        //    yeniSiparis.SiparisDurumu = "1";
        //    yeniSiparis.BirimFiyat = selectedStok.SatisFiyati1;
        //    yeniSiparis.BrutTutar = yeniSiparis.BirimFiyat * yeniSiparis.Miktar; // ?
        //    yeniSiparis.NetTutar = yeniSiparis.BirimFiyat * yeniSiparis.Miktar;  // ?
        //    yeniSiparis.Kalan = selectedStok.bakiye - yeniSiparis.Miktar;

        //    //siparis_d

        //    //siparis_h
        //    yeniSiparis.BelgeTarihi = dateString;
        //    yeniSiparis.Aciklama = "";

        //    yeniSiparis.HesapKodu = hesapKodu;
        //    yeniSiparis.Depokodu = depokodu;
        //    yeniSiparis.VadeTarihi = dateString;
        //    yeniSiparis.TeslimatTarihi = dateStringDetail;
        //    yeniSiparis.HesapAciklamasi = hesapAciklamasi;
        //    yeniSiparis.BelgeSaati = belgeSaati; //'10:47:38.257',
        //    yeniSiparis.SevkTarihi = dateString;
        //    yeniSiparis.USER = user;
        //    yeniSiparis.RECDATE = dateStringDetailMiliseconds; // '2016-12-27 10:48:28.54
        //    //siparis_h
        //}

        //bool makeQueryAndExecute(Siparis yeniSiparis)
        //{
        //    DBHelper.Instance.sendSiparis(yeniSiparis);
        //    return true;
        //}


    }
}