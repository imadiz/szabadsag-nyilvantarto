using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace LeaveAPI
{
    [Authorize]
    public class PrivateController : ApiController
    {
        // GET api/private 
        public IEnumerable<string> Get()
        {
            return new string[] { "Minta", "Mókus" };
        }
    }
}
