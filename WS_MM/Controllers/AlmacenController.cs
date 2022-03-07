using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;

using static WS_MM.Models.AlmacenModel;
using WS_MM.Models;
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
    public class AlmacenController : BaseController
    {
        [HttpGet]
        [ActionName("PMMM_getAlmacen_List")]
        public IHttpActionResult PMMM_getAlmacen_List(int prmintMaterial, int prmintCentro)
        {
            try
            {
                DataTable dt = Instancia.getAlmacen_List(prmintMaterial, prmintCentro, JWT.IdEmpresa);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpGet]
        [ActionName("PMMM_GetAlmacenes")]
        public IHttpActionResult PMMM_GetAlmacenes(int prmintEmpresa, int prmintCentro, Boolean? prmbitActivo)
        {
            try
            {
                DataTable dt = Instancia.GetAlmacenes(prmintEmpresa, prmintCentro, prmbitActivo);
                string json = JsonConvert.SerializeObject(dt, Newtonsoft.Json.Formatting.None);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpPost]
        [ActionName("PMMM_Guardar")]
        public IHttpActionResult Guardar([FromBody] dynamic request)
        {
            try
            {
                int Almacen = request.Almacen;
                int Centro = request.Centro;
                string Codigo = request.Codigo;
                string Nombre = request.Nombre;
                string Descripcion  = request.Descripcion;
                string TipoAlmacen  = request.TipoAlmacen;
                bool Activo  = request.Activo;
                string Direccion  = request.Direccion;

                DataTable dt = AlmacenModel.Instancia.Guardar(Almacen, JWT.IdEmpresa, Centro, Codigo, Nombre, Descripcion, TipoAlmacen, Activo, JWT.Login, Direccion);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleError(ex));
            }
        }

        [HttpPost]
        [ActionName("PMMM_ActualizarActivo")]
        public IHttpActionResult PMMM_ActualizarActivo([FromBody] dynamic request)
        {
            try
            {
                int Almacen = request.Almacen;
                bool Activo = request.Activo;
                DataTable dt = AlmacenModel.Instancia.ActualizarActivo(Almacen, JWT.IdEmpresa, Activo, JWT.Login);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleError(ex));
            }
        }

        [HttpGet]
        [ActionName("PMMM_Obtener")]
        public IHttpActionResult PMMM_Obtener(int Almacen)
        {
            try
            {
                DataTable dt = AlmacenModel.Instancia.Obtener(Almacen, JWT.IdEmpresa);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleError(ex));
            }
        }

        [HttpGet]
        [ActionName("PMMM_Listar")]
        public IHttpActionResult PMMM_Listar(int Centro, int Activo)
        {
            try
            {
                DataTable dt = AlmacenModel.Instancia.Listar(JWT.IdEmpresa, Centro, Activo);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleError(ex));
            }
        }

        [HttpGet]
        [ActionName("PMMM_GetTipoAlmacen")]
        public IHttpActionResult PMMM_GetTipoAlmacen()
        {
            try
            {
                DataTable dt = AlmacenModel.Instancia.TipoAlmacen(JWT.IdEmpresa);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleError(ex));
            }
        }
    }
}
