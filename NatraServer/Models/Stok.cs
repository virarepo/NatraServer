using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NatraServer.Models
{
    public class Stok
    {
        public string OlcuBirimi1 { get; set; }
        public string OlcuBirimi2 { get; set; }
        public string StokAciklamasi { get; set; }
        public string StokKodu { get; set; }

        public int bakiye { get; set; }
        public int KDV { get; set; }
        public int Carpan { get; set; }
        public int SatisFiyati1 { get; set; }
    }
}