using System;
using System.Data;

using static WS_SD.Models.BancoModel;
using Shared.Controllers;
using Shared.Helpers;
using System.Web.Http;
using Shared.Util;

namespace WS_SD.Controllers
{
    [CorsOrigin]
    [Authorize]
    public class BancoController : BaseController
    {
        [HttpGet]
        [ActionName("SD_GetBanco")]
        public IHttpActionResult SD_GetBanco(int? prmintActivo)
        {
            try
            {
                DataTable dt = Instancia.GetBanco(JWT.IdEmpresa, prmintActivo);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpGet]
        [ActionName("SD_GetBancoById")]
        public IHttpActionResult SD_GetBancoById(int prmintBanco)
        {
            try
            {
                DataTableCollection dt = Instancia.GetBancoById(prmintBanco);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpPost]
        [ActionName("SD_SaveUpdate_Banco")]
        public IHttpActionResult SD_SaveUpdate_Banco(dynamic obj)
        {
            try
            {
                int prmintBanco = obj.Banco;
                int prmintSociedad = obj.Sociedad;
                string prmstrClaveBanco = obj.ClaveBanco;
                string prmstrNombreBanco = obj.NombreBanco;
                string prmstrSwiftCode = obj.SwiftCode;
                string prmstrDireccion = obj.Direccion;
                int prmintActivo = obj.Activo;
                int prmintDefecto= obj.Defecto;
                int idEmpresa = JWT.IdEmpresa;
                string xml = obj.strXML;
                string username = JWT.Login;

                int dt = Instancia.SaveUpdate_Banco(prmintBanco, prmintSociedad, prmstrClaveBanco, prmstrNombreBanco, prmstrSwiftCode, prmstrDireccion, prmintDefecto, prmintActivo, idEmpresa, xml, username);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpGet]
        [ActionName("SD_Anular_Banco")]
        public IHttpActionResult SD_Anular_Banco(int prmintBanco)
        {
            try
            {
                string prmstrUsuario = JWT.Login;

                int dt = Instancia.Anular_Banco(prmintBanco, prmstrUsuario);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }
    }
}