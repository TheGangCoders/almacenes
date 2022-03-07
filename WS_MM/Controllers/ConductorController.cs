using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;

using static WS_MM.Models.ConductorModel;
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
    public class ConductorController : BaseController
    {
        [HttpGet]
        [ActionName("MM_GetConductor")]
        public IHttpActionResult MM_GetConductor(int? prmintActivo)
        {
            try
            {
                DataTable dt = Instancia.GetConductor(JWT.IdEmpresa, prmintActivo);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpPost]
        [ActionName("MM_SaveUpdate_Conductor")]
        public IHttpActionResult MM_SaveUpdate_Conductor(dynamic obj)
        {
            try
            {
                int prmintConductor = obj.Conductor;
                int prmintProveedor = obj.Proveedor;
                string prmstrNombres = obj.Nombres;
                string prmstrDocIdentidad = obj.DocIdentidad;
                string prmstrLicencia = obj.Licencia;
                int prmintActivo = obj.Activo;

                int idEmpresa = JWT.IdEmpresa;
                string username = JWT.Login;

                int dt = Instancia.SaveUpdate_Conductor(prmintConductor, prmintProveedor, prmstrNombres, prmstrDocIdentidad, prmstrLicencia, prmintActivo, idEmpresa, username);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpGet]
        [ActionName("MM_Anular_Conductor")]
        public IHttpActionResult MM_Anular_Conductor(int prmintConductor)
        {
            try
            {
                string prmstrUsuario = JWT.Login;

                int dt = Instancia.Anular_Conductor(prmintConductor, prmstrUsuario);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }
    }
}