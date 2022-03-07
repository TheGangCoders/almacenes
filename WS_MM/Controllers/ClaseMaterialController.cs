using System;
using System.Data;

using WS_MM.Models;
using Shared.Controllers;
using Shared.Helpers;
using System.Web.Http;
using Shared.Util;


namespace WS_MM.Controllers
{
    [CorsOrigin]
    [Authorize]
    public class ClaseMaterialController : BaseController
    {
        [HttpGet]
        [ActionName("MM_GetClaseMaterial")]
        public IHttpActionResult MM_GetClaseMaterial(int Activo)
        {
            try
            {
                DataTable dt = ClaseMaterialModel.Instancia.GetClaseMaterial(JWT.IdEmpresa, Activo);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleError(ex));
            }
        }

        [HttpPost]
        [ActionName("MM_SaveUpdate_ClaseMaterial")]
        public IHttpActionResult MM_SaveUpdate_ClaseMaterial(dynamic obj)
        {
            try
            {
                int prmintClaseMaterial = obj.ClaseMaterial;
                string prmstrCodigo = obj.Codigo;
                string prmstrNombre = obj.Nombre;
                string prmstrDescripcion = obj.Descripcion;
                int prmintActivo = obj.Activo;

                int idEmpresa = JWT.IdEmpresa;
                string ussername = JWT.Login;

                int dt = ClaseMaterialModel.Instancia.SaveUpdate_ClaseMaterial(prmintClaseMaterial, prmstrCodigo, prmstrNombre, prmstrDescripcion, prmintActivo, idEmpresa, ussername);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpGet]
        [ActionName("MM_Anular_ClaseMaterial")]
        public IHttpActionResult MM_Anular_ClaseMaterial(int prmintClaseMaterial)
        {
            try
            {
                string prmstrUsuario = JWT.Login;

                int dt = ClaseMaterialModel.Instancia.Anular_ClaseMaterial(prmintClaseMaterial, prmstrUsuario);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }
    }
}