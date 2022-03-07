using System;
using System.Data;

using WS_MM.Models;
using Shared.Controllers;
using Shared.Helpers;
using System.Web.Http;
using Shared.Util;
using Newtonsoft.Json;

namespace WS_MM.Controllers
{
    [CorsOrigin]
    [Authorize]
    public class OrdenCompraController : BaseController
    {

        [HttpGet]
        [ActionName("PMMM_ObtenerPorCorrelativo")]
        public IHttpActionResult PMMM_ObtenerPorCorrelativo(string Correlativo)
        {
            try
            {
                DataTable dt = OrdenCompraModel.Instancia.ObtenerPorCorrelativo(JWT.IdEmpresa, Correlativo);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleError(ex));
            }
        }

        [HttpGet]
        [ActionName("PMMM_ObtenerOrdenServicioCorrelativo")]
        public IHttpActionResult PMMM_ObtenerOrdenServicioCorrelativo(string Correlativo)
        {
            try
            {
                DataTable dt = OrdenCompraModel.Instancia.ObtenerOrdenServicioCorrelativo(JWT.IdEmpresa, Correlativo);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleError(ex));
            }
        }

        [HttpGet]
        [ActionName("PMMM_ObtenerOrdenTransporteCorrelativo")]
        public IHttpActionResult PMMM_ObtenerOrdenTransporteCorrelativo(string Correlativo)
        {
            try
            {
                DataTable dt = OrdenCompraModel.Instancia.ObtenerOrdenTransporteCorrelativo(JWT.IdEmpresa, Correlativo);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleError(ex));
            }
        }


    }
}
