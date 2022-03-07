using System;
using System.Data;
using static WS_MM.Models.TipoDocumentoModel;
using Shared.Controllers;
using Shared.Helpers;
using System.Web.Http;
using Shared.Util;


namespace WS_MM.Controllers 
{
    [CorsOrigin]
    [Authorize]
    public class TipoDocumentoController : BaseController
    {
        // GET: TipoDocumento
        [HttpGet]
        [ActionName("PMMM_getTipoDocumento")]
        public IHttpActionResult PMMM_getTipoDocumento()
        {
            try
            {
                DataTable dt = Instancia.getTipoDocumento(JWT.IdEmpresa);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

    }
}