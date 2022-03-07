using System;
using System.Data;

using static WS_PP.Models.VersionFabricacionModel;
using Shared.Controllers;
using Shared.Helpers;
using System.Web.Http;
using Shared.Util;
using Shared.Token;
using Newtonsoft.Json;
using System.Net.Http;
using System.Linq;
using System.Collections.Generic;
using System.Net;

namespace WS_PP.Controllers
{
    [CorsOrigin]
    [Authorize]
    public class VersionFabricacionController : BaseController
    {
        [HttpGet]
        [ActionName("PPPM_getVersionFabricacion")]
        public IHttpActionResult PPPM_getVersionFabricacion(int? prminCentro, int? prmintNave, int? prmintActivo, int? prmintVersion)
        {
            try
            {
                DataTable dt = Instancia.getVersionFabricacion(prminCentro, prmintNave, prmintActivo, JWT.IdEmpresa, prmintVersion);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpGet]
        [ActionName("PPPM_GetListaMaterialesByMaterial")]
        public HttpResponseMessage PPPM_GetListaMaterialesByMaterial(int prmintCentro, int prmintMaterial, int UnidadBase)
        {
            try
            {
                DataTable dt = Instancia.GetListaMaterialesByMaterial(prmintCentro, prmintMaterial, JWT.IdEmpresa, UnidadBase);

                var obj = (from c in dt.AsEnumerable()
                           select new
                           {
                               ListaMaterial = Convert.ToInt64(c["ListaMaterial"]),
                               Correlativo = c["Correlativo"].ToString(),
                               Nombre = c["Nombre"].ToString(),
                               DescListaMaterial = c["DescListaMaterial"].ToString(),
                               DescUnidadMedidaLM = c["DescUnidadMedidaLM"].ToString(),
                               UnidadMedidaLM = Convert.ToInt64(c["UnidadMedidaLM"]),

                               Detalles = (from d in dt.AsEnumerable()
                                           where Convert.ToInt64(c["ListaMaterial"]).Equals(Convert.ToInt64(d["ListaMaterial"]))
                                           select new
                                           {
                                               DListaMaterial = Convert.ToInt64(d["DListaMaterial"]),
                                               CantListaMaterial = Convert.ToDecimal(d["CantListaMaterial"]),
                                               UnidadMedida = d["UnidadMedida"].ToString(),
                                               DescUnidadMedida = d["DescUnidadMedida"].ToString(),
                                               Material = d["Material"].ToString(),
                                               CodMaterial = d["CodMaterial"].ToString(),
                                               DescMaterial = d["DescMaterial"].ToString()                                        

                                           }).GroupBy(d => new { d.DListaMaterial }).Select(d => d.First()),
                           }).GroupBy(c => new { c.ListaMaterial }).Select(c => c.First());

                var httpResponseMessage = Request.CreateResponse<IEnumerable<Object>>(HttpStatusCode.OK, obj);
                httpResponseMessage.Headers.Add("Access-Control-Allow-Origin", "*");

                return httpResponseMessage;
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        
        [HttpGet]
        [ActionName("PPPM_getRecetaDetalle")]
        public IHttpActionResult PPPM_getRecetaDetalle(int? prminReceta)
        {
            try
            {
                DataTable dt = Instancia.getRecetaDetalle(prminReceta);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpPost]
        [ActionName("PPPM_putEstadoVersionFabricacion")]
        public IHttpActionResult PPPM_putEstadoVersionFabricacion(dynamic obj)
        {
            try
            {
                int idVersion = obj.Version;
                Boolean Estado = obj.Estado;
                string usuario = obj.UsuarioRegistro;

                int dt = Instancia.putEstadoVersionFabricacion(idVersion, Estado, usuario);
                return Ok(dt);                
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpGet]
        [ActionName("PPPM_getRecetaByMaterial")]
        public IHttpActionResult PPPM_getRecetaByMaterial(int? prminCentro, int? prmintNave, Boolean? prmintActivo, int prmintMaterial, Boolean? prmintGenerico)
        {
            try
            {
                DataTable dt = Instancia.getRecetaByMaterial(prminCentro, prmintNave, prmintActivo, prmintMaterial, prmintGenerico);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }
        
        [HttpPost]
        [ActionName("PPPM_postRegistrarVersionFabricacion")]
        public IHttpActionResult PPPM_postRegistrarVersionFabricacion(dynamic obj)
        {
            try
            {
                int Version = obj.Version;
                string Codigo = obj.Codigo;
                string Nombre = obj.Nombre;
                Boolean Activo = obj.Activo;
                Boolean PorDefecto = obj.PorDefecto;
                Boolean Generico = obj.Generico;
                int Centro = obj.Centro;
                int Nave = obj.Nave;
                int TipoMaterial = obj.TipoMaterial;
                int MaterialBase = obj.MaterialBase;
                int UnidadMedida = obj.UnidadMedida;
                int ListaMaterial = obj.ListaMaterial;
                int Receta = obj.Receta;
                string InicioVigencia = obj.InicioVigencia;
                string FinVigencia = obj.FinVigencia;
                string UsuarioRegistro = obj.UsuarioRegistro;

                DataTable dt = Instancia.postRegistrarVersionFabricacion(Version, Codigo, Nombre, Activo, PorDefecto, Generico, Centro, Nave, TipoMaterial, MaterialBase, ListaMaterial, Receta, InicioVigencia, FinVigencia, UsuarioRegistro, JWT.IdEmpresa, UnidadMedida);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }






    }
}
