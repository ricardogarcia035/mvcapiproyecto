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
    public class AlumnoxProyectoController : BaseController
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
                    List<ListAlumnoxProyectoViewModel> lst = List(db);
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
        public Reply Add([FromBody] AlumnoxProyectoViewModel model)
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
                    alumnoxproyecto oAlumnoxProyecto = new alumnoxproyecto();
                    oAlumnoxProyecto.id_alumno = model.IdAlumno;
                    oAlumnoxProyecto.id_proyecto = model.IdProyecto;
                    

                    db.alumnoxproyecto.Add(oAlumnoxProyecto);
                    db.SaveChanges();

                    List<ListAlumnoxProyectoViewModel> lst = List(db);
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
        public Reply Delete([FromBody] AlumnoxProyectoViewModel model)
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
                    alumnoxproyecto oAlumnoxProyecto = db.alumnoxproyecto.Find(model.Id);

                    db.Entry(oAlumnoxProyecto).State = System.Data.Entity.EntityState.Deleted;
                    db.SaveChanges();

                    List<ListAlumnoxProyectoViewModel> lst = List(db);

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

        private bool Validate(AlumnoxProyectoViewModel model)
        {
            if (model.IdAlumno == 0)
            {
                error = "La ID de alumno es obligatorio";
                return false;
            }
            return true;
        }

        private List<ListAlumnoxProyectoViewModel> List(mvcapiEntities db)
        {
            List<ListAlumnoxProyectoViewModel> lst = (from d in db.alumnoxproyecto

                                             select new ListAlumnoxProyectoViewModel
                                             {
                                                 IdAlumno = d.id_alumno,
                                                 IdProyecto =d.id_proyecto
                                             }).ToList();
            return lst;
        }
        #endregion
    }
}