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
    public class DetalleOrdenCompraController : BaseController
    {
        [HttpGet]
        [ActionName("PMMM_Obtener")]
        public IHttpActionResult PMMM_Obtener(int OrdenCompra)
        {
            try
            {
                DataTable dt = DetalleOrdenCompraModel.Instancia.Obtener(JWT.IdEmpresa, OrdenCompra);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleError(ex));
            }
        }

        [HttpGet]
        [ActionName("PMMM_ObtenerFaltante")]
        public IHttpActionResult PMMM_ObtenerFaltante(int OrdenCompra)
        {
            try
            {
                DataTable dt = DetalleOrdenCompraModel.Instancia.ObtenerFaltante(JWT.IdEmpresa, OrdenCompra);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleError(ex));
            }
        }

        [HttpGet]
        [ActionName("PMMM_ObtenerFaltanteServicio")]
        public IHttpActionResult PMMM_ObtenerFaltanteServicio(int OrdenCompra)
        {
            try
            {
                DataTable dt = DetalleOrdenCompraModel.Instancia.ObtenerFaltanteServicio(JWT.IdEmpresa, OrdenCompra);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleError(ex));
            }
        }

        [HttpGet]
        [ActionName("PMMM_ObtenerFaltanteTransporte")]
        public IHttpActionResult PMMM_ObtenerFaltanteTransporte(int OrdenCompra)
        {
            try
            {
                DataTable dt = DetalleOrdenCompraModel.Instancia.ObtenerFaltanteTransporte(JWT.IdEmpresa, OrdenCompra);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleError(ex));
            }
        }


    }
}
