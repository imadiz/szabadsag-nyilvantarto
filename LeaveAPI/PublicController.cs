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
        // GET api/public/ 
        public JObject GetCurrentTime/*A függvény neve a /-jel után jön, utána ha van, ?paraméter1=érték1&paraméter2=érték2*/()
        {
            JObject JsonDateTime = new JObject
            {
                { "CurrentDateTimeOffset", DateTimeOffset.Now }
            };
            return JsonDateTime;
        }
    }
}
