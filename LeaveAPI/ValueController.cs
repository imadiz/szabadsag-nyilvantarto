using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace LeaveAPI
{
    public class ValueController : ApiController
    {
        // GET api/values 
        public JObject Get_time(string p = null)
        {
            switch (p)
            {
                case "time":
                    return new JObject
                    {
                        { "CurrentDateTimeOffset", DateTimeOffset.Now }
                    };
                default:
                    return new JObject
                    {
                        { "Error", "Unkown parameter" }
                    };
            }
        }

        // GET api/values/
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values 
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5 
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5 
        public void Delete(int id)
        {
        }
    }
}
