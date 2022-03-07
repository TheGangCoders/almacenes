using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;

using static WS_MM.Models.NaveModel;
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
    public class NaveController : BaseController
    {
        [HttpGet]
        [ActionName("PMMM_GetNaves")]
        public IHttpActionResult PMMM_GetNaves(int? prmintSociedad, int? prmintCentro, Boolean? prmbitActivo)
        {
            try
            {
                DataTable dt = Instancia.GetNaves(JWT.IdEmpresa, prmintSociedad, prmintCentro, prmbitActivo);
                string json = JsonConvert.SerializeObject(dt, Newtonsoft.Json.Formatting.None);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpPost]
        [ActionName("PMMM_Guardar")]
        public IHttpActionResult Guardar([FromBody] dynamic request)
        {
            try
            {
                int Nave = request.Nave;
                int Centro = request.Centro;
                string Codigo = request.Codigo;
                string Nombre = request.Nombre;
                string Descripcion = request.Descripcion;
                bool Activo = request.Activo;

                DataTable dt = NaveModel.Instancia.Guardar(Nave, JWT.IdEmpresa, Centro, Codigo, Nombre, Descripcion, Activo, JWT.Login);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleError(ex));
            }
        }

        [HttpPost]
        [ActionName("PMMM_ActualizarActivo")]
        public IHttpActionResult PMMM_ActualizarActivo([FromBody] dynamic request)
        {
            try
            {
                int Nave = request.Nave;
                bool Activo = request.Activo;
                DataTable dt = NaveModel.Instancia.ActualizarActivo(Nave, JWT.IdEmpresa, Activo, JWT.Login);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleError(ex));
            }
        }

        [HttpGet]
        [ActionName("PMMM_Obtener")]
        public IHttpActionResult PMMM_Obtener(int Nave)
        {
            try
            {
                DataTable dt = NaveModel.Instancia.Obtener(Nave, JWT.IdEmpresa);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleError(ex));
            }
        }

        [HttpGet]
        [ActionName("PMMM_Listar")]
        public IHttpActionResult PMMM_Listar(int Sociedad, int Activo)
        {
            try
            {
                DataTable dt = NaveModel.Instancia.Listar(JWT.IdEmpresa, Sociedad, Activo);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleError(ex));
            }
        }
    }
}
