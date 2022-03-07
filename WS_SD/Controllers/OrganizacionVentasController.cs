using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;

using static WS_SD.Models.OrganizacionVentasModel;
using System.Web;
using System.Security.Cryptography;
using Shared.Controllers;
using Shared.Helpers;
using System.Web.Http;
using Shared.Util;
using Shared.Token;
using Newtonsoft.Json;

namespace WS_SD.Controllers
{
    [CorsOrigin]
    [Authorize]
    public class OrganizacionVentasController : BaseController
    {
        [HttpGet]
        [ActionName("SD_GetOrgVentas")]
        public IHttpActionResult SD_GetOrgVentas(int? prmintSociedad, int? prmintActivo)
        {
            try
            {
                DataTable dt = Instancia.GetOrgVentas(JWT.IdEmpresa, prmintSociedad, prmintActivo);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpPost]
        [ActionName("SD_SaveUpdate_OrgVentas")]
        public IHttpActionResult SD_SaveUpdate_OrgVentas(dynamic obj)
        {
            try
            {
                int prmintOrgVentas = obj.OrgVentas;
                int prmintSociedad = obj.Sociedad;
                string prmstrDescripcion = obj.Descripcion;
                string prmstrCodigo = obj.Codigo;                
                string prmstrMoneda = obj.Moneda;
                int prmintActivo = obj.Activo;

                int idEmpresa = JWT.IdEmpresa;
                string ussername = JWT.Login;

                int dt = Instancia.SaveUpdate_OrgVentas(prmintOrgVentas, prmintSociedad, prmstrDescripcion, prmstrCodigo, prmstrMoneda, prmintActivo, idEmpresa, ussername);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpGet]
        [ActionName("SD_Anular_OrgVentas")]
        public IHttpActionResult Anular_OrgVentas(int prmintOrgVentas)
        {
            try
            {
                string prmstrUsuario = JWT.Login;

                int dt = Instancia.Anular_OrgVentas(prmintOrgVentas, prmstrUsuario);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }
    }
}