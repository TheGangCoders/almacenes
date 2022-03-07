using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using static WS_PP.Models.IngresoMateriaModelDAL;
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
    public class IngresoMateriaController : BaseController
    {
        #region MATERIA PRIMA
        [HttpGet]
        [ActionName("PP_getIngresoMaterias_List")]
        public IHttpActionResult PP_getIngresoMaterias_List(string FechaInicio, string FechaFin, int prmintOrigen, int pmrintDestino, string prmstrEstado)
        {
            try
            {
                DataTable dt = Instancia.getIngresoMaterias_List(FechaInicio, FechaFin, prmintOrigen, pmrintDestino, prmstrEstado);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpPost]
        [ActionName("PP_enableDisable_IngresoMateria")]
        public IHttpActionResult PP_enableDisable_IngresoMateria(dynamic obj)
        {
            try
            {
                int prmintGuia = obj.prmintGuia;
                int prmintMigo = obj.prmintMigo;
                Boolean prmbitActivo = obj.prmbitActivo;
                string prmstrEstado = obj.prmstrEstado;
                string prmstrUsuario = obj.prmstrUsuario;

                int dt = Instancia.enableDisable_IngresoMateria(prmintGuia, prmintMigo, prmbitActivo, prmstrEstado, prmstrUsuario);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }
        #endregion

        #region MATERIALES
        
        [HttpGet]
        [ActionName("PP_getIngresoMateriales_List")]
        public IHttpActionResult PP_getIngresoMateriales_List(string FechaInicio, string FechaFin, string prmstrEstado)
        {
            try
            {
                DataTable dt = Instancia.getIngresoMateriales_List(FechaInicio, FechaFin, prmstrEstado, JWT.IdEmpresa);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpPost]
        [ActionName("PP_enableDisable_IngresoMateriales")]
        public IHttpActionResult PP_enableDisable_IngresoMateriales(dynamic obj)
        {
            try
            {
                int prmintOrden = obj.prmintOrden;
                int prmintMigo = obj.prmintMigo;
                Boolean prmbitActivo = obj.prmbitActivo;
                string prmstrEstado = obj.prmstrEstado;
                string prmstrUsuario = obj.prmstrUsuario;

                int dt = Instancia.enableDisable_IngresoMateriales(prmintOrden, prmintMigo, prmbitActivo, prmstrEstado, prmstrUsuario);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        #endregion






    }
}
