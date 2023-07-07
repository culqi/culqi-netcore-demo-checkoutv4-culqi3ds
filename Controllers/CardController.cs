using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using culqi.net;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Text.Json;
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
        //[Consumes("application/x-www-form-urlencoded")]
        public ResponseCulqi Post([FromBody] JsonElement json)
        {
            string customer_id = json.GetProperty("customer_id").GetString();
            string token_id = json.GetProperty("token_id").GetString();
            //string customer_id = form["customer_id"].FirstOrDefault();
            //string token_id = form["token"].FirstOrDefault();
            security = securityKeys();
            //string eci = form["eci"].FirstOrDefault();
            //string xid = form["xid"].FirstOrDefault();
            //string cavv = form["cavv"].FirstOrDefault();
            //string protocolVersion = form["protocolVersion"].FirstOrDefault();
            //string directoryServerTransactionId = form["directoryServerTransactionId"].FirstOrDefault();
            if (!json.TryGetProperty("eci", out JsonElement eciProperty))
            {
                Dictionary<string, object> map = new Dictionary<string, object>
                {
                     {"customer_id", customer_id},
                     {"token_id", token_id}
                };

                ResponseCulqi json_object = new Card(security).Create(map);
                return json_object;
            }
            else
            {
                string eci = json.GetProperty("eci").GetString();
                string xid = json.GetProperty("xid").GetString();
                string cavv = json.GetProperty("cavv").GetString();
                string protocolVersion = json.GetProperty("protocolVersion").GetString();
                string directoryServerTransactionId = json.GetProperty("directoryServerTransactionId").GetString();
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
                ResponseCulqi json_object = new Card(security).Create(map);
                return json_object;
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

