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
    public class MaterialNombreComercialController : BaseController
    {

        [HttpGet]
        [ActionName("PMMM_ListarPorMaterialYOrgVentas")]
        public IHttpActionResult PMMM_ListarPorMaterialYOrgVentas(int Material, int OrgVentas)
        {
            try
            {
                DataTable dt = MaterialNombreComercialModelDAL.Instancia.ListarPorEmpresaYMaterialYOrgVentas(JWT.IdEmpresa, Material, OrgVentas);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleError(ex));
            }
        }      

    }
}
