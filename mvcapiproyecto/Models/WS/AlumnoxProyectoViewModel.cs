using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mvcapiproyecto.Models.WS
{
    public class AlumnoxProyectoViewModel: SecurityViewModel
    {
        public int Id { get; set; }
        public int IdAlumno { get; set; }
        public int IdProyecto { get; set; }
    }
}