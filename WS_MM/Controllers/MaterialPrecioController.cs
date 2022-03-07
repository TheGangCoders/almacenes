using System;
using System.Data;
using Shared.Controllers;
using Shared.Helpers;
using System.Web.Http;
using Shared.Util;
using WS_MM.Models;


namespace WS_MM.Controllers
{
    [CorsOrigin]
    [Authorize]
    public class MaterialPrecioController : BaseController
    {

        [HttpGet]
        [ActionName("PMMM_ListarPorMaterialOrgVentas")]
        public IHttpActionResult PMMM_ListarPorMaterialOrgVentas(int MaterialOrgVentas)
        {
            try
            {
                DataTable dt = MaterialPrecioModelDAL.Instancia.ListarPorEmpresaYMaterialOrgVentas(JWT.IdEmpresa, MaterialOrgVentas);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleError(ex));
            }
        }      

    }
}
