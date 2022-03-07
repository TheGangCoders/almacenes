using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WS_IT.Constants;

namespace WS_IT.DTOs
{
    public class OpcionVistaAccionDTO
    {
        public int IdOpcion { get; set; }
        public int? IdOpcionPadre { get; set; }
        public string Opcion_Nombre { get; set; }
        public string Opcion_Plataforma { get; set; }
        public string Opcion_Tooltip { get; set; }
        public string Opcion_NombreProyecto { get; set; }
        public string Opcion_Icono { get; set; }
        public bool Opcion_VisibleNavegacion { get; set; }
        public int Opcion_Orden { get; set; }
        public int? IdVista { get; set; }
        public string Vista_Nombre { get; set; }
        public string Vista_Descripcion { get; set; }
        public string Vista_Codigo { get; set; }
        public int? IdAccion { get; set; }
        public string Accion_Nombre { get; set; }
        public string Accion_Codigo { get; set; }
        public int? IdPermiso { get; set; }
        public bool Checked { get; set; }

        public string ObtenerTipoNavegacion()
        {
            if(IdAccion != null)
            {
                return TipoNavegacion.ACCION;
            }
            if(IdVista != null)
            {
                return TipoNavegacion.VISTA;
            }
            return TipoNavegacion.OPCION;
        }

        public bool TieneAccion()
        {
            return IdAccion != null;
        }

        public bool TieneVista()
        {
            return IdVista != null;
        }

        public bool EsPrimerPadre()
        {
            return IdOpcionPadre == null;
        }

    }
}