using System;
using System.Data;

using static WS_MM.Models.CentroModelDAL;
using WS_MM.Models;
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
    public class CentrosController : BaseController
    {
        [HttpGet]
        [ActionName("PMMM_getCentros_List")]
        public IHttpActionResult PMMM_getCentros_List(int prmintSociedad,Boolean? prmbitActivo)
        {
            try
            {
                DataTable dt = Instancia.getCentros_List(JWT.IdSociedad, JWT.IdEmpresa, prmbitActivo);
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
                int Centro = request.Centro;
                int Sociedad = request.Sociedad;
                string Codigo = request.Codigo;
                string Nombre = request.Nombre;
                string Descripcion  = request.Descripcion;
                string Direccion  = request.Direccion;
                bool Activo  = request.Activo;

                DataTable dt = CentroModelDAL.Instancia.Guardar(Centro, JWT.IdEmpresa, JWT.IdSociedad, Codigo, Nombre, Descripcion, Direccion, Activo, JWT.Login);
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
                int Centro = request.Centro;
                bool Activo = request.Activo;
                DataTable dt = CentroModelDAL.Instancia.ActualizarActivo(Centro, JWT.IdEmpresa, Activo, JWT.Login);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleError(ex));
            }
        }

        [HttpGet]
        [ActionName("PMMM_Obtener")]
        public IHttpActionResult PMMM_Obtener(int Centro)
        {
            try
            {
                DataTable dt = CentroModelDAL.Instancia.Obtener(Centro, JWT.IdEmpresa);
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
                DataTable dt = CentroModelDAL.Instancia.Listar(JWT.IdEmpresa, JWT.IdSociedad, Activo);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleError(ex));
            }
        }
    }
}
