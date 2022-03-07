using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;

using static WS_SD.Models.GrupoClienteModel;
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
    public class GrupoClienteController : BaseController
    {
        [HttpGet]
        [ActionName("SD_GetGrupoCliente")]
        public IHttpActionResult SD_GetGrupoCliente(int? prmintActivo)
        {
            try
            {
                DataTable dt = Instancia.GetGrupoCliente(JWT.IdEmpresa, prmintActivo);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpPost]
        [ActionName("SD_SaveUpdate_GrupoCliente")]
        public IHttpActionResult SD_SaveUpdate_GrupoCliente(dynamic obj)
        {
            try
            {
                int prmintGrupoCliente = obj.GrupoCliente;
                string prmstrNombre = obj.Nombre;
                string prmstrDescripcion = obj.Descripcion;
                int prmintActivo = obj.Activo;

                int idEmpresa = JWT.IdEmpresa;
                string ussername = JWT.Login;

                int dt = Instancia.SaveUpdate_GrupoCliente(prmintGrupoCliente, prmstrNombre, prmstrDescripcion, prmintActivo, idEmpresa, ussername);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpGet]
        [ActionName("SD_Anular_GrupoCliente")]
        public IHttpActionResult SD_Anular_GrupoCliente(int prmintGrupoCliente)
        {
            try
            {
                string prmstrUsuario = JWT.Login;

                int dt = Instancia.Anular_GrupoCliente(prmintGrupoCliente, prmstrUsuario);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }
    }
}