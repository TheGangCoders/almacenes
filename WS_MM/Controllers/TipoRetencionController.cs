using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;

using static WS_MM.Models.TipoRetencionModel;
using System.Web;
using System.Security.Cryptography;
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
    public class TipoRetencionController : BaseController
    {
        [HttpGet]
        [ActionName("MM_GetTipoRetencion")]
        public IHttpActionResult MM_GetTipoRetencion(int? prmintActivo)
        {
            try
            {
                DataTable dt = Instancia.GetTipoRetencion(JWT.IdEmpresa, prmintActivo);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpPost]
        [ActionName("MM_SaveUpdate_TipoRetencion")]
        public IHttpActionResult MM_SaveUpdate_TipoRetencion(dynamic obj)
        {
            try
            {
                int prmintTipoRetencion = obj.TipoRetencion;
                string prmstrCodigo = obj.Codigo;
                string prmstrNombre = obj.Nombre;
                int prmintActivo = obj.Activo;

                int idEmpresa = JWT.IdEmpresa;
                string username = JWT.Login;

                int dt = Instancia.SaveUpdate_TipoRetencion(prmintTipoRetencion, prmstrCodigo, prmstrNombre, prmintActivo, idEmpresa, username);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpGet]
        [ActionName("MM_Anular_TipoRetencion")]
        public IHttpActionResult MM_Anular_TipoRetencion(int prmintTipoRetencion)
        {
            try
            {
                string prmstrUsuario = JWT.Login;

                int dt = Instancia.Anular_TipoRetencion(prmintTipoRetencion, prmstrUsuario);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }
    }
}