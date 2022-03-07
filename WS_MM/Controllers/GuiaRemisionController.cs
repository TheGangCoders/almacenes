using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;

using static WS_MM.Models.GuiaRemisionModel;
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
    public class GuiaRemisionController : BaseController
    {
        [HttpGet]
        [ActionName("PMMM_GetSociedades")]
        public IHttpActionResult PMMM_GetSociedades(Boolean? prmbitActivo)
        {
            try
            {
                DataTable dt = Instancia.GetSociedades(JWT.IdEmpresa, prmbitActivo);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpGet]
        [ActionName("PMMM_GetGuiaRemision")]
        public IHttpActionResult PMMM_GetGuiaRemision(string prmbstrFechaInicio, string prmbstrFechaFin, int prmbintOrigen, int prmbintDestino, int prmintSociedad)
        {
            try
            {
                DataTable dt = Instancia.GetGuiaRemision(prmbstrFechaInicio, prmbstrFechaFin, prmbintOrigen, prmbintDestino, JWT.IdEmpresa, JWT.IdSociedad);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpPost]
        [ActionName("PMMM_SaveUpdateGuiaRemision")]
        public IHttpActionResult PMMM_SaveUpdateGuiaRemision(dynamic obj)
        {
            try
            {
                string xml = "<?xml version='1.0' encoding='ISO-8859-1'?><root>";
                string xmlDGuia = "<?xml version='1.0' encoding='ISO-8859-1'?><root>";


                xml += "<GuiaRemision ";
                xml += "GuiaRemision ='" + obj.GuiaRemision + "' ";
                xml += "OrdenCompra ='" + obj.OrdenCompra + "' ";
                xml += "Empresa ='" + JWT.IdEmpresa + "' ";
                xml += "Sociedad ='" + JWT.IdSociedad + "' ";
                xml += "Correlativo ='" + obj.Correlativo + "' ";
                xml += "FechaDocumento ='" + obj.FechaDocumento + "' ";
                xml += "Serie ='" + obj.Serie + "' ";
                xml += "Numero ='" + obj.Numero + "' ";
                xml += "Origen ='" + obj.Origen + "' ";
                xml += "Destino ='" + obj.Destino + "' ";
                xml += "Proveedor ='" + obj.Proveedor + "' ";
                xml += "TipoCargo ='" + obj.TipoCargo + "' ";
                xml += "TipoEnvio ='" + obj.TipoEnvio + "' ";
                xml += "ClaseTransporte ='" + obj.ClaseTransporte + "' ";
                xml += "Cliente ='" + obj.Cliente + "' ";
                xml += "DespachoEntrega ='" + obj.DespachoEntrega + "' ";
                xml += "Pedido ='" + obj.Pedido + "' ";
                xml += "Despacho ='" + obj.Despacho + "' ";
                xml += "FechaDespacho ='" + obj.FechaDespacho + "' ";
                xml += "FechaEtaDestino ='" + obj.FechaEtaDestino + "' ";
                xml += "FechaEtd ='" + obj.FechaEtd + "' ";
                xml += "FechaDD ='" + obj.FechaDD + "' ";
                xml += "FechaEtaOrigen ='" + obj.FechaEtaOrigen + "' ";


                if (obj.Transportista != null && obj.Transportista.ToString() != "")
                {
                    xml += "Transportista ='" + obj.Transportista + "' ";
                }

                if (obj.Vehiculo != null && obj.Vehiculo.ToString() != "")
                {
                    xml += "Vehiculo ='" + obj.Vehiculo + "' ";
                }

                if (obj.Conductor != null && obj.Conductor.ToString() != "")
                {
                    xml += "Conductor ='" + obj.Conductor + "' ";
                }

                if (obj.FechaSalida != null && obj.FechaSalida.ToString() != "" && obj.FechaSalida.ToString() != "1970-01-01")
                {
                    xml += "FechaSalida ='" + obj.FechaSalida + "' ";
                }

                if (obj.HoraSalida != null && obj.HoraSalida.ToString() != "")
                {
                    xml += "HoraSalida ='" + obj.HoraSalida + "' ";
                }

                if (obj.FechaLlegada != null && obj.FechaLlegada.ToString() != "" && obj.FechaLlegada.ToString() != "1970-01-01")
                {
                    xml += "FechaLlegada ='" + obj.FechaLlegada + "' ";
                }

                if (obj.HoraLlegada != null && obj.HoraLlegada.ToString() != "")
                {
                    xml += "HoraLlegada ='" + obj.HoraLlegada + "' ";
                }

                if (obj.FechaRecepcion != null && obj.FechaRecepcion.ToString() != "" && obj.FechaRecepcion.ToString() != "1970-01-01")
                {
                    xml += "FechaRecepcion ='" + obj.FechaRecepcion + "' ";
                }

                if (obj.HoraRecepcion != null && obj.HoraRecepcion.ToString() != "")
                {
                    xml += "HoraRecepcion ='" + obj.HoraRecepcion + "' ";
                }


                xml += "IncIgv ='" + obj.IncIgv.ToString(System.Globalization.CultureInfo.InvariantCulture) + "' ";
                xml += "TipoMoneda ='" + obj.TipoMoneda + "' ";
                xml += "TasaCambio ='" + obj.TasaCambio.ToString(System.Globalization.CultureInfo.InvariantCulture) + "' ";
                xml += "SubTotal ='" + obj.SubTotal.ToString(System.Globalization.CultureInfo.InvariantCulture) + "' ";
                xml += "Descuento ='" + obj.Descuento.ToString(System.Globalization.CultureInfo.InvariantCulture) + "' ";
                xml += "IgvPorcentaje ='" + obj.IgvPorcentaje.ToString(System.Globalization.CultureInfo.InvariantCulture) + "' ";
                xml += "MontoIgv ='" + obj.MontoIgv.ToString(System.Globalization.CultureInfo.InvariantCulture) + "' ";
                xml += "Total ='" + obj.Total.ToString(System.Globalization.CultureInfo.InvariantCulture) + "' ";
                xml += "Anulado ='" + obj.Anulado + "' ";
                xml += "Ruta ='" + obj.Ruta + "' ";
                xml += "TipoIngreso ='" + obj.TipoIngreso + "' ";

                if (obj.FechaInicioDescarga != null && obj.FechaInicioDescarga.ToString() != "" && obj.FechaInicioDescarga.ToString() != "1970-01-01")
                {
                    xml += "FechaInicioDescarga ='" + obj.FechaInicioDescarga + "' ";
                }

                if (obj.HoraInicioDescarga != null && obj.HoraInicioDescarga.ToString() != "")
                {
                    xml += "HoraInicioDescarga ='" + obj.HoraInicioDescarga + "' ";
                }

                if (obj.FechaFinDescarga != null && obj.FechaFinDescarga.ToString() != "" && obj.FechaFinDescarga.ToString() != "1970-01-01")
                {
                    xml += "FechaFinDescarga ='" + obj.FechaFinDescarga + "' ";
                }

                if (obj.HoraFinDescarga != null && obj.HoraFinDescarga.ToString() != "")
                {
                    xml += "HoraFinDescarga ='" + obj.HoraFinDescarga + "' ";
                }

                xml += "FacturacionPorCapacidad ='" + obj.FacturacionPorCapacidad + "' ";
                xml += "GastoTransporteEjecutado ='" + obj.GastoTransporteEjecutado + "' ";
                xml += "SerieInterna ='" + obj.SerieInterna + "' ";
                xml += "NumeroInterno ='" + obj.NumeroInterno + "' ";
                xml += "GastoTransporteProyectado ='" + obj.GastoTransporteProyectado + "' ";
                xml += "/>";

                foreach (dynamic dm in obj.DGuia)
                {
                    xmlDGuia += "<DGuiaRemision ";
                    xmlDGuia += "DGuiaRemision = '" + dm.DGuiaRemision + "' ";
                    xmlDGuia += "DOrdenCompra = '" + dm.DOrdenCompra + "' ";
                    xmlDGuia += "GuiaRemision = '" + dm.GuiaRemision + "' ";
                    xmlDGuia += "Empresa = '" + JWT.IdEmpresa + "' ";
                    xmlDGuia += "Material = '" + dm.Material + "' ";


                    if (dm.UnidadAgricola != null && dm.UnidadAgricola.ToString() != "" && dm.UnidadAgricola.ToString() != "0")
                    {
                        xmlDGuia += "UnidadAgricola ='" + dm.UnidadAgricola + "' ";
                    }

                    if (dm.Modulo != null && dm.Modulo.ToString() != "" && dm.Modulo.ToString() != "0")
                    {
                        xmlDGuia += "Modulo ='" + dm.Modulo + "' ";
                    }

                    if (dm.Campo != null && dm.Campo.ToString() != "" && dm.Campo.ToString() != "0")
                    {
                        xmlDGuia += "Campo ='" + dm.Campo + "' ";
                    }

                    xmlDGuia += "Cantidad = '" + dm.Cantidad.ToString(System.Globalization.CultureInfo.InvariantCulture) + "' ";
                    xmlDGuia += "ValorUnitario = '" + dm.ValorUnitario.ToString(System.Globalization.CultureInfo.InvariantCulture) + "' ";
                    xmlDGuia += "PrecioUnitario = '" + dm.PrecioUnitario.ToString(System.Globalization.CultureInfo.InvariantCulture) + "' ";
                    xmlDGuia += "DescuentoValor = '" + dm.DescuentoValor.ToString(System.Globalization.CultureInfo.InvariantCulture) + "' ";
                    xmlDGuia += "DescuentoPrecio = '" + dm.DescuentoPrecio.ToString(System.Globalization.CultureInfo.InvariantCulture) + "' ";
                    xmlDGuia += "ValorTotal = '" + dm.ValorTotal.ToString(System.Globalization.CultureInfo.InvariantCulture) + "' ";
                    xmlDGuia += "PrecioTotal = '" + dm.PrecioTotal.ToString(System.Globalization.CultureInfo.InvariantCulture) + "' ";
                    xmlDGuia += "UnidadMedida = '" + dm.UnidadMedida + "' ";
                    xmlDGuia += "LoteMat ='" + dm.LoteMat + "' ";
                    xmlDGuia += "Almacen ='" + dm.Almacen + "' ";

                    if (dm.FechaCosecha != null && dm.FechaCosecha.ToString() != "")
                    {
                        xmlDGuia += "FechaCosecha ='" + dm.FechaCosecha + "' ";
                    }

                    //xmlDGuia += "FechaCosecha = '" + dm.FechaCosecha + "' ";
                    xmlDGuia += "Anulado = '" + dm.Anulado + "' ";

                    if (dm.Lote != null && dm.Lote.ToString() != "" && dm.Lote.ToString() != "0")
                    {
                        xmlDGuia += "Lote ='" + dm.Lote + "' ";
                    }

                    xmlDGuia += "TipoEnvase ='" + dm.TipoEnvase + "' ";
                    xmlDGuia += "CantidadEnvases ='" + dm.CantidadEnvases + "' ";

                    xmlDGuia += "/>";
                }

                xml += "</root>";
                xmlDGuia += "</root>";

                int res = Instancia.SaveUpdateGuiaRemision(JWT.Login, xml, xmlDGuia);
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
                    mensaje = "La transacción ya se encuentra registrada.";
                }

                if (res == -2)
                {
                    success = true;
                    mensaje = "Existe una guia registrada con la serie y número ingresado.";
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

                //var httpResponseMessage = Request.CreateResponse(HttpStatusCode.OK, respuesta);
                //httpResponseMessage.Headers.Add("Access-Control-Allow-Origin", "*");

                //return httpResponseMessage;

                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                //return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
                //var error = MetodoComun.ObtenerExceptionModel(ex);

                //var httpResponseMessage = Request.CreateResponse(HttpStatusCode.BadRequest, error);
                //httpResponseMessage.Headers.Add("Access-Control-Allow-Origin", "*");

                //return httpResponseMessage;
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [ActionName("PMMM_GetCabeceraDetalleGuia")]
        public IHttpActionResult PMMM_GetCabeceraDetalleGuia(int prmintGuia)
        {
            try
            {
                DataTable dt = Instancia.GetCabeceraDetalleGuia(prmintGuia);

                var obj = (from c in dt.AsEnumerable()
                           select new
                           {
                               GuiaRemision = Convert.ToInt64(c["GuiaRemision"]),
                               OrdenCompra = Convert.ToInt64(c["OrdenCompra"]),
                               CodOrdenCompra = c["CodOrdenCompra"].ToString(),
                               Empresa = c["Empresa"].ToString(),
                               Sociedad = c["Sociedad"].ToString(),
                               Correlativo = c["Correlativo"].ToString(),
                               Serie = c["Serie"].ToString(),
                               Numero = c["Numero"].ToString(),
                               FechaDocumento = c["FechaDocumento"].ToString(),
                               Origen = Convert.ToInt64(c["Origen"]),
                               Destino = Convert.ToInt64(c["Destino"]),
                               Proveedor = Convert.ToInt64(c["Proveedor"]),
                               RazonProveedor = c["RazonProveedor"].ToString(),
                               Direccion = c["Direccion"].ToString(),
                               RUC = c["RUC"].ToString(),


                               Transportista = c["Transportista"].ToString() == "" ? c["Transportista"] : Convert.ToInt64(c["Transportista"]),
                               Vehiculo = c["Vehiculo"].ToString() == "" ? c["Vehiculo"] : Convert.ToInt64(c["Vehiculo"]),
                               Conductor = c["Conductor"].ToString() == "" ? c["Conductor"] : Convert.ToInt64(c["Conductor"]),


                               // Transportista = Convert.ToInt64(c["Transportista"]),
                               //Vehiculo = Convert.ToInt64(c["Vehiculo"]),
                               //Conductor = Convert.ToInt64(c["Conductor"]),
                               FechaSalida = c["FechaSalida"].ToString(),
                               HoraSalida = c["HoraSalida"].ToString(),
                               FechaLlegada = c["FechaLlegada"].ToString(),
                               HoraLlegada = c["HoraLlegada"].ToString(),
                               FechaRecepcion = c["FechaRecepcion"].ToString(),
                               HoraRecepcion = c["HoraRecepcion"].ToString(),
                               TipoMoneda = c["TipoMoneda"].ToString(),
                               TasaCambio = Convert.ToDecimal(c["TasaCambio"].ToString()),
                               ValorIGV = Convert.ToDecimal(c["IgvPorcentaje"].ToString()),
                               IncIGV = Convert.ToBoolean(c["IncluyeIgv"].ToString()),
                               SubTotal = Convert.ToDecimal(c["SubTotal"].ToString()),
                               Descuento = Convert.ToDecimal(c["Descuento"].ToString()),
                               MontoIgv = Convert.ToDecimal(c["Igv"].ToString()),
                               Total = Convert.ToDecimal(c["Total"].ToString()),

                               Ruta = Convert.ToInt64(c["Ruta"]),
                               DescRuta = c["DescRuta"].ToString(),
                               TipoIngreso = c["TipoIngreso"].ToString(),
                               DescTipoIngreso = c["DescTipoIngreso"].ToString(),
                               FechaInicioDescarga = c["FechaInicioDescarga"].ToString(),
                               HoraInicioDescarga = c["HoraInicioDescarga"].ToString(),
                               FechaFinDescarga = c["FechaFinDescarga"].ToString(),
                               HoraFinDescarga = c["HoraFinDescarga"].ToString(),
                               Capacidad = Convert.ToInt32(c["Capacidad"]),
                               Costo = Convert.ToDecimal(c["Costo"]),
                               FacturacionPorCapacidad = Convert.ToInt32(c["FacturacionPorCapacidad"]),
                               GastoTransporteEjecutado = Convert.ToDecimal(c["GastoTransporteEjecutado"]),
                               SerieInterna = c["SerieInterna"].ToString(),
                               NumeroInterno = c["NumeroInterno"].ToString(),
                               GastoTransporteProyectado = Convert.ToDecimal(c["GastoTransporteProyectado"]),
                               ClaseTransporte = c["ClaseTransporte"].ToString(),
                               DespachoCode = c["DespachoCode"].ToString(),
                               Cliente = Convert.ToInt64(c["Cliente"]),
                               CodCliente = c["CodCliente"].ToString(),
                               RazonSocial = c["RazonSocial"].ToString(),
                               NroDocumento = c["NroDocumento"].ToString(),
                               FechaDespacho = c["FechaDespacho"].ToString() == "" ? null : Convert.ToDateTime(c["FechaDespacho"]).ToString("yyyy-MM-dd HH:mm:ss"),
                               FechaEtaDestino = c["FechaEtaDestino"].ToString() == "" ? null : Convert.ToDateTime(c["FechaEtaDestino"]).ToString("yyyy-MM-dd HH:mm:ss"),
                               FechaEtd = c["FechaEtd"].ToString() == "" ? null : Convert.ToDateTime(c["FechaEtd"]).ToString("yyyy-MM-dd HH:mm:ss"),
                               FechaDD = c["FechaDD"].ToString() == "" ? null : Convert.ToDateTime(c["FechaDD"]).ToString("yyyy-MM-dd HH:mm:ss"),
                               FechaEtaOrigen = c["FechaEtaOrigen"].ToString() == "" ? null : Convert.ToDateTime(c["FechaEtaOrigen"]).ToString("yyyy-MM-dd HH:mm:ss"),
                               TipoEnvio = c["TipoEnvio"].ToString(),
                               TipoCarga = c["TipoCarga"].ToString(),
                               DTipoCargo = c["DTipoCargo"].ToString(),
                               DTipoEnvio = c["DTipoEnvio"].ToString(),
                               DespachoEntrega = Convert.ToInt64(c["DespachoEntrega"]),
                               PedidoVenta = Convert.ToInt64(c["PedidoVenta"]),

                               Detalles = (from d in dt.AsEnumerable()
                                           where Convert.ToInt64(c["GuiaRemision"]).Equals(Convert.ToInt64(d["GuiaRemision"]))
                                           select new
                                           {
                                               DGuiaRemision = Convert.ToInt64(d["DGuiaRemision"]),
                                               DOrdenCompra = Convert.ToInt64(d["DOrdenCompra"]),
                                               Material = Convert.ToInt64(d["Material"]),
                                               CodDetaMaterial = d["CodDetaMaterial"].ToString(),
                                               DescMaterial = d["DescMaterial"].ToString(),
                                               Cantidad = Convert.ToDecimal(d["Cantidad"].ToString()),
                                               ValorUnitario = Convert.ToDecimal(d["ValorUnitario"].ToString()),
                                               PrecioUnitario = Convert.ToDecimal(d["PrecioUnitario"].ToString()),
                                               DescuentoValor = Convert.ToDecimal(d["DescuentoValor"].ToString()),
                                               DescuentoPrecio = Convert.ToDecimal(d["DescuentoPrecio"].ToString()),
                                               ValorTotal = Convert.ToDecimal(d["ValorTotal"].ToString()),
                                               PrecioTotal = Convert.ToDecimal(d["PrecioTotal"].ToString()),

                                               UnidadAgricola = d["UnidadAgricola"].ToString() == "" ? d["UnidadAgricola"] : Convert.ToInt64(d["UnidadAgricola"]),
                                               Modulo = d["Modulo"].ToString() == "" ? d["Modulo"] : Convert.ToInt64(d["Modulo"]),
                                               Turno = d["Campo"].ToString() == "" ? d["Campo"] : Convert.ToInt64(d["Campo"]),

                                               // UnidadAgricola = Convert.ToInt64(d["UnidadAgricola"].ToString()),
                                               DescUnidadAgricola = d["DescUnidadAgricola"].ToString(),
                                               // Modulo = Convert.ToInt64(d["Modulo"].ToString()),
                                               DescModulo = d["DescModulo"].ToString(),
                                               // Turno = Convert.ToInt64(d["Campo"].ToString()),
                                               DescTurno = d["DesCampo"].ToString(),
                                               UnidadMedida = Convert.ToInt64(d["UnidadMedida"].ToString()),
                                               DescUnidadMedida = d["DescUnidadMedida"].ToString(),
                                               DetalleFechaCosecha = d["FechaCosecha"].ToString(),
                                               RestringirCantidad = Convert.ToBoolean(d["RestringirCantidad"].ToString()),
                                               RecibidoGuia = Convert.ToDecimal(d["RecibidoGuia"].ToString()),
                                               Activo = Convert.ToInt64(d["Activo"]),

                                               Lote = d["Lote"].ToString() == "" ? d["Lote"] : Convert.ToInt64(d["Lote"]),
                                               DescLote = d["DescLote"].ToString(),
                                               TipoEnvase = d["TipoEnvase"].ToString(),
                                               DescTipoEnvase = d["DescTipoEnvase"].ToString(),
                                               CantidadEnvases = Convert.ToInt32(d["CantidadEnvases"]),
                                               PosicionOrdenCompra = d["PosicionOrdenCompra"].ToString(),
                                               LoteMat = d["LoteMat"].ToString(),
                                               Almacen = Convert.ToInt64(d["Almacen"].ToString()),
                                               DesAlmacen = d["DesAlmacen"].ToString(),

                                           }).GroupBy(d => new { d.DGuiaRemision }).Select(d => d.First()),
                           }).GroupBy(c => new { c.GuiaRemision }).Select(c => c.First());

                //var httpResponseMessage = Request.CreateResponse<IEnumerable<Object>>(HttpStatusCode.OK, obj);
                //httpResponseMessage.Headers.Add("Access-Control-Allow-Origin", "*");

                //return httpResponseMessage;

                return Ok(obj);
            }
            catch (Exception ex)
            {
                //return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
                //var error = MetodoComun.ObtenerExceptionModel(ex);

                //var httpResponseMessage = Request.CreateResponse(HttpStatusCode.BadRequest, error);
                //httpResponseMessage.Headers.Add("Access-Control-Allow-Origin", "*");

                //return httpResponseMessage;

                return BadRequest(ex.Message);
            }
        }


        [HttpPost]
        [ActionName("PMMM_EnableDisable_GuiaRemision")]
        public HttpResponseMessage PMMM_EnableDisable_GuiaRemision(dynamic obj)
        {
            try
            {
                int GuiaRemision = obj.GuiaRemision;
                Boolean Activo = obj.Estado;
                string Usuario = obj.Usuario;


                int res = Instancia.EnableDisable_GuiaRemision(GuiaRemision, Activo, Usuario);
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
                    mensaje = "La transacción ya se encuentra registrada.";
                }

                if (res == -2)
                {
                    success = true;
                    mensaje = "Existe una guia registrada con la serie y número ingresado.";
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
        [ActionName("PMMM_GetOrdenCompraGuia")]
        public HttpResponseMessage PMMM_GetOrdenCompraGuia(string FechaInicio, string FechaFin, int Proveedor)
        {
            try
            {
                DataTable dt = Instancia.GetOrdenCompraGuia(FechaInicio, FechaFin, Proveedor);

                var obj = (from c in dt.AsEnumerable()
                           select new
                           {
                               OrdenCompra = Convert.ToInt64(c["OrdenCompra"]),
                               CodOrdenCompra = c["CodOrdenCompra"].ToString(),
                               TipoDocumento = c["TipoDocumento"].ToString(),
                               FormaPago = c["FormaPago"].ToString(),
                               FechaEmision = c["FechaEmision"].ToString(),
                               TipoMoneda = c["TipoMoneda"].ToString(),
                               DescMoneda = c["DescMoneda"].ToString(),
                               ValorIGV = Convert.ToDecimal(c["ValorIGV"].ToString()),
                               IncIGV = Convert.ToBoolean(c["IncIGV"].ToString()),
                               TasaCambio = Convert.ToDecimal(c["TasaCambio"].ToString()),
                               SubTotal = Convert.ToDecimal(c["SubTotal"].ToString()),
                               Descuento = Convert.ToDecimal(c["Descuento"].ToString()),
                               MontoIgv = Convert.ToDecimal(c["MontoIgv"].ToString()),
                               Total = Convert.ToDecimal(c["Total"].ToString()),
                               Detalles = (from d in dt.AsEnumerable()
                                           where Convert.ToInt64(c["OrdenCompra"]).Equals(Convert.ToInt64(d["OrdenCompra"]))
                                           select new
                                           {
                                               DOrdenCompra = Convert.ToInt64(d["DOrdenCompra"]),
                                               DGuiaRemision = Convert.ToInt64(d["DGuiaRemision"]),
                                               Material = Convert.ToInt64(d["Material"]),
                                               CodDetaMaterial = Convert.ToInt64(d["CodDetaMaterial"]),
                                               DescMaterial = d["DescMaterial"].ToString(),
                                               CantidadOrden = Convert.ToDecimal(d["Cantidad"].ToString()),
                                               Cantidad = Convert.ToDecimal(d["Cantidad"].ToString()),                                               
                                               ValorUnitario = Convert.ToDecimal(d["ValorUnitario"].ToString()),
                                               PrecioUnitario = Convert.ToDecimal(d["PrecioUnitario"].ToString()),
                                               DescuentoValor = Convert.ToDecimal(d["DescuentoValor"].ToString()),
                                               DescuentoPrecio = Convert.ToDecimal(d["DescuentoPrecio"].ToString()),
                                               ValorTotal = Convert.ToDecimal(d["ValorTotal"].ToString()),
                                               PrecioTotal = Convert.ToDecimal(d["PrecioTotal"].ToString()),
                                               UnidadMedida = Convert.ToInt64(d["UnidadMedida"].ToString()),
                                               DescUnidadMedida = d["DescUnidadMedida"].ToString(),
                                               CantidadEnGuia = Convert.ToDecimal(d["CantidadEnGuia"].ToString()),
                                               RestringirCantidad = Convert.ToBoolean(d["RestringirCantidad"].ToString()),
                                               RecibidoGuia = Convert.ToDecimal(d["RecibidoGuia"].ToString()),

                                               DetalleFechaCosecha = d["DetalleFechaCosecha"].ToString(),
                                               UnidadAgricola = Convert.ToDecimal(d["UnidadAgricola"].ToString()),
                                               Modulo = Convert.ToDecimal(d["Modulo"].ToString()),
                                               Turno = Convert.ToDecimal(d["Turno"].ToString()),
                                               Posicion = d["Posicion"].ToString(),


                                           }).GroupBy(d => new { d.DOrdenCompra }).Select(d => d.First()),
                           }).GroupBy(c => new { c.OrdenCompra }).Select(c => c.First());

                var httpResponseMessage = Request.CreateResponse<IEnumerable<Object>>(HttpStatusCode.OK, obj);
                httpResponseMessage.Headers.Add("Access-Control-Allow-Origin", "*");

                return httpResponseMessage;
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpGet]
        [ActionName("PMMM_ObtenerDespacho")]
        public IHttpActionResult PMMM_ObtenerDespacho()
        {
            try
            {
                DataTable dt = Instancia.ObtenerDespacho();

                var obj = (from c in dt.AsEnumerable()
                           select new
                           {
                               DespachoEntrega = Convert.ToInt64(c["DespachoEntrega"]),
                               PedidoVenta = Convert.ToInt64(c["PedidoVenta"]),
                               DespachoCode = c["DespachoCode"].ToString(),
                               Cliente = Convert.ToInt64(c["Cliente"]),
                               CodCliente = c["CodCliente"].ToString(),
                               RazonSocial = c["RazonSocial"].ToString(),
                               NroDocumento = c["NroDocumento"].ToString(),
                               FechaDespacho = c["FechaDespacho"].ToString() == "" ? null : Convert.ToDateTime(c["FechaDespacho"]).ToString("yyyy-MM-dd HH:mm:ss"),
                               FechaEtaDestino = c["FechaEtaDestino"].ToString() == "" ? null : Convert.ToDateTime(c["FechaEtaDestino"]).ToString("yyyy-MM-dd HH:mm:ss"),
                               FechaEtd = c["FechaEtd"].ToString() == "" ? null : Convert.ToDateTime(c["FechaEtd"]).ToString("yyyy-MM-dd HH:mm:ss"),
                               FechaDD = c["FechaDD"].ToString() == "" ? null : Convert.ToDateTime(c["FechaDD"]).ToString("yyyy-MM-dd HH:mm:ss"),
                               FechaEtaOrigen = c["FechaEtaOrigen"].ToString() == "" ? null : Convert.ToDateTime(c["FechaEtaOrigen"]).ToString("yyyy-MM-dd HH:mm:ss"),
                               TipoEnvio = c["TipoEnvio"].ToString(),
                               TipoCarga = c["TipoCarga"].ToString(),
                               DTipoCargo = c["DTipoCargo"].ToString(),
                               DTipoEnvio = c["DTipoEnvio"].ToString(),
                               Detalles = (from d in dt.AsEnumerable()
                                           where Convert.ToInt64(c["DespachoEntrega"]).Equals(Convert.ToInt64(d["DespachoEntrega"]))
                                           select new
                                           {
                                               DDespachoEntrega = Convert.ToInt64(d["DDespachoEntrega"]),
                                               DGuiaRemision = Convert.ToInt64(d["DGuiaRemision"]),
                                               Material = Convert.ToInt64(d["Material"]),
                                               Codigo = Convert.ToInt64(d["Codigo"]),
                                               Descripcion = d["Descripcion"].ToString(),
                                               Cantidad = Convert.ToDecimal(d["Cantidad"].ToString()),
                                               LoteMat = d["Lote"].ToString(),
                                               Almacen = Convert.ToInt64(d["Almacen"].ToString()),
                                               DesAlmacen = d["DesAlmacen"].ToString(),
                                               UnidadMaterial = Convert.ToInt64(d["UnidadMaterial"]),
                                               Abreviatura = d["Abreviatura"].ToString(),
                                               DespachoCode = d["DespachoCode"].ToString(),
                                               Pos = Convert.ToInt64(d["Pos"]),
                                               DPosOC = Convert.ToInt64(d["DPosOC"]),
                                               ValorUnitario = Convert.ToDecimal(d["ValorUnitario"].ToString()),
                                               PrecioUnitario = Convert.ToDecimal(d["PrecioUnitario"].ToString()),
                                               DescuentoValor = Convert.ToDecimal(d["DescuentoValor"].ToString()),
                                               DescuentoPrecio = Convert.ToDecimal(d["DescuentoPrecio"].ToString()),

                                           }).GroupBy(d => new { d.DDespachoEntrega }).Select(d => d.First()),
                           }).GroupBy(c => new { c.DespachoEntrega }).Select(c => c.First());

                //var httpResponseMessage = Request.CreateResponse<IEnumerable<Object>>(HttpStatusCode.OK, obj);
                //httpResponseMessage.Headers.Add("Access-Control-Allow-Origin", "*");

                //return httpResponseMessage;
                return Ok(obj);
            }
            catch (Exception ex)
            {
                //return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
                //var error = MetodoComun.ObtenerExceptionModel(ex);

                //var httpResponseMessage = Request.CreateResponse(HttpStatusCode.BadRequest, error);
                //httpResponseMessage.Headers.Add("Access-Control-Allow-Origin", "*");

                //return httpResponseMessage;
                return BadRequest(ex.Message);
            }
        }


        [HttpGet]
        [ActionName("PMMM_ObtenerClaseTranspote")]
        public IHttpActionResult PMMM_ObtenerClaseTranspote()
        {
            try
            {
                DataTable dt = Instancia.ObtenerClaseTranspote();

                return Ok(dt);
            }
            catch (Exception ex)
            {
                //return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
                return BadRequest(ex.Message);
            }
        }



    }
}
