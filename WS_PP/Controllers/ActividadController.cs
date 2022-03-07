using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;

using static WS_PP.Models.ActividadModel;
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
    public class ActividadController : BaseController
    {
        [HttpGet]
        [ActionName("PPPM_GetActividades")]
        public IHttpActionResult PPPM_GetActividades(int? prmintActividad, int? prmintActivo)
        {
            try
            {
                DataTable dt = Instancia.GetActividades(JWT.IdEmpresa, prmintActividad, prmintActivo);
                string json = JsonConvert.SerializeObject(dt, Newtonsoft.Json.Formatting.None);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }
                
        [HttpPost]
        [ActionName("PPPM_SaveUpdateActividad")]
        public HttpResponseMessage PPPM_SaveUpdateActividad(dynamic obj)
        {
            try
            {
                int res = Instancia.SaveUpdateActividad(JWT.Login, Convert.ToInt64(obj.Actividad), JWT.IdEmpresa, Convert.ToString(obj.Codigo),
                        Convert.ToString(obj.Nombre), Convert.ToString(obj.Descripcion), Convert.ToInt64(obj.UnidadMedida),
                        Convert.ToInt32(obj.Activo));

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
                    mensaje = "La actividad ya se encuentra registrada.";
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
        [ActionName("PPPM_enableDisableActividad")]
        public IHttpActionResult PPPM_enableDisableActividad(dynamic obj)
        {
            try
            {
                int prmintActividad = obj.prmintActividad;
                Boolean prmbitActivo = obj.prmbitActivo;

                int dt = Instancia.EnableDisableActividad(JWT.Login, prmintActividad, prmbitActivo);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }
    }
}