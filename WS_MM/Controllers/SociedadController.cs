using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;

using static WS_MM.Models.SociedadModelDAL;
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
    public class SociedadController : BaseController
    {
        [HttpGet]
        [ActionName("PMMM_GetSociedades")]
        public IHttpActionResult PMMM_GetSociedades(Boolean? prmbitActivo)
        {
            try
            {
                DataTable dt = Instancia.GetSociedades(JWT.IdEmpresa, prmbitActivo);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }
    }
}
