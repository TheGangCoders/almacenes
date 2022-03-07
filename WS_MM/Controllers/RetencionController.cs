using System;
using System.Data;
using static WS_MM.Models.RetencionModelDAL;
using Shared.Controllers;
using Shared.Helpers;
using System.Web.Http;
using Shared.Util;
using Newtonsoft.Json;
using System.Xml;

namespace WS_MM.Controllers
{
    [CorsOrigin]
    [Authorize]
    public class RetencionController : BaseController
    {
        [HttpGet]
        [ActionName("PMMM_GetRetencion_list")]
        public IHttpActionResult PMMM_GetRetencion_list(Boolean? prmbitStatus)
        {
            try
            {
                DataTable dt = Instancia.getRetencion_list(JWT.IdEmpresa, prmbitStatus);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpGet]
        [ActionName("MM_GetRetencion")]
        public IHttpActionResult MM_GetRetencion(int? prmintActivo)
        {
            try
            {
                DataTable dt = Instancia.GetRetencion(JWT.IdEmpresa, prmintActivo);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpPost]
        [ActionName("MM_SaveUpdate_Retencion")]
        public IHttpActionResult MM_SaveUpdate_Retencion(dynamic obj)
        {
            try
            {
                int prmintRetencion = obj.Retencion;
                int prmintTipoRetencion = obj.TipoRetencion;
                string prmstrCodigo = obj.Codigo;
                string prmstrNombre = obj.Nombre;
                Decimal prmdecPorcentaje = obj.PorcentajeRetencion;
                int prmintActivo = obj.Activo;

                int idEmpresa = JWT.IdEmpresa;
                string username = JWT.Login;

                int dt = Instancia.SaveUpdate_Retencion(prmintRetencion, prmintTipoRetencion, prmstrCodigo, prmstrNombre, prmdecPorcentaje, prmintActivo, idEmpresa, username);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpGet]
        [ActionName("MM_Anular_Retencion")]
        public IHttpActionResult MM_Anular_Retencion(int prmintRetencion)
        {
            try
            {
                string prmstrUsuario = JWT.Login;

                int dt = Instancia.Anular_Retencion(prmintRetencion, prmstrUsuario);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }
    }
}
