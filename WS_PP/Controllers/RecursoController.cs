using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;

using static WS_PP.Models.RecursoModel;
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
    public class RecursoController : BaseController
    {        
        [HttpPost]
        [ActionName("PPPM_SaveUpdateRecurso")]
        public HttpResponseMessage PPPM_SaveUpdateRecurso(dynamic obj)
        {
            try
            {
                string xml = "<?xml version='1.0' encoding='ISO-8859-1'?><root>";
                string xmlRecursoNave = "<?xml version='1.0' encoding='ISO-8859-1'?><root>";
                string xmlRecursoActividad = "<?xml version='1.0' encoding='ISO-8859-1'?><root>";

                xml += "<Recurso ";
                xml += "Recurso ='" + obj.Recurso + "' ";
                xml += "Empresa ='" + JWT.IdEmpresa + "' ";
                xml += "Codigo ='" + obj.Codigo + "' ";
                xml += "Nombre ='" + obj.Nombre + "' ";
                xml += "Descripcion ='" + obj.Descripcion + "' ";
                xml += "Activo ='" + obj.Activo + "' />";

                foreach (dynamic rn in obj.RecursoNave)
                {
                    xmlRecursoNave += "<RecursoNave ";
                    xmlRecursoNave += "RecursoNave = '" + rn.RecursoNave + "' ";
                    xmlRecursoNave += "Empresa = '" + JWT.IdEmpresa + "' ";
                    xmlRecursoNave += "Recurso = '" + rn.Recurso + "' ";
                    xmlRecursoNave += "Nave = '" + rn.Nave + "' ";
                    xmlRecursoNave += "Activo = '" + rn.Activo + "' />";
                }

                foreach (dynamic ra in obj.RecursoActividad)
                {
                    xmlRecursoActividad += "<RecursoActividad ";
                    xmlRecursoActividad += "RecursoActividad = '" + ra.RecursoActividad + "' ";
                    xmlRecursoActividad += "Empresa = '" + JWT.IdEmpresa + "' ";
                    xmlRecursoActividad += "Recurso = '" + ra.Recurso + "' ";
                    xmlRecursoActividad += "Actividad = '" + ra.Actividad + "' ";
                    xmlRecursoActividad += "Activo = '" + ra.Activo + "' />";
                }

                xml += "</root>";
                xmlRecursoNave += "</root>";
                xmlRecursoActividad += "</root>";

                int res = Instancia.SaveUpdateRecurso(JWT.Login, xml, xmlRecursoNave, xmlRecursoActividad);
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

        [HttpGet]
        [ActionName("PPPM_GetRecursos")]
        public IHttpActionResult PPPM_GetRecursos(int? prmintCentro, int? prmintNave, int? prmintActivo)
        {
            try
            {
                DataTable dt = Instancia.GetRecursos(JWT.IdEmpresa, prmintCentro, prmintNave, prmintActivo);
                string json = JsonConvert.SerializeObject(dt, Newtonsoft.Json.Formatting.None);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }


        [HttpGet]
        [ActionName("PPPM_GetRecursoById")]
        public HttpResponseMessage PPPM_GetRecursoById(int prmintRecurso)
        {
            try
            {
                DataTable dt = Instancia.GetRecursoById(JWT.IdEmpresa, prmintRecurso);

                var obj = (from r in dt.AsEnumerable()
                           select new
                           {
                               Recurso = Convert.ToInt64(r["Recurso"]),
                               Empresa = Convert.ToInt64(r["Empresa"]),
                               CodigoRecurso = r["CodigoRecurso"].ToString(),
                               NombreRecurso = r["NombreRecurso"].ToString(),
                               DescripcionRecurso = r["DescripcionRecurso"].ToString(),
                               ActivoRecurso = Convert.ToInt32(r["ActivoRecurso"]),
                               DesActivoRecurso = r["DesActivoRecurso"].ToString(),

                               RecursoNave = (from rn in dt.AsEnumerable()
                                           where Convert.ToInt64(r["Recurso"]).Equals(Convert.ToInt64(rn["Recurso"]))
                                           select new
                                           {
                                               RecursoNave = Convert.ToInt64(rn["RecursoNave"]),
                                               Recurso = Convert.ToInt64(r["Recurso"]),
                                               Nave = Convert.ToInt64(rn["Nave"]),
                                               CodigoNave = rn["CodigoNave"].ToString(),
                                               NombreNave = rn["NombreNave"].ToString(),
                                               DescripcionNave = rn["DescripcionNave"].ToString(),
                                               Centro = Convert.ToInt64(rn["Centro"]),
                                               CodigoCentro = rn["CodigoCentro"].ToString(),
                                               NombreCentro = rn["NombreCentro"].ToString(),
                                               ActivoRecursoNave = Convert.ToInt32(rn["ActivoRecursoNave"]),
                                               DesActivoRecursoNave = rn["DesActivoRecursoNave"].ToString(),
                                           }).GroupBy(rn => new { rn.RecursoNave }).Select(rn => rn.First()),

                               RecursoActividad = (from ra in dt.AsEnumerable()
                                              where Convert.ToInt64(r["Recurso"]).Equals(Convert.ToInt64(ra["Recurso"]))
                                              select new
                                              {
                                                  RecursoActividad = Convert.ToInt64(ra["RecursoActividad"]),
                                                  Recurso = Convert.ToInt64(r["Recurso"]),
                                                  Actividad = Convert.ToInt64(ra["Actividad"]),
                                                  CodigoActividad = ra["CodigoActividad"].ToString(),
                                                  NombreActividad = ra["NombreActividad"].ToString(),
                                                  DescripcionActividad = ra["DescripcionActividad"].ToString(),
                                                  UnidadMedida = Convert.ToInt64(ra["UnidadMedida"]),
                                                  NombreUnidadMedida = ra["NombreUnidadMedida"].ToString(),
                                                  AbreviaturaUM = ra["AbreviaturaUM"].ToString(),
                                                  ActivoRecursoActividad = Convert.ToInt32(ra["ActivoRecursoActividad"]),
                                                  DesActivoRecursoActividad = ra["DesActivoRecursoActividad"].ToString(),
                                              }).GroupBy(ra => new { ra.RecursoActividad }).Select(ra => ra.First()),
                           }).GroupBy(r => new { r.Recurso }).Select(r => r.First());

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
        [ActionName("PPPM_enableDisableRecurso")]
        public IHttpActionResult PPPM_enableDisableRecurso(dynamic obj)
        {
            try
            {
                int prmintRecurso = obj.prmintRecurso;
                Boolean prmbitActivo = obj.prmbitActivo;

                int dt = Instancia.EnableDisableRecurso(JWT.Login, prmintRecurso, prmbitActivo);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }       

    }
}