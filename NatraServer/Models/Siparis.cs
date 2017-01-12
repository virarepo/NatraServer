using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NatraServer.Models
{
    public class Siparis
    {
        public Stok stok { get; set; }
        public string OlcuBirimi { get; set; }
        public int Miktar { get; set; }
        public string SiparisNotlari { get; set; }
        public string HesapKodu { get; set; }
        public double GenelToplam { get; set; }
        public double BrutTutar { get; set; }
        public double KDVToplam { get; set; }
    }
}