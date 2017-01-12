using NatraServer.Models;
using NatraServer.Natra;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace NatraServer.Controllers
{
    public class NatraController : ApiController
    {
        // GET: Natra

        [System.Web.Http.HttpPost]
        [System.Web.Http.ActionName("getStoks")]
        public string getStoks([FromBody] User user)
        {

            if (!DBHelper.Instance.authUser(user)) return "invalid user";

            var stoks = DBHelper.Instance.getStoksFromDB();
            List<string> jsonStoks = new List<string>();
            //foreach(var stok in stoks)
            //{
            //    jsonStoks.Add(JsonConvert.SerializeObject(stok));
            //}
            //jsonStoks.Add(JsonConvert.SerializeObject(stoks));
            var ser = JsonConvert.SerializeObject(stoks);
            var des = JsonConvert.DeserializeObject<List<Stok>>(ser);

            Console.WriteLine("sleep begin");

            System.Threading.Thread.Sleep(2000); // test purposes will be removed

            Console.WriteLine("sleep end");

            return JsonConvert.SerializeObject(stoks);
            // return jsonStoks.ToArray();
        }


        [System.Web.Http.HttpPost]
        [System.Web.Http.ActionName("sepetOnay")]
        public string sepetOnay([FromBody] Siparis_h siparis_h)
        {
            IEnumerable<string> headerValues;
            var userData = string.Empty;
            var keyFound = Request.Headers.TryGetValues("userData", out headerValues);
            if (keyFound)
            {
                userData = headerValues.FirstOrDefault();
            }

            new Natra.Natra().addSiparises(siparis_h, JsonConvert.DeserializeObject<User>(userData));
            //return value+"donnnn";
            System.Threading.Thread.Sleep(2000); // test purposes will be removed



            return "onay";
        }


        public class denemeJson
        {
            public string name { get; set; }
        }
    }
}