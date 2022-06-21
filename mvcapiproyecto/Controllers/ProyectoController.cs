using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using mvcapiproyecto.Models.WS;
using mvcapiproyecto.Models;

namespace mvcapiproyecto.Controllers
{
    public class ProyectoController : BaseController
    {


        [HttpPost]
        public Reply Get([FromBody] SecurityViewModel model)
        {
            Reply oR = new Reply();
            oR.result = 0;

            if (!Verify(model.token))
            {
                oR.message = "No autorizado";
                return oR;
            }

            try
            {

                using (mvcapiEntities db = new mvcapiEntities())
                {
                    List<ListProyectoViewModel> lst = List(db);
                    oR.data = lst;
                    oR.result = 1;
                }
            }
            catch (Exception ex)
            {
                oR.message = "Ocurrio un error en el servidor";
            }
            return oR;
        }
        [HttpPost]
        public Reply Add([FromBody] ProyectoViewModel model)
        {
            Reply oR = new Reply();
            oR.result = 0;

            if (!Verify(model.token))
            {
                oR.message = "No autorizado";
                return oR;
            }

            //validaciones
            if (!Validate(model))
            {
                oR.message = error;
                return oR;
            }

            try
            {
                using (mvcapiEntities db = new mvcapiEntities())
                {
                    proyecto oProyecto = new proyecto();
                    oProyecto.nombre_p = model.Nombre;
                    oProyecto.fecha_inicio = model.inicio;
                    oProyecto.fecha_final = model.final;

                    db.proyecto.Add(oProyecto);
                    db.SaveChanges();

                    List<ListProyectoViewModel> lst = List(db);
                    oR.result = 1;
                    oR.data = lst;

                }
            }
            catch (Exception ex)
            {

                oR.message = "Ocurrio un erro en el servidor";

            }
            return oR;


        }

        [HttpPost]
        public Reply Edit([FromBody] ProyectoViewModel model)
        {
            Reply oR = new Reply();
            oR.result = 0;

            if (!Verify(model.token))
            {
                oR.message = "No autorizado";
                return oR;
            }

            //validaciones
            if (!Validate(model))
            {
                oR.message = error;
                return oR;
            }

            try
            {
                using (mvcapiEntities db = new mvcapiEntities())
                {
                    proyecto oProyecto = db.proyecto.Find(model.Id);
                    oProyecto.nombre_p = model.Nombre;
                    oProyecto.fecha_inicio = model.inicio;
                    oProyecto.fecha_final = model.final;

                    db.Entry(oProyecto).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();



                    List<ListProyectoViewModel> lst = List(db);

                    oR.result = 1;
                    oR.data = lst;

                }
            }
            catch (Exception ex)
            {

                oR.message = "Ocurrio un erro en el servidor";

            }
            return oR;


        }

        [HttpPost]
        public Reply Delete([FromBody] ProyectoViewModel model)
        {
            Reply oR = new Reply();
            oR.result = 0;

            if (!Verify(model.token))
            {
                oR.message = "No autorizado";
                return oR;
            }



            try
            {
                using (mvcapiEntities db = new mvcapiEntities())
                {
                    proyecto oProyecto = db.proyecto.Find(model.Id);


                    db.Entry(oProyecto).State = System.Data.Entity.EntityState.Deleted;
                    db.SaveChanges();


                    List<ListProyectoViewModel> lst = List(db);

                    oR.result = 1;
                    oR.data = lst;

                }
            }
            catch (Exception ex)
            {

                oR.message = "Ocurrio un erro en el servidor";

            }
            return oR;


        }

        #region HELPERS

        private bool Validate(ProyectoViewModel model)
        {
            if (model.Nombre == "")
            {
                error = "El nombre es obligatorio";
                return false;
            }

            return true;
        }

        private List<ListProyectoViewModel> List(mvcapiEntities db)
        {
            List<ListProyectoViewModel> lst = (from d in db.proyecto

                                             select new ListProyectoViewModel
                                             {
                                                 Nombre = d.nombre_p,
                                                


                                             }).ToList();
            return lst;
        }

        #endregion
    }
}
