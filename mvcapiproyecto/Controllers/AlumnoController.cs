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
    public class AlumnoController : BaseController
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
                    List<ListAlumnoViewModel> lst = List(db);
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
        public Reply Add([FromBody] AlumnoViewModel model)
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
                    alumno oAlumno = new alumno();
                    oAlumno.nombre_a = model.Nombre;
                    oAlumno.apellido_a = model.Apellido;
                    oAlumno.escuela_a = model.Escuela;
                    oAlumno.semestre_a = model.Semestre;

                    db.alumno.Add(oAlumno);
                    db.SaveChanges();

                    List<ListAlumnoViewModel> lst = List(db);
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
        public Reply Edit([FromBody] AlumnoViewModel model)
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
                    alumno oAlumno = db.alumno.Find(model.Id);
                    oAlumno.nombre_a = model.Nombre;
                    oAlumno.apellido_a = model.Apellido;
                    oAlumno.escuela_a = model.Escuela;
                    oAlumno.semestre_a = model.Semestre;

                    db.Entry(oAlumno).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();

                    List<ListAlumnoViewModel> lst = List(db);

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
        public Reply Delete([FromBody] AlumnoViewModel model)
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
                    alumno oAlumno = db.alumno.Find(model.Id);

                    db.Entry(oAlumno).State = System.Data.Entity.EntityState.Deleted;
                    db.SaveChanges();

                    List<ListAlumnoViewModel> lst = List(db);

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

        private bool Validate(AlumnoViewModel model)
        {
            if (model.Nombre == "")
            {
                error = "El nombre es obligatorio";
                return false;
            }
            return true;
        }

        private List<ListAlumnoViewModel> List(mvcapiEntities db)
        {
            List<ListAlumnoViewModel> lst = (from d in db.alumno
                                             
                                             select new ListAlumnoViewModel
                                             {
                                                 Nombre=d.nombre_a,
                                                 Apellido= d.apellido_a
                                             }).ToList();
            return lst;
        }
        #endregion
    }
}
