using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using mvcapiproyecto.Models;

namespace mvcapiproyecto.Controllers
{
    public class BaseController : ApiController
    {
        public string error = "";
        public bool Verify(string token)
        {
            using (mvcapiEntities db = new mvcapiEntities())
            {
                if (db.user.Where(d => d.token == token && d.idEstatus == 1).Count() > 0)
                    return true;
                        }
            return false;

        }
    }
}
