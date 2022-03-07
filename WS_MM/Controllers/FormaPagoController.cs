using System;
using System.Data;
using static WS_MM.Models.FormaPagoModel;
using Shared.Controllers;
using Shared.Helpers;
using System.Web.Http;
using Shared.Util;


namespace WS_MM.Controllers
{
    [CorsOrigin]
    [Authorize]
    public class FormaPagoController : BaseController
    {
        [HttpGet]
        [ActionName("PMMM_getFormaPago")]
        public IHttpActionResult PMMM_getFormaPago()
        {
            try
            {
                DataTable dt = Instancia.getFormaPago(JWT.IdEmpresa);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }


        [HttpGet]
        [ActionName("PMMM_getTipoMoneda")]
        public IHttpActionResult PMMM_getTipoMoneda()
        {
            try
            {
                DataTable dt = Instancia.getTipoMoneda(JWT.IdEmpresa);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }


        

    }
}