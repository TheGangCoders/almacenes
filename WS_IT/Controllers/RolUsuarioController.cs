using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Security.Cryptography;
using Shared.Controllers;
using Shared.Helpers;
using System.Web.Http;
using Shared.Util;
using Shared.Token;
using Newtonsoft.Json;
using SharedEmail;
using SharedEmail.Helpers;
using SharedEmail.Email;
using SharedEmail.Entity;
using WS_IT.Models;
using WS_IT.DTOs;
using WS_IT.Constants;
using WS_IT.Util;

namespace WS_IT.Controllers
{
    [CorsOrigin]
    [Authorize]
    public class RolUsuarioController : BaseController
    {
        [HttpPost]
        [ActionName("Guardar")]
        public IHttpActionResult Guardar([FromBody] dynamic request)
        {
            try
            {
                int RolUsuario = request.RolUsuario;
                int Usuario = request.Usuario;
                int Rol = request.Rol;

                DataTable dt = RolUsuarioModel.Instancia.Guardar(RolUsuario, JWT.IdEmpresa, Usuario, Rol, JWT.Login);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleError(ex));
            }
        }

        [HttpGet]
        [ActionName("ListarPorUsuario")]
        public IHttpActionResult ListarPorUsuario(int Usuario)
        {
            try
            {
                DataTable dt = RolUsuarioModel.Instancia.ListarPorUsuario(Usuario, JWT.IdEmpresa);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleError(ex));
            }
        }
    }
}
