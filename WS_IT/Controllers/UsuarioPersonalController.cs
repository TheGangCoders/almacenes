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
    public class UsuarioPersonalController : BaseController
    {
        [HttpPost]
        [ActionName("Guardar")]
        public IHttpActionResult Guardar([FromBody] dynamic request)
        {
            try
            {
                int Usuario = request.Usuario;
                string NroPersonal = request.NroPersonal;
                string Nombres = request.Nombres;
                string Login = request.Login;
                string Password = request.Password;
                string DocIdentidad = request.DocIdentidad;
                //bool AccesoDominioExterno = request.AccesoDominioExterno;
                int Sociedad = request.Sociedad;
                string ApellidoPaterno = request.ApellidoPaterno;
                string ApellidoMaterno = request.ApellidoMaterno;
                string Correo = request.Correo;
                string Telefono = request.Telefono;
                string Direccion = request.Direccion;
                string FechaNacimiento = request.FechaNacimiento;
                string FechaIngreso = request.FechaIngreso;
                string FechaCese = request.FechaCese;
                string Genero = request.Genero;

                if(Usuario > 0) {
                    DataTable dt = UsuarioPersonalModel.Instancia.Guardar(Usuario, NroPersonal, JWT.IdEmpresa, Nombres, Login,
                                                                    GlobalUtil.EncryptPassword(Password), DocIdentidad, Sociedad, ApellidoPaterno, 
                                                                    ApellidoMaterno, Correo, Telefono, Direccion, FechaNacimiento, 
                                                                    FechaIngreso, FechaCese, Genero, JWT.Login);
                    return Ok(dt);
                } else {
                    DGrupoSeguridadDTO dGrupoSeguridad = DGrupoSeguridadModel.Instancia.ObtenerPorIdEmpresa(JWT.IdEmpresa);
                    dGrupoSeguridad.LoadRegex();
                    ResponseSimpleDTO responseSimpleMatchRegex = dGrupoSeguridad.Match(Password);

                    if(responseSimpleMatchRegex.Ok)
                    {
                        DataTable dt = UsuarioPersonalModel.Instancia.Guardar(Usuario, NroPersonal, JWT.IdEmpresa, Nombres, Login,
                                                                        GlobalUtil.EncryptPassword(Password), DocIdentidad, Sociedad, ApellidoPaterno, 
                                                                        ApellidoMaterno, Correo, Telefono, Direccion, FechaNacimiento, 
                                                                        FechaIngreso, FechaCese, Genero, JWT.Login);
                        return Ok(dt);
                    } else
                    {
                        return Ok(new List<dynamic> {responseSimpleMatchRegex});
                    }
                }
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
                int Usuario = request.Usuario;
                string NroPersonal = request.NroPersonal;
                bool Anulado = request.Anulado;
                DataTable dt = UsuarioPersonalModel.Instancia.ActualizarAnulado(Usuario, JWT.IdEmpresa, Anulado, JWT.Login);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleError(ex));
            }
        }

        [HttpGet]
        [ActionName("Obtener")]
        public IHttpActionResult Obtener(int Usuario)
        {
            try
            {
                DataTable dt = UsuarioPersonalModel.Instancia.Obtener(Usuario, JWT.IdEmpresa);
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
                DataTable dt = UsuarioPersonalModel.Instancia.Listar(JWT.IdEmpresa);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleError(ex));
            }
        }

        [HttpGet]
        [ActionName("ListarPorSociedad")]
        public IHttpActionResult ListarPorSociedad(int Sociedad)
        {
            try
            {
                DataTable dt = UsuarioPersonalModel.Instancia.ListarPorSociedad(Sociedad, JWT.IdEmpresa);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleError(ex));
            }
        }
        [HttpGet]
        [ActionName("IT_ListarActivos")]
        public IHttpActionResult IT_ListarActivos()
        {
            try
            {
                DataTable dt = UsuarioPersonalModel.Instancia.ListarUsuariosActivos(JWT.IdEmpresa);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleError(ex));
            }
        }
    }
}
