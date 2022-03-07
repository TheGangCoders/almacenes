using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;

using static WS_MM.Models.TipoMovimientoModel;
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
    public class TipoMovimientoController : BaseController
    {
        [HttpGet]
        [ActionName("PMMM_getTipoMovimiento_List")]
        public IHttpActionResult PMMM_getTipoMovimiento_List(Boolean? prmbitStatus)
        {
            try
            {
                DataTable dt = Instancia.getTipoMovimiento(JWT.IdEmpresa, prmbitStatus);
                string json = JsonConvert.SerializeObject(dt, Newtonsoft.Json.Formatting.None);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpGet]
        [ActionName("PMMM_getTipoMovimiento_cbo")]
        public IHttpActionResult PMMM_getTipoMovimiento_cbo(Boolean? prmbitStatus)
        {
            try
            {
                DataTable dt = Instancia.getTipoMovimiento_cbo(JWT.IdEmpresa, prmbitStatus);
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
