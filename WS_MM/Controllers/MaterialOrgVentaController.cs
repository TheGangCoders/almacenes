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
    public class MaterialOrgVentaController : BaseController
    {

        [HttpGet]
        [ActionName("PMMM_ListarPorMaterialYSociedad")]
        public IHttpActionResult PMMM_ListarPorMaterialYSociedad(int Material, int Sociedad)
        {
            try
            {
                DataTable dt = MaterialOrgVentaModelDAL.Instancia.ListarPorEmpresaYMaterialYSociedad(JWT.IdEmpresa, Material, Sociedad);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleError(ex));
            }
        }      

    }
}
