using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;

using static WS_MM.Models.PeriodoAlmacenModel;
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
    public class PeriodoAlmacenController : BaseController
    {
        [HttpGet]
        [ActionName("PMMM_GetPeriodosAlmacen")]
        public IHttpActionResult PMMM_GetPeriodosAlmacen()
        {
            try
            {
                DataTable dt = Instancia.GetPeriodosAlmacen(JWT.IdEmpresa);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpGet]
        [ActionName("PMMM_GetCierreAlmacen")]
        public IHttpActionResult PMMM_GetCierreAlmacen(int prmintAnio, int prmintCerrado)
        {
            try
            {
                DataTable dt = Instancia.GetCierreAlmacen(JWT.IdEmpresa, prmintAnio, prmintCerrado);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpGet]
        [ActionName("PMMM_GetAnio")]
        public IHttpActionResult PMMM_GetAnio()
        {
            try
            {
                DataTable dt = Instancia.GetAnio();
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpGet]
        [ActionName("PMMM_GetMes")]
        public IHttpActionResult PMMM_GetMes()
        {
            try
            {
                DataTable dt = Instancia.GetMes();
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpGet]
        [ActionName("PMMM_GetDatosUltimoCierre")]
        public IHttpActionResult PMMM_GetDatosUltimoCierre(int prmintAnioPeriodo, string prmstrMesPeriodo, int prmintAlmacen, string prmstrLote, int prmintMaterial, Boolean prmbitMostrarAnulados, Boolean prmbitAgruparAlmacen)
        {
            try
            {
                DataTable dt = Instancia.GetDatosUltimoCierre(JWT.IdEmpresa, prmintAnioPeriodo, prmstrMesPeriodo, prmintAlmacen, prmstrLote, prmintMaterial, prmbitMostrarAnulados, prmbitAgruparAlmacen);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpGet]
        [ActionName("PMMM_GetKardexValorizado")]
        public IHttpActionResult PMMM_GetKardexValorizado(int prmintAnioPeriodo, string prmstrMesPeriodo, int prmintAlmacen, string prmstrLote, int prmintMaterial, Boolean prmbitMostrarAnulados, Boolean prmbitAgruparAlmacen)
        {
            try
            {
                DataTable dt = Instancia.GetKardexValorizado(JWT.IdEmpresa, prmintAnioPeriodo, prmstrMesPeriodo, prmintAlmacen, prmstrLote, prmintMaterial, prmbitMostrarAnulados, prmbitAgruparAlmacen);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpPost]
        [ActionName("ChangeStatus_Cierre")]
        public IHttpActionResult ChangeStatus_Cierre(dynamic obj)
        {
            try
            {
                int prmintPeriodoContable = obj.prmintPeriodoContable;
                int prmintAnio = obj.prmintAnio;
                string prmstrMes = obj.prmstrMes;
                Boolean prmbitCerrado = obj.prmbitCerrado;
                string prmstrUsuario = JWT.Login;

                int dt = Instancia.ChangeStatus_Cierre(prmintPeriodoContable, prmintAnio, prmstrMes, !prmbitCerrado, prmstrUsuario,JWT.IdEmpresa);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpPost]
        [ActionName("Anular_CierreAlmacen")]
        public IHttpActionResult Anular_CierreAlmacen(dynamic obj)
        {
            try
                {
                int prmintPeriodoContable = obj.prmintPeriodoContable;
                string prmstrUsuario = JWT.Login;

                int dt = Instancia.Anular_CierreAlmacen(prmintPeriodoContable, prmstrUsuario);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpPost]
        [ActionName("SaveUpdate_Cierre")]
        public IHttpActionResult SaveUpdate_Cierre(dynamic obj)
        {
            try
            {
                int prmintPeriodoAlmacen = obj.PeriodoAlmacen;
                int prmintAnio = obj.Anio;
                string prmstrMes = obj.Mes;
                int idEmpresa = JWT.IdEmpresa;
                string prmstrDesMes = obj.DesMes;
                int prmintCerrado = obj.Cerrado;
                string ussername = JWT.Login;

                int dt = Instancia.SaveUpdate_Cierre(prmintPeriodoAlmacen, prmintAnio, prmstrMes, idEmpresa, prmstrDesMes, prmintCerrado, ussername);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpGet]
        [ActionName("PMMM_GetReporteSaldoInventarios")]
        public IHttpActionResult PMMM_GetReporteSaldoInventarios(int AnioPeriodo, string MesPeriodo, int? Almacen, int? TipoMaterial, int? Material)
        {
            try
            {
                DataTable dt = Instancia.GetReporteSaldoInventarios(JWT.IdEmpresa, AnioPeriodo, MesPeriodo, Almacen, TipoMaterial, Material);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpGet]
        [ActionName("PMMM_GetKardexValorizadoSunat")]
        public IHttpActionResult PMMM_GetKardexValorizadoSunat(int AnioPeriodo, string MesPeriodo, int? Almacen, int? Material)
        {
            try
            {
                DataTableCollection dt = Instancia.GetKardexValorizadoSunat(JWT.IdEmpresa, AnioPeriodo, MesPeriodo, Almacen, Material);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpGet]
        [ActionName("PMMM_GetReporteSaldoInventarioMasivo")]
        public HttpResponseMessage PMMM_GetReporteSaldoInventarioMasivo(int AnioPeriodo, string MesPeriodo, int? Almacen, int? Material)
        {
            try
            {
                DataTable dt = Instancia.GetReporteSaldoInventarioMasivo(JWT.IdEmpresa, AnioPeriodo, MesPeriodo, Almacen, Material, null);
                var obj = (from c in dt.AsEnumerable()
                           select new
                           {
                               Empresa = c["Empresa"].ToString(),
                               Material = c["Material"].ToString(),
                               Almacen = c["Almacen"].ToString(),
                               Periodo = c["Periodo"].ToString(),
                               Fecha = c["Fecha"].ToString(),
                               RUC = c["RUC"].ToString(),
                               RazonSocial = c["RazonSocial"].ToString(),
                               Establecimiento = c["Establecimiento"].ToString(),
                               CodigoExistencia = c["CodigoExistencia"].ToString(),
                               TipoExistencia = c["TipoExistencia"].ToString(),
                               Existencia = c["Existencia"].ToString(),
                               UnidadMedida = c["UnidadMedida"].ToString(),
                               MetodoValuacion = c["MetodoValuacion"].ToString(),

                               DetalleReporte = (from d in dt.AsEnumerable()
                                               where Convert.ToInt64(c["Empresa"]).Equals(Convert.ToInt64(d["Empresa"])) 
                                                   && Convert.ToInt64(c["Material"]).Equals(Convert.ToInt64(d["Material"])) && Convert.ToInt64(c["Almacen"]).Equals(Convert.ToInt64(d["Almacen"]))
                                                 select new
                                               {
                                                     TipoDocumento = d["TipoDocumento"],
                                                     Serie = d["Serie"],
                                                     Numero = d["Numero"],
                                                     TipoOperacion = d["TipoOperacion"],
                                                     CantidadIngreso = d["CantidadIngreso"],
                                                     CostoUnitarioIngreso = d["CostoUnitarioIngreso"],
                                                     CostoTotalIngreso = d["CostoTotalIngreso"],
                                                     CantidadSalida = d["CantidadSalida"],
                                                     CostoUnitarioSalida = d["CostoUnitarioSalida"],
                                                     CostoTotalSalida = d["CostoTotalSalida"],
                                                     CantidadSaldo = d["CantidadSaldo"],
                                                     CostoUnitarioSaldo = d["CostoUnitarioSaldo"],
                                                     CostoTotalSaldo = d["CostoTotalSaldo"]
                                         
                                               })
                           }).GroupBy(c => new { c.Empresa, c.Material, c.Almacen }).Select(c => c.First());

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
        [ActionName("PMMM_RecalcularCierreAlmacen")]
        public IHttpActionResult PMMM_RecalcularCierreAlmacen(dynamic obj)
        {
            try
            {
                int prmintAnio = Convert.ToInt32(obj.prmintAnio);
                string prmstrMes = Convert.ToString(obj.prmstrMes);


                DataTableCollection dt = Instancia.RecalcularCierreAlmacen(JWT.IdEmpresa, prmintAnio, prmstrMes);
                string json = JsonConvert.SerializeObject(dt, Newtonsoft.Json.Formatting.None);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpPost]
        [ActionName("PMMM_RevisarSaldosFinales")]
        public IHttpActionResult PMMM_RevisarSaldosFinales(dynamic obj)
        {
            try
            {
                int prmintAnio = Convert.ToInt32(obj.prmintAnio);
                string prmstrMes = Convert.ToString(obj.prmstrMes);

                DataTableCollection dt = Instancia.RevisarSaldosFinales(JWT.IdEmpresa, prmintAnio, prmstrMes);
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