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
    public class MaterialAlmacenController : BaseController
    {

        [HttpGet]
        [ActionName("PMMM_ListarPorMaterialYCentro")]
        public IHttpActionResult PMMM_ListarPorMaterialYCentro(int Material, int Centro)
        {
            try
            {
                DataTable dt = MaterialAlmacenModelDAL.Instancia.ListarPorEmpresaYMaterialYCentro(JWT.IdEmpresa, Material, Centro);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleError(ex));
            }
        }      

    }
}
