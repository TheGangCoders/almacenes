using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using static WS_SD.Models.FacturaVentaModel;
using System.Web;
using System.Security.Cryptography;
using Shared.Controllers;
using Shared.Helpers;
using System.Web.Http;
using Shared.Util;
using Shared.Token;
using Newtonsoft.Json;

namespace WS_SD.Controllers
{
    [CorsOrigin]
    [Authorize]
    public class FacturaVentaController : BaseController
    {
        [HttpGet]
        [ActionName("SD_getClaseFactura")]
        public IHttpActionResult SD_getClaseFactura()
        {
            try
            {
                DataTable dt = Instancia.getClaseFactura(JWT.IdEmpresa);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpGet]
        [ActionName("SD_getEstadoFactura")]
        public IHttpActionResult SD_getEstadoFactura()
        {
            try
            {
                DataTable dt = Instancia.getEstadoFactura(JWT.IdEmpresa);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }         

        [HttpGet]
        [ActionName("SD_getListadoFacturasVenta")]
        public IHttpActionResult SD_getListadoFacturasVenta(int ClaseFactura, string EstadoFactura, string FechaInicio, string FechaFin)
        {
            try
            {
                DataTable dt = Instancia.getListadoFacturasVenta(JWT.IdEmpresa, ClaseFactura, EstadoFactura, FechaInicio, FechaFin);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpGet]
        [ActionName("SD_getListadoDespachos_Factura")]
        public IHttpActionResult SD_getListadoDespachos_Factura(int TipoSalida, string NumeroDocEntrega, string Cliente, string CodCliente, string FechaIncio, string FechaFin, int ClaseFactura)
        {
            try
            {
                DataTable dt = Instancia.getListadoDespachos_Factura(JWT.IdEmpresa, JWT.IdSociedad, TipoSalida, NumeroDocEntrega, Cliente, CodCliente, FechaIncio, FechaFin, ClaseFactura);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpGet]
        [ActionName("SD_getResponsablesPago")]
        public IHttpActionResult SD_getResponsablesPago(int Cliente, int OrganizacionVenta, int Sector)
        {
            try
            {
                DataTable dt = Instancia.getResponsablesPago(JWT.IdEmpresa, OrganizacionVenta, Sector, Cliente);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpPost]
        [ActionName("SD_getDetallesDespachoFactura")]
        public HttpResponseMessage SD_getDetallesDespachoFactura(dynamic obj)
        {
            try
            {
                string xml = "<?xml version='1.0' encoding='ISO-8859-1'?><root>";

                foreach (dynamic dm in obj)
                {
                    xml += "<Detalles ";
                    xml += "DespachoEntrega = '" + dm.DespachoEntrega + "' />";
                }

                xml += "</root>";

                DataTable dt = Instancia.getDetallesDespachoFactura(xml);

                var rpta = (from c in dt.AsEnumerable()
                           select new
                           {
                               DDespachoEnrega = Convert.ToInt64(c["DDespachoEnrega"]),
                               Inafecto = Convert.ToInt64(c["Inafecto"]),
                               Posicion = c["Posicion"].ToString(),
                               PosicionDespacho = c["PosicionDespacho"].ToString(),
                               CodDespacho = c["CodDespacho"].ToString(),
                               Material = c["Material"].ToString(),
                               CodMaterial = c["CodMaterial"].ToString(),
                               DescMaterial = c["DescMaterial"].ToString(),
                               Cantidad = Convert.ToDecimal(c["CantidadEntrega"].ToString()),                               
                               UnidadMaterial = Convert.ToInt64(c["UnidadMaterial"]),
                               DescUnidadMedida = c["DescUnidadMedida"].ToString(),
                               DetallePrecioUnitario = Convert.ToDecimal(c["DetallePrecioUnitario"].ToString()),

                               PrecioUnitario = Convert.ToDecimal(c["PrecioUnitario"].ToString()),
                               ValorUnitario = Convert.ToDecimal(c["ValorUnitario"].ToString()),

                               DetalleDescuentoUnitario = Convert.ToDecimal(c["DetalleDescuentoUnitario"].ToString()),
                               DescuentoPrecio = Convert.ToDecimal(c["DescuentoPrecio"].ToString()),
                               DescuentoValor = Convert.ToDecimal(c["DescuentoValor"].ToString()),
                               PrecioTotal = Convert.ToDecimal(c["PrecioTotal"].ToString()),
                               ValorTotal = Convert.ToDecimal(c["ValorTotal"].ToString()),
                               PesoBruto = Convert.ToDecimal(c["PesoBruto"].ToString()),
                               PesoNeto = Convert.ToDecimal(c["PesoNeto"].ToString()),

                               PrecioFob = Convert.ToDecimal(c["precioFob"].ToString()),
                               Factor = c["Factor"].ToString() == "" ? c["Factor"] : Convert.ToDecimal(c["Factor"]),
                               UnidadPrecio = c["UnidadPrecio"].ToString() == "" ? c["UnidadPrecio"] : Convert.ToDecimal(c["UnidadPrecio"]),
                               

                               DetallesLotes = (from d in dt.AsEnumerable()
                                           where Convert.ToInt64(c["DDespachoEnrega"]).Equals(Convert.ToInt64(d["DDespachoEnrega"]))
                                           select new
                                           {
                                               DDespachoLotes = Convert.ToInt64(d["DDespachoLotes"]),
                                               Posicion = d["Posicion"].ToString(),
                                               Cantidad = Convert.ToDecimal(d["CantidadLote"].ToString()),
                                               PesoBruto = Convert.ToDecimal(d["PesoBrutoLote"].ToString()),
                                               PesoNeto = Convert.ToDecimal(d["PesoNetoLote"].ToString()),
                                               CodMaterial = d["CodMaterial"].ToString(),
                                               Almacen = d["AlmacenLote"].ToString(),
                                               DesAlmacenLote = d["DesAlmacenLote"].ToString(),
                                               DescUnidadMedida = d["DescUnidadMedida"].ToString(),
                                               Lotes = d["Lote"].ToString(),                                               
                                           }).GroupBy(d => new { d.DDespachoLotes }).Select(d => d.First()),                                           
                           }).GroupBy(c => new { c.DDespachoEnrega }).Select(c => c.First());

                var httpResponseMessage = Request.CreateResponse<IEnumerable<Object>>(HttpStatusCode.OK, rpta);
                httpResponseMessage.Headers.Add("Access-Control-Allow-Origin", "*");

                return httpResponseMessage;
                
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }
        
        [HttpPost]
        [ActionName("SD_SaveUpdateFacturaVenta")]
        public HttpResponseMessage SD_SaveUpdateFacturaVenta(dynamic obj)
        {
            try
            {
                string xml = "<?xml version='1.0' encoding='ISO-8859-1'?><root>";
                string xmlDetalles = "<?xml version='1.0' encoding='ISO-8859-1'?><root>";
                string xmlLotes = "<?xml version='1.0' encoding='ISO-8859-1'?><root>";

                xml += "<FacturaVenta ";
                xml += "FacturaVenta ='" + obj.FacturaVenta + "' ";
                xml += "ClaseDocVta ='" + obj.ClaseDocVta + "' ";
                xml += "CodClaseDocVta ='" + obj.CodClaseDocVta + "' ";
                xml += "CodClaseDocVtaAnt ='" + obj.CodClaseDocVtaAnt + "' ";              
                xml += "Empresa ='" + JWT.IdEmpresa + "' ";
                xml += "Sociedad ='" + JWT.IdSociedad + "' ";

                if (obj.OrgVentas != null && obj.OrgVentas.ToString() != "")
                {
                    xml += "OrgVentas ='" + obj.OrgVentas + "' ";
                }

                xml += "Canal ='" + obj.Canal + "' ";
                xml += "CodCanal ='" + obj.CodCanal + "' ";
                xml += "TipoDocumento ='" + obj.TipoDocumento + "' ";                
                xml += "Moneda ='" + obj.Moneda + "' ";                
                xml += "NroDocPago ='" + obj.NroDocPago + "' ";
                xml += "SerieDocPago ='" + obj.SerieDocPAgo + "' ";                
                xml += "FechaDocPago ='" + obj.FechaDocPago + "' ";
                xml += "Estado ='" + obj.Estado + "' ";
                xml += "Cliente ='" + obj.Cliente + "' ";
                if (obj.Incoterm != null && obj.Incoterm.ToString() != "" && obj.Incoterm.ToString() != "0")
                {
                    xml += "Incoterm ='" + obj.Incoterm + "' ";
                }
                // xml += "Incoterm ='" + obj.Incoterm + "' ";
                xml += "TextoIncoterm ='" + obj.TextoIncoterm + "' ";
                xml += "Comision ='" + obj.Comision.ToString(System.Globalization.CultureInfo.InvariantCulture) + "' ";

                xml += "ComisionPorcentaje ='" + obj.ComisionPorcentaje.ToString(System.Globalization.CultureInfo.InvariantCulture) + "' ";

                if (obj.PaisDestino != null && obj.PaisDestino.ToString() != "")
                {
                    xml += "PaisDestino ='" + obj.PaisDestino + "' ";
                }

                xml += "ResponsablePago ='" + obj.ResponsablePago + "' ";

                if (obj.IncluyeIgv = true)
                {
                    xml += "IncluyeIgv ='" + 1 + "' ";
                } else
                {
                    xml += "IncluyeIgv ='" + 0 + "' ";
                }
                
                if (obj.DestinoMercancia != null && obj.DestinoMercancia.ToString() != "")
                {
                    xml += "DestinoMercancia ='" + obj.DestinoMercancia + "' ";
                }

                xml += "TipoEnvio ='" + obj.TipoEnvio + "' ";
                xml += "CuentaBancaria ='" + obj.CuentaBancaria + "' ";                
                xml += "IgvPorcentaje ='" + obj.IgvPorcentaje.ToString(System.Globalization.CultureInfo.InvariantCulture) + "' ";
                xml += "Igv ='" + obj.Igv.ToString(System.Globalization.CultureInfo.InvariantCulture) + "' ";
                xml += "SubTotal ='" + obj.SubTotal.ToString(System.Globalization.CultureInfo.InvariantCulture) + "' ";
                xml += "Descuento ='" + obj.Descuento.ToString(System.Globalization.CultureInfo.InvariantCulture) + "' ";
                xml += "Total ='" + obj.Total.ToString(System.Globalization.CultureInfo.InvariantCulture) + "' ";               
                xml += "Anulado ='" + obj.Anulado + "' />";

                foreach (dynamic dm in obj.DetalleMateriales)
                {
                    xmlDetalles += "<Detalles ";
                    xmlDetalles += "Posicion = '" + dm.Posicion + "' ";
                    xmlDetalles += "DDespachoEntrega = '" + dm.DDespachoEntrega + "' ";
                    xmlDetalles += "PosicionEntrega = '" + dm.PosicionEntrega + "' ";
                    xmlDetalles += "Material = '" + dm.Material + "' ";
                    xmlDetalles += "ValorUnitario = '" + dm.ValorUnitario.ToString(System.Globalization.CultureInfo.InvariantCulture) + "' ";
                    xmlDetalles += "PrecioUnitario = '" + dm.PrecioUnitario.ToString(System.Globalization.CultureInfo.InvariantCulture) + "' ";
                    xmlDetalles += "DescuentoPrecio = '" + dm.DescuentoPrecio.ToString(System.Globalization.CultureInfo.InvariantCulture) + "' ";
                    xmlDetalles += "DescuentoValor = '" + dm.DescuentoValor.ToString(System.Globalization.CultureInfo.InvariantCulture) + "' ";
                    xmlDetalles += "CantidadFactura = '" + dm.CantidadFactura.ToString(System.Globalization.CultureInfo.InvariantCulture) + "' ";                    
                    xmlDetalles += "PrecioTotal = '" + dm.PrecioTotal.ToString(System.Globalization.CultureInfo.InvariantCulture) + "' ";
                    xmlDetalles += "ValorTotal = '" + dm.ValorTotal.ToString(System.Globalization.CultureInfo.InvariantCulture) + "' ";
                    xmlDetalles += "PesoNeto = '" + dm.PesoNeto.ToString(System.Globalization.CultureInfo.InvariantCulture) + "' ";
                    xmlDetalles += "PesoBruto = '" + dm.PesoBruto.ToString(System.Globalization.CultureInfo.InvariantCulture) + "' ";
                    xmlDetalles += "UnidadMedida = '" + dm.UnidadMedida + "' ";
                    xmlDetalles += "PrecioFob = '" + dm.PrecioFob.ToString(System.Globalization.CultureInfo.InvariantCulture) + "' ";
                    xmlDetalles += "Factor = '" + dm.Factor.ToString(System.Globalization.CultureInfo.InvariantCulture) + "' ";
                    xmlDetalles += "UnidadPrecio = '" + dm.UnidadPrecio + "' ";         
                    xmlDetalles += "Activo = '" + dm.Activo + "' />";
                }

                foreach (dynamic dp in obj.DetalleLote)
                {
                    xmlLotes += "<DetalleLote ";
                    xmlLotes += "Posicion = '" + dp.Posicion + "' ";
                    xmlLotes += "Cantidad = '" + dp.Cantidad + "' ";
                    xmlLotes += "Lote = '" + dp.Lote + "' ";
                    xmlLotes += "Almacen = '" + dp.Almacen + "' ";
                    xmlLotes += "PesoBruto = '" + dp.PesoBruto.ToString(System.Globalization.CultureInfo.InvariantCulture) + "' ";
                    xmlLotes += "PesoNeto = '" + dp.PesoNeto.ToString(System.Globalization.CultureInfo.InvariantCulture) + "' ";                    
                    xmlLotes += "Activo = '" + dp.Activo + "' />";
                }


                xml += "</root>";
                xmlDetalles += "</root>";
                xmlLotes += "</root>";

                int res = Instancia.SaveUpdateFacturaVenta(JWT.Login, xml, xmlDetalles, xmlLotes);
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
                    mensaje = "No existe un tipo de cambio para la fecha indicada.";
                }

                if (res == -2)
                {
                    success = true;
                    mensaje = "No existe un tipo de cambio para la fecha indicada.";
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
        [ActionName("SD_GetDatosFacturaVenta")]
        public HttpResponseMessage SD_GetDatosFacturaVenta(int FacturaVenta)
        {
            try
            {
                DataTable dt = Instancia.GetDatosFacturaVenta(FacturaVenta);
                var rpta = (from c in dt.AsEnumerable()
                            select new
                            {
                                DocumentoPago = Convert.ToInt64(c["DocumentoPago"]),
                                SerieDocPAgo = c["SerieDocPAgo"].ToString(),
                                NroDocPago = c["NroDocPago"].ToString(),
                                ClaseDocVta = Convert.ToInt64(c["ClaseDocVta"]),
                                MontoComision = Convert.ToDecimal(c["MontoComision"].ToString()),
                                PorcentajeComision = Convert.ToDecimal(c["PorcentajeComision"].ToString()),

                                SubTotal = Convert.ToDecimal(c["SubTotal"].ToString()),
                                IgvPorcentaje = Convert.ToDecimal(c["IgvPorcentaje"].ToString()),
                                Igv = Convert.ToDecimal(c["Igv"].ToString()),
                                Descuento = Convert.ToDecimal(c["Descuento"].ToString()),
                                Total = Convert.ToDecimal(c["Total"].ToString()),
                                IncluyeIgv = Convert.ToBoolean(c["IncluyeIgv"]),

                                FechaDocPago = c["FechaDocPago"] == null ? c["FechaDocPago"].ToString() : Convert.ToDateTime(c["FechaDocPago"]).ToString("yyyy-MM-dd HH:mm:ss"),
                                
                                Cliente = c["Cliente"].ToString(),
                                Ruc = c["Ruc"].ToString(),
                                DireccionCliente = c["DireccionCliente"].ToString(),
                                RazonCliente = c["RazonCliente"].ToString(),
                                Canal = c["Canal"].ToString(),
                                Moneda = c["Moneda"].ToString(),
                                CuentaBancaria = c["CuentaBancaria"].ToString(),

                                DestinoMercancia = c["DestinoMercancia"].ToString(),
                                NombreDestinoMercancia = c["NombreDestinoMercancia"].ToString(),
                                PaisDestino = c["PaisDestino"].ToString(),
                                DesPais = c["DesPais"].ToString(),                           

                                Incoterm = c["Incoterm"].ToString(),
                                TipoEnvio = c["TipoEnvio"].ToString(),
                                OrgVentas = c["OrgVentas"].ToString(),
                                DescOrgVenta = c["DescOrgVenta"].ToString(),
                                Sector = c["Sector"].ToString(),
                                Responsable = Convert.ToInt64(c["Responsable"]),
                                TipoDocumento = c["TipoDocumento"].ToString(),


                                Detalles = (from d in dt.AsEnumerable()
                                                 where Convert.ToInt64(c["DocumentoPago"]).Equals(Convert.ToInt64(d["DocumentoPago"]))
                                                 select new
                                                 {
                                                     DDespachoEntrega = Convert.ToInt64(d["DDespachoEntrega"]),
                                                     Posicion = d["Posicion"].ToString(),
                                                     PosicionDespacho = d["PosicionDespacho"].ToString(),
                                                     CodDespacho = d["CodDespacho"].ToString(),
                                                     Material = d["Material"].ToString(),
                                                     CodMaterial = d["CodMaterial"].ToString(),
                                                     DescMaterial = d["DescMaterial"].ToString(),
                                                     Cantidad = Convert.ToDecimal(d["CantidadEntrega"].ToString()),
                                                     UnidadMaterial = Convert.ToInt64(d["UnidadMaterial"]),
                                                     DescUnidadMedida = d["DescUnidadMedida"].ToString(),




                                                     DetallePrecioUnitario = Convert.ToDecimal(d["DetallePrecioUnitario"].ToString()),
                                                     DetalleDescuentoUnitario = Convert.ToDecimal(d["DetalleDescuentoUnitario"].ToString()),
                                                     DetallePrecioTotal = Convert.ToDecimal(d["DetallePrecioTotal"].ToString()),

                                                     PrecioUnitario = Convert.ToDecimal(d["PrecioUnitario"].ToString()),
                                                     ValorUnitario = Convert.ToDecimal(d["ValorUnitario"].ToString()),
                                                     DescuentoPrecio = Convert.ToDecimal(d["DescuentoPrecio"].ToString()),
                                                     DescuentoValor = Convert.ToDecimal(d["DescuentoValor"].ToString()),
                                                     PrecioTotal = Convert.ToDecimal(d["PrecioTotal"].ToString()),
                                                     ValorTotal = Convert.ToDecimal(d["ValorTotal"].ToString()),
                                                     PesoBruto = Convert.ToDecimal(d["PesoBruto"].ToString()),
                                                     PesoNeto = Convert.ToDecimal(d["PesoNeto"].ToString()),

                                                     PrecioFob = Convert.ToDecimal(d["precioFob"].ToString()),
                                                     Factor = c["Factor"].ToString() == "" ? d["Factor"] : Convert.ToDecimal(d["Factor"]),
                                                     UnidadPrecio = d["UnidadPrecio"].ToString() == "" ? c["UnidadPrecio"] : Convert.ToDecimal(c["UnidadPrecio"]),


                                                     DetallesLotes = (from m in dt.AsEnumerable()
                                                                      where Convert.ToInt64(d["DDespachoEntrega"]).Equals(Convert.ToInt64(m["DDespachoEntrega"]))
                                                                      select new
                                                                      {
                                                                          DDespachoLotes = Convert.ToInt64(m["DDespachoLotes"]),
                                                                          Posicion = m["Posicion"].ToString(),
                                                                          Cantidad = Convert.ToDecimal(m["CantidadLote"].ToString()),
                                                                          PesoBruto = Convert.ToDecimal(m["PesoBrutoLote"].ToString()),
                                                                          PesoNeto = Convert.ToDecimal(m["PesoNetoLote"].ToString()),
                                                                          CodMaterial = m["CodMaterial"].ToString(),
                                                                          Almacen = m["AlmacenLote"].ToString(),
                                                                          DesAlmacenLote = m["DesAlmacenLote"].ToString(),
                                                                          DescUnidadMedida = m["DescUnidadMedida"].ToString(),
                                                                          Lotes = m["Lote"].ToString(),
                                                                      }).GroupBy(m => new { m.DDespachoLotes }).Select(m => m.First()),


                                                 }).GroupBy(d => new { d.DDespachoEntrega }).Select(d => d.First()),
                            }).GroupBy(c => new { c.DocumentoPago }).Select(c => c.First());

                var httpResponseMessage = Request.CreateResponse<IEnumerable<Object>>(HttpStatusCode.OK, rpta);
                httpResponseMessage.Headers.Add("Access-Control-Allow-Origin", "*");

                return httpResponseMessage;
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }


        [HttpPost]
        [ActionName("SD_postUpdateAnularFacturaVenta")]
        public HttpResponseMessage SD_postUpdateAnularFacturaVenta(dynamic obj)
        {
            try
            {
                int Codigo = obj.Codigo;
                string Estado = obj.Estado;


                int res = Instancia.postUpdateAnularFacturaVenta(Codigo, JWT.Login, Estado);
                string mensaje = "";
                bool success = false;
                if (res == 0)
                {
                    success = true;
                    mensaje = "Se anulo correctamente la factura seleccionada.";
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
        [ActionName("GetPdfFacturaVenta")]
        public IHttpActionResult GetPdfFacturaVenta(int prmintFacturaVenta, int prmintOpcIdioma)
        {
            try
            {
                DataTableCollection dt = Instancia.GetPdfFacturaVenta(prmintFacturaVenta, prmintOpcIdioma);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleError(ex));
            }
        }


    }
}