using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace NatraServer.Models
{
    public class Siparis_h 
    {
        public List<Siparis_d> siparis_dList { get; set; }
        public string SiparisNotlari { get; set; }
        public double GenelToplam { get; set; }
        public double BrutTutar { get; set; }
        public double KDVToplam { get; set; }

        //public Siparis_h()
        //{
        //    siparis_dList = new List<Siparis_d>();
        //}
    }
}