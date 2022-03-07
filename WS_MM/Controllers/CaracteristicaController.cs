using System;
using System.Data;

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
    public class CaracteristicaController : BaseController
    {

        [HttpPost]
        [ActionName("PMMM_Guardar")]
        public IHttpActionResult Guardar([FromBody] dynamic request)
        {
            try
            {
                int Caracteristica = request.Caracteristica;
                string Nombre = request.Nombre;
                string TipoDato = request.TipoDato;
                string Descripcion  = request.Descripcion;
                bool Activo  = request.Activo;
                string xml = request.strXML;

                DataTable dt = CaracteristicaModelDAL.Instancia.Guardar(Caracteristica, JWT.IdEmpresa, Nombre, TipoDato, Descripcion, Activo, xml, JWT.Login);
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
                int Caracteristica = request.Caracteristica;
                bool Activo = request.Activo;
                DataTable dt = CaracteristicaModelDAL.Instancia.ActualizarActivo(Caracteristica, JWT.IdEmpresa, Activo, JWT.Login);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleError(ex));
            }
        }

        [HttpGet]
        [ActionName("PMMM_Obtener")]
        public IHttpActionResult PMMM_Obtener(int Caracteristica)
        {
            try
            {
                DataTableCollection dt = CaracteristicaModelDAL.Instancia.Obtener(Caracteristica, JWT.IdEmpresa);
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
                DataTable dt = CaracteristicaModelDAL.Instancia.Listar(JWT.IdEmpresa, Activo);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleError(ex));
            }
        }

        [HttpGet]
        [ActionName("PMMM_GetTipoDato")]
        public IHttpActionResult PMMM_GetTipoDato()
        {
            try
            {
                DataTable dt = CaracteristicaModelDAL.Instancia.GetTipoDato(JWT.IdEmpresa);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleError(ex));
            }
        }
    }
}
