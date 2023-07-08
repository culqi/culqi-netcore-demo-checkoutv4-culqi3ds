﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using culqi.net;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Text.Json;
using culqinet.util;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Demo.Controllers
{
    
    [Route("api/[controller]")]
    public class CardController : GenericController
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
            string propertyName = "authentication_3DS"; 
            JObject jsonObject = JObject.Parse(json.GetRawText());
            bool propertyExists = jsonObject.ContainsKey(propertyName);

            if (!propertyExists){
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
                string eci = "";
                string xid = "";
                string cavv = "";
                string protocolVersion = "";
                string directoryServerTransactionId = "";
                if (json.ValueKind == JsonValueKind.Object)
                {
                    if (json.TryGetProperty("authentication_3DS", out JsonElement authentication3DSProperty) && authentication3DSProperty.ValueKind == JsonValueKind.Object)
                    {
                        if (authentication3DSProperty.TryGetProperty("eci", out JsonElement eciProperty) && eciProperty.ValueKind == JsonValueKind.String)
                        {
                            eci = eciProperty.GetString();
                            Console.WriteLine("El valor de 'eci' es: " + eci);
                        }

                        if (authentication3DSProperty.TryGetProperty("xid", out JsonElement xidProperty) && xidProperty.ValueKind == JsonValueKind.String)
                        {
                            xid = xidProperty.GetString();
                            Console.WriteLine("El valor de 'xid' es: " + xid);
                        }
                        if (authentication3DSProperty.TryGetProperty("cavv", out JsonElement cavvProperty) && cavvProperty.ValueKind == JsonValueKind.String)
                        {
                            cavv = cavvProperty.GetString();
                            Console.WriteLine("El valor de 'xid' es: " + cavv);
                        }
                        if (authentication3DSProperty.TryGetProperty("protocolVersion", out JsonElement protocolVersionProperty) && protocolVersionProperty.ValueKind == JsonValueKind.String)
                        {
                            protocolVersion = protocolVersionProperty.GetString();
                            Console.WriteLine("El valor de 'xid' es: " + protocolVersion);
                        }
                        if (authentication3DSProperty.TryGetProperty("directoryServerTransactionId", out JsonElement directoryServerTransactionIdProperty) && directoryServerTransactionIdProperty.ValueKind == JsonValueKind.String)
                        {
                            directoryServerTransactionId = directoryServerTransactionIdProperty.GetString();
                            Console.WriteLine("El valor de 'xid' es: " + directoryServerTransactionId);
                        }
                    }
                }

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
                if (encrypt == "1")
                {
                    ResponseCulqi json_object = new Card(security).Create(map, security.rsa_id, security.rsa_key);
                    return json_object;
                }
                else
                {
                    ResponseCulqi json_object = new Card(security).Create(map);
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

