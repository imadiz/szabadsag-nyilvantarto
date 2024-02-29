using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;


namespace LeaveAPI 
{
    public class PublicController : ApiController
    {
        // GET api/values 
        public JObject GetCurrentTime()
        {
            JObject JsonDateTime = new JObject
            {
                { "CurrentDateTimeOffset", DateTimeOffset.Now }
            };
            return JsonDateTime;
        }
    }
}
