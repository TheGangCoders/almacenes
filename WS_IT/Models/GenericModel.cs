using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Shared.Util;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web;
using WS_IT.Constants;
using WS_IT.DTOs;

namespace WS_IT.Models
{
    public class GenericModel
    {
        private static readonly GenericModel _instancia = new GenericModel();
        public static GenericModel Instancia
        {
            get { return GenericModel._instancia; }
        }

        private List<OpcionVistaAccionDTO> ConvertDataTableToListOpcionVistaAccionDTO(DataTable dt) {
            List<OpcionVistaAccionDTO> listOpcionesVistasAcciones = new List<OpcionVistaAccionDTO>();
            foreach (DataRow dr in dt.Rows) {
                var tmp = new OpcionVistaAccionDTO();
                if (!Convert.IsDBNull(dr["IdOpcion"]))
                {
                    tmp.IdOpcion = Convert.ToInt32(dr["IdOpcion"]);
                }
                if (!Convert.IsDBNull(dr["IdOpcionPadre"]))
                {
                    tmp.IdOpcionPadre = Convert.ToInt32(dr["IdOpcionPadre"]);
                }
                if (!Convert.IsDBNull(dr["Opcion_Nombre"]))
                {
                    tmp.Opcion_Nombre = Convert.ToString(dr["Opcion_Nombre"]);
                }
                if (!Convert.IsDBNull(dr["Opcion_Plataforma"]))
                {
                    tmp.Opcion_Plataforma = Convert.ToString(dr["Opcion_Plataforma"]);
                }
                if (!Convert.IsDBNull(dr["Opcion_Tooltip"]))
                {
                    tmp.Opcion_Tooltip = Convert.ToString(dr["Opcion_Tooltip"]);
                }
                if (!Convert.IsDBNull(dr["Opcion_NombreProyecto"]))
                {
                    tmp.Opcion_NombreProyecto = Convert.ToString(dr["Opcion_NombreProyecto"]);
                }
                if (!Convert.IsDBNull(dr["Opcion_Icono"]))
                {
                    tmp.Opcion_Icono = Convert.ToString(dr["Opcion_Icono"]);
                }
                if (!Convert.IsDBNull(dr["Opcion_VisibleNavegacion"]))
                {
                    tmp.Opcion_VisibleNavegacion = Convert.ToBoolean(dr["Opcion_VisibleNavegacion"]);
                }
                if (!Convert.IsDBNull(dr["Opcion_Orden"]))
                {
                    tmp.Opcion_Orden = Convert.ToInt32(dr["Opcion_Orden"]);
                }
                if (!Convert.IsDBNull(dr["IdVista"]))
                {
                    tmp.IdVista = Convert.ToInt32(dr["IdVista"]);
                }
                if (!Convert.IsDBNull(dr["Vista_Nombre"]))
                {
                    tmp.Vista_Nombre = Convert.ToString(dr["Vista_Nombre"]);
                }
                if (!Convert.IsDBNull(dr["Vista_Descripcion"]))
                {
                    tmp.Vista_Descripcion = Convert.ToString(dr["Vista_Descripcion"]);
                }
                if (!Convert.IsDBNull(dr["Vista_Codigo"]))
                {
                    tmp.Vista_Codigo = Convert.ToString(dr["Vista_Codigo"]);
                }
                if (!Convert.IsDBNull(dr["IdAccion"]))
                {
                    tmp.IdAccion = Convert.ToInt32(dr["IdAccion"]);
                }
                if (!Convert.IsDBNull(dr["Accion_Nombre"]))
                {
                    tmp.Accion_Nombre = Convert.ToString(dr["Accion_Nombre"]);
                }
                if (!Convert.IsDBNull(dr["Accion_Codigo"]))
                {
                    tmp.Accion_Codigo = Convert.ToString(dr["Accion_Codigo"]);
                }
                listOpcionesVistasAcciones.Add(tmp);
            }
            return listOpcionesVistasAcciones;
        }

        public List<OpcionVistaAccionDTO> ObtenerOpcionesVistasAcciones()
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspIT_ObtenerOpcionesVistasAccionesWEB");
            try
            {
                List<OpcionVistaAccionDTO> listOpcionesVistasAcciones = ConvertDataTableToListOpcionVistaAccionDTO(db.ExecuteDataSet(cmd).Tables[0]);
                return listOpcionesVistasAcciones;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public NavegacionDTO GenerarNavegacion(OpcionVistaAccionDTO opcionVistaAccionSeleccionado, int nivel, string tipoNavegacion, List<OpcionVistaAccionDTO> listOpcionesVistasAcciones)
        {
            if (opcionVistaAccionSeleccionado == null)
            {
                NavegacionDTO navegacionRaiz = new NavegacionDTO
                {
                    Tipo = TipoNavegacion.RAIZ,
                    Nombre = "Raiz",
                    Nivel = nivel,
                    TipoSubNavegacion = TipoNavegacion.NOTHING,
                    SubNavegacion = new List<NavegacionDTO>()
                };
                bool esPadreOpcion = listOpcionesVistasAcciones
                    .Where(opcionVistaAccion => opcionVistaAccion.EsPrimerPadre())
                    .Count() > 0;
                if (esPadreOpcion)
                {
                    List<int> idOpciones = new List<int>();
                    navegacionRaiz.TipoSubNavegacion = TipoNavegacion.OPCION;
                    listOpcionesVistasAcciones
                        .Where(opcionVistaAccion => opcionVistaAccion.EsPrimerPadre())
                        .ToList()
                        .ForEach(opcionVistaAccion =>
                        {
                            if(idOpciones.IndexOf(opcionVistaAccion.IdOpcion) == -1)
                            {
                                idOpciones.Add(opcionVistaAccion.IdOpcion);
                                navegacionRaiz.SubNavegacion.Add(GenerarNavegacion(opcionVistaAccion, nivel + 1, TipoNavegacion.OPCION, listOpcionesVistasAcciones));
                            }
                        });

                    navegacionRaiz.SubNavegacion
                        .Sort((navegacion1, navegacion2) => navegacion1.Orden.CompareTo(navegacion2.Orden));
                }
                return navegacionRaiz;
            }
            if (tipoNavegacion.Equals(TipoNavegacion.OPCION))
            {
                NavegacionDTO navegacionOpcion = new NavegacionDTO
                {
                    Tipo = TipoNavegacion.OPCION,
                    Nombre = opcionVistaAccionSeleccionado.Opcion_Nombre,
                    Url = opcionVistaAccionSeleccionado.Opcion_NombreProyecto,
                    Icono = opcionVistaAccionSeleccionado.Opcion_Icono,
                    VisibleNavegacion = opcionVistaAccionSeleccionado.Opcion_VisibleNavegacion,
                    Nivel = nivel,
                    Orden = opcionVistaAccionSeleccionado.Opcion_Orden,
                    TipoSubNavegacion = TipoNavegacion.NOTHING,
                    SubNavegacion = new List<NavegacionDTO>()
                };
                bool esPadreOpcion = listOpcionesVistasAcciones
                    .Where(opcionVistaAccion => opcionVistaAccion.IdOpcionPadre.Equals(opcionVistaAccionSeleccionado.IdOpcion))
                    .Count() > 0;
                if (esPadreOpcion)
                {
                    List<int> idOpciones = new List<int>();
                    navegacionOpcion.TipoSubNavegacion = TipoNavegacion.OPCION;
                    listOpcionesVistasAcciones
                    .Where(opcionVistaAccion => opcionVistaAccion.IdOpcionPadre.Equals(opcionVistaAccionSeleccionado.IdOpcion))
                    .ToList()
                    .ForEach(opcionVistaAccion =>
                    {
                        if (idOpciones.IndexOf(opcionVistaAccion.IdOpcion) == -1)
                        {
                            idOpciones.Add(opcionVistaAccion.IdOpcion);
                            navegacionOpcion.SubNavegacion.Add(GenerarNavegacion(opcionVistaAccion, nivel + 1, TipoNavegacion.OPCION, listOpcionesVistasAcciones));
                        }
                    });

                    navegacionOpcion.SubNavegacion
                        .Sort((navegacion1, navegacion2) => navegacion1.Orden.CompareTo(navegacion2.Orden));
                }
                else
                {
                    bool esPadreVista = listOpcionesVistasAcciones
                        .Where(opcionVistaAccion => opcionVistaAccion.IdOpcion.Equals(opcionVistaAccionSeleccionado.IdOpcion) &&
                                                    opcionVistaAccion.TieneVista())
                        .Count() > 0;
                    if (esPadreVista)
                    {
                        List<string> idVistas = new List<string>();
                        navegacionOpcion.TipoSubNavegacion = TipoNavegacion.VISTA;
                        listOpcionesVistasAcciones
                        .Where(opcionVistaAccion => opcionVistaAccion.IdOpcion.Equals(opcionVistaAccionSeleccionado.IdOpcion) &&
                                                    opcionVistaAccion.TieneVista())
                        .ToList()
                        .ForEach(opcionVistaAccion =>
                        {
                            if (idVistas.IndexOf(opcionVistaAccion.IdOpcion + "-" + opcionVistaAccion.IdVista) == -1)
                            {
                                idVistas.Add(opcionVistaAccion.IdOpcion + "-" + opcionVistaAccion.IdVista);
                                navegacionOpcion.SubNavegacion.Add(GenerarNavegacion(opcionVistaAccion, nivel + 1, TipoNavegacion.VISTA, listOpcionesVistasAcciones));
                            }
                        });
                    }
                }
                return navegacionOpcion;
            }
            if (tipoNavegacion.Equals(TipoNavegacion.VISTA))
            {
                NavegacionDTO navegacionVista = new NavegacionDTO
                {
                    Tipo = TipoNavegacion.VISTA,
                    Codigo = opcionVistaAccionSeleccionado.Vista_Codigo,
                    Nombre = opcionVistaAccionSeleccionado.Vista_Nombre,
                    Nivel = nivel,
                    TipoSubNavegacion = TipoNavegacion.NOTHING,
                    SubNavegacion = new List<NavegacionDTO>()
                };

                bool esPadreAccion = listOpcionesVistasAcciones
                    .Where(opcionVistaAccion => opcionVistaAccion.IdVista.Equals(opcionVistaAccionSeleccionado.IdVista) &&
                                                opcionVistaAccion.ObtenerTipoNavegacion().Equals(TipoNavegacion.ACCION))
                    .Count() > 0;
                if (esPadreAccion)
                {
                    navegacionVista.TipoSubNavegacion = TipoNavegacion.ACCION;
                    listOpcionesVistasAcciones
                    .Where(opcionVistaAccion => opcionVistaAccion.IdVista.Equals(opcionVistaAccionSeleccionado.IdVista) &&
                                                opcionVistaAccion.ObtenerTipoNavegacion().Equals(TipoNavegacion.ACCION))
                    .ToList()
                    .ForEach(opcionVistaAccion =>
                    {
                        navegacionVista.SubNavegacion.Add(GenerarNavegacion(opcionVistaAccion, nivel + 1, TipoNavegacion.ACCION, listOpcionesVistasAcciones));
                    });
                }
                return navegacionVista;
            }

            if (tipoNavegacion.Equals(TipoNavegacion.ACCION))
            {
                NavegacionDTO navegacionAccion = new NavegacionDTO
                {
                    Tipo = TipoNavegacion.ACCION,
                    Codigo = opcionVistaAccionSeleccionado.Accion_Codigo,
                    Nombre = opcionVistaAccionSeleccionado.Accion_Nombre,
                    Nivel = nivel,
                    TipoSubNavegacion = TipoNavegacion.NOTHING,
                    SubNavegacion = new List<NavegacionDTO>(),
                    Opcion = opcionVistaAccionSeleccionado.IdOpcion,
                    Vista = opcionVistaAccionSeleccionado.IdVista,
                    Accion = opcionVistaAccionSeleccionado.IdAccion,
                    Permiso = opcionVistaAccionSeleccionado.IdPermiso, //Permisos
                    Checked = opcionVistaAccionSeleccionado.Checked //Permisos
                };
                return navegacionAccion;
            }
            return null;
        }

        public List<OpcionVistaAccionDTO> GenerarPermisos(bool first, List<PermisoDTO> listPermisos, List<OpcionVistaAccionDTO> listOpcionesVistasAcciones, OpcionVistaAccionDTO opcionVistaAccionSeleccionado)
        {
            List<OpcionVistaAccionDTO> listOpcionesVistasAccionesFiltrado = new List<OpcionVistaAccionDTO>();
            if (first)
            {
                listPermisos
                    .ForEach(permiso =>
                    {
                        List<OpcionVistaAccionDTO> listOpcionesVistasAccionesPermiso = listOpcionesVistasAcciones
                            .Where(opcionVistaAccion => permiso.IdOpcion.Equals(opcionVistaAccion.IdOpcion) &&
                                                        permiso.IdVista.Equals(opcionVistaAccion.IdVista) &&
                                                        permiso.IdAccion.Equals(opcionVistaAccion.IdAccion))
                            .ToList();
                        if (listOpcionesVistasAccionesPermiso.Count() > 0)
                        {
                            listOpcionesVistasAccionesFiltrado.Add(listOpcionesVistasAccionesPermiso[0]);
                            listOpcionesVistasAccionesFiltrado.AddRange(GenerarPermisos(false, listPermisos, listOpcionesVistasAcciones, listOpcionesVistasAccionesPermiso[0]));
                        }
                    });
                listOpcionesVistasAccionesFiltrado = listOpcionesVistasAccionesFiltrado.Distinct().ToList();
            }
            else
            {
                if (opcionVistaAccionSeleccionado.IdOpcionPadre != null)
                {
                    List<OpcionVistaAccionDTO> listOpcionesVistasAccionesPadre = listOpcionesVistasAcciones
                           .Where(opcionVistaAccion => opcionVistaAccion.IdOpcion.Equals(opcionVistaAccionSeleccionado.IdOpcionPadre))
                           .ToList();
                    if (listOpcionesVistasAccionesPadre.Count() > 0)
                    {
                        listOpcionesVistasAccionesFiltrado.Add(listOpcionesVistasAccionesPadre[0]);
                        listOpcionesVistasAccionesFiltrado.AddRange(GenerarPermisos(false, listPermisos, listOpcionesVistasAcciones, listOpcionesVistasAccionesPadre[0]));
                    }
                }
            }
            return listOpcionesVistasAccionesFiltrado;
        }
    }
}