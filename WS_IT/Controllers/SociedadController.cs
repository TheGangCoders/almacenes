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
    public class SociedadController : BaseController
    {
        [HttpPost]
        [ActionName("Guardar")]
        public IHttpActionResult Guardar([FromBody] dynamic request)
        {
            try
            {
                int Sociedad = request.Sociedad;
                string RazonSocial = request.RazonSocial;
                string NombreComercial  = request.NombreComercial;
                string Direccion  = request.Direccion;
                string Ruc  = request.Ruc;
                string Telefono  = request.Telefono;
                float Latitud  = request.Latitud;
                float Longitud  = request.Longitud;
                DataTable dt = SociedadModel.Instancia.Guardar(Sociedad, JWT.IdUsuario, JWT.IdEmpresa, RazonSocial, NombreComercial, 
                                                                Direccion, Ruc, Telefono, Latitud, Longitud, JWT.Login);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleError(ex));
            }
        }

        [HttpPost]
        [ActionName("ActualizarActivo")]
        public IHttpActionResult ActualizarActivo([FromBody] dynamic request)
        {
            try
            {
                int Sociedad = request.Sociedad;
                bool Activo = request.Activo;
                DataTable dt = SociedadModel.Instancia.ActualizarActivo(Sociedad, JWT.IdEmpresa, Activo, JWT.Login);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleError(ex));
            }
        }

        [HttpGet]
        [ActionName("Obtener")]
        public IHttpActionResult Obtener(int Sociedad)
        {
            try
            {
                DataTable dt = SociedadModel.Instancia.Obtener(Sociedad, JWT.IdEmpresa);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleError(ex));
            }
        }

        [HttpGet]
        [ActionName("Listar")]
        public IHttpActionResult Listar()
        {
            try
            {
                DataTable dt = SociedadModel.Instancia.Listar(JWT.IdEmpresa);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleError(ex));
            }
        }
    }
}
