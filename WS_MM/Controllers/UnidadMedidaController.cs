using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;

using static WS_MM.Models.UnidadMedidaModel;
using System.Web;
using System.Security.Cryptography;
using Shared.Controllers;
using Shared.Helpers;
using System.Web.Http;
using Shared.Util;
using Shared.Token;
using Newtonsoft.Json;
using System;

namespace WS_MM.Controllers
{
    [CorsOrigin]
    [Authorize]
    public class UnidadMedidaController : BaseController
    {
        [HttpGet]
        [ActionName("PMMM_GetUnidadMedida")]
        public IHttpActionResult PMMM_GetUnidadMedida(Boolean? prmbitActivo)
        {
            try
            {
                DataTable dt = Instancia.GetUnidadMedida(JWT.IdEmpresa, prmbitActivo);
                string json = JsonConvert.SerializeObject(dt, Newtonsoft.Json.Formatting.None);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpPost]
        [ActionName("GetUnidadMedidaByFilters")]
        public IHttpActionResult GetUnidadMedidaByFilters(dynamic obj)
        {
            try
            {
                int prmintTipoUnidadMedida = obj.prmintTipoUnidad;
                int prmintActivo = obj.prmintActivo;
                int prmintEmpresa = JWT.IdEmpresa;

                DataTable dt = Instancia.GetUnidadMedidaByFilters(prmintTipoUnidadMedida, prmintActivo, prmintEmpresa);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpPost]
        [ActionName("ChangeStatus_UnidadMedida")]
        public IHttpActionResult ChangeStatus_TasaCambio(dynamic obj)
        {
            try
            {
                int prmintUnidadMedida = obj.prmintUnidadMedida;
                Boolean prmbitActivo = obj.prmbitActivo;
                string prmstrUsuario = JWT.Login;

                int dt = Instancia.ChangeStatus_UnidadMedida(prmintUnidadMedida, prmbitActivo, prmstrUsuario);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpGet]
        [ActionName("GetTipoUnidad")]
        public IHttpActionResult GetTipoUnidad()
        {
            var prmintEmpresa = JWT.IdEmpresa;

            try
            {
                DataTable dt = Instancia.GetTipoUnidad(prmintEmpresa);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleError(ex));
            }
        }

        [HttpPost]
        [ActionName("GetUnidadMedida")]
        public IHttpActionResult GetUnidadMedida(dynamic obj)
        {
            int prmintUnidadMedida = obj.prmintUnidadMedida;
            var prmintEmpresa = JWT.IdEmpresa;

            try
            {
                DataTable dt = Instancia.GetUMedida(prmintUnidadMedida,prmintEmpresa);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleError(ex));
            }
        }

        [HttpGet]
        [ActionName("GetUnidadMedidaById")]
        public IHttpActionResult GetUnidadMedidaById(int prmintUnidadMedida)
        {
            try
            {
                DataTableCollection dt = Instancia.GetUnidadMedidaById(prmintUnidadMedida);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleError(ex));
            }
        }

        [HttpPost]
        [ActionName("SaveUpdate_UnidadMedida")]
        public IHttpActionResult SaveUpdate_UnidadMedida(dynamic obj)
        {
            try
            {
                int prmintUnidadMedida = obj.UnidadMedida;
                string prmstrNombre = obj.Nombre;
                string prmstrNombreIngles = obj.NombreIngles;
                string prmstrAbreviatura = obj.Abreviatura;
                int prmintTipoUnidad = obj.TipoUnidadMedida;
                int prmintActivo = obj.Activo;
                int idEmpresa = JWT.IdEmpresa;
                string xml = obj.strXML;
                string ussername = JWT.Login;

                int dt = Instancia.SaveUpdate_UnidadMedida(prmintUnidadMedida, prmstrNombre, prmstrNombreIngles, prmstrAbreviatura, prmintTipoUnidad, prmintActivo, idEmpresa, xml, ussername);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }
    }
}
