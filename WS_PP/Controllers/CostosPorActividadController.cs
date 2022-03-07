using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;

using static WS_PP.Models.CostosPorActividadModel;
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
    public class CostosPorActividadController : BaseController
    {
        [HttpGet]
        [ActionName("PP_GetActividades")]
        public IHttpActionResult PP_GetActividades()
        {
            try
            {
                DataTable dt = Instancia.GetActividades(JWT.IdEmpresa);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpGet]
        [ActionName("PP_GetCostoActividad")]
        public IHttpActionResult PP_GetCostoActividad(int prmintActivo)
        {
            try
            {
                DataTable dt = Instancia.GetCostoActividad(JWT.IdEmpresa, prmintActivo);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpPost]
        [ActionName("PP_SaveUpdate_CostoActividad")]
        public IHttpActionResult PP_SaveUpdate_CostoActividad(dynamic obj)
        {
            try
            {
                int prmintCostoActividad = obj.ActividadCosto;
                int prmintActividad = obj.Actividad;
                Decimal prmdecCosto = obj.Costo;
                DateTime prmdateFechaInicio = obj.FechaInicio;
                DateTime prmdateFechaFin = obj.FechaFin;
                int prmintActivo = obj.Activo;

                int prmintEmpresa = JWT.IdEmpresa;
                string ussername = JWT.Login;

                int dt = Instancia.SaveUpdate_CostoActividad(prmintCostoActividad, prmintActividad, prmdecCosto, prmdateFechaInicio, prmdateFechaFin, prmintActivo, prmintEmpresa, ussername);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpGet]
        [ActionName("PP_Anular_CostoActividad")]
        public IHttpActionResult PP_Anular_CostoActividad(int prmintCostoActividad)
        {
            try
            {
                string prmstrUsuario = JWT.Login;

                int dt = Instancia.Anular_CostoActividad(prmintCostoActividad, prmstrUsuario);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }
    }
}