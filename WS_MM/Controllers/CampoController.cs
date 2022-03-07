using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;

using static WS_MM.Models.CampoModel;
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
    public class CampoController : BaseController
    {
        [HttpGet]
        [ActionName("PMMM_GetUnidadAgricola")]
        public IHttpActionResult PMMM_GetUnidadAgricola(int prmintSociedad, Boolean? prmbitActivo)
        {
            try
            {
                DataTable dt = Instancia.GetUnidadAgricola(prmintSociedad, prmbitActivo);
                string json = JsonConvert.SerializeObject(dt, Newtonsoft.Json.Formatting.None);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpGet]
        [ActionName("PMMM_GetModulos")]
        public IHttpActionResult PMMM_GetModulos(int prmintSociedad, int prmintUnidadAgricola, Boolean? prmbitActivo)
        {
            try
            {
                DataTable dt = Instancia.GetModulos(prmintSociedad, prmintUnidadAgricola, prmbitActivo);
                string json = JsonConvert.SerializeObject(dt, Newtonsoft.Json.Formatting.None);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpGet]
        [ActionName("PMMM_GetCampos")]
        public IHttpActionResult PMMM_GetCampos(int prmintSociedad, int prmintModulo, Boolean? prmbitActivo)
        {
            try
            {
                DataTable dt = Instancia.GetCampos(prmintSociedad, prmintModulo, prmbitActivo);
                string json = JsonConvert.SerializeObject(dt, Newtonsoft.Json.Formatting.None);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpGet]
        [ActionName("MM_listarLotes")]
        public IHttpActionResult MM_ListarLotes(int prmintCampo, Boolean? prmbitActivo)
        {
            try
            {
                DataTable dt = Instancia.ListarLotes(JWT.IdSociedad, prmintCampo, prmbitActivo);
                string json = JsonConvert.SerializeObject(dt, Newtonsoft.Json.Formatting.None);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpGet]
        [ActionName("MM_listarRuta")]
        public IHttpActionResult MM_ListarRuta(int prmintRuta, int prmintOrigen, int prmintDestino)
        {
            try
            {
                DataTable dt = Instancia.ListarRutas(prmintRuta, prmintOrigen, prmintDestino);

                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

    }
}
