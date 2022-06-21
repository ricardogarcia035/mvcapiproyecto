
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mvcapiproyecto.Models.WS
{
    public class ProyectoViewModel: SecurityViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        public DateTime inicio { get; set; }
        public DateTime final { get; set; }

    }
}