using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Security.Cryptography;
using Shared.Controllers;
using Shared.Helpers;
using System.Web.Http;
using Shared.Util;
using Shared.Token;
using Newtonsoft.Json;
using WS_MM.Models;

namespace WS_MM.Controllers
{
    [CorsOrigin]
    [Authorize]
    public class OrdenTransporteController : BaseController
    {
        [HttpPost]
        [ActionName("MM_SaveUpdateOrdenTransporte")]
        public IHttpActionResult MM_SaveUpdateOrdenTransporte(dynamic obj)
        {
            try
            {
                int prmintOrdenTransporte = obj.OrdenTransporte;
                int prmintSociedad = JWT.IdSociedad;
                int prmintEmpresa = JWT.IdEmpresa;
                int prmintTransportista = obj.Transportista;
                int? prmintVehiculo = obj.Vehiculo;
                int? prmintConductor = obj.Conductor;
                int prminAlmacenOrigen = obj.AlmacenOrigen;
                int prmintAlmacenDestino = obj.AlmacenDestino;
                DateTime prmdateFechaEmision = obj.FechaEmision;
                string prmstrTipoDocumento = obj.TipoDocumento;
                string prmstrFormaPago = obj.FormaPago;
                string prmstrTipoMoneda = obj.TipoMoneda;
                //Decimal prmdecTasaCambio = obj.TasaCambio;
                int prmintIGV = obj.IGV;
                int prmintRenta = obj.Renta;
                Decimal prmdecSubTotal = obj.SubTotal;
                Decimal prmdecDescuento = obj.Descuento;
                Decimal prmdecPorcentajeIGV = obj.PorcentajeIGV;
                Decimal prmdecValorIGV = obj.ValorIGV;
                Decimal prmdecPorcentajeRenta = obj.PorcentajeRenta;
                Decimal prmdecValorRenta = obj.ValorRenta;
                Decimal prmdecTotal = obj.Total;
                int prmintActivo = obj.Activo;

                string prmstrEstado = obj.Estado;

                string prmstrUsuario = JWT.Login;

                string prmstrXml = obj.strXML;

                int dt = OrdenTransporteModel.Instancia.SaveUpdate_OrdenTransporte(prmintOrdenTransporte, prmintSociedad, prmintEmpresa, prmintTransportista, prmintVehiculo,
                         prmintConductor, prminAlmacenOrigen, prmintAlmacenDestino, prmdateFechaEmision, prmstrTipoDocumento, prmstrFormaPago, prmstrTipoMoneda/*, prmdecTasaCambio*/, 
                         prmintIGV, prmintRenta, prmdecSubTotal, prmdecDescuento, prmdecPorcentajeIGV, prmdecValorIGV, prmdecPorcentajeRenta, prmdecValorRenta, prmdecTotal, 
                         prmintActivo, prmstrEstado, prmstrUsuario, prmstrXml);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpPost]
        [ActionName("MM_ChangeStatusOrdenTransporte")]
        public IHttpActionResult MM_ChangeStatusOrdenTransporte(dynamic obj)
        {
            try
            {
                string prmstrEstado = obj.Estado;
                string prmstrUsuario = JWT.Login;
                int prmintUsuario = JWT.IdUsuario;
                string prmstrXml = obj.strXML;

                int dt = OrdenTransporteModel.Instancia.ChangeStatusOrdenTransporte(prmstrUsuario, prmintUsuario, prmstrEstado, prmstrXml);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpGet]
        [ActionName("MM_GetOrdenTransporte")]
        public IHttpActionResult MM_GetOrdenTransporte(string prmstrEstado, DateTime prmdateFechaDesde, DateTime prmdateFechaHasta)
        {
            try
            {
                DataTable dt = OrdenTransporteModel.Instancia.GetOrdenTransporte(JWT.IdSociedad, prmstrEstado, prmdateFechaDesde, prmdateFechaHasta);

                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpGet]
        [ActionName("MM_GetOrdenTransporteById")]
        public IHttpActionResult MM_GetOrdenTransporteById(int prmintOrdenTransporte)
        {
            try
            {
                DataTableCollection dt = OrdenTransporteModel.Instancia.GetOrdenTransporteById(prmintOrdenTransporte);

                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpGet]
        [ActionName("MM_GetEstado")]
        public IHttpActionResult MM_GetEstado()
        {
            try
            {
                DataTable dt = OrdenTransporteModel.Instancia.GetEstado(JWT.IdEmpresa);

                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpGet]
        [ActionName("MM_GetMotivoTranspote")]
        public IHttpActionResult MM_GetMotivoTranspote()
        {
            try
            {
                DataTable dt = OrdenTransporteModel.Instancia.GetMotivoTranspote(JWT.IdEmpresa);

                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }
    }
}