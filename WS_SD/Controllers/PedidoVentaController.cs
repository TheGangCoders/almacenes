using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using static WS_SD.Models.PedidoVentaModel;
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
    public class PedidoVentaController : BaseController
    {
        [HttpGet]
        [ActionName("PP_getClaseVenta")]
        public IHttpActionResult PP_getClaseVenta(int? Activo, string Organizacion)
        {
            try
            {
                DataTable dt = Instancia.getClaseVenta(JWT.IdEmpresa, Activo, Organizacion);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpGet]
        [ActionName("PP_getOrganzacionVenta")]
        public IHttpActionResult PP_getOrganzacionVenta(int? Activo)
        {
            try
            {
                DataTable dt = Instancia.getOrganzacionVenta(JWT.IdEmpresa, JWT.IdSociedad, Activo);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }
        
        [HttpGet]
        [ActionName("PP_getListadoPedidosVenta")]
        public IHttpActionResult PP_getListadoPedidosVenta(int Clase, int Organizacion, string Cliente, string CodigoCliente, string FechaInicio, string FechaFin)
        {
            try
            {
                DataTable dt = Instancia.getListadoPedidosVenta(JWT.IdEmpresa, Clase, Organizacion, Cliente, CodigoCliente, FechaInicio, FechaFin);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpGet]
        [ActionName("PP_getListadoPedidosVentaMigo")]
        public IHttpActionResult PP_getListadoPedidosVentaMigo(int Clase, int Organizacion, string Cliente, string CodigoCliente, string FechaInicio, string FechaFin)
        {
            try
            {
                DataTable dt = Instancia.getListadoPedidosVentaMigo(JWT.IdEmpresa, Clase, Organizacion, Cliente, CodigoCliente, FechaInicio, FechaFin, JWT.IdSociedad);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }        

        [HttpGet]
        [ActionName("PP_getSectorVentas")]
        public IHttpActionResult PP_getSectorVentas(int? Activo)
        {
            try
            {
                DataTable dt = Instancia.getSectorVenta(JWT.IdEmpresa, Activo);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpGet]
        [ActionName("PP_getPaises")]
        public IHttpActionResult PP_getPaises(int? Activo)
        {
            try
            {
                DataTable dt = Instancia.getPaises(Activo);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }
        
        [HttpGet]
        [ActionName("PP_getSearchClientes")]
        public IHttpActionResult PP_getSearchClientes(string Cliente, string CodCliente, int? Pais, int? Organizacion, int? Sector, int Tipo)
        {
            try
            {
                DataTable dt = Instancia.getSearchClientes(JWT.IdEmpresa, Cliente, CodCliente, Pais, Organizacion, Sector, Tipo);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpGet]
        [ActionName("PP_getTipoMoneda")]
        public IHttpActionResult PP_getTipoMoneda()
        {
            try
            {
                DataTable dt = Instancia.getTipoMoneda(JWT.IdEmpresa);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpGet]
        [ActionName("PP_getCondicionPago")]
        public IHttpActionResult PP_getCondicionPago()
        {
            try
            {
                DataTable dt = Instancia.getCondicionPago(JWT.IdEmpresa);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpGet]
        [ActionName("PP_getVaporPedidoVenta")]
        public IHttpActionResult PP_getVaporPedidoVenta(int? Activo)
        {
            try
            {
                DataTable dt = Instancia.getVaporPedidoVenta(JWT.IdEmpresa, Activo);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpGet]
        [ActionName("PP_getTipoEnvio")]
        public IHttpActionResult PP_getTipoEnvio()
        {
            try
            {
                DataTable dt = Instancia.getTipoEnvio(JWT.IdEmpresa);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }
      
        [HttpGet]
        [ActionName("PP_getSearchProveedor")]
        public IHttpActionResult PP_getSearchProveedor(string CondicionBusqueda, string Documento, string RazonSocial, int? Pais)
        {
            try
            {
                DataTable dt = Instancia.getSearchProveedor(JWT.IdEmpresa, CondicionBusqueda, Documento, RazonSocial, Pais);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }
        
        [HttpGet]
        [ActionName("PP_getTipoFecha")]
        public IHttpActionResult PP_getTipoFecha()
        {
            try
            {
                DataTable dt = Instancia.getTipoFecha(JWT.IdEmpresa);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }
        
        [HttpGet]
        [ActionName("PP_getSearchMateriales")]
        public IHttpActionResult PP_getSearchMateriales(string CodMaterial, string Material, string Organizacion, string TipoMaterial, string GrupoArticulo)
        {
            try
            {
                DataTable dt = Instancia.getSearchMateriales(JWT.IdEmpresa, CodMaterial, Material, Organizacion, TipoMaterial, GrupoArticulo);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpPost]
        [ActionName("PSDM_SaveUpdatePedidoVenta")]
        public HttpResponseMessage PSDM_SaveUpdatePedidoVenta(dynamic obj)
        {
            try
            {
                string xml = "<?xml version='1.0' encoding='ISO-8859-1'?><root>";
                string xmlDPedido = "<?xml version='1.0' encoding='ISO-8859-1'?><root>";
                string xmlPagos = "<?xml version='1.0' encoding='ISO-8859-1'?><root>";


                xml += "<PedidoVenta ";
                xml += "PedidoVenta ='" + obj.PedidoVenta + "' ";
                xml += "Empresa ='" + JWT.IdEmpresa + "' ";
                xml += "Sociedad ='" + JWT.IdSociedad + "' ";
                xml += "Cliente ='" + obj.Cliente + "' ";
                xml += "Sector ='" + obj.Sector + "' ";
                xml += "OrgVentas ='" + obj.OrgVentas + "' ";
                xml += "ClaseDocVta ='" + obj.ClaseDocVta + "' ";
                xml += "TipoMoneda ='" + obj.TipoMoneda + "' ";
                xml += "ValorMoneda ='" + obj.ValorMoneda + "' ";
                xml += "IncluyeIgv ='" + obj.IncluyeIgv + "' ";
                xml += "IgvPorcentaje ='" + obj.IgvPorcentaje.ToString(System.Globalization.CultureInfo.InvariantCulture) + "' ";
                xml += "Igv ='" + obj.Igv.ToString(System.Globalization.CultureInfo.InvariantCulture) + "' ";
                xml += "SubTotal ='" + obj.SubTotal.ToString(System.Globalization.CultureInfo.InvariantCulture) + "' ";
                xml += "Descuento ='" + obj.Descuento.ToString(System.Globalization.CultureInfo.InvariantCulture) + "' ";
                xml += "Total ='" + obj.Total.ToString(System.Globalization.CultureInfo.InvariantCulture) + "' ";
                xml += "MetodoProduccion ='" + obj.MetodoProduccion + "' ";
                xml += "Canal ='" + obj.Canal + "' ";                

                xml += "NombreMetodoProduccion ='" + obj.NombreMetodoProduccion + "' ";

                if (obj.FechaEmision != null)
                {
                    xml += "FechaEmision ='" + obj.FechaEmision + "' ";
                }


                if (obj.FechaEntrega != null)
                {
                    xml += "FechaEntrega ='" + obj.FechaEntrega + "' ";
                }


                if (obj.FechaDespacho != null)
                {
                    xml += "FechaDespacho ='" + obj.FechaDespacho + "' ";
                }


                if (obj.FechaEtaOrigen != null)
                {
                    xml += "FechaEtaOrigen ='" + obj.FechaEtaOrigen + "' ";
                }
            
                if (obj.FechaEtd != null)
                {
                    xml += "FechaEtd ='" + obj.FechaEtd + "' ";
                }

                if (obj.FechaDD != null)
                {
                    xml += "FechaDD ='" + obj.FechaDD + "' ";
                }

                if (obj.FechaEtaDestino != null)
                {
                    xml += "FechaEtaDestino ='" + obj.FechaEtaDestino + "' ";
                }


                if (obj.TipoEnvio != null)
                {
                    xml += "TipoEnvio ='" + obj.TipoEnvio + "' ";
                }


                if (obj.ValorTipoEnvio != null)
                {
                    xml += "ValorTipoEnvio ='" + obj.ValorTipoEnvio + "' ";
                }

                if (obj.NroBooking != null)
                {
                    xml += "NroBooking ='" + obj.NroBooking + "' ";
                }

                if (obj.Precinto != null)
                {
                    xml += "Precinto ='" + obj.Precinto + "' ";
                }

                if (obj.NroBL != null)
                {
                    xml += "NroBL ='" + obj.NroBL + "' ";
                }

                if (obj.DuaDAM != null)
                {
                    xml += "DuaDAM ='" + obj.DuaDAM + "' ";
                }

                if (obj.FechaDAM != null)
                {
                    xml += "FechaDAM ='" + obj.FechaDAM + "' ";
                }

                if (obj.TipoCarga != null)
                {
                    xml += "TipoCarga ='" + obj.TipoCarga + "' ";
                }

                if (obj.TipoEmbalaje != null)
                {
                    xml += "TipoEmbalaje ='" + obj.TipoEmbalaje + "' ";
                }
                
                if (obj.DestinoMercancia > 0)
                {
                    xml += "DestinoMercancia ='" + obj.DestinoMercancia + "' ";
                }

                if (obj.TerminalAlmacenamiento > 0)
                {
                    xml += "TerminalAlmacenamiento ='" + obj.TerminalAlmacenamiento + "' ";
                }

                if (obj.AgenteAduana > 0)
                {
                    xml += "AgenteAduana ='" + obj.AgenteAduana + "' ";
                }

                if (obj.AgenteCarga > 0)
                {
                    xml += "AgenteCarga ='" + obj.AgenteCarga + "' ";
                }

                if (obj.OperadorLogistico > 0)
                {
                    xml += "OperadorLogistico ='" + obj.OperadorLogistico + "' ";
                }                

                if (obj.Transportista > 0)
                {
                    xml += "Transportista ='" + obj.Transportista + "' ";
                }


                if (obj.PuertoCarga > 0)
                {
                    xml += "PuertoCarga ='" + obj.PuertoCarga + "' ";
                }

                if (obj.Vapor > 0)
                {
                    xml += "Vapor ='" + obj.Vapor + "' ";
                }

                if (obj.PorComision > 0)
                {
                    xml += "PorComision ='" + obj.PorComision + "' ";
                }

                if (obj.PorComision == 0)
                {
                    xml += "PorComision ='0' ";
                }

                if (obj.MontoComision > 0)
                {
                    xml += "MontoComision ='" + obj.MontoComision + "' ";
                }

                if (obj.MontoComision == 0)
                {
                    xml += "MontoComision ='0' ";
                }

                if (obj.Broker > 0)
                {
                    xml += "Broker ='" + obj.Broker + "' ";
                }

                xml += "NroContenedor ='" + obj.NroContenedor + "' ";
                xml += "CuentaBancaria ='" + obj.CuentaBancaria + "' ";
                xml += "PushOrder ='" + obj.PushOrder + "' ";
                xml += "Estado ='" + obj.Estado + "' ";
                xml += "DEstado ='" + obj.DEstado + "' ";
                xml += "Incoterm ='" + obj.Incoterm + "' ";
                xml += "Flete ='" + obj.Flete + "' ";
                xml += "FleteReal ='" + obj.FleteReal + "' ";
                xml += "Observacion ='" + obj.Observacion + "' ";                
                xml += "Activo ='" + obj.Activo + "' ";
                xml += "Anulado ='" + obj.Anulado + "' />";
              
                foreach (dynamic dm in obj.Detalle)
                {                    
                    xmlDPedido += "<DPedidoVenta ";
                    xmlDPedido += "DPedidoVenta = '" + dm.DPedidoVenta + "' ";
                    xmlDPedido += "Empresa = '" + JWT.IdEmpresa + "' ";
                    xmlDPedido += "Posicion = '" + dm.Posicion + "' ";                    
                    xmlDPedido += "Material = '" + dm.Material + "' ";
                    xmlDPedido += "ValorUnitario = '" + dm.ValorUnitario.ToString(System.Globalization.CultureInfo.InvariantCulture) + "' ";
                    xmlDPedido += "PrecioUnitario = '" + dm.PrecioUnitario.ToString(System.Globalization.CultureInfo.InvariantCulture) + "' ";
                    xmlDPedido += "DescuentoPrecio = '" + dm.DescuentoPrecio.ToString(System.Globalization.CultureInfo.InvariantCulture) + "' ";
                    xmlDPedido += "DescuentoValor = '" + dm.DescuentoValor.ToString(System.Globalization.CultureInfo.InvariantCulture) + "' ";
                    xmlDPedido += "Cantidad = '" + dm.Cantidad.ToString(System.Globalization.CultureInfo.InvariantCulture) + "' ";
                    xmlDPedido += "PrecioTotal = '" + dm.PrecioTotal.ToString(System.Globalization.CultureInfo.InvariantCulture) + "' ";
                    xmlDPedido += "ValorTotal = '" + dm.ValorTotal.ToString(System.Globalization.CultureInfo.InvariantCulture) + "' ";                 
                    xmlDPedido += "UnidadMedida = '" + dm.UnidadMedida + "' ";
                    if (dm.CentroExpedicion != null)
                    {
                        xmlDPedido += "CentroExpedicion ='" + dm.CentroExpedicion + "' ";
                    }
                    xmlDPedido += "TipoFecha = '" + dm.TipoFecha + "' ";
                    xmlDPedido += "FechaReparto = '" + dm.FechaReparto + "' ";                                       
                    xmlDPedido += "FechaRepartoFinal = '" + dm.FechaRepartoFinal + "' ";
                    xmlDPedido += "PrecioFob = '" + dm.PrecioFob + "' ";
                    xmlDPedido += "Observaciones = '" + dm.Observaciones + "' ";
                    xmlDPedido += "Factor = '" + dm.Factor.ToString(System.Globalization.CultureInfo.InvariantCulture) + "' ";
                    xmlDPedido += "PesoNeto = '" + dm.PesoNeto.ToString(System.Globalization.CultureInfo.InvariantCulture) + "' ";

                    if (dm.UnidadPeso > 0)
                    {
                        xmlDPedido += "UnidadPeso ='" + dm.UnidadPeso + "' ";
                    }

                    xmlDPedido += "UnidadPrecio = '" + dm.UnidadPrecio + "' ";
                    xmlDPedido += "Activo = '" + dm.Activo + "' />";
                }

                foreach (dynamic dp in obj.DetallePago)
                {
                    xmlPagos += "<PagosPedido ";
                    xmlPagos += "DPago = '" + dp.DPago + "' ";
                    xmlPagos += "PedidoVenta = '" + dp.PedidoVenta + "' ";
                    xmlPagos += "CondicionPago = '" + dp.CondicionPago + "' ";
                    xmlPagos += "TipoFecha = '" + dp.TipoFecha + "' ";
                    xmlPagos += "DTipoFecha = '" + dp.DTipoFecha + "' ";
                    xmlPagos += "NroDias = '" + dp.NroDias + "' ";
                    xmlPagos += "Porcentaje = '" + dp.Porcentaje.ToString(System.Globalization.CultureInfo.InvariantCulture) + "' ";

                    if (dp.Fecha != null)
                    {
                        xmlPagos += "Fecha ='" + dp.Fecha + "' ";
                    }

                    xmlPagos += "Activo = '" + dp.Activo + "' />";
                }


                xml += "</root>";
                xmlDPedido += "</root>";
                xmlPagos += "</root>";

                int res = Instancia.SaveUpdatePedidoVenta(JWT.Login, xml, xmlDPedido, xmlPagos);
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
        [ActionName("PP_getDatosPedidoVenta")]
        public HttpResponseMessage PP_getDatosPedidoVenta(int PedidoVenta)
        {
            try
           {
                DataTable dt = Instancia.getDatosPedidoVenta(PedidoVenta);

                var obj = (from c in dt.AsEnumerable()
                           select new
                           {     
                               PedidoVenta = Convert.ToInt64(c["PedidoVenta"]),
                               ClaseDocVta = c["ClaseDocVta"].ToString(),
                               OrgVentas = c["OrgVentas"].ToString(),
                               Sector = c["Sector"].ToString(),
                               Cliente = Convert.ToInt64(c["Cliente"]),
                               PosicionMax = c["PosicionMax"].ToString() == "" ? c["PosicionMax"] : Convert.ToInt64(c["PosicionMax"]),
                               IdSector = Convert.ToInt64(c["IdSector"]),

                               IdOrgVentas = Convert.ToInt64(c["IdOrgVentas"]),
                               IdClaseDocVta = Convert.ToInt64(c["IdClaseDocVta"]),

                               Canal = c["Canal"].ToString(),

                               UnidadCliente = c["UnidadCliente"].ToString(),
                               CodCliente = c["CodCliente"].ToString(),
                               RazonCliente = c["RazonCliente"].ToString(),
                               DireCliente = c["DireCliente"].ToString(),
                               FechaEmision = c["FechaEmision"].ToString() == "" ? null : Convert.ToDateTime(c["FechaEmision"]).ToString("yyyy-MM-dd HH:mm:ss"),
                               FechaEntrega = c["FechaEntrega"].ToString() == "" ? null : Convert.ToDateTime(c["FechaEntrega"]).ToString("yyyy-MM-dd HH:mm:ss"),
                               CabSubTotal = Convert.ToDecimal(c["CabSubTotal"].ToString()),
                               CabPorcIgv = Convert.ToDecimal(c["CabPorcIgv"].ToString()),
                               CabIgv = Convert.ToDecimal(c["CabIgv"].ToString()),
                               CabDescuento = Convert.ToDecimal(c["CabDescuento"].ToString()),
                               CabTotal = Convert.ToDecimal(c["CabTotal"].ToString()),
                               FechaDespacho = c["FechaDespacho"].ToString() == "" ? null : Convert.ToDateTime(c["FechaDespacho"]).ToString("yyyy-MM-dd HH:mm:ss"),
                               FechaEtaOrigen = c["FechaEtaOrigen"].ToString() == "" ? null : Convert.ToDateTime(c["FechaEtaOrigen"]).ToString("yyyy-MM-dd HH:mm:ss"),
                               FechaEtd = c["FechaEtd"].ToString() == "" ? null : Convert.ToDateTime(c["FechaEtd"]).ToString("yyyy-MM-dd HH:mm:ss"),           
                               FechaDD = c["FechaDD"].ToString() == "" ? null : Convert.ToDateTime(c["FechaDD"]).ToString("yyyy-MM-dd HH:mm:ss"),
                               FechaEtaDestino = c["FechaEtaDestino"].ToString() == "" ? null : Convert.ToDateTime(c["FechaEtaDestino"]).ToString("yyyy-MM-dd HH:mm:ss"),
                               FechaDAM = c["FechaDAM"].ToString() == "" ? null : Convert.ToDateTime(c["FechaDAM"]).ToString("yyyy-MM-dd HH:mm:ss"),
                               DestinoMercancia = c["DestinoMercancia"].ToString() == "" ? c["DestinoMercancia"] : Convert.ToInt64(c["DestinoMercancia"]),
                               RazonDestinoMercaderia = c["RazonDestinoMercaderia"].ToString(),
                               TerminalAlmacenamiento = c["TerminalAlmacenamiento"].ToString() == "" ? c["TerminalAlmacenamiento"] : Convert.ToInt64(c["TerminalAlmacenamiento"]),
                               RazonTerminal = c["RazonTerminal"].ToString(),                        
                               AgenteCarga = c["AgenteCarga"].ToString() == "" ? c["AgenteCarga"] : Convert.ToInt64(c["AgenteCarga"]),
                               RazonAgenteCarga = c["RazonAgenteCarga"].ToString(),
                               AgenteAduana = c["AgenteAduana"].ToString() == "" ? c["AgenteAduana"] : Convert.ToInt64(c["AgenteAduana"]),                            
                               RazonAgenteAduana = c["RazonAgenteAduana"].ToString(),
                               MetodoProduccion = c["MetodoProduccion"].ToString(),
                               TipoMoneda = c["TipoMoneda"].ToString(),
                               Vapor = c["Vapor"].ToString() == "" ? c["Vapor"] : Convert.ToInt64(c["Vapor"]),
                               TipoEnvio = c["TipoEnvio"].ToString(),
                               DEstado = c["DEstado"].ToString(),
                               Estado = c["Estado"].ToString(),
                               NroBooking = c["NroBooking"].ToString(),
                               AplicaIncoterm = c["AplicaIncoterm"].ToString(),
                               AplicaIGV = c["AplicaIGV"].ToString(),
                               IncluyeIgv = Convert.ToBoolean(c["IncluyeIgv"].ToString()),
                               Precinto = c["Precinto"].ToString(),
                               NroBL = c["NroBL"].ToString(),
                               DuaDAM = c["DuaDAM"].ToString(),
                               NroProforma = c["NroProforma"].ToString(),
                               Broker = c["Broker"].ToString(),
                               NombreBroker = c["NombreBroker"].ToString(),
                               Transportista = c["Transportista"].ToString() == "" ? c["Transportista"] : Convert.ToInt64(c["Transportista"]),
                               RazonTransportista = c["RazonTransportista"].ToString(),
                               PuertoCarga = c["PuertoCarga"].ToString() == "" ? c["PuertoCarga"] : Convert.ToInt64(c["PuertoCarga"]),
                               RazonPuertoCarga = c["RazonPuertoCarga"].ToString(),
                               Observacion = c["Observacion"].ToString(),
                               FleteProyectado = Convert.ToDecimal(c["Flete"].ToString()),
                               FleteEjecutado = Convert.ToDecimal(c["FleteReal"].ToString()),
                               Incoterm = Convert.ToInt64(c["incoterm"]),
                               CuentaBancaria = c["CuentaBancaria"].ToString() == "" ? c["CuentaBancaria"] : Convert.ToInt64(c["CuentaBancaria"]),
                               Banco = c["Banco"].ToString() == "" ? c["Banco"] : Convert.ToInt64(c["Banco"]),
                               OperadorLogistico = c["AgenteLogistico"].ToString() == "" ? c["AgenteLogistico"] : Convert.ToInt64(c["AgenteLogistico"]),
                               RazonLogistico = c["RazonLogistico"].ToString(),
                               NroContenedor = c["NroContenedor"].ToString(),
                               PushOrder = c["PushOrder"].ToString(),

                               PorcentajeComision = c["PorcentajeComision"].ToString() == "" ? c["PorcentajeComision"] : Convert.ToDecimal(c["PorcentajeComision"]),
                               MontoComision = c["MontoComision"].ToString() == "" ? c["MontoComision"] : Convert.ToDecimal(c["MontoComision"]),
                               TipoCarga = c["TipoCarga"].ToString(),
                               TipoEmbalaje = c["TipoEmbalaje"].ToString(),


                               Detalles = (from d in dt.AsEnumerable()
                                           where Convert.ToInt32(d["Activo"]) == 1 && Convert.ToInt64(c["PedidoVenta"]).Equals(Convert.ToInt64(d["PedidoVenta"]))
                                           select new
                                           {
                                               DPedidoVenta = Convert.ToInt64(d["DPedidoVenta"]),
                                               Posicion = Convert.ToInt64(d["Posicion"]),
                                               Material = Convert.ToInt64(d["Material"].ToString()),
                                               CodDetaMaterial = d["CodDetaMaterial"].ToString(),
                                               DescMaterial = d["DescMaterial"].ToString(),
                                               DescUnidadMedida = d["DescUnidadMedida"].ToString(),
                                               UnidadMedida = Convert.ToInt64(d["UnidadMedida"].ToString()),
                                               // Activo = Convert.ToBoolean(d["Activo"].ToString()),



                                               DetallePrecioUnitario = Convert.ToDecimal(d["DetallePrecioUnitario"].ToString()),
                                               DescuentoUnitario = Convert.ToDecimal(d["DescuentoUnitario"].ToString()),
                                               DetallePrecioTotal = Convert.ToDecimal(d["DetallePrecioTotal"].ToString()),
                                               CentroExpedicion = d["CentroExpedicion"].ToString() == "" ? d["CentroExpedicion"] : Convert.ToDecimal(d["CentroExpedicion"]),
                                               // CentroExpedicion = Convert.ToInt64(d["CentroExpedicion"].ToString()),
                                               TipoFecha = d["TipoFecha"].ToString(),
                                               Observaciones = d["DetalleObservacion"].ToString(),
                                               DescCentroExpedicion = d["DescCentroExpedicion"].ToString(),
                                               DescFechaReparto = Convert.ToDateTime(d["FechaReparto"]).ToString("yyyy-MM-dd"),
                                               FechaReparto = Convert.ToDateTime(d["FechaReparto"]).ToString("dd-MM-yyyy"),
                                               FechaRepartoFinal = Convert.ToDateTime(d["FechaRepartoFinal"]).ToString("dd-MM-yyyy"),
                                               Cantidad = Convert.ToDecimal(d["Cantidad"].ToString()),
                                               ValorUnitario = Convert.ToDecimal(d["ValorUnitario"].ToString()),
                                               PrecioUnitario = Convert.ToDecimal(d["PrecioUnitario"].ToString()),
                                               DescuentoValor = Convert.ToDecimal(d["DescuentoValor"].ToString()),
                                               DescuentoPrecio = Convert.ToDecimal(d["DescuentoPrecio"].ToString()),
                                               ValorTotal = Convert.ToDecimal(d["ValorTotal"].ToString()),
                                               PrecioTotal = Convert.ToDecimal(d["PrecioTotal"].ToString()),
                                               PrecioFob = Convert.ToDecimal(d["precioFob"].ToString()),
                                               PesoNeto = Convert.ToDecimal(d["PesoNeto"].ToString()),
                                               CalculoPesoNeto = Convert.ToDecimal(d["CalculoPesoNeto"].ToString()),
                                               FleteDistribuido = Convert.ToDecimal(d["FleteDistribuido"].ToString()),
                                               FleteUnitario = Convert.ToDecimal(d["FleteUnitario"].ToString()),
                                               Factor = Convert.ToDecimal(d["Factor"].ToString()),
                                               UnidadPrecio = Convert.ToInt64(d["UnidadPrecio"].ToString()),

                                               PesoNetoUnidad = d["PesoNetoUnidad"].ToString() == "" ? d["PesoNetoUnidad"] : Convert.ToDecimal(d["PesoNetoUnidad"]),
                                               idUnidadPesoPedido = d["UnidadPeso"].ToString() == "" ? d["UnidadPeso"] : Convert.ToInt64(d["UnidadPeso"]),

                                               UnidadPesoNeto = d["UnidadPesoNeto"].ToString(),

                                               idUnidadMaterialBase = Convert.ToInt64(d["idUnidadMaterialBase"].ToString()),
                                               idUnidadMaterialPeso = Convert.ToInt64(d["idUnidadMaterialPeso"].ToString()),

                                               MaterialOrgVta = Convert.ToInt64(d["MaterialOrgVentas"].ToString()),
                                               Inafecto = d["Inafecto"],


                                           }).GroupBy(d => new { d.DPedidoVenta }).Select(d => d.First()),


                               DetallesPagos = (from m in dt.AsEnumerable()
                                                where Convert.ToInt32(m["ActivoPago"]) == 1 && Convert.ToInt64(c["PedidoVenta"]).Equals(Convert.ToInt64(m["PedidoPCP"]))
                                                select new
                                                {
                                                    DPago = Convert.ToInt64(m["PedidoCondicionPago"]),
                                                    PedidoVenta = Convert.ToInt64(m["PedidoPCP"]),
                                                    NroDias = Convert.ToInt64(m["NroDias"].ToString()),
                                                    FechaVencimiento = m["FechaVencimiento"].ToString() == "" ? m["FechaVencimiento"] : Convert.ToDateTime(m["FechaVencimiento"]).ToString("yyyy-MM-dd HH:mm:ss"),
                                                    CondicionPago = Convert.ToInt64(m["CondicionPago"].ToString()),
                                                    TipoFechaPago = m["TipoFechaPago"].ToString(),
                                                    DCondicionPago = m["DCondicionPago"].ToString(),
                                                    DTipoFecha = m["DTipoFecha"].ToString(),
                                                    Fecha = m["FechaPago"].ToString() == "" ? m["FechaPago"] : Convert.ToDateTime(m["FechaPago"]).ToString("yyyy-MM-dd HH:mm:ss"),
                                                    Porcentaje = Convert.ToDecimal(m["Porcentaje"].ToString()),
                                                    Activo = Convert.ToInt64(m["ActivoPago"].ToString()),
                                                }).GroupBy(m => new { m.DPago }).Select(m => m.First()),







                               DetallesAnulados = (from f in dt.AsEnumerable()
                                                   where Convert.ToInt32(f["Activo"]) == 0 && Convert.ToInt64(c["PedidoVenta"]).Equals(Convert.ToInt64(f["PedidoVenta"]))
                                                   select new
                                                   {
                                                       DPedidoVenta = Convert.ToInt64(f["DPedidoVenta"]),
                                                       Posicion = Convert.ToInt64(f["Posicion"]),
                                                       Material = Convert.ToInt64(f["Material"].ToString()),
                                                       CodDetaMaterial = f["CodDetaMaterial"].ToString(),
                                                       DescMaterial = f["DescMaterial"].ToString(),
                                                       DescUnidadMedida = f["DescUnidadMedida"].ToString(),
                                                       UnidadMedida = Convert.ToInt64(f["UnidadMedida"].ToString()),
                                                       Activo = Convert.ToBoolean(f["Activo"].ToString()),



                                                       DetallePrecioUnitario = Convert.ToDecimal(f["DetallePrecioUnitario"].ToString()),
                                                       DescuentoUnitario = Convert.ToDecimal(f["DescuentoUnitario"].ToString()),
                                                       DetallePrecioTotal = Convert.ToDecimal(f["DetallePrecioTotal"].ToString()),

                                                       CentroExpedicion = Convert.ToInt64(f["CentroExpedicion"].ToString()),
                                                       TipoFecha = f["TipoFecha"].ToString(),
                                                       Observaciones = f["DetalleObservacion"].ToString(),
                                                       DescCentroExpedicion = f["DescCentroExpedicion"].ToString(),
                                                       DescFechaReparto = Convert.ToDateTime(f["FechaReparto"]).ToString("yyyy-MM-dd"),
                                                       FechaReparto = Convert.ToDateTime(f["FechaReparto"]).ToString("dd-MM-yyyy"),
                                                       FechaRepartoFinal = Convert.ToDateTime(f["FechaRepartoFinal"]).ToString("dd-MM-yyyy"),
                                                       Cantidad = Convert.ToDecimal(f["Cantidad"].ToString()),
                                                       ValorUnitario = Convert.ToDecimal(f["ValorUnitario"].ToString()),
                                                       PrecioUnitario = Convert.ToDecimal(f["PrecioUnitario"].ToString()),
                                                       DescuentoValor = Convert.ToDecimal(f["DescuentoValor"].ToString()),
                                                       DescuentoPrecio = Convert.ToDecimal(f["DescuentoPrecio"].ToString()),
                                                       ValorTotal = Convert.ToDecimal(f["ValorTotal"].ToString()),
                                                       PrecioTotal = Convert.ToDecimal(f["PrecioTotal"].ToString()),
                                                       Factor = Convert.ToDecimal(f["Factor"].ToString()),
                                                       UnidadPrecio = Convert.ToInt64(f["UnidadPrecio"].ToString()),
                                                       MaterialOrgVta = Convert.ToInt64(f["MaterialOrgVentas"].ToString()),
                                                       Inafecto = f["Inafecto"],
                                                   }).GroupBy(f => new { f.DPedidoVenta }).Select(f => f.First()),

                           }).GroupBy(c => new { c.PedidoVenta }).Select(c => c.First());

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
        [ActionName("SD_postUpdateAnularPedidoVenta")]
        public HttpResponseMessage SD_postUpdateAnularPedidoVenta(dynamic obj)
        {
            try
            {
                int Codigo = obj.Codigo;                
                string Accion = obj.Accion;
                string Estado = obj.Estado;


                int res = Instancia.postUpdateAnularPedidoVenta(Codigo, Accion, JWT.Login, Estado, JWT.IdEmpresa);
                string mensaje = "";
                bool success = false;
                if (res == 0 && Accion == "A")
                {
                    success = true;
                    mensaje = "Se anulo correctamente el pedido de venta.";
                }

                if (res == 0 && Accion == "I")
                {
                    success = true;                   
                }

                if (res == -1 && Accion == "A")
                {
                    success = true;
                    mensaje = "El pedido seleccionado no se puede anular, tiene movimientos realizados.";
                }

                if (res == -1 && Accion == "I")
                {
                    success = true;
                    mensaje = "El producto seleccionado no se puede eliminar, tiene movimiento realizados.";
                }

                if (res == -2 && Accion == "D")
                {
                    success = true;
                    mensaje = "Por favor ingrese el destino mercancia del pedido de venta antes de ser PROGRAMADO.";
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
        [ActionName("PP_GetINCOTERM")]
        public IHttpActionResult PP_GetINCOTERM()
        {
            try
            {
                DataTable dt = Instancia.GetINCOTERM();
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpGet]
        [ActionName("PP_GetSeguroVenta")]
        public IHttpActionResult PP_GetSeguroVenta(int? Activo)
        {
            try
            {
                DataTable dt = Instancia.GetSeguroVenta(Activo, JWT.IdEmpresa);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }


        [HttpGet]
        [ActionName("SD_GetTipoCarga")]
        public IHttpActionResult SD_GetTipoCarga()
        {
            try
            {
                DataTable dt = Instancia.GetTipoCarga(JWT.IdEmpresa);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }


        [HttpGet]
        [ActionName("SD_GetTipoEmbalaje")]
        public IHttpActionResult SD_GetTipoEmbalaje()
        {
            try
            {
                DataTable dt = Instancia.GetTipoEmbalaje(JWT.IdEmpresa);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpGet]
        [ActionName("SD_GetListarBancos")]
        public IHttpActionResult SD_GetListarBancos()
        {
            try
            {
                DataTable dt = Instancia.GetListarBancos(JWT.IdEmpresa);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpGet]
        [ActionName("SD_GetListarCuentasInterbancarias")]
        public IHttpActionResult SD_GetListarCuentasInterbancarias(int Banco, string TipoMoneda)
        {
            try
            {
                DataTable dt = Instancia.GetListarCuentasInterbancarias(Banco, TipoMoneda);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }


        [HttpGet]
        [ActionName("SD_GetUnidadMedidaPrecio")]
        public IHttpActionResult SD_GetUnidadMedidaPrecio(int MaterialOrganizacionVenta, int Material)
        {
            try
            {
                DataTable dt = Instancia.GetUnidadMedidaPrecio(MaterialOrganizacionVenta, Material, JWT.IdEmpresa);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        
        [HttpGet]
        [ActionName("SD_GetTipoFechaPago")]
        public IHttpActionResult SD_GetTipoFechaPago()
        {
            try
            {
                DataTable dt = Instancia.GetTipoFechaPago(JWT.IdEmpresa);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpGet]
        [ActionName("GetProformaPedidoVenta")]
        public IHttpActionResult GetProformaPedidoVenta(int prmintPedidoVenta, int prmintOpcIdioma)
        {
            try
            {
                DataTableCollection dt = Instancia.GetProformaPedidoVenta(prmintPedidoVenta, prmintOpcIdioma);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleError(ex));
            }
        }

        [HttpGet]
        [ActionName("SD_ObtenerPorcentajeComision")]
        public IHttpActionResult SD_ObtenerPorcentajeComision(int prmintPedidoVenta, string prmstrFechaEntrega)
        {
            try
            {
                DataTable dt = Instancia.ObtenerPorcentajeComision(prmintPedidoVenta, prmstrFechaEntrega);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleError(ex));
            }
        }


        [HttpPost]
        [ActionName("PSDM_SaveUpdateBorradorPedidoVenta")]
        public HttpResponseMessage PSDM_SaveUpdateBorradorPedidoVenta(dynamic obj)
        {
            try
            {
                string xml = "<?xml version='1.0' encoding='ISO-8859-1'?><root>";
                string xmlDPedido = "<?xml version='1.0' encoding='ISO-8859-1'?><root>";
                string xmlPagos = "<?xml version='1.0' encoding='ISO-8859-1'?><root>";


                xml += "<PedidoVenta ";
                xml += "PedidoVenta ='" + obj.PedidoVenta + "' ";
                xml += "Empresa ='" + JWT.IdEmpresa + "' ";
                xml += "Sociedad ='" + JWT.IdSociedad + "' ";
                xml += "Cliente ='" + obj.Cliente + "' ";
                xml += "Sector ='" + obj.Sector + "' ";
                xml += "OrgVentas ='" + obj.OrgVentas + "' ";
                xml += "ClaseDocVta ='" + obj.ClaseDocVta + "' ";
                xml += "TipoMoneda ='" + obj.TipoMoneda + "' ";
                xml += "ValorMoneda ='" + obj.ValorMoneda + "' ";
                xml += "IncluyeIgv ='" + obj.IncluyeIgv + "' ";
                xml += "IgvPorcentaje ='" + obj.IgvPorcentaje.ToString(System.Globalization.CultureInfo.InvariantCulture) + "' ";
                xml += "Igv ='" + obj.Igv.ToString(System.Globalization.CultureInfo.InvariantCulture) + "' ";
                xml += "SubTotal ='" + obj.SubTotal.ToString(System.Globalization.CultureInfo.InvariantCulture) + "' ";
                xml += "Descuento ='" + obj.Descuento.ToString(System.Globalization.CultureInfo.InvariantCulture) + "' ";
                xml += "Total ='" + obj.Total.ToString(System.Globalization.CultureInfo.InvariantCulture) + "' ";
                xml += "MetodoProduccion ='" + obj.MetodoProduccion + "' ";
                xml += "Canal ='" + obj.Canal + "' ";

                xml += "NombreMetodoProduccion ='" + obj.NombreMetodoProduccion + "' ";

                if (obj.FechaEmision != null)
                {
                    xml += "FechaEmision ='" + obj.FechaEmision + "' ";
                }


                if (obj.FechaEntrega != null)
                {
                    xml += "FechaEntrega ='" + obj.FechaEntrega + "' ";
                }


                if (obj.FechaDespacho != null)
                {
                    xml += "FechaDespacho ='" + obj.FechaDespacho + "' ";
                }


                if (obj.FechaEtaOrigen != null)
                {
                    xml += "FechaEtaOrigen ='" + obj.FechaEtaOrigen + "' ";
                }

                if (obj.FechaEtd != null)
                {
                    xml += "FechaEtd ='" + obj.FechaEtd + "' ";
                }

                if (obj.FechaDD != null)
                {
                    xml += "FechaDD ='" + obj.FechaDD + "' ";
                }

                if (obj.FechaEtaDestino != null)
                {
                    xml += "FechaEtaDestino ='" + obj.FechaEtaDestino + "' ";
                }


                if (obj.TipoEnvio != null)
                {
                    xml += "TipoEnvio ='" + obj.TipoEnvio + "' ";
                }


                if (obj.ValorTipoEnvio != null)
                {
                    xml += "ValorTipoEnvio ='" + obj.ValorTipoEnvio + "' ";
                }

                if (obj.NroBooking != null)
                {
                    xml += "NroBooking ='" + obj.NroBooking + "' ";
                }

                if (obj.Precinto != null)
                {
                    xml += "Precinto ='" + obj.Precinto + "' ";
                }

                if (obj.NroBL != null)
                {
                    xml += "NroBL ='" + obj.NroBL + "' ";
                }

                if (obj.DuaDAM != null)
                {
                    xml += "DuaDAM ='" + obj.DuaDAM + "' ";
                }

                if (obj.FechaDAM != null)
                {
                    xml += "FechaDAM ='" + obj.FechaDAM + "' ";
                }

                if (obj.TipoCarga != null)
                {
                    xml += "TipoCarga ='" + obj.TipoCarga + "' ";
                }

                if (obj.TipoEmbalaje != null)
                {
                    xml += "TipoEmbalaje ='" + obj.TipoEmbalaje + "' ";
                }

                if (obj.DestinoMercancia > 0)
                {
                    xml += "DestinoMercancia ='" + obj.DestinoMercancia + "' ";
                }

                if (obj.TerminalAlmacenamiento > 0)
                {
                    xml += "TerminalAlmacenamiento ='" + obj.TerminalAlmacenamiento + "' ";
                }

                if (obj.AgenteAduana > 0)
                {
                    xml += "AgenteAduana ='" + obj.AgenteAduana + "' ";
                }

                if (obj.AgenteCarga > 0)
                {
                    xml += "AgenteCarga ='" + obj.AgenteCarga + "' ";
                }

                if (obj.OperadorLogistico > 0)
                {
                    xml += "OperadorLogistico ='" + obj.OperadorLogistico + "' ";
                }

                if (obj.Transportista > 0)
                {
                    xml += "Transportista ='" + obj.Transportista + "' ";
                }


                if (obj.PuertoCarga > 0)
                {
                    xml += "PuertoCarga ='" + obj.PuertoCarga + "' ";
                }

                if (obj.Vapor > 0)
                {
                    xml += "Vapor ='" + obj.Vapor + "' ";
                }

                if (obj.PorComision > 0)
                {
                    xml += "PorComision ='" + obj.PorComision + "' ";
                }

                if (obj.PorComision == 0)
                {
                    xml += "PorComision ='0' ";
                }

                if (obj.MontoComision > 0)
                {
                    xml += "MontoComision ='" + obj.MontoComision + "' ";
                }

                if (obj.MontoComision == 0)
                {
                    xml += "MontoComision ='0' ";
                }

                if (obj.Broker > 0)
                {
                    xml += "Broker ='" + obj.Broker + "' ";
                }

                xml += "NroContenedor ='" + obj.NroContenedor + "' ";

                if (obj.CuentaBancaria > 0)
                {
                    xml += "CuentaBancaria ='" + obj.CuentaBancaria + "' ";
                }

                xml += "PushOrder ='" + obj.PushOrder + "' ";
                xml += "Estado ='" + obj.Estado + "' ";
                xml += "DEstado ='" + obj.DEstado + "' ";
                xml += "Incoterm ='" + obj.Incoterm + "' ";
                xml += "Flete ='" + obj.Flete + "' ";
                xml += "FleteReal ='" + obj.FleteReal + "' ";
                xml += "Observacion ='" + obj.Observacion + "' ";
                xml += "Activo ='" + obj.Activo + "' ";
                xml += "Anulado ='" + obj.Anulado + "' />";

                foreach (dynamic dm in obj.Detalle)
                {
                    xmlDPedido += "<DPedidoVenta ";
                    xmlDPedido += "DPedidoVenta = '" + dm.DPedidoVenta + "' ";
                    xmlDPedido += "Empresa = '" + JWT.IdEmpresa + "' ";
                    xmlDPedido += "Posicion = '" + dm.Posicion + "' ";
                    xmlDPedido += "Material = '" + dm.Material + "' ";
                    xmlDPedido += "ValorUnitario = '" + dm.ValorUnitario.ToString(System.Globalization.CultureInfo.InvariantCulture) + "' ";
                    xmlDPedido += "PrecioUnitario = '" + dm.PrecioUnitario.ToString(System.Globalization.CultureInfo.InvariantCulture) + "' ";
                    xmlDPedido += "DescuentoPrecio = '" + dm.DescuentoPrecio.ToString(System.Globalization.CultureInfo.InvariantCulture) + "' ";
                    xmlDPedido += "DescuentoValor = '" + dm.DescuentoValor.ToString(System.Globalization.CultureInfo.InvariantCulture) + "' ";
                    xmlDPedido += "Cantidad = '" + dm.Cantidad.ToString(System.Globalization.CultureInfo.InvariantCulture) + "' ";
                    xmlDPedido += "PrecioTotal = '" + dm.PrecioTotal.ToString(System.Globalization.CultureInfo.InvariantCulture) + "' ";
                    xmlDPedido += "ValorTotal = '" + dm.ValorTotal.ToString(System.Globalization.CultureInfo.InvariantCulture) + "' ";
                    xmlDPedido += "UnidadMedida = '" + dm.UnidadMedida + "' ";

                    if (dm.CentroExpedicion != null)
                    {
                        xmlDPedido += "CentroExpedicion ='" + dm.CentroExpedicion + "' ";
                    }

                    // xmlDPedido += "CentroExpedicion = '" + dm.CentroExpedicion + "' ";
                    xmlDPedido += "TipoFecha = '" + dm.TipoFecha + "' ";
                    xmlDPedido += "FechaReparto = '" + dm.FechaReparto + "' ";
                    xmlDPedido += "FechaRepartoFinal = '" + dm.FechaRepartoFinal + "' ";
                    xmlDPedido += "PrecioFob = '" + dm.PrecioFob + "' ";
                    xmlDPedido += "Observaciones = '" + dm.Observaciones + "' ";
                    xmlDPedido += "Factor = '" + dm.Factor.ToString(System.Globalization.CultureInfo.InvariantCulture) + "' ";
                    xmlDPedido += "PesoNeto = '" + dm.PesoNeto.ToString(System.Globalization.CultureInfo.InvariantCulture) + "' ";

                    if (dm.UnidadPeso > 0)
                    {
                        xmlDPedido += "UnidadPeso ='" + dm.UnidadPeso + "' ";
                    }
                    xmlDPedido += "UnidadPrecio = '" + dm.UnidadPrecio + "' ";
                    xmlDPedido += "Activo = '" + dm.Activo + "' />";
                }

                foreach (dynamic dp in obj.DetallePago)
                {
                    xmlPagos += "<PagosPedido ";
                    xmlPagos += "DPago = '" + dp.DPago + "' ";
                    xmlPagos += "PedidoVenta = '" + dp.PedidoVenta + "' ";
                    xmlPagos += "CondicionPago = '" + dp.CondicionPago + "' ";
                    xmlPagos += "TipoFecha = '" + dp.TipoFecha + "' ";
                    xmlPagos += "DTipoFecha = '" + dp.DTipoFecha + "' ";
                    xmlPagos += "NroDias = '" + dp.NroDias + "' ";
                    xmlPagos += "Porcentaje = '" + dp.Porcentaje.ToString(System.Globalization.CultureInfo.InvariantCulture) + "' ";

                    if (dp.Fecha != null)
                    {
                        xmlPagos += "Fecha ='" + dp.Fecha + "' ";
                    }

                    xmlPagos += "Activo = '" + dp.Activo + "' />";
                }


                xml += "</root>";
                xmlDPedido += "</root>";
                xmlPagos += "</root>";

                int res = Instancia.SaveUpdateBorradorPedidoVenta(JWT.Login, xml, xmlDPedido, xmlPagos);
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
        [ActionName("SD_GetEquivalenciaUnidadPV")]
        public IHttpActionResult SD_GetEquivalenciaUnidadPV(int Material, int UnidadMedida)
        {
            try
            {
                DataTable dt = Instancia.GetEquivalenciaUnidadPV(Material, UnidadMedida, JWT.IdEmpresa);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }






    }
}
