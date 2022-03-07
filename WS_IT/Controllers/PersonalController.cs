using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;

using static WS_IT.Models.PersonalModelDAL;
using Shared.Controllers;
using Shared.Helpers;
using System.Web.Http;
using Shared.Util;
using Shared.Token;

namespace WS_IT.Controllers
{
    [CorsOrigin]
    [Authorize]
    public class PersonalController : BaseController
    {
        [HttpGet]
        [ActionName("ZITG_getPersonalData")]
        public IHttpActionResult ZITG_getPersonalData()
        {
            try
            {
                DataTable dt = Instancia.getPersonalData(JWT.IdUsuario);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

    }
}
