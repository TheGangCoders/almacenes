using System;
using System.Data;
using static WS_MM.Models.MultitablaModelDAL;
using Shared.Controllers;
using Shared.Helpers;
using System.Web.Http;
using Shared.Util;
using WS_MM.Models;


namespace WS_MM.Controllers
{
    [CorsOrigin]
    [Authorize]
    public class MultitablaController : BaseController
    {

        [HttpGet]
        [ActionName("PMMM_GetClaseAprovisionamiento_list")]
        public IHttpActionResult PMMM_GetClaseAprovisionamiento_list()
        {
            try
            {
                DataTable dt = Instancia.ListarClaseAprovisionamientoPorEmpresa(JWT.IdEmpresa);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleError(ex));
            }
        }

        [HttpGet]
        [ActionName("PMMM_GetTipoExistencia_list")]
        public IHttpActionResult PMMM_GetTipoExistencia_list()
        {
            try
            {
                DataTable dt = Instancia.ListarTipoExistenciaPorEmpresa(JWT.IdEmpresa);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleError(ex));
            }
        }

        [HttpGet]
        [ActionName("PMMM_GetIdiomas_list")]
        public IHttpActionResult PMMM_GetIdiomas_list()
        {
            try
            {
                DataTable dt = Instancia.getIdiomas_list(JWT.IdEmpresa);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpGet]
        [ActionName("PMMM_GetPlanificador_list")]
        public IHttpActionResult PMMM_GetPlanificador_list()
        {
            try
            {
                DataTable dt = Instancia.getPlanificador_list(JWT.IdEmpresa);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpGet]
        [ActionName("PMMM_ListarTipoDocumentoIdentidad")]
        public IHttpActionResult PMMM_ListarTipoDocumentoIdentidad()
        {
            try
            {
                DataTable dt = MultitablaModelDAL.Instancia.ListarTipoDocumentoIdentidad(JWT.IdEmpresa);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleError(ex));
            }
        }

        [HttpGet]
        [ActionName("PMMM_ListarGrupoCuenta")]
        public IHttpActionResult PMMM_ListarGrupoCuenta()
        {
            try
            {
                DataTable dt = MultitablaModelDAL.Instancia.ListarGrupoCuenta(JWT.IdEmpresa);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleError(ex));
            }
        }

        [HttpGet]
        [ActionName("PMMM_ListarIdioma")]
        public IHttpActionResult PMMM_ListarIdioma()
        {
            try
            {
                DataTable dt = MultitablaModelDAL.Instancia.ListarIdioma(JWT.IdEmpresa);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleError(ex));
            }
        }

        [HttpGet]
        [ActionName("PMMM_ListarTipoDocumento")]
        public IHttpActionResult PMMM_ListarTipoDocumento()
        {
            try
            {
                DataTable dt = MultitablaModelDAL.Instancia.ListarTipoDocumento(JWT.IdEmpresa);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleError(ex));
            }
        }

        [HttpGet]
        [ActionName("PMMM_ListarFormaPago")]
        public IHttpActionResult PMMM_ListarFormaPago()
        {
            try
            {
                DataTable dt = MultitablaModelDAL.Instancia.ListarFormaPago(JWT.IdEmpresa);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleError(ex));
            }
        }

        [HttpGet]
        [ActionName("PMMM_ListarTipoMoneda")]
        public IHttpActionResult PMMM_ListarTipoMoneda()
        {
            try
            {
                DataTable dt = MultitablaModelDAL.Instancia.ListarTipoMoneda(JWT.IdEmpresa);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleError(ex));
            }
        }

        [HttpGet]
        [ActionName("PMMM_ListarEstadoFacturaCompra")]
        public IHttpActionResult PMMM_ListarEstadoFacturaCompra()
        {
            try
            {
                DataTable dt = MultitablaModelDAL.Instancia.ListarEstadoFacturaCompra(JWT.IdEmpresa);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleError(ex));
            }
        }
    }
}
