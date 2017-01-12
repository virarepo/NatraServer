using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace NatraServer.Models
{
    public class Siparis_d 
    {
        public Stok stok { get; set; }
        public string OlcuBirimi { get; set; }
        public int Miktar { get; set; }
        public string HesapKodu { get; set; }
        public double BrutTutar { get; set; }
    }
}