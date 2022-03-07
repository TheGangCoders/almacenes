using System;
using System.Data;

using static WS_MM.Models.CentroCostoModel;
using WS_MM.Models;
using Shared.Controllers;
using Shared.Helpers;
using System.Web.Http;
using Shared.Util;
using Shared.Token;
using Newtonsoft.Json;


namespace WS_MM.Controllers
{
    [CorsOrigin]
    [Authorize]
    public class CentroCostoController : BaseController
    {
        [HttpGet]
        [ActionName("PMMM_getCentroCosto")]
        public IHttpActionResult PMMM_getCentroCosto()
        {
            try
            {
                DataTable dt = Instancia.getCentroCosto();
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }
    }
}