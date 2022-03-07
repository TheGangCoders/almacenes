using System;
using System.Data;

using WS_MM.Models;
using Shared.Controllers;
using Shared.Helpers;
using System.Web.Http;
using Shared.Util;
using Newtonsoft.Json;
using System.Xml;

namespace WS_MM.Controllers
{
    [CorsOrigin]
    [Authorize]
    public class FacturaCompraController : BaseController
    {

        [HttpPost]
        [ActionName("PMMM_Guardar")]
        public IHttpActionResult Guardar([FromBody] dynamic request)
        {
            try
            {
                int FacturaCompra = request.FacturaCompra;
                int Sociedad = request.Sociedad;
                int Proveedor = request.Proveedor;
                int? OrdenCompra = request.OrdenCompra;
                int? OrdenServicio = request.OrdenServicio;
                int? OrdenTransporte = request.OrdenTransporte;
                string ValorDoc = request.ValorDoc;
                string CodigoCotizacion = request.CodigoCotizacion;
                string TipoDocumento = request.TipoDocumento;
                string FormaPago = request.FormaPago;
                string Serie = request.Serie;
                string Numero = request.Numero;
                string FechaEmision = request.FechaEmision;
                string FechaEntrega = request.FechaEntrega;
                string TipoMoneda = request.TipoMoneda;
                decimal TasaCambio = request.TasaCambio;
                bool IncluyeIgv = request.IncluyeIgv;
                bool IncluyeRenta = request.IncluyeRenta;
                decimal SubTotal = request.SubTotal;
                decimal Descuento = request.Descuento;
                decimal IgvPorcentaje = request.IgvPorcentaje != null ? request.IgvPorcentaje : 0;
                decimal Igv = request.Igv != null ? request.Igv : 0;
                decimal RentaPorcentaje = request.RentaPorcentaje != null ? request.RentaPorcentaje : 0;
                decimal Renta = request.Renta != null ? request.Renta : 0;
                decimal Total = request.Total;
                decimal Detraccion = request.Detraccion;
                string Estado = request.Estado;
                bool Activo = request.Activo;

                var detalle = JsonConvert.SerializeObject(request.Detalle);
                XmlNode detalleXmlNode = JsonConvert.DeserializeXmlNode("{\"row\":" + detalle + "}", "Detalle");
                string detalleXml = detalleXmlNode.InnerXml;

                DataTable dt = FacturaCompraModel.Instancia.Guardar(FacturaCompra, JWT.IdEmpresa, Sociedad, Proveedor,
                                                               OrdenCompra, CodigoCotizacion, TipoDocumento, FormaPago,
                                                               FechaEmision, FechaEntrega, TipoMoneda, TasaCambio,
                                                               IncluyeIgv, IncluyeRenta, SubTotal, Descuento, IgvPorcentaje,
                                                               Igv, RentaPorcentaje, Renta, Total, Detraccion, Estado, 
                                                                JWT.Login, Serie, Numero, OrdenServicio, OrdenTransporte, ValorDoc, detalleXml);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleError(ex));
            }
        }

        [HttpPost]
        [ActionName("PMMM_Anular")]
        public IHttpActionResult Anular([FromBody] dynamic request)
        {
            try
            {
                int FacturaCompra = request.FacturaCompra;

                DataTable dt = FacturaCompraModel.Instancia.Anular(FacturaCompra, JWT.IdEmpresa, JWT.Login);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleError(ex));
            }
        }

        [HttpGet]
        [ActionName("PMMM_Obtener")]
        public IHttpActionResult PMMM_Obtener(int FacturaCompra)
        {
            try
            {
                DataTable dt = FacturaCompraModel.Instancia.Obtener(FacturaCompra, JWT.IdEmpresa);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleError(ex));
            }
        }

        [HttpGet]
        [ActionName("PMMM_Listar")]
        public IHttpActionResult PMMM_Listar(string Estado, string FechaDesde, string FechaHasta)
        {
            try
            {
                DataTable dt = FacturaCompraModel.Instancia.Listar(JWT.IdEmpresa, Estado, FechaDesde, FechaHasta);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleError(ex));
            }
        }
    }
}
