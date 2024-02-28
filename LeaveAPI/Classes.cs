using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveAPI
{
    public class APIresponse
    {
        public string ApiName;
        public int statusCode = 200;
        public string status = "OK";        
    }
    public class Classes
    {
        #region ThereforeHoliday
        public class ThereforeHoliday
        {
            public string holiday;
            public string description;
        }

        public class ThereforeHolidays 
        {
            public APIresponse ApiResponse= new APIresponse();
            public List<ThereforeHoliday> Holidays = new List<ThereforeHoliday>();
        }
        #endregion
    }
}
