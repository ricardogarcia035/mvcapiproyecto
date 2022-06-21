using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using mvcapiproyecto.Models;
using mvcapiproyecto.Models.WS;

namespace mvcapiproyecto.Controllers
{
    public class AccessController : ApiController
    {
        [HttpGet]
        public Reply HelloWorld() {

            Reply oR = new Reply();
            oR.result = 1;
            oR.message = "Hi World";
            return oR;
        }

        [HttpPost]
        public Reply Login([FromBody]AccessViewModel model)
        {
            Reply oR = new Reply();
            try
            {
                using (mvcapiEntities  db = new mvcapiEntities()) 
                {
                    var lst = db.user.Where(d => d.email == model.email && d.password == model.password && d.idEstatus == 1);
                    if (lst.Count() > 0)
                    {
                        oR.result = 1;
                        oR.data = Guid.NewGuid().ToString();

                        user oUser = lst.First();
                        oUser.token = (string)oR.data;
                        db.Entry(oUser).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }
                    else {
                        oR.message = "Datos incorrectos";
                    }

                }
            }
            catch (Exception ex)
            {
                oR.result = 0;
                oR.message = "Ocurrio un error";

            }
            return oR;
        }
    }
}
