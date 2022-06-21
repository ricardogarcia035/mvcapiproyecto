using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mvcapiproyecto.Models.WS
{
    public class AlumnoViewModel: SecurityViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Escuela { get; set; }
        public int Semestre { get; set; }
    }
}