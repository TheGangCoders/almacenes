using System;
using System.Data;

using WS_SD.Models;
using Shared.Controllers;
using Shared.Helpers;
using System.Web.Http;
using Shared.Util;


namespace WS_SD.Controllers
{
    [CorsOrigin]
    [Authorize]
    public class ClaseDocVentaController : BaseController
    {
        [HttpGet]
        [ActionName("SD_GetClaseDocVenta")]
        public IHttpActionResult SD_GetClaseDocVenta(int ClaseCodVentaExc, int Activo)
        {
            try
            {
                DataTable dt = ClaseDocVentaModel.Instancia.GetClaseDocVenta(JWT.IdEmpresa, ClaseCodVentaExc, Activo);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleError(ex));
            }
        }

        [HttpPost]
        [ActionName("SD_SaveUpdate_ClaseDocVenta")]
        public IHttpActionResult SD_SaveUpdate_ClaseDocVenta(dynamic obj)
        {
            try
            {
                int prmintClaseDocVenta = obj.ClaseDocVenta;
                string prmstrTipoDoc = obj.TipoDoc;
                string prmstrCodigoVta = obj.CodigoVta;
                string prmstrDescripcion = obj.Descripcion;
                string prmstrRangoInicial = obj.RangoInicial;
                string prmstrRangoFinal = obj.RangoFinal;
                int prmintIGV = obj.IGV;
                int prmintIncoterm = obj.Incoterm;
                string prmstrClaseDocVtaSgte = obj.ClaseDocVtaSgte;
                int prmintActivo = obj.Activo;

                int idEmpresa = JWT.IdEmpresa;
                string ussername = JWT.Login;

                int dt = ClaseDocVentaModel.Instancia.SaveUpdate_ClaseDocVenta(prmintClaseDocVenta, prmstrTipoDoc, prmstrCodigoVta, prmstrDescripcion, prmstrRangoInicial, prmstrRangoFinal, prmintIGV, prmintIncoterm, prmstrClaseDocVtaSgte, prmintActivo, idEmpresa, ussername);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpGet]
        [ActionName("SD_Anular_ClaseDocVenta")]
        public IHttpActionResult SD_Anular_ClaseDocVenta(int prmintClaseDocVenta)
        {
            try
            {
                string prmstrUsuario = JWT.Login;

                int dt = ClaseDocVentaModel.Instancia.Anular_ClaseDocVenta(prmintClaseDocVenta, prmstrUsuario);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }
    }
}