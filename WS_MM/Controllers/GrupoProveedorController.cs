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
    public class GrupoProveedorController : BaseController
    {
        [HttpGet]
        [ActionName("PMMM_Listar")]
        public IHttpActionResult PMMM_Listar(int Activo)
        {
            try
            {
                DataTable dt = GrupoProveedorModelDAL.Instancia.Listar(JWT.IdEmpresa, Activo);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleError(ex));
            }
        }

        [HttpGet]
        [ActionName("MM_GetGrupoProveedor")]
        public IHttpActionResult MM_GetGrupoProveedor(int Activo)
        {
            try
            {
                DataTable dt = GrupoProveedorModelDAL.Instancia.GetGrupoProveedor(JWT.IdEmpresa, Activo);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleError(ex));
            }
        }

        [HttpPost]
        [ActionName("MM_SaveUpdate_GrupoProveedor")]
        public IHttpActionResult MM_SaveUpdate_GrupoProveedor(dynamic obj)
        {
            try
            {
                int prmintGrupoProveedor = obj.GrupoProveedor;
                string prmstrNombre = obj.Nombre;
                string prmstrDescripcion = obj.Descripcion;
                int prmintEmite = obj.Emite;
                int prmintActivo = obj.Activo;

                int idEmpresa = JWT.IdEmpresa;
                string ussername = JWT.Login;

                int dt = GrupoProveedorModelDAL.Instancia.SaveUpdate_GrupoProveedor(prmintGrupoProveedor, prmstrNombre, prmstrDescripcion, prmintEmite, prmintActivo, idEmpresa, ussername);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpGet]
        [ActionName("MM_Anular_GrupoProveedor")]
        public IHttpActionResult MM_Anular_GrupoProveedor(int prmintGrupoProveedor)
        {
            try
            {
                string prmstrUsuario = JWT.Login;

                int dt = GrupoProveedorModelDAL.Instancia.Anular_GrupoProveedor(prmintGrupoProveedor, prmstrUsuario);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }
    }
}
