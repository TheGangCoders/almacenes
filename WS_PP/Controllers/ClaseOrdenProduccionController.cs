using System;
using System.Data;

using WS_PP.Models;
using Shared.Controllers;
using Shared.Helpers;
using System.Web.Http;
using Shared.Util;

namespace WS_PP.Controllers
{
    [CorsOrigin]
    [Authorize]
    public class ClaseOrdenProduccionController : BaseController
    {
        [HttpGet]
        [ActionName("PP_GetClaseOrdenProduccion")]
        public IHttpActionResult PP_GetClaseOrdenProduccion( int prmintActivo)
        {
            try
            {
                DataTable dt = ClaseOrdenProduccionModel.Instancia.GetClaseOrdenProduccion(JWT.IdEmpresa, prmintActivo);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleError(ex));
            }
        }

        [HttpPost]
        [ActionName("PP_SaveUpdate_ClaseOrdenProduccion")]
        public IHttpActionResult PP_SaveUpdate_ClaseOrdenProduccion(dynamic obj)
        {
            try
            {
                int prmintClaseOrdenProduccion = obj.ClaseOrdenProduccion;
                int prmintTipoMaterial = obj.TipoMaterial;
                string prmstrCodigo = obj.Codigo;
                string prmstrNombre = obj.Nombre;
                string prmstrDescripcion = obj.Descripcion;
                string prmstrRangoInicial = obj.RangoInicial;
                string prmstrRangoFinal = obj.RangoFinal;
                int prmintActivo = obj.Activo;

                int idEmpresa = JWT.IdEmpresa;
                string ussername = JWT.Login;

                int dt = ClaseOrdenProduccionModel.Instancia.SaveUpdate_ClaseOrdenProduccion(prmintClaseOrdenProduccion, prmintTipoMaterial, prmstrCodigo, prmstrNombre, prmstrDescripcion, prmstrRangoInicial, prmstrRangoFinal, prmintActivo, idEmpresa, ussername);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpGet]
        [ActionName("PP_Anular_ClaseOrdenProduccion")]
        public IHttpActionResult PP_Anular_ClaseOrdenProduccion(int prmintClaseOrdenProduccion)
        {
            try
            {
                string prmstrUsuario = JWT.Login;

                int dt = ClaseOrdenProduccionModel.Instancia.Anular_ClaseOrdenProduccion(prmintClaseOrdenProduccion, prmstrUsuario);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }
    }
}