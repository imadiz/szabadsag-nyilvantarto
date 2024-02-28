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
    public class PublicController: ApiController
    {
        // GET api/values 
        public Classes.ThereforeHolidays GetThereforeHolidays(string from, string to)
        {
            return new Classes.ThereforeHolidays();
        }
    }
}
