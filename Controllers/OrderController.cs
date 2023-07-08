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
    public class OrderController : GenericController
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
            security = securityKeys();


            Int32 amount = json.GetProperty("amount").GetInt32();
            string currency_code = json.GetProperty("currency_code").GetString();          
            string description = json.GetProperty("description").GetString();
            string order_number = json.GetProperty("order_number").GetString();

           
            DateTimeOffset fechaActual = DateTimeOffset.Now;   
            DateTimeOffset fechaMasUnDia = fechaActual.AddDays(1);           
            long epoch = fechaMasUnDia.ToUnixTimeSeconds();

            Dictionary<string, object> client_details = new Dictionary<string, object>
                {
                    {"first_name","Jordan"},
                    {"last_name", "Diaz Diaz"},                    
                    {"email", "jordandiaz2016@gmail.com"},
                    {"phone_number", "921484773"}
                }; 
          
            Dictionary<string, object> map = new Dictionary<string, object>
                {
                    {"amount",amount},
                    {"currency_code", currency_code},                    
                    {"description", description},
                    {"order_number", order_number},
                    {"client_details", client_details},
                    {"expiration_date", epoch}
                };

              ResponseCulqi json_object = new Order(security).Create(map);
              return json_object;
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

