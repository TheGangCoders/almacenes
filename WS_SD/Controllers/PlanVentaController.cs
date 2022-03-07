using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using static WS_SD.Models.PlanVentaModel;
using System.Web;
using System.Security.Cryptography;
using Shared.Controllers;
using Shared.Helpers;
using System.Web.Http;
using Shared.Util;
using Shared.Token;
using Newtonsoft.Json;
namespace WS_SD.Controllers
{
    [CorsOrigin]
    [Authorize]
    public class PlanVentaController : BaseController
    {
        [HttpGet]
        [ActionName("SD_RptPlanVentas")]
        public IHttpActionResult SD_RptPlanVentas(string prmSociedad, string prmTipoVenta, string prmTipoFecha, string pmrFechaInicio, string prmFechaFin, string pmrintCliente, string pmrGrupoArticulo, string pmrTipoMaterial)
        {
            try
            {
                DataTable dt = Instancia.RptPlanVentas(JWT.IdEmpresa, prmSociedad, prmTipoVenta, prmTipoFecha, pmrFechaInicio, prmFechaFin, pmrintCliente, pmrGrupoArticulo, pmrTipoMaterial);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }
    }
}
