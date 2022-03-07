using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WS_IT.DTOs
{
    public class NavegacionDTO
    {
        public string Tipo { get; set; }
        public int Nivel { get; set; }
        public int Orden { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Icono { get; set; }
        public string Url { get; set; }
        public bool VisibleNavegacion { get; set; }
        public string TipoSubNavegacion { get; set; }
        public List<NavegacionDTO> SubNavegacion { get; set; }
        public int? Opcion { get; set; }
        public int? Vista { get; set; }
        public int? Accion { get; set; }
        public int? Permiso { get; set; }
        public bool Checked { get; set; }

    }
}