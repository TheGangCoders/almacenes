using System;
using System.Data;

using static WS_MM.Models.ReservaMaterialModel;
using WS_MM.Models;
using Shared.Controllers;
using Shared.Helpers;
using System.Web.Http;
using Shared.Util;
using Shared.Token;
using Newtonsoft.Json;
using System.Xml;

namespace WS_MM.Controllers
{
    [CorsOrigin]
    [Authorize]
    public class ReservaMaterialController : BaseController
    {
        [HttpGet]
        [ActionName("PMMM_getSearchMaterialesAlmacen")]
        public IHttpActionResult PMMM_getSearchMaterialesAlmacen(string CodMaterial, string Material, string TipoMaterial, string GrupoArticulo, string Almacen)
        {
            try
            {
                DataTable dt = Instancia.getSearchMaterialesAlmacen(JWT.IdEmpresa, CodMaterial, Material, TipoMaterial, GrupoArticulo, Almacen);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }


        [HttpPost]
        [ActionName("PMMM_saveUpdateReservaMaterial")]
        public IHttpActionResult PMMM_saveUpdateReservaMaterial(dynamic obj)
        {
            try
            {
                int ID=obj.ID;
                string FechaReserva=obj.FechaReserva;
                string FechaPlanificada=obj.FechaPlanificada;
                //string FechaSalida=obj.FechaSalida;
                int idAlmacen=obj.Almacen;
                int idEmpresa = JWT.IdEmpresa;

                string XML = obj.xml;
                string Usuario =JWT.Login;

                DataTable dt = Instancia.saveUpdateReservaMaterial(ID, FechaReserva, FechaPlanificada, /*FechaSalida,*/ idAlmacen, idEmpresa, XML, Usuario);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }


        
        [HttpGet]
        [ActionName("PMMM_getEstadoReservaMaterial_list")]
        public IHttpActionResult PMMM_getEstadoReservaMaterial_list()
        {
            try
            {
                DataTable dt = Instancia.getEstadoReservaMaterial_list();
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }


        [HttpGet]
        [ActionName("PMMM_getReservaMaterial_list")]
        public IHttpActionResult PMMM_getReservaMaterial_list(string prmstrFechaInicio, string prmstrFechaFin, string Almacen, string Estado)
        {
            try
            {
                int idEmpresa = JWT.IdEmpresa;
                string usuario = JWT.Login;
                DataTable dt = Instancia.getReservaMaterial_list(idEmpresa, prmstrFechaInicio, prmstrFechaFin, Almacen,Estado, usuario);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpGet]
        [ActionName("PMMM_getReservaMaterial_Data")]
        public IHttpActionResult PMMM_getReservaMaterial_Data(int prmintReserva)
        {
            try
            {
                int idEmpresa = JWT.IdEmpresa;
                DataTableCollection dt = Instancia.getReservaMaterial_Data(prmintReserva, idEmpresa);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpPost]
        [ActionName("PMMM_enableDisable_ReservaMaterial")]
        public IHttpActionResult PMMM_enableDisable_ReservaMaterial(dynamic obj)
        {
            try
            {
                int ReservaMaterial = obj.ID;
                bool FutureStatus = obj.FutureStatus;
                string Usuario = JWT.Login;
                DataTable dt = Instancia.enableDisable_ReservaMaterial(ReservaMaterial, FutureStatus, Usuario);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        
    }
}
