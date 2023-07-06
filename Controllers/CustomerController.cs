using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using culqi.net;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Demo.Controllers
{
   
    [Route("api/[controller]")]
    public class CustomerController : GenericController
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
        public ResponseCulqi Post([FromBody] JsonElement json)
        {
           
            string address = json.GetProperty("address").GetString();
            string address_city = json.GetProperty("address_city").GetString();
            string country_code = json.GetProperty("country_code").GetString();
            string email = json.GetProperty("email").GetString();
            string first_name = json.GetProperty("first_name").GetString();
            string last_name = json.GetProperty("last_name").GetString();
            string phone_number = json.GetProperty("phone_number").GetString();
     
            security = securityKeys();

            Dictionary<string, object> map = new Dictionary<string, object>
            {
                {"address", address},
                {"address_city", address_city},
                {"country_code", country_code},
                {"email", email},
                {"first_name", first_name},
                {"last_name", last_name},
                {"phone_number", Convert.ToInt32(phone_number)}
            };

            ResponseCulqi json_object = new Customer(security).Create(map);
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

        protected static string GetRandomString()
        {
            string path = Path.GetRandomFileName();
            path = path.Replace(".", "");
            return path;
        }
    }
}

