﻿using System;
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
    public class RolController : BaseController
    {
        [HttpPost]
        [ActionName("Guardar")]
        public IHttpActionResult Guardar([FromBody] dynamic request)
        {
            try
            {
                int Rol = request.Rol;
                string Nombre = request.Nombre;
                string Descripcion  = request.Descripcion;
                DataTable dt = RolModel.Instancia.Guardar(Rol, JWT.IdEmpresa, Nombre, Descripcion, JWT.Login);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleError(ex));
            }
        }

        [HttpPost]
        [ActionName("ActualizarAnulado")]
        public IHttpActionResult ActualizarAnulado([FromBody] dynamic request)
        {
            try
            {
                int Rol = request.Rol;
                bool Anulado = request.Anulado;
                DataTable dt = RolModel.Instancia.ActualizarAnulado(Rol, JWT.IdEmpresa, Anulado, JWT.Login);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleError(ex));
            }
        }

        [HttpGet]
        [ActionName("Obtener")]
        public IHttpActionResult Obtener(int Rol)
        {
            try
            {
                DataTable dt = RolModel.Instancia.Obtener(Rol, JWT.IdEmpresa);
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
                DataTable dt = RolModel.Instancia.Listar(JWT.IdEmpresa);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleError(ex));
            }
        }
    }
}
