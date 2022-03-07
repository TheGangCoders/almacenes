using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using static WS_SD.Models.NotasModel;
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
    public class NotasController : BaseController
    {
        [HttpGet]
        [ActionName("SD_getClaseNotas")]
        public IHttpActionResult SD_getClaseNotas()
        {
            try
            {
                DataTable dt = Instancia.getClaseNotas(JWT.IdEmpresa);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpGet]
        [ActionName("SD_getListNotas")]
        public IHttpActionResult SD_getListNotas(int ClaseNota, string Estado, string FechaInicio, string FechaFin)
        {
            try
            {
                DataTable dt = Instancia.getListNotas(JWT.IdEmpresa, JWT.IdSociedad, ClaseNota, Estado, FechaInicio, FechaFin);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpGet]
        [ActionName("SD_getListadoFacturaVenta")]
        public IHttpActionResult SD_getListadoFacturaVenta(string ClaseFactura, string CodigoFactura, string FechaInicio, string FechaFin)
        {
            try
            {
                DataTable dt = Instancia.getListadoFacturaVenta(JWT.IdEmpresa, JWT.IdSociedad, ClaseFactura, CodigoFactura, FechaInicio, FechaFin);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpPost]
        [ActionName("SD_getDetallesFacturas")]
        public HttpResponseMessage SD_getDetallesFacturas(dynamic obj)
        {
            try
            {
                string xml = "<?xml version='1.0' encoding='ISO-8859-1'?><root>";

                foreach (dynamic dm in obj)
                {
                    xml += "<Detalles ";
                    xml += "DocumentoPago = '" + dm.DocumentoPago + "' />";
                }

                xml += "</root>";

                DataTable dt = Instancia.getDetallesFacturas(xml);

                var rpta = (from c in dt.AsEnumerable()
                            select new
                            {
                                DetalleDocPago = Convert.ToInt64(c["DetalleDocPago"]),
                                PosicionFactura = c["PosicionFactura"].ToString(),
                                PosicionNota = Convert.ToInt64(c["PosicionNota"]),
                                Material = c["Material"].ToString(),
                                CodMaterial = c["CodMaterial"].ToString(),
                                DescMaterial = c["DescMaterial"].ToString(),
                                UnidadMedida = Convert.ToInt64(c["UnidadMedida"]),
                                DescUnidadMedida = c["DescUnidadMedida"].ToString(),                                
                                CantidadFactura = Convert.ToDecimal(c["CantidadFactura"].ToString()),
                                CantidadNota = Convert.ToDecimal(c["CantidadFactura"].ToString()),
                                PrecioNota = Convert.ToDecimal(c["PrecioFactura"].ToString()),
                                MontoNota = Convert.ToDecimal(c["PrecioFactura"].ToString()) * Convert.ToDecimal(c["CantidadFactura"].ToString()),
                                ValorUnitario = Convert.ToDecimal(c["ValorUnitario"].ToString()),
                                PrecioUnitario = Convert.ToDecimal(c["PrecioUnitario"].ToString()),
                                ValorMonto = Convert.ToDecimal(c["ValorMonto"].ToString()),
                                PrecioMonto = Convert.ToDecimal(c["PrecioMonto"].ToString()),
                                Inafecto = Convert.ToInt64(c["Inafecto"]),
                                PesoNetoMaterial = Convert.ToDecimal(c["PesoNetoMaterial"].ToString()),
                                PesoNeto = Convert.ToDecimal(c["PesoNetoMaterial"].ToString()) * Convert.ToDecimal(c["CantidadFactura"].ToString()),

                            }).GroupBy(c => new { c.DetalleDocPago }).Select(c => c.First());

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
        [ActionName("SD_SaveUpdateNotaFactura")]
        public HttpResponseMessage SD_SaveUpdateNotaFactura(dynamic obj)
        {
            try
            {
                string xml = "<?xml version='1.0' encoding='ISO-8859-1'?><root>";
                string xmlMateriales = "<?xml version='1.0' encoding='ISO-8859-1'?><root>";

                xml += "<NotaFactura ";
                xml += "NotaFactura ='" + obj.NotaFactura + "' ";
                xml += "ClaseDocVta ='" + obj.ClaseDocVta + "' ";
                xml += "CodClaseDocVta ='" + obj.CodClaseDocVta + "' ";
                xml += "Empresa ='" + JWT.IdEmpresa + "' ";
                xml += "Sociedad ='" + JWT.IdSociedad + "' ";

                if (obj.OrgVentas != null && obj.OrgVentas.ToString() != "")
                {
                    xml += "OrgVentas ='" + obj.OrgVentas + "' ";
                }

                xml += "Canal ='" + obj.Canal + "' ";
                xml += "CodCanal ='" + obj.CodCanal + "' ";
                xml += "Moneda ='" + obj.Moneda + "' ";
                xml += "NroDoc ='" + obj.NroDoc + "' ";
                xml += "SerieDoc ='" + obj.SerieDoc + "' ";
                xml += "FechaDoc ='" + obj.FechaDoc + "' ";
                xml += "Estado ='" + obj.Estado + "' ";
                xml += "Cliente ='" + obj.Cliente + "' ";
                xml += "Incoterm ='" + obj.Incoterm + "' ";
                xml += "TextoIncoterm ='" + obj.TextoIncoterm + "' ";
                xml += "Observaciones ='" + obj.Observaciones + "' ";

                

                if (obj.PaisDestino != null && obj.PaisDestino.ToString() != "")
                {
                    xml += "PaisDestino ='" + obj.PaisDestino + "' ";
                }

                xml += "ResponsablePago ='" + obj.ResponsablePago + "' ";

                xml += "IncluyeIgv ='" + obj.IncluyeIgv + "' ";

                //if (obj.IncluyeIgv = true)
                //{
                //    xml += "IncluyeIgv ='" + 1 + "' ";
                //}
                //else
                //{
                //    xml += "IncluyeIgv ='" + 0 + "' ";
                //}

                if (obj.DestinoMercancia != null && obj.DestinoMercancia.ToString() != "")
                {
                    xml += "DestinoMercancia ='" + obj.DestinoMercancia + "' ";
                }

                xml += "TipoEnvio ='" + obj.TipoEnvio + "' ";

                if (obj.CuentaBancaria != null && obj.CuentaBancaria.ToString() != "")
                {
                    xml += "CuentaBancaria ='" + obj.CuentaBancaria + "' ";
                }

                xml += "IgvPorcentaje ='" + obj.IgvPorcentaje.ToString(System.Globalization.CultureInfo.InvariantCulture) + "' ";
                xml += "Igv ='" + obj.Igv.ToString(System.Globalization.CultureInfo.InvariantCulture) + "' ";
                xml += "SubTotal ='" + obj.SubTotal.ToString(System.Globalization.CultureInfo.InvariantCulture) + "' ";
                xml += "Total ='" + obj.Total.ToString(System.Globalization.CultureInfo.InvariantCulture) + "' ";
                xml += "Anulado ='" + obj.Anulado + "' />";


                foreach (dynamic dp in obj.DetalleMateriales)
                {
                    xmlMateriales += "<DetalleMateriales ";
                    xmlMateriales += "DNota = '" + dp.DNota + "' ";
                    xmlMateriales += "PosicionNota = '" + dp.Posicion + "' ";
                    xmlMateriales += "PosicionFactura = '" + dp.PosicionFactura + "' ";
                    xmlMateriales += "DetalleDocPago = '" + dp.DetalleDocPago + "' ";
                    xmlMateriales += "Material = '" + dp.Material + "' ";
                    xmlMateriales += "DescMaterial = '" + dp.DescMaterial + "' ";
                    xmlMateriales += "UnidadMedida = '" + dp.UnidadMedida + "' ";
                    xmlMateriales += "CantidadFactura = '" + dp.CantidadFactura + "' ";
                    xmlMateriales += "CantidadNota = '" + dp.CantidadNota + "' ";
                    xmlMateriales += "ValorUnitario = '" + dp.ValorUnitario.ToString(System.Globalization.CultureInfo.InvariantCulture) + "' ";
                    xmlMateriales += "PrecioUnitario = '" + dp.PrecioUnitario.ToString(System.Globalization.CultureInfo.InvariantCulture) + "' ";                    
                    xmlMateriales += "ValorMonto = '" + dp.ValorMonto.ToString(System.Globalization.CultureInfo.InvariantCulture) + "' ";
                    xmlMateriales += "PrecioMonto = '" + dp.PrecioMonto.ToString(System.Globalization.CultureInfo.InvariantCulture) + "' ";
                    xmlMateriales += "PesoNeto = '" + dp.PesoNeto.ToString(System.Globalization.CultureInfo.InvariantCulture) + "' ";
                    xmlMateriales += "Inafecto = '" + dp.Inafecto + "' ";
                    xmlMateriales += "Activo = '" + dp.Activo + "' />";
                }


                xml += "</root>";
                xmlMateriales += "</root>";

                int res = Instancia.SaveUpdateNotaFactura(JWT.Login, xml, xmlMateriales);
                string mensaje = "";
                bool success = false;
                if (res > 0)
                {
                    success = true;
                    mensaje = "Se registró correctamente.";
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
        [ActionName("SD_GetDatosNotaFactura")]
        public HttpResponseMessage SD_GetDatosNotaFactura(int NotaFactura)
        {
            try
            {
                DataTable dt = Instancia.getDatosNotaFactura(NotaFactura);
                var rpta = (from c in dt.AsEnumerable()
                            select new
                            {
                                Nota = Convert.ToInt64(c["Nota"]),
                                ClaseNota = Convert.ToInt64(c["ClaseNota"]),                                
                                SerieDoc = c["SerieDoc"].ToString(),
                                NroDoc = c["NroDoc"].ToString(),
                                RazonCliente = c["RazonCliente"].ToString(),
                                Ruc = c["Ruc"].ToString(),
                                DireccionCliente = c["DireccionCliente"].ToString(),
                                Canal = c["Canal"].ToString(),
                                Moneda = c["Moneda"].ToString(),
                                CuentaBancaria = c["CuentaBancaria"].ToString(),
                                TipoEnvio = c["TipoEnvio"].ToString(),
                                Responsable = c["Responsable"].ToString(),
                                codMoneda = c["codMoneda"].ToString(),
                                codCuentaBancaria = c["codCuentaBancaria"].ToString(),
                                Documento = c["Documento"].ToString(),
                                Observaciones = c["Observaciones"].ToString(),
                                FechaDoc = c["FechaDoc"] == null ? c["FechaDoc"].ToString() : Convert.ToDateTime(c["FechaDoc"]).ToString("yyyy-MM-dd HH:mm:ss"),
                                DescOrgVenta = c["DescOrgVenta"].ToString(),
                                SubTotal = Convert.ToDecimal(c["Subtotal"].ToString()),
                                IgvPorcentaje = Convert.ToDecimal(c["IgvPorcentaje"].ToString()),
                                IncluyeIgv = Convert.ToInt64(c["IncluyeIgv"]),
                                Total = Convert.ToDecimal(c["Total"].ToString()),
                                Igv = Convert.ToDecimal(c["Igv"].ToString()),

                                DetalleMateriales = (from e in dt.AsEnumerable()
                                                     where Convert.ToInt64(c["Nota"]).Equals(Convert.ToInt64(e["Nota"]))
                                                     select new
                                                     {
                                                         DNota = Convert.ToInt64(e["DNota"]),
                                                         PosicionNota = e["PosicionNota"].ToString(),
                                                         PosicionFactura = e["PosicionFactura"].ToString(),
                                                         DetalleDocPago = e["DetalleDocPago"].ToString(),
                                                         Material = e["Material"].ToString(),
                                                         CodMaterial = e["CodMaterial"].ToString(),
                                                         DescMaterial = e["DescMaterial"].ToString(),
                                                         UnidadMedida = Convert.ToInt64(e["UnidadMedida"]),
                                                         DescUnidadMedida = e["DescUnidadMedida"].ToString(),
                                                         CantidadFactura = Convert.ToDecimal(e["CantidadFactura"].ToString()),
                                                         CantidadNota = Convert.ToDecimal(e["CantidadFactura"].ToString()),
                                                         PrecioNota = Convert.ToDecimal(e["PrecioNota"].ToString()),
                                                         MontoNota = Convert.ToDecimal(e["MontoNota"].ToString()),
                                                         ValorUnitario = Convert.ToDecimal(e["ValorUnitario"].ToString()),
                                                         PrecioUnitario = Convert.ToDecimal(e["PrecioUnitario"].ToString()),
                                                         ValorMonto = Convert.ToDecimal(e["ValorMonto"].ToString()),
                                                         PrecioMonto = Convert.ToDecimal(e["PrecioMonto"].ToString()),
                                                         Inafecto = Convert.ToInt64(e["Inafecto"]),
                                                         PesoNetoMaterial = Convert.ToDecimal(e["PesoNetoMaterial"].ToString()),
                                                         PesoNeto = Convert.ToDecimal(e["PesoNeto"].ToString()),

                                                     }).GroupBy(e => new { e.DNota }).Select(e => e.First()),


                            }).GroupBy(c => new { c.Nota }).Select(c => c.First());

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
        [ActionName("SD_postUpdateAnularNotaFactura")]
        public HttpResponseMessage SD_postUpdateAnularNotaFactura(dynamic obj)
        {
            try
            {
                int Codigo = obj.Codigo;
                string Estado = obj.Estado;


                int res = Instancia.postUpdateAnularNotaFactura(Codigo, JWT.Login, Estado);
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

        

    }
}