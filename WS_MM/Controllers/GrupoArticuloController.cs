using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;

using static WS_MM.Models.GrupoArticuloModel;
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
    public class GrupoArticuloController : BaseController
    {
        [HttpGet]
        [ActionName("PMMM_GetGrupoArticulo")]
        public IHttpActionResult PMMM_GetGrupoArticulo(Boolean? prmbitStatus)
        {
            try
            {
                DataTable dt = Instancia.GetGrupoArticulo(JWT.IdEmpresa, prmbitStatus);
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
                int GrupoArticulo = request.GrupoArticulo;
                string Codigo = request.Codigo;
                string Nombre = request.Nombre;
                string Descripcion = request.Descripcion;
                string Descripcion_ENG = request.Descripcion_ENG;
                string Abreviatura = request.Abreviatura;
                bool Activo = request.Activo;

                DataTable dt = GrupoArticuloModel.Instancia.Guardar(GrupoArticulo, JWT.IdEmpresa, Codigo, Nombre, Descripcion, Descripcion_ENG, Abreviatura, Activo, JWT.Login);
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
                int GrupoArticulo = request.GrupoArticulo;
                bool Activo = request.Activo;
                DataTable dt = GrupoArticuloModel.Instancia.ActualizarActivo(GrupoArticulo, JWT.IdEmpresa, Activo, JWT.Login);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleError(ex));
            }
        }

        [HttpGet]
        [ActionName("PMMM_Obtener")]
        public IHttpActionResult PMMM_Obtener(int GrupoArticulo)
        {
            try
            {
                DataTable dt = GrupoArticuloModel.Instancia.Obtener(GrupoArticulo, JWT.IdEmpresa);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleError(ex));
            }
        }

        [HttpGet]
        [ActionName("PMMM_Listar")]
        public IHttpActionResult PMMM_Listar(int Activo)
        {
            try
            {
                DataTable dt = GrupoArticuloModel.Instancia.Listar(JWT.IdEmpresa, Activo);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleError(ex));
            }
        }
    }
}
