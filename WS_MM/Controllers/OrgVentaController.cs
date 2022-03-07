using System;
using System.Data;
using static WS_MM.Models.OrgVentaModelDAL;
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
    public class OrgVentaController : BaseController
    {
        [HttpGet]
        [ActionName("PMMM_GetOrgVenta_list")]
        public IHttpActionResult PMMM_GetOrgVenta_List(int prmSociedad, Boolean? prmbitStatus)
        {
            try
            {
                DataTable dt = Instancia.getOrgVenta_list(JWT.IdEmpresa, prmSociedad, prmbitStatus);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }
    }
}
