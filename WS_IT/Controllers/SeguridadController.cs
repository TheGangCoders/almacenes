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
    public class SeguridadController : BaseController
    {
        [AllowAnonymous]
        [HttpPost]
        [ActionName("ZITG_Login")]
        public IHttpActionResult ZITG_Login([FromBody] dynamic request)
        {
            try
            {
                //string dominio = request.dominio;
                string login = request.login;
                string password = request.password;

                //if (!(dominio != null && dominio != ""))
                //{
                //    throw new Exception("Dominio requerido");
                //}
                if (!(login != null && login != ""))
                {
                    throw new Exception("Login requerido");
                }
                if (!(password != null && password != ""))
                {
                    throw new Exception("Password requerido");
                }

                ValueJWT valueJWT = SeguridadModel.Instancia.AutenticarUsuario(/*dominio, */login, GlobalUtil.EncryptPassword(password));
                if (valueJWT != null && valueJWT.Autenticado)
                {
                    return Ok(new
                    {
                        Ok = true,
                        JWT = ManageJWT.GenerateToken(valueJWT)
                    });
                }
                else
                {
                    return Ok(new
                    {
                        Ok = false,
                        Message = "Usuario y/o clave incorrectos"
                    });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleError(ex));
            }
        }

        [HttpGet]
        [ActionName("RefreshToken")]
        public IHttpActionResult RefreshToken()
        {
            try
            {
                return Ok(new
                {
                    Ok = true,
                    JWT = ManageJWT.GenerateToken(JWT)
                });
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleError(ex));
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [ActionName("ForgotPassword")]
        public IHttpActionResult ForgotPassword([FromBody] dynamic request)
        {
            try
            {
                string dominio = request.dominio;
                string email = request.email;
                string login = request.login;

                if (!(dominio != null && dominio != ""))
                {
                    throw new Exception("Dominio requerido");
                }
                if (!(email != null && email != ""))
                {
                    throw new Exception("Email requerido");
                }
                if (!(login != null && login != ""))
                {
                    throw new Exception("Login requerido");
                }

                UsuarioSimpleDTO usuarioSimple = SeguridadModel.Instancia.ValidarUsuarioPorDominioEmailLogin(dominio, email, login);
                if (usuarioSimple != null)
                {
                    DGrupoSeguridadDTO dGrupoSeguridad = DGrupoSeguridadModel.Instancia.ObtenerPorIdUsuario(usuarioSimple.IdUsuario);
                    TemplateNewPassword data = new TemplateNewPassword
                    {
                        newPassword = dGrupoSeguridad.GenerateRandomPassword()
                    };
                    ResponseSimpleDTO responseSimple = SeguridadModel.Instancia.ActualizarPassword(usuarioSimple.IdEmpresa, usuarioSimple.IdUsuario, null, GlobalUtil.EncryptPassword(data.newPassword), TipoRegistroLogClave.FORGOT_PASSWORD, login);
                    if (responseSimple != null && responseSimple.Ok)
                    {
                        EmailHelper.SendTemplateNewPassword(EmailSMTPServer.OUTLOOK, new string[] { usuarioSimple.Email }, new string[] { }, data);
                        return Ok(new
                        {
                            Ok = true,
                            Message = "Correo enviado"
                        });
                    } else
                    {
                        return Ok(new
                        {
                            Ok = false,
                            Message = "Error interno"
                        });
                    }
                }
                else
                {
                    return Ok(new
                    {
                        Ok = false,
                        Message = "Datos incorrectos"
                    });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleError(ex));
            }
        }

        [HttpPost]
        [ActionName("ResetPassword")]
        public IHttpActionResult ResetPassword([FromBody] dynamic request)
        {
            try
            {
                int Usuario = request.Usuario;
                string Password = request.Password;

                ResponseSimpleDTO responseSimple = SeguridadModel.Instancia.ValidarPassword(JWT.IdUsuario, JWT.IdEmpresa, Usuario, GlobalUtil.EncryptPassword(Password));
                if (responseSimple != null && responseSimple.Ok)
                {
                    DataTable dt = UsuarioPersonalModel.Instancia.Obtener(Usuario, JWT.IdEmpresa);
                    DGrupoSeguridadDTO dGrupoSeguridad = DGrupoSeguridadModel.Instancia.ObtenerPorIdUsuario(Usuario);
                    TemplateNewPassword data = new TemplateNewPassword
                    {
                        newPassword = dGrupoSeguridad.GenerateRandomPassword()
                    };
                    ResponseSimpleDTO responseSimple2 = SeguridadModel.Instancia.ActualizarPassword(JWT.IdEmpresa, Usuario, null, GlobalUtil.EncryptPassword(data.newPassword), TipoRegistroLogClave.RESET_PASSWORD, JWT.Login);
                    if (responseSimple2 != null && responseSimple2.Ok)
                    {
                        EmailHelper.SendTemplateNewPassword(EmailSMTPServer.OUTLOOK, new string[] { dt.Rows[0]["Correo"].ToString() }, new string[] { }, data);
                        return Ok(new
                        {
                            Ok = true,
                            Message = "Correo enviado"
                        });
                    } else
                    {
                        return Ok(new
                        {
                            Ok = false,
                            Message = "Error interno"
                        });
                    }
                }
                else
                {
                    return Ok(new
                    {
                        Ok = false,
                        Message = responseSimple.Message
                    });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleError(ex));
            }
        }

        [HttpPost]
        [ActionName("ChangePassword")]
        public IHttpActionResult ChangePassword([FromBody] dynamic request)
        {
            try
            {
                string passwordActual = request.passwordActual;
                string passwordNueva = request.passwordNueva;

                if (!(passwordActual != null && passwordActual != ""))
                {
                    throw new Exception("Password actual requerida");
                }
                if (!(passwordNueva != null && passwordNueva != ""))
                {
                    throw new Exception("Password nueva requerida");
                }

                DGrupoSeguridadDTO dGrupoSeguridad = DGrupoSeguridadModel.Instancia.ObtenerPorIdUsuario(JWT.IdUsuario);
                dGrupoSeguridad.LoadRegex();
                ResponseSimpleDTO responseSimpleMatchRegex = dGrupoSeguridad.Match(passwordNueva);

                if(responseSimpleMatchRegex.Ok)
                {
                    ResponseSimpleDTO responseSimpleValidation = SeguridadModel.Instancia.ValidarPasswordNueva(JWT.IdUsuario, passwordNueva, GlobalUtil.EncryptPassword(passwordNueva));
                    if (responseSimpleValidation.Ok)
                    {
                        ResponseSimpleDTO responseSimple = SeguridadModel.Instancia.ActualizarPassword(JWT.IdEmpresa, JWT.IdUsuario, GlobalUtil.EncryptPassword(passwordActual), GlobalUtil.EncryptPassword(passwordNueva), TipoRegistroLogClave.CHANGE_PASSWORD, JWT.Login);
                        if (responseSimple != null)
                        {
                            return Ok(responseSimple);
                        }
                        else
                        {
                            return Ok(new
                            {
                                Ok = false,
                                Message = "Error interno"
                            });
                        }
                    } else
                    {
                        return Ok(responseSimpleValidation);
                    }
                } else
                {
                    return Ok(responseSimpleMatchRegex);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleError(ex));
            }
        }

        [HttpGet]
        [ActionName("GetPermissions")]
        public IHttpActionResult GetPermissions()
        {
            try
            {
                List<PermisoDTO> listPermisos = PermisoModel.Instancia.ObtenerPorIdUsuario(JWT.IdUsuario);
                List<OpcionVistaAccionDTO> listOpcionesVistasAcciones = GenericModel.Instancia.ObtenerOpcionesVistasAcciones();

                List<OpcionVistaAccionDTO> listOpcionesVistasAccionesFiltrado = GenericModel.Instancia.GenerarPermisos(true, listPermisos, listOpcionesVistasAcciones, null);
                NavegacionDTO navegacion = GenericModel.Instancia.GenerarNavegacion(null, 1, null, listOpcionesVistasAccionesFiltrado);
                
                return Ok(new {
                    Ok = true,
                    Data = navegacion
                });
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleError(ex));
            }
        }


        [HttpGet]
        [ActionName("ZITG_ObtenerOpcionesUsuario")]
        public IHttpActionResult ZITG_ObtenerOpcionesUsuario()
        {
            DataTable dt = SeguridadModel.Instancia.ObtenerPermisos(JWT.IdUsuario);
            try
            {
                var accesosdirectos = (from c in dt.AsEnumerable()
                                       select new
                                       {
                                           //nivel3: GrupoOpcionId = dd["GRUPOOPCIONID"].ToString(),
                                           title = c["GRUPOOPCION"].ToString(),
                                           bullet = "line",
                                           icon = c["ICONOGRUPOOPCION"].ToString(),
                                           submenu = (from ddd in dt.AsEnumerable()
                                                      where c["APLICACIONID"].ToString() == ddd["APLICACIONID"].ToString() &&
                                                              c["GRUPOOPCIONID"].ToString() == ddd["GRUPOOPCIONID"].ToString()
                                                      select new
                                                      {
                                                          //nivel4: OpcionId = ddd["OPCIONID"].ToString(),
                                                          title = ddd["OPCION"].ToString(),
                                                          //icon = ddd["ICONOOPCION"].ToString(),
                                                          page = ddd["NOMBREPROYECTO"].ToString(),
                                                          //viewType = ddd["NOMBREOBJETO"].ToString(),

                                                      })
                                       }).GroupBy(o => new { o.title }).Select(o => o.First());

                return Ok(accesosdirectos);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleError(ex));
            }
        }

        [AllowAnonymous]
        [HttpGet]
        [ActionName("getLogeoUsuario")]
        public IHttpActionResult getLogeoUsuario(string prmstrUsuario, string prmstrcontrasena)
        {
            try
            {

                prmstrcontrasena = CRYPT.Encrypt<TripleDESCryptoServiceProvider>(prmstrcontrasena);
                prmstrcontrasena = prmstrcontrasena.ToString().Replace('+', ' ');

                DataTable dt = SeguridadModel.Instancia.getLogeoUsuario(prmstrUsuario, prmstrcontrasena);
                if (dt.Rows.Count == 0)
                    return Ok(new Object());

                DataRow dr = dt.Rows[0];
                var valueJWT = new ValueJWT
                {
                    IdUsuario = Convert.ToInt32(dr["idUsuario"]),
                    Nombres = Convert.ToString(dr["Nombres"]),
                    Login = Convert.ToString(dr["Usuario"]),
                    Email = Convert.ToString(dr["Correo"]),
                    IdSociedad = Convert.ToInt32(dr["sociedad"]),
                    IdEmpresa = Convert.ToInt32(dr["Empresa"])
                };

                var respuesta = from c in dt.AsEnumerable()
                                select new
                                {
                                    idUsuario = c["idUsuario"],
                                    idPersonal = c["idPersonal"],
                                    usuario = c["Usuario"],
                                    nombres = c["Nombres"],
                                    apellidos = c["Apellidos"],
                                    correo = c["Correo"],
                                    cargo = c["Cargo"],
                                    activo = c["Activo"],
                                    rolMovil = c["RolMovil"],
                                    sociedad = c["Sociedad"],
                                    empresa = c["Empresa"],
                                    autenticado = c["autenticado"],
                                    token = (Convert.ToInt32(c["autenticado"]) == 0) ? "" : ManageJWT.GenerateToken(valueJWT)
                                };

                return Ok(respuesta.FirstOrDefault());

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [ActionName("ZITG_verifyPassword")]
        public IHttpActionResult ZITG_verifyPassword(string password)
        {
            try
            {
                string prmPassword = CRYPT.Encrypt<TripleDESCryptoServiceProvider>(password);
                prmPassword = prmPassword.ToString().Replace('+', ' ');

                int dt = SeguridadModel.Instancia.verifyPassword(JWT.IdUsuario, prmPassword);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }


        [HttpPost]
        [ActionName("ZITG_updatePassword")]
        public IHttpActionResult ZITG_updatePassword(dynamic obj)
        {
            try
            {
                string prmstrPassword = obj.prmstrPassword;
                string prmstrUsuario = obj.prmstrUsuario;
                string prmPassword = CRYPT.Encrypt<TripleDESCryptoServiceProvider>(prmstrPassword);
                prmPassword = prmPassword.ToString().Replace('+', ' ');

                int dt = SeguridadModel.Instancia.updatePassword(JWT.IdUsuario, prmPassword, prmstrUsuario);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }
    }
}
