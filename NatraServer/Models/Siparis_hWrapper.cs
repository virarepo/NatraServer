﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace NatraServer.Models
{
    public class Siparis_hWrapper : Siparis_h
    {

        public List<Siparis_dWrapper> siparis_dWrapperList;

        public string EvrakNo { get; set; } // a
        public string TerminTarihi { get; set; }
        public string SiparisDurumu { get; set; }
        public string Aciklama { get; set; }


        //public double BirimFiyat { get; set; }
        //public double NetTutar { get; set; }
        //public double NetBirimFiyat { get; set; }

        //public int Kalan { get; set; }
        public int BelgeNo { get; set; }

        public string BelgeTarihi { get; set; }

        public string Depokodu { get; set; }
        public string VadeTarihi { get; set; }
        public string TeslimatTarihi { get; set; }
        public string HesapAciklamasi { get; set; }
        public string BelgeSaati { get; set; } //'10:47:38.257',
        public string SevkTarihi { get; set; }
        public string USER { get; set; }
        public string RECDATE { get; set; } // '2016-12-27 10:48:28.54

        public double YedekParcaToplam { get; set; }



        public Siparis_hWrapper(Siparis_h p)
        {
            foreach (FieldInfo prop in p.GetType().GetFields())
                GetType().GetField(prop.Name).SetValue(this, prop.GetValue(p));

            foreach (PropertyInfo prop in p.GetType().GetProperties())
                GetType().GetProperty(prop.Name).SetValue(this, prop.GetValue(p, null), null);

            siparis_dWrapperList = new List<Siparis_dWrapper>();

            fillSiparis_dWrapperList();
        }

        void fillSiparis_dWrapperList()
        {
            foreach (var siparis_d in siparis_dList)
            {
                siparis_dWrapperList.Add(new Siparis_dWrapper(siparis_d));
            }
        }
    }
}