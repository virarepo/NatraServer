using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace NatraServer.Models
{
    public class Siparis_h : Siparis
    {
        public string EvrakNo { get; set; } // a
        public string TerminTarihi { get; set; }
        public string SiparisDurumu { get; set; }
        public string Aciklama { get; set; }


        public double BirimFiyat { get; set; }
        public double NetTutar { get; set; }
        public double NetBirimFiyat { get; set; }

        public int Kalan { get; set; }
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

        public Siparis_h(Siparis p)
        {
            foreach (FieldInfo prop in p.GetType().GetFields())
                GetType().GetField(prop.Name).SetValue(this, prop.GetValue(p));

            foreach (PropertyInfo prop in p.GetType().GetProperties())
                GetType().GetProperty(prop.Name).SetValue(this, prop.GetValue(p, null), null);
        }

    }
}