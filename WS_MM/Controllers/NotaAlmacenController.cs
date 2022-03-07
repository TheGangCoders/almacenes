using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;

using static WS_MM.Models.NotaAlmacenModel;
using WS_MM.Models;
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
    public class NotaAlmacenController : BaseController
    {
        [HttpGet]
        [ActionName("PMMM_GetTipoNota")]
        public IHttpActionResult PMMM_GetTipoNota()
        {
            try
            {
                DataTable dt = Instancia.GetTipoNota(JWT.IdEmpresa);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpGet]
        [ActionName("PMMM_GetNotasAlmacen")]
        public IHttpActionResult PMMM_GetNotasAlmacen(int prmintCentro, int prmintAlmacen, string prmstrTipoNota, string prmstrFechaInicio, string prmstrFechaFin)
        {
            try
            {
                DataTable dt = Instancia.GetNotasAlmacen(prmintCentro, prmintAlmacen, prmstrTipoNota, prmstrFechaInicio, prmstrFechaFin);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpGet]
        [ActionName("PMMM_GetNotasAlmacenByIdNotaAlmacen")]
        public IHttpActionResult PMMM_GetNotasAlmacenByIdNotaAlmacen(int prmintNotaAlmacen)
        {
            try
            {
                DataTableCollection dt = Instancia.GetNotasAlmacenByIdNotaAlmacen(prmintNotaAlmacen);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }
    }
}