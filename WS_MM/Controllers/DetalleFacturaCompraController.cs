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
    public class DetalleFacturaCompraController : BaseController
    {
        [HttpGet]
        [ActionName("PMMM_Obtener")]
        public IHttpActionResult PMMM_Obtener(int FacturaCompra)
        {
            try
            {
                DataTable dt = DetalleFacturaCompraModel.Instancia.Obtener(JWT.IdEmpresa, FacturaCompra);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleError(ex));
            }
        }
    }
}
