using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;

using static WS_MM.Models.PuntoAcopioModel;
using System.Web;
using System.Security.Cryptography;
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
    public class PuntoAcopioController : BaseController
    {
        [HttpGet]
        [ActionName("PMMM_GetPuntoAcopio")]
        public IHttpActionResult PMMM_GetPuntoAcopio(string prmintEmpresa, int prmintCentro, Boolean? prmbitActivo)
        {
            try
            {
                DataTable dt = Instancia.GetPuntoAcopio(JWT.IdEmpresa, prmintCentro, prmbitActivo);
                string json = JsonConvert.SerializeObject(dt, Newtonsoft.Json.Formatting.None);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

    }
}
