using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;

using static WS_PP.Models.RecetaModel;
using System.Web;
using System.Security.Cryptography;
using Shared.Controllers;
using Shared.Helpers;
using System.Web.Http;
using Shared.Util;
using Shared.Token;
using Newtonsoft.Json;

namespace WS_PP.Controllers
{
    [CorsOrigin]
    [Authorize]
    public class RecetaController : BaseController
    {
        [HttpGet]
        [ActionName("PPPM_GetRecetas")]
        public IHttpActionResult PPPM_GetRecetas(int? prmintCentro, int? prmintNave, int? prmintActivo)
        {
            try
            {
                DataTable dt = Instancia.GetRecetas(JWT.IdEmpresa, prmintCentro, prmintNave, prmintActivo);
                string json = JsonConvert.SerializeObject(dt, Newtonsoft.Json.Formatting.None);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }


        [HttpGet]
        [ActionName("PPPM_GetRecetaById")]
        public HttpResponseMessage PPPM_GetRecetaById(int prmintReceta)
        {
            try
            {
                DataTable dt = Instancia.GetRecetaById(JWT.IdEmpresa, prmintReceta);

                var obj = (from r in dt.AsEnumerable()
                           select new
                           {
                               Receta = Convert.ToInt64(r["Receta"]),
                               Empresa = Convert.ToInt64(r["Empresa"]),
                               CodigoReceta = r["CodigoReceta"].ToString(),
                               NombreReceta = r["NombreReceta"].ToString(),
                               CantidadBaseReceta = Convert.ToDecimal(r["CantidadBaseReceta"]),
                               Nave = Convert.ToInt64(r["Nave"]),
                               CodigoNave = r["CodigoNave"].ToString(),
                               NombreNave = r["NombreNave"].ToString(),
                               DescripcionNave = r["DescripcionNave"].ToString(),
                               Centro = Convert.ToInt64(r["Centro"]),
                               CodigoCentro = r["CodigoCentro"].ToString(),
                               NombreCentro = r["NombreCentro"].ToString(),
                               UnidadMedida = Convert.ToInt64(r["UnidadMedida"]),
                               NombreUnidadMedida = r["NombreUnidadMedida"].ToString(),
                               AbreviaturaUM = r["AbreviaturaUM"].ToString(),
                               CantidadEmpleadosReceta = Convert.ToInt32(r["CantidadEmpleadosReceta"]),
                               ActivoReceta = Convert.ToInt32(r["ActivoReceta"]),
                               GenericoReceta = Convert.ToInt32(r["GenericoReceta"]),
                               DesActivoReceta = r["DesActivoReceta"].ToString(),

                               RecursoActividad = (
                                   from rm in dt.AsEnumerable()
                                   where Convert.ToInt64(r["Receta"]).Equals(Convert.ToInt64(rm["Receta"]))
                                   select new
                                   {
                                       RecetaRecursoActividad = Convert.ToInt64(rm["RecetaRecursoActividad"]),
                                       Recurso = Convert.ToInt64(rm["Recurso"]),
                                       NombreRecurso = rm["NombreRecurso"].ToString(),
                                       Actividad = Convert.ToInt64(rm["Actividad"]),
                                       RecursoActividad = Convert.ToInt64(rm["RecursoActividad"]),
                                       CodigoActividad = rm["CodigoActividad"].ToString(),
                                       NombreActividad = rm["NombreActividad"].ToString(),

                                       CANT_BASE = Convert.ToInt64(rm["CANT_BASE"]),
                                       UM_BASE = Convert.ToInt64(rm["UM_BASE"]),
                                       CANT_ACTIVIDAD = Convert.ToInt64(rm["CANT_ACTIVIDAD"]),
                                       UM_ACTIVIDAD = Convert.ToInt64(rm["UM_ACTIVIDAD"]),
                                       CANT_EMPLEADOS = Convert.ToInt64(rm["CANT_EMPLEADOS"]),

                                       UM_BASE_DESC = rm["UM_BASE_DESC"].ToString(),
                                       UM_ACTIVIDADDESC = rm["UM_ACTIVIDADDESC"].ToString(),
                                       
                                       ActivoRecetaRecursoActividad = Convert.ToInt16(rm["ActivoRecetaRecursoActividad"]),
                                       DesActivoRecetaRecursoActividad = rm["DesActivoRecetaRecursoActividad"].ToString(),

                                   }).GroupBy(rm => new { rm.RecetaRecursoActividad }).Select(rm => rm.First()
                               ),

                               RecetaMaterial = (from rm in dt.AsEnumerable()
                                              where Convert.ToInt64(r["Receta"]).Equals(Convert.ToInt64(rm["Receta"]))
                                              select new
                                              {
                                                  RecetaMaterial = Convert.ToInt64(rm["RecetaMaterial"]),
                                                  Receta = Convert.ToInt64(rm["Receta"]),
                                                  Material = Convert.ToInt64(rm["Material"]),
                                                  CodigoMaterial = rm["CodigoMaterial"].ToString(),
                                                  DescripcionMaterial = rm["DescripcionMaterial"].ToString(),
                                                  ActivoRecetaMaterial = Convert.ToInt32(rm["ActivoRecetaMaterial"]),
                                                  DesActivoRecetaMaterial = rm["DesActivoRecetaMaterial"].ToString(),
                                              }).GroupBy(rm => new { rm.RecetaMaterial }).Select(rm => rm.First()),
                                              
                           }).GroupBy(r => new { r.Receta }).Select(r => r.First());

                var httpResponseMessage = Request.CreateResponse<IEnumerable<Object>>(HttpStatusCode.OK, obj);
                httpResponseMessage.Headers.Add("Access-Control-Allow-Origin", "*");

                return httpResponseMessage;
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpPost]
        [ActionName("PPPM_SaveUpdateReceta")]
        public HttpResponseMessage PPPM_SaveUpdateReceta(dynamic obj)
        {
            try
            {
                string xml = "<?xml version='1.0' encoding='ISO-8859-1'?><root>";
                string xmlRecetaMaterial = "<?xml version='1.0' encoding='ISO-8859-1'?><root>";
                string xmlRecetaRecursoActividad = "<?xml version='1.0' encoding='ISO-8859-1'?><root>";

                xml += "<Receta ";
                xml += "Receta ='" + obj.Receta + "' ";
                xml += "Empresa ='" + JWT.IdEmpresa + "' ";
                xml += "Centro ='" + obj.Centro + "' ";
                xml += "Nave ='" + obj.Nave + "' ";
                xml += "Codigo ='" + obj.Codigo + "' ";
                xml += "Nombre ='" + obj.Nombre + "' ";
                xml += "CantidadBase ='" + obj.CantidadBase + "' ";
                xml += "UnidadMedida ='" + obj.UnidadMedida + "' ";
                xml += "CantidadEmpleados ='" + obj.CantidadEmpleados + "' ";
                xml += "Generico ='" + obj.Generico + "' ";
                xml += "Activo ='" + obj.Activo + "' />";

                foreach (dynamic rm in obj.RecetaMaterial)
                {
                    xmlRecetaMaterial += "<RecetaMaterial ";
                    xmlRecetaMaterial += "RecetaMaterial = '" + rm.RecetaMaterial + "' ";
                    xmlRecetaMaterial += "Empresa = '" + JWT.IdEmpresa + "' ";
                    xmlRecetaMaterial += "Receta = '" + rm.Receta + "' ";
                    xmlRecetaMaterial += "Material = '" + rm.Material + "' ";
                    xmlRecetaMaterial += "Activo = '" + rm.Activo + "' />";
                }

                foreach (dynamic ra in obj.RecetaRecursoActividad)
                {
                    xmlRecetaRecursoActividad += "<RecetaRecursoActividad ";
                    xmlRecetaRecursoActividad += "RecetaRecursoActividad = '" + ra.RecetaRecursoActividad + "' ";
                    xmlRecetaRecursoActividad += "Empresa = '" + JWT.IdEmpresa + "' ";
                    xmlRecetaRecursoActividad += "Receta = '" + ra.Receta + "' ";
                    xmlRecetaRecursoActividad += "RecursoActividad = '" + ra.RecursoActividad + "' ";
                    xmlRecetaRecursoActividad += "CantidadBase = '" + ra.CantidadBase + "' ";
                    xmlRecetaRecursoActividad += "UnidadMedidaBase = '" + ra.UnidadMedidaBase + "' ";
                    xmlRecetaRecursoActividad += "CantidadActividad = '" + ra.CantidadActividad + "' ";
                    xmlRecetaRecursoActividad += "UnidadMedidaActividad = '" + ra.UnidadMedidaActividad + "' ";
                    xmlRecetaRecursoActividad += "CantidadEmpleados = '" + ra.CantidadEmpleados + "' ";
                    xmlRecetaRecursoActividad += "Activo = '" + ra.Activo + "' />";
                }

                xml += "</root>";
                xmlRecetaMaterial += "</root>";
                xmlRecetaRecursoActividad += "</root>";

                int res = Instancia.SaveUpdateReceta(JWT.Login, xml, xmlRecetaMaterial, xmlRecetaRecursoActividad);
                string mensaje = "";
                bool success = false;
                if (res > 0)
                {
                    success = true;
                    mensaje = "Se registró correctamente.";
                }

                if (res == -1)
                {
                    success = true;
                    mensaje = "El recurso ya se encuentra registrado.";
                }

                if (res == -2)
                {
                    success = true;
                    mensaje = "Ya existe una receta generica para el centro ingresado.";
                }

                if (res == -3)
                {
                    success = true;
                    mensaje = "Ya existe una receta generica para el centro ingresado.";
                }


                if (res == 0)
                {
                    success = false;
                    mensaje = "Ocurrió un error.";
                }

                var respuesta = (new
                {
                    Success = success,
                    Mensaje = mensaje,
                    Respuesta = res
                }
                );

                var httpResponseMessage = Request.CreateResponse(HttpStatusCode.OK, respuesta);

                //var httpResponseMessage = Request.CreateResponse<object>(HttpStatusCode.OK, Grupo);
                httpResponseMessage.Headers.Add("Access-Control-Allow-Origin", "*");

                return httpResponseMessage;
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpPost]
        [ActionName("PPPM_enableDisableReceta")]
        public IHttpActionResult PPPM_enableDisableReceta(dynamic obj)
        {
            try
            {
                int prmintReceta = obj.prmintReceta;
                Boolean prmbitActivo = obj.prmbitActivo;

                int dt = Instancia.EnableDisableReceta(JWT.Login, prmintReceta, prmbitActivo);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpGet]
        [ActionName("PPPM_GetValidacionUnidadMedidaEquivalencia")]
        public IHttpActionResult PPPM_GetValidacionUnidadMedidaEquivalencia(int prmintMaterial, int prmintUnidadMedida)
        {
            try
            {
                DataTable dt = Instancia.GetValidacionUnidadMedidaEquivalencia(JWT.IdEmpresa, prmintMaterial, prmintUnidadMedida);
                string json = JsonConvert.SerializeObject(dt, Newtonsoft.Json.Formatting.None);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }
    }
}