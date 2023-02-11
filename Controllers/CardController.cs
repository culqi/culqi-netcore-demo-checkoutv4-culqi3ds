using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using culqi.net;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Demo.Controllers
{
    
    [Route("api/[controller]")]
    public class CardController : GenericController
    {
        Security security = null;
        // GET: api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        [Consumes("application/x-www-form-urlencoded")]
        public String Post([FromForm] IFormCollection form)
        {
            string customer_id = form["customer_id"].FirstOrDefault();
            string token_id = form["token"].FirstOrDefault();
            security = securityKeys();


            string eci = form["eci"].FirstOrDefault();
            string xid = form["xid"].FirstOrDefault();
            string cavv = form["cavv"].FirstOrDefault();
            string protocolVersion = form["protocolVersion"].FirstOrDefault();
            string directoryServerTransactionId = form["directoryServerTransactionId"].FirstOrDefault();

            if (eci == null)
            {
                Dictionary<string, object> map = new Dictionary<string, object>
                {
                     {"customer_id", customer_id},
                     {"token_id", token_id}
                };

                var json_object = JObject.Parse(new Card(security).Create(map));
                return json_object.ToString();
            }
            else
            {
                Dictionary<string, object> authentication_3DS = new Dictionary<string, object>
                {
                    {"eci", eci},
                    {"xid", xid},
                    {"cavv", cavv},
                    {"protocolVersion", protocolVersion},
                    {"directoryServerTransactionId", directoryServerTransactionId}
                };
                Dictionary<string, object> map = new Dictionary<string, object>
                {
                    {"customer_id", customer_id},
                    {"token_id", token_id},
                    {"authentication_3DS", authentication_3DS},
                };
                var json_object = JObject.Parse(new Card(security).Create(map));
                return json_object.ToString();
            }
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

