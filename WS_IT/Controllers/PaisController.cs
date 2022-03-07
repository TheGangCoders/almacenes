using System;
using System.Data;

using WS_IT.Models;
using Shared.Controllers;
using Shared.Helpers;
using System.Web.Http;
using Shared.Util;

namespace WS_IT.Controllers
{
    [CorsOrigin]
    [Authorize]
    public class PaisController : BaseController
    {
        [HttpGet]
        [ActionName("Listar")]
        public IHttpActionResult Listar(int Activo)
        {
            try
            {
                DataTable dt = PaisModelDAL.Instancia.Listar(Activo);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleError(ex));
            }
        }

        [HttpGet]
        [ActionName("IT_getDepartamentos_list")]
        public IHttpActionResult IT_getDepartamentos_list(int Pais,int Activo)
        {
            try
            {
                DataTable dt = PaisModelDAL.Instancia.getDepartamentos_list(Pais,Activo);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleError(ex));
            }
        }

        [HttpGet]
        [ActionName("IT_getProvincia_list")]
        public IHttpActionResult IT_getProvincia_list(int Departamento,int Activo)
        {
            try
            {
                DataTable dt = PaisModelDAL.Instancia.getProvincia_list(Departamento,Activo);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleError(ex));
            }
        }

        [HttpGet]
        [ActionName("IT_getVereda_list")]
        public IHttpActionResult IT_getVereda_list(int Provincia, int Activo)
        {
            try
            {
                DataTable dt = PaisModelDAL.Instancia.getVereda_list(Provincia, Activo);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }




    }
}
