using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;

using WS_MM.Models;
using static WS_MM.Models.TipoMaterialModel;
using System.Web;
using System.Security.Cryptography;
using Shared.Controllers;
using Shared.Helpers;
using System.Web.Http;
using Shared.Util;
using Shared.Token;
using Newtonsoft.Json;
using System.Xml;

namespace WS_MM.Controllers
{
    [CorsOrigin]
    [Authorize]
    public class TipoMaterialController : BaseController
    {
        [HttpGet]
        [ActionName("PMMM_GetTipoMaterial")]
        public IHttpActionResult PMMM_GetTipoMaterial(Boolean? prmbitStatus)
        {
            try
            {
                DataTable dt = Instancia.GetTipoMaterial(JWT.IdEmpresa, prmbitStatus);
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
                int TipoMaterial = request.TipoMaterial;
                string Codigo = request.Codigo;
                string Nombre = request.Nombre;
                string Descripcion = request.Descripcion;
                string RangoInicial = request.RangoInicial;
                string RangoFinal = request.RangoFinal;
                bool Activo = request.Activo;

                var Caracteristicas = JsonConvert.SerializeObject(request.Caracteristicas);
                XmlNode CaracteristicasXmlNode = JsonConvert.DeserializeXmlNode("{\"row\":" + Caracteristicas + "}", "Data");
                string CaracteristicasXml = CaracteristicasXmlNode.InnerXml;

                DataTable dt = TipoMaterialModel.Instancia.Guardar(TipoMaterial, JWT.IdEmpresa, Codigo, Nombre, Descripcion, RangoInicial, RangoFinal, Activo, JWT.Login, CaracteristicasXml);
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
                int TipoMaterial = request.TipoMaterial;
                bool Activo = request.Activo;
                DataTable dt = TipoMaterialModel.Instancia.ActualizarActivo(TipoMaterial, JWT.IdEmpresa, Activo, JWT.Login);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleError(ex));
            }
        }

        [HttpGet]
        [ActionName("PMMM_Obtener")]
        public IHttpActionResult PMMM_Obtener(int TipoMaterial)
        {
            try
            {
                DataTable dt = TipoMaterialModel.Instancia.Obtener(TipoMaterial, JWT.IdEmpresa);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleError(ex));
            }
        }

        [HttpGet]
        [ActionName("PMMM_Listar")]
        public IHttpActionResult PMMM_Listar(int Activo)
        {
            try
            {
                DataTable dt = TipoMaterialModel.Instancia.Listar(JWT.IdEmpresa, Activo);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleError(ex));
            }
        }
    }
}
