using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using culqi.net;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Demo.Controllers
{
    
    [Route("api/[controller]")]
    public class ChargeController : GenericController
    {
        Security security = null;
        string encrypt = "0";
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
            security = securityKeys();


            Int32 amount = json.GetProperty("amount").GetInt32();
            string currency_code = json.GetProperty("currency_code").GetString();          
            string email = json.GetProperty("email").GetString();
            string source_id = json.GetProperty("source_id").GetString();
          
            
          
            if (!json.TryGetProperty("eci", out JsonElement eciProperty))
            {
               

                Dictionary<string, object> map = new Dictionary<string, object>
                {
                    {"amount",amount},
                    {"currency_code", currency_code},                    
                    {"email", email},
                    {"source_id", source_id}
                };
                if(encrypt=="1")
                {
                    ResponseCulqi json_object = new Charge(security).Create(map, security.rsa_id, security.rsa_key);
                    return json_object;
                }
                else
                {
                    ResponseCulqi json_object = new Charge(security).Create(map);
                    return json_object;
                }
               
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
                    {"amount", Convert.ToInt32(amount)},
                    {"currency_code", currency_code},                   
                    {"email", email},
                    {"source_id", source_id},
                    {"authentication_3DS", authentication_3DS},
                };
                if(encrypt=="1")
                {
                    ResponseCulqi json_object = new Charge(security).Create(map, security.rsa_id, security.rsa_key);
                    return json_object;
                }
                else
                {
                    ResponseCulqi json_object = new Charge(security).Create(map);
                    return json_object;
                }
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

