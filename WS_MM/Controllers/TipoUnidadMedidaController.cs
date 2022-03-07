using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;

using static WS_MM.Models.TipoUnidadMedidaModel;
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
    public class TipoUnidadMedidaController : BaseController
    {
        [HttpGet]
        [ActionName("MM_GetTipoUnidadMedida")]
        public IHttpActionResult MM_GetTipoUnidadMedida(int? prmintActivo)
        {
            try
            {
                DataTable dt = Instancia.GetTipoUnidadMedida(JWT.IdEmpresa, prmintActivo);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpPost]
        [ActionName("MM_SaveUpdate_TipoUnidadMedida")]
        public IHttpActionResult MM_SaveUpdate_TipoUnidadMedida(dynamic obj)
        {
            try
            {
                int prmintTipoUnidadMedida = obj.TipoUnidadMedida;
                string prmstrNombre = obj.Nombre;
                string prmstrAbreviatura = obj.Abreviatura;
                int prmintActivo = obj.Activo;

                int idEmpresa = JWT.IdEmpresa;
                string username = JWT.Login;

                int dt = Instancia.SaveUpdate_TipoUnidadMedida(prmintTipoUnidadMedida, prmstrNombre, prmstrAbreviatura, prmintActivo, idEmpresa, username);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpGet]
        [ActionName("MM_Anular_TipoUnidadMedida")]
        public IHttpActionResult MM_Anular_TipoUnidadMedida(int prmintTipoUnidadMedida)
        {
            try
            {
                string prmstrUsuario = JWT.Login;

                int dt = Instancia.Anular_TipoUnidadMedida(prmintTipoUnidadMedida, prmstrUsuario);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }
    }
}