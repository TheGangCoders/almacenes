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
    public class PermisoController : BaseController
    {
        [HttpPost]
        [ActionName("Guardar")]
        public IHttpActionResult Guardar([FromBody] dynamic request)
        {
            try
            {
                int Permiso = request.Permiso;
                int Opcion = request.Opcion;
                int Vista = request.Vista;
                int Accion = request.Accion;
                int Rol = request.Rol;

                DataTable dt = PermisoModel.Instancia.Guardar(Permiso, JWT.IdEmpresa, Opcion, Vista, Accion, Rol, JWT.Login);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleError(ex));
            }
        }

        [HttpGet]
        [ActionName("ObtenerNavegacionPorRol")]
        public IHttpActionResult ObtenerNavegacionPorRol(int Rol)
        {
            try
            {
                List<OpcionVistaAccionDTO> listOpcionesVistasAcciones = PermisoModel.Instancia.ListarPorRol(Rol, JWT.IdEmpresa);
                NavegacionDTO navegacion = GenericModel.Instancia.GenerarNavegacion(null, 1, null, listOpcionesVistasAcciones);
                
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

    }
}
