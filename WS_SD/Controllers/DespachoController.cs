using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using static WS_SD.Models.DespachoModel;
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
    public class DespachoController : BaseController
    {
        [HttpGet]
        [ActionName("SD_getTipoSalidaDespacho")]
        public IHttpActionResult SD_getTipoSalidaDespacho(int? Activo)
        {
            try
            {
                DataTable dt = Instancia.getTipoSalidaDespacho(JWT.IdEmpresa, Activo);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpGet]
        [ActionName("SD_getTipoSalidaFactura")]
        public IHttpActionResult SD_getTipoSalidaFactura(int? Activo, int ClaseFactura)
        {
            try
            {
                DataTable dt = Instancia.getTipoSalidaFactura(JWT.IdEmpresa, Activo, ClaseFactura);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpGet]
        [ActionName("SD_getListadoDespachos")]
        public IHttpActionResult SD_getListadoDespachos(int TipoSalida, string NumeroDocEntrega, string Cliente, string CodCliente, string FechaIncio, string FechaFin)
        {
            try
            {
                DataTable dt = Instancia.getListadoDespachos(JWT.IdEmpresa, JWT.IdSociedad, TipoSalida, NumeroDocEntrega, Cliente, CodCliente, FechaIncio, FechaFin);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpGet]
        [ActionName("SD_getPosicionesPedidoVenta")]
        public IHttpActionResult SD_getPosicionesPedidoVenta(string PedidoVenta)
        {
            try
            {
                DataTable dt = Instancia.getPosicionesPedidoVenta(PedidoVenta, JWT.IdEmpresa, JWT.IdSociedad);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpGet]
        [ActionName("SD_getValidarPedidoVentaDespacho")]
        public HttpResponseMessage SD_getValidarPedidoVentaDespacho(string PedidoVenta, string FechaEntrega, int InicioPosicion, int FinPosicion)
        {
            try
            {
                DataTable dt = Instancia.getValidarPedidoVentaDespacho(PedidoVenta, FechaEntrega, InicioPosicion, FinPosicion, JWT.IdEmpresa, JWT.IdSociedad);

                if (dt.Rows.Count > 0)
                {
                    string Respuesta = dt.Rows[0]["Respuesta"].ToString();

                    if (Respuesta == "0")
                    {
                        var obj = (from c in dt.AsEnumerable()
                                   select new
                                   {
                                       Respuesta = c["Respuesta"].ToString(),
                                       PedidoVenta = Convert.ToInt64(c["PedidoVenta"]),
                                       TipoSalida = Convert.ToInt64(c["TipoSalida"]),
                                       DescTipoSalida = c["DescTipoSalida"].ToString(),
                                       CodTipoSalida = c["CodTipoSalida"].ToString(),
                                       Correlativo = c["Correlativo"].ToString(),
                                       Incoterm = c["Incoterm"].ToString(),
                                       CodIncoterm = c["CodIncoterm"].ToString(),
                                       DestinoMercancia = c["DestinoMercancia"].ToString(),
                                       DescDestinoMercancia = c["DescDestinoMercancia"].ToString(),
                                       Cliente = Convert.ToInt64(c["Cliente"]),
                                       OrgVentas = Convert.ToInt64(c["OrgVentas"]),
                                       MetodoProduccion = c["MetodoProduccion"].ToString(),
                                       NombreMetodoProduccion = c["NombreMetodoProduccion"].ToString(),

                                       Detalles = (from d in dt.AsEnumerable()
                                                   where Convert.ToInt64(c["PedidoVenta"]).Equals(Convert.ToInt64(d["PedidoVenta"]))
                                                   select new
                                                   {
                                                       DPedidoVenta = Convert.ToInt64(d["DPedidoVenta"]),
                                                       PosicionDespacho = Convert.ToInt64(d["PosicionDespacho"]),
                                                       PosicionPedido = Convert.ToInt64(d["PosicionPedido"]),
                                                       Material = Convert.ToInt64(d["Material"].ToString()),
                                                       CodMaterial = d["CodMaterial"].ToString(),
                                                       DenominacionMaterial = d["DenominacionMaterial"].ToString(),
                                                       UnidadMedidaMaterial = Convert.ToInt64(d["UnidadMedidaMaterial"].ToString()),
                                                       DescUnidadMedida = d["DescUnidadMedida"].ToString(),
                                                       CantPedido = Convert.ToDecimal(d["CantPedido"].ToString()),
                                                       CantEntrega = d["CantEntrega"].ToString(),
                                                       CantidadAntigua = Convert.ToDecimal(d["CantidadDespacho"].ToString()),
                                                       PesoBrutoMaterial = Convert.ToDecimal(d["PesoBrutoMaterial"].ToString()),
                                                       PesoNetoMaterial = Convert.ToDecimal(d["PesoNetoMaterial"].ToString()),
                                                       VolumenMaterial = Convert.ToDecimal(d["VolumenMaterial"].ToString()),
                                                       Almacen = d["Almacen"].ToString(),
                                                       PesoBruto = d["PesoBruto"].ToString(),
                                                       PesoNeto = d["PesoNeto"].ToString(),
                                                   }).GroupBy(d => new { d.DPedidoVenta }).Select(d => d.First()),


                                   }).GroupBy(c => new { c.PedidoVenta }).Select(c => c.First());

                        var httpResponseMessage = Request.CreateResponse<IEnumerable<Object>>(HttpStatusCode.OK, obj);
                        httpResponseMessage.Headers.Add("Access-Control-Allow-Origin", "*");

                        return httpResponseMessage;
                    }
                    else
                    {
                        var httpResponseMessage = Request.CreateResponse(HttpStatusCode.OK, dt);
                        httpResponseMessage.Headers.Add("Access-Control-Allow-Origin", "*");

                        return httpResponseMessage;
                    }
                }
                else
                {
                    var httpResponseMessage = Request.CreateResponse(HttpStatusCode.OK, dt);
                    httpResponseMessage.Headers.Add("Access-Control-Allow-Origin", "*");

                    return httpResponseMessage;
                }


            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpPost]
        [ActionName("SD_ObtenerMaterialesPedido")]
        public HttpResponseMessage SD_ObtenerMaterialesPedido(dynamic obj)
        {
            try
            {
                string xmlMateriales = "<?xml version='1.0' encoding='ISO-8859-1'?><root>";
                int PedidoVenta = obj.PedidoVenta;
                int Busqueda = obj.Busqueda;

                foreach (dynamic dm in obj.Detalle)
                {
                    xmlMateriales += "<Posiciones ";
                    xmlMateriales += "PosicionPedido = '" + dm.PosicionPedido + "' />";
                }

                xmlMateriales += "</root>";

                DataTable dt = Instancia.ObtenerMaterialesPedido(PedidoVenta, Busqueda, xmlMateriales);


                var httpResponseMessage = Request.CreateResponse(HttpStatusCode.OK, dt);
                httpResponseMessage.Headers.Add("Access-Control-Allow-Origin", "*");

                return httpResponseMessage;
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpGet]
        [ActionName("SD_ValidarLoteStockMaterial")]
        public IHttpActionResult SD_ValidarLoteStockMaterial(int Material, int Almacen, string Lote)
        {
            try
            {
                DataTable dt = Instancia.ValidarLoteStockMaterial(Material, Almacen, Lote);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpGet]
        [ActionName("SD_getEstadoDespacho")]
        public IHttpActionResult SD_getEstadoDespacho()
        {
            try
            {
                DataTable dt = Instancia.getEstadoDespacho();
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpPost]
        [ActionName("PSDM_SaveUpdateDespachoEntrega")]
        public HttpResponseMessage PSDM_SaveUpdateDespachoEntrega(dynamic obj)
        {
            try
            {
                string xmlDespacho = "<?xml version='1.0' encoding='ISO-8859-1'?><root>";
                string xmlDDespacho = "<?xml version='1.0' encoding='ISO-8859-1'?><root>";
                string xmlDDespachoLote = "<?xml version='1.0' encoding='ISO-8859-1'?><root>";

                xmlDespacho += "<Despacho ";
                xmlDespacho += "DespachoEntrega ='" + obj.DespachoEntrega + "' ";
                xmlDespacho += "Empresa ='" + JWT.IdEmpresa + "' ";
                xmlDespacho += "Sociedad ='" + JWT.IdSociedad + "' ";
                xmlDespacho += "Correlativo ='" + obj.Correlativo + "' ";
                xmlDespacho += "Cliente ='" + obj.Cliente + "' ";
                xmlDespacho += "ClaseDocVenta ='" + obj.ClaseDocVenta + "' ";
                xmlDespacho += "OrgVenta ='" + obj.OrgVenta + "' ";
                xmlDespacho += "PedidoVenta ='" + obj.PedidoVenta + "' ";
                xmlDespacho += "MetodoProduccion ='" + obj.MetodoProduccion + "' ";
                xmlDespacho += "NombreMetodoProduccion ='" + obj.NombreMetodoProduccion + "' ";
                xmlDespacho += "FechaDocumento ='" + obj.FechaDocumento + "' ";
                xmlDespacho += "FechaContable ='" + obj.FechaContable + "' ";
                xmlDespacho += "Centro ='" + obj.Centro + "' ";


                if (obj.FechaCarga != null)
                {
                    xmlDespacho += "FechaCarga ='" + obj.FechaCarga + "' ";
                }

                if (obj.HoraCarga != null)
                {
                    xmlDespacho += "HoraCarga ='" + obj.HoraCarga + "' ";
                }

                if (obj.FechaSMPlan != null)
                {
                    xmlDespacho += "FechaSMPlan ='" + obj.FechaSMPlan + "' ";
                }

                if (obj.HoraSMPlan != null)
                {
                    xmlDespacho += "HoraSMPlan ='" + obj.HoraSMPlan + "' ";
                }

                if (obj.FechaSMReal != null)
                {
                    xmlDespacho += "FechaSMReal ='" + obj.FechaSMReal + "' ";
                }

                if (obj.HoraSMReal != null)
                {
                    xmlDespacho += "HoraSMReal ='" + obj.HoraSMReal + "' ";
                }

                if (obj.FechaEntregaDestino != null)
                {
                    xmlDespacho += "FechaEntregaDestino ='" + obj.FechaEntregaDestino + "' ";
                }

                if (obj.HoraEntrega != null)
                {
                    xmlDespacho += "HoraEntrega ='" + obj.HoraEntrega + "' ";
                }



                //xmlDespacho += "FechaCarga ='" + obj.FechaCarga + "' ";
                //xmlDespacho += "HoraCarga ='" + obj.HoraCarga + "' ";
                //xmlDespacho += "FechaSMPlan ='" + obj.FechaSMPlan + "' ";
                //xmlDespacho += "HoraSMPlan ='" + obj.HoraSMPlan + "' ";
                //xmlDespacho += "FechaSMReal ='" + obj.FechaSMReal + "' ";
                // xmlDespacho += "HoraSMReal ='" + obj.HoraSMReal + "' ";
                //xmlDespacho += "FechaEntregaDestino ='" + obj.FechaEntregaDestino + "' ";
                //xmlDespacho += "HoraEntrega ='" + obj.HoraEntrega + "' ";
                xmlDespacho += "Estado ='" + obj.Estado + "' ";
                xmlDespacho += "DescEstado ='" + obj.DescEstado + "' ";

                if (obj.Incoterm != null && obj.Incoterm.ToString() != "" && obj.Incoterm.ToString() != "0")
                {
                    xmlDespacho += "Incoterm ='" + obj.Incoterm + "' ";
                }



                xmlDespacho += "DestinoMercancia ='" + obj.DestinoMercancia + "' ";
                xmlDespacho += "PesoBruto ='" + obj.PesoBruto.ToString(System.Globalization.CultureInfo.InvariantCulture) + "' ";
                xmlDespacho += "PesoNeto ='" + obj.PesoNeto.ToString(System.Globalization.CultureInfo.InvariantCulture) + "' ";
                xmlDespacho += "Volumen ='" + obj.Volumen.ToString(System.Globalization.CultureInfo.InvariantCulture) + "' ";
                xmlDespacho += "Anulado ='" + obj.Anulado + "' ";
                xmlDespacho += "Serie ='" + obj.Serie + "' ";
                xmlDespacho += "Numero ='" + obj.Numero + "' ";
                xmlDespacho += "Origen ='" + obj.Origen + "' ";
               

                if (obj.Transportista != null && obj.Transportista.ToString() != "")
                {
                    xmlDespacho += "Transportista ='" + obj.Transportista + "' ";
                }

                if (obj.Vehiculo != null && obj.Vehiculo.ToString() != "")
                {
                    xmlDespacho += "Vehiculo ='" + obj.Vehiculo + "' ";
                }

                if (obj.Conductor != null && obj.Conductor.ToString() != "")
                {
                    xmlDespacho += "Conductor ='" + obj.Conductor + "' ";
                }

                xmlDespacho += "Destino ='" + obj.Destino + "' />";
            

                foreach (dynamic dm in obj.DetalleMateriales)
                {
                    xmlDDespacho += "<DDespacho ";
                    xmlDDespacho += "DDespachoEntrega = '" + dm.DDespachoEntrega + "' ";
                    xmlDDespacho += "DPedidoVenta = '" + dm.DPedidoVenta + "' ";
                    xmlDDespacho += "PosicionPedido = '" + dm.PosicionPedido + "' ";
                    xmlDDespacho += "PosicionEntrega = '" + dm.PosicionDespacho + "' ";



                    xmlDDespacho += "Material = '" + dm.Material + "' ";
                    xmlDDespacho += "UnidadMaterial = '" + dm.UnidadMaterial + "' ";
                    xmlDDespacho += "CantidadPedido = '" + dm.CantidadPedido.ToString(System.Globalization.CultureInfo.InvariantCulture) + "' ";

                    if (dm.CantidadEntrega != null && dm.CantidadEntrega.ToString() != "")
                    {
                        xmlDDespacho += "CantidadEntrega ='" + dm.CantidadEntrega.ToString(System.Globalization.CultureInfo.InvariantCulture) + "' ";
                    }
                    else
                    {
                        xmlDDespacho += "CantidadEntrega ='" + 0.ToString(System.Globalization.CultureInfo.InvariantCulture) + "' ";
                    }

                    xmlDDespacho += "CantidadNuevo = '" + dm.CantidadNuevo.ToString(System.Globalization.CultureInfo.InvariantCulture) + "' ";


                    //xmlDDespacho += "CantidadEntrega = '" + dm.CantidadEntrega.ToString(System.Globalization.CultureInfo.InvariantCulture) + "' ";

                    if (dm.PesoNeto != null && dm.PesoNeto.ToString() != "")
                    {
                        xmlDDespacho += "PesoNeto ='" + dm.PesoNeto.ToString(System.Globalization.CultureInfo.InvariantCulture) + "' ";
                    }
                    else
                    {
                        xmlDDespacho += "PesoNeto ='" + 0.ToString(System.Globalization.CultureInfo.InvariantCulture) + "' ";
                    }

                    // xmlDDespacho += "PesoNeto = '" + dm.PesoNeto.ToString(System.Globalization.CultureInfo.InvariantCulture) + "' ";

                    if (dm.PesoBruto != null && dm.PesoBruto.ToString() != "")
                    {
                        xmlDDespacho += "PesoBruto ='" + dm.PesoBruto.ToString(System.Globalization.CultureInfo.InvariantCulture) + "' ";
                    }
                    else
                    {
                        xmlDDespacho += "PesoBruto ='" + 0.ToString(System.Globalization.CultureInfo.InvariantCulture) + "' ";
                    }

                    // xmlDDespacho += "PesoBruto = '" + dm.PesoBruto.ToString(System.Globalization.CultureInfo.InvariantCulture) + "' ";
                    xmlDDespacho += "EstadoFactura ='" + dm.EstadoFactura + "' ";
                    xmlDDespacho += "Anulado = '" + dm.Anulado + "' />";
                }

                foreach (dynamic ddm in obj.DetalleLote)
                {
                    xmlDDespachoLote += "<DDespachoLote ";
                    xmlDDespachoLote += "DDespachoLotes = '" + ddm.DDespachoLotes + "' ";
                    xmlDDespachoLote += "PosicionEntrega = '" + ddm.PosicionEntrega + "' ";
                    xmlDDespachoLote += "Cantidad = '" + ddm.Cantidad.ToString(System.Globalization.CultureInfo.InvariantCulture) + "' ";


                    xmlDDespachoLote += "Lote = '" + ddm.Lote + "' ";
                    xmlDDespachoLote += "Almacen = '" + ddm.Almacen + "' ";
                    xmlDDespachoLote += "PesoBruto = '" + ddm.PesoBruto.ToString(System.Globalization.CultureInfo.InvariantCulture) + "' ";
                    xmlDDespachoLote += "PesoNeto = '" + ddm.PesoNeto.ToString(System.Globalization.CultureInfo.InvariantCulture) + "' ";
                    xmlDDespachoLote += "Anulado = '" + ddm.Anulado + "' />";
                }

                xmlDespacho += "</root>";
                xmlDDespacho += "</root>";
                xmlDDespachoLote += "</root>";

                int res = Instancia.SaveUpdateDespachoEntrega(JWT.Login, xmlDespacho, xmlDDespacho, xmlDDespachoLote);
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
                    mensaje = "El pedido de venta ya ha sido despachado";
                }

                if (res == -2)
                {
                    success = true;
                    mensaje = "Existen detalles en los pedidos que ya han sido despachados.";
                }

                if (res == -3)
                {
                    success = true;
                    mensaje = "No hay stock suficiente en uno de los lotes ingresados.";
                }

                if (res == -4)
                {
                    success = true;
                    mensaje = "El periodo para este documento no esta abierto.";
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
        [ActionName("PSDM_getDatosDespachoEntrega")]
        public HttpResponseMessage PSDM_getDatosDespachoEntrega(int DespachoEntrega)
        {
            try
            {
                DataTable dt = Instancia.getDatosDespachoEntrega(DespachoEntrega);

                var obj = (from c in dt.AsEnumerable()
                           select new
                           {
                               DespachoEntrega = Convert.ToInt64(c["DespachoEntrega"]),
                               Correlativo = c["Correlativo"].ToString(),
                               Cliente = Convert.ToInt64(c["Cliente"]),
                               PosicionActual = Convert.ToInt64(c["PosicionActual"]),
                               ClaseDocVta = Convert.ToInt64(c["ClaseDocVta"]),
                               DescClaseDocVta = c["DescClaseDocVta"].ToString(),
                               CodTipoSalida = c["CodTipoSalida"].ToString(),
                               
                               OrgVta = Convert.ToInt64(c["OrgVta"]),
                               FechaDocumento = c["FechaDocumento"] == null ? c["FechaDocumento"].ToString() : Convert.ToDateTime(c["FechaDocumento"]).ToString("yyyy-MM-dd HH:mm:ss"),
                               FechaContable = c["FechaContable"] == null ? c["FechaContable"].ToString() : Convert.ToDateTime(c["FechaContable"]).ToString("yyyy-MM-dd HH:mm:ss"),

                               PedidoVenta = Convert.ToInt64(c["PedidoVenta"]),
                               CodPedidoVenta = c["CodPedidoVenta"].ToString(),

                               MetodoProduccion = c["MetodoProduccion"].ToString(),
                               NombreMetodoProduccion = c["NombreMetodoProduccion"].ToString(),
                               Centro = Convert.ToInt64(c["Centro"]),
                               DescCentro = c["DescCentro"].ToString(),

                               FechaCarga = c["FechaCarga"].ToString() == "" ? c["FechaCarga"] : Convert.ToDateTime(c["FechaCarga"]).ToString("yyyy-MM-dd HH:mm:ss"),
                               HoraCarga = c["HoraCarga"].ToString(),

                               FechaSMPlan = c["FechaSMPlan"].ToString() == "" ? c["FechaSMPlan"] : Convert.ToDateTime(c["FechaSMPlan"]).ToString("yyyy-MM-dd HH:mm:ss"),
                               HoraSMPlan = c["HoraSMPlan"].ToString(),

                               FechaSMReal = c["FechaSMReal"].ToString() == "" ? c["FechaSMReal"] : Convert.ToDateTime(c["FechaSMReal"]).ToString("yyyy-MM-dd HH:mm:ss"),
                               HoraSMReal = c["HoraSMReal"].ToString(),

                               FechaEntregaDestino = c["FechaEntregaDestino"].ToString() == "" ? c["FechaEntregaDestino"] : Convert.ToDateTime(c["FechaEntregaDestino"]).ToString("yyyy-MM-dd HH:mm:ss"),
                               HoraEntregaDestino = c["HoraEntregaDestino"].ToString(),


                               Estado = c["Estado"].ToString(),
                               Incoterm = c["Incoterm"].ToString() == "" ? c["Incoterm"] : Convert.ToInt64(c["Incoterm"]),
                               DescIncoterm = c["DescIncoterm"].ToString(),
                               DestinoMercancia = c["DestinoMercancia"].ToString() == "" ? c["DestinoMercancia"] : Convert.ToInt64(c["DestinoMercancia"]),
                               DescDestinoMercancia = c["DescDestinoMercancia"].ToString(),


                               PesoBrutoDespacho = Convert.ToDecimal(c["PesoBrutoDespacho"].ToString()),
                               PesoNetoDespacho = Convert.ToDecimal(c["PesoNetoDespacho"].ToString()),
                               VolumenDespacho = Convert.ToDecimal(c["VolumenDespacho"].ToString()),

                               Serie = c["Serie"].ToString(),
                               Numero = c["Numero"].ToString(),
                               Origen = c["Origen"].ToString() == "" ? c["Origen"] : Convert.ToInt64(c["Origen"]),
                               Destino = c["Destino"].ToString() == "" ? c["Destino"] : Convert.ToInt64(c["Destino"]),
                               Transportista = c["Transportista"].ToString() == "" ? c["Transportista"] : Convert.ToInt64(c["Transportista"]),
                               Vehiculo = c["Vehiculo"].ToString() == "" ? c["Vehiculo"] : Convert.ToInt64(c["Vehiculo"]),
                               Conductor = c["Conductor"].ToString() == "" ? c["Conductor"] : Convert.ToInt64(c["Conductor"]),


                               DetallesMateriales = (from d in dt.AsEnumerable()
                                                     where Convert.ToInt32(d["Anulado"]) == 0 && Convert.ToInt64(c["DespachoEntrega"]).Equals(Convert.ToInt64(d["DespachoEntrega"]))
                                                     select new
                                                     {
                                                         DDespachoEnrega = Convert.ToInt64(d["DDespachoEnrega"]),
                                                         DPedidoVenta = Convert.ToInt64(d["DPedidoVenta"]),
                                                         PosicionPedido = Convert.ToInt64(d["PosicionPedido"].ToString()),
                                                         PosicionDespacho = d["PosicionEntrega"].ToString(),
                                                         Material = d["Material"].ToString(),
                                                         CodMaterial = d["CodMaterial"].ToString(),
                                                         DenominacionMaterial = d["DenominacionMaterial"].ToString(),
                                                         UnidadMedidaMaterial = d["UnidadMaterial"].ToString(),
                                                         DescUnidadMedida = d["DescUnidadMedida"].ToString(),
                                                         Almacen = d["Almacen"].ToString(),
                                                         DescAlmacen = d["DescAlmacen"].ToString(),
                                                         PesoNetoMaterial = d["PesoNetoMaterial"].ToString() == "" ? d["PesoNetoMaterial"] : Convert.ToDecimal(d["PesoNetoMaterial"].ToString()),
                                                         PesoBrutoMaterial = d["PesoBrutoMaterial"].ToString() == "" ? d["PesoBrutoMaterial"] : Convert.ToDecimal(d["PesoBrutoMaterial"].ToString()),
                                                         VolumenMaterial = d["VolumenMaterial"].ToString() == "" ? d["VolumenMaterial"] : Convert.ToDecimal(d["VolumenMaterial"].ToString()),
                                                         CantPedido = d["CantidadPedido"].ToString() == "" ? d["CantidadPedido"] : Convert.ToDecimal(d["CantidadPedido"].ToString()),
                                                         CantEntrega = d["CantidadEntrega"].ToString() == "" ? d["CantidadEntrega"] : Convert.ToDecimal(d["CantidadEntrega"].ToString()),

                                                         CantidadAntigua = d["CantidadAntigua"].ToString() == "" ? d["CantidadAntigua"] : Convert.ToDecimal(d["CantidadAntigua"].ToString()),


                                                         PesoNeto = d["PesoNeto"].ToString() == "" ? d["PesoNeto"] : Convert.ToDecimal(d["PesoNeto"].ToString()),
                                                         PesoBruto = d["PesoBruto"].ToString() == "" ? d["PesoBruto"] : Convert.ToDecimal(d["PesoBruto"].ToString()),

                                                         Anulado = Convert.ToInt64(d["Anulado"]),

                                                         DetalleLotes = (from e in dt.AsEnumerable()
                                                                         where Convert.ToInt32(e["AnuladoLote"]) == 0 && Convert.ToInt64(d["DDespachoEnrega"]).Equals(Convert.ToInt64(e["DDespachoEntrega"]))
                                                                         select new
                                                                         {
                                                                             DDespachoLotes = Convert.ToInt64(e["DDespachoLotes"]),
                                                                             PosicionDespacho = Convert.ToInt64(e["PosicionEntrega"]),
                                                                             Material = Convert.ToInt64(e["Material"]),
                                                                             CodMaterial = e["CodMaterial"].ToString(),
                                                                             UnidadMedidaMaterial = e["UnidadMaterial"].ToString(),
                                                                             DescUnidadMedida = e["DescUnidadMedida"].ToString(),
                                                                             Cantidad = Convert.ToDecimal(e["Cantidad"]),
                                                                             Almacen = Convert.ToInt64(e["AlmacenLote"]),
                                                                             Lotes = e["Lote"].ToString(),
                                                                             DesAlmacenLote = e["DesAlmacenLote"].ToString(),
                                                                             Valido = 1,

                                                                             PesoNetoMaterial = Convert.ToDecimal(d["PesoNetoMaterial"].ToString()),
                                                                             PesoBrutoMaterial = Convert.ToDecimal(d["PesoBrutoMaterial"].ToString()),
                                                                             PesoBruto = Convert.ToDecimal(e["PesoBrutoLote"].ToString()),
                                                                             PesoNeto = Convert.ToDecimal(e["PesoNetoLote"].ToString()),
                                                                             Anulado = Convert.ToInt64(e["AnuladoLote"]),

                                                                         }).GroupBy(e => new { e.DDespachoLotes }).Select(e => e.First()),

                                                         DetalleLotesEliminados = (from f in dt.AsEnumerable()
                                                                                   where Convert.ToInt32(f["AnuladoLote"]) == 1 && Convert.ToInt64(d["DDespachoEnrega"]).Equals(Convert.ToInt64(f["DDespachoEntrega"]))
                                                                                   select new
                                                                                   {
                                                                                       DDespachoLotes = Convert.ToInt64(f["DDespachoLotes"]),
                                                                                       PosicionDespacho = Convert.ToInt64(f["PosicionEntrega"]),
                                                                                       Material = Convert.ToInt64(f["Material"]),
                                                                                       CodMaterial = f["CodMaterial"].ToString(),
                                                                                       UnidadMedidaMaterial = f["UnidadMaterial"].ToString(),
                                                                                       DescUnidadMedida = f["DescUnidadMedida"].ToString(),
                                                                                       Cantidad = Convert.ToDecimal(f["Cantidad"]),
                                                                                       Almacen = Convert.ToInt64(f["AlmacenLote"]),
                                                                                       Lotes = f["Lote"].ToString(),
                                                                                       DesAlmacenLote = f["DesAlmacenLote"].ToString(),
                                                                                       Valido = 1,

                                                                                       PesoNetoMaterial = Convert.ToDecimal(d["PesoNetoMaterial"].ToString()),
                                                                                       PesoBrutoMaterial = Convert.ToDecimal(d["PesoBrutoMaterial"].ToString()),
                                                                                       PesoBruto = Convert.ToDecimal(f["PesoBrutoLote"].ToString()),
                                                                                       PesoNeto = Convert.ToDecimal(f["PesoNetoLote"].ToString()),
                                                                                       Anulado = Convert.ToInt64(f["AnuladoLote"]),

                                                                                   }).GroupBy(f => new { f.DDespachoLotes }).Select(f => f.First()),

                                                     }).GroupBy(d => new { d.DDespachoEnrega }).Select(d => d.First()),

                           }).GroupBy(c => new { c.DespachoEntrega }).Select(c => c.First());

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
        [ActionName("SD_getDatosPedidoVentaPosiciones")]
        public IHttpActionResult SD_getPosicionesPedidoVenta(int PedidoVenta)
        {
            try
            {
                DataTable dt = Instancia.getDatosPedidoVentaPosiciones(PedidoVenta);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpGet]
        [ActionName("SD_ListarLotesAlmacenDespacho")]
        public IHttpActionResult SD_ListarLotesAlmacenDespacho(int Material, int Almacen)
        {
            try
            {
                DataTable dt = Instancia.getListarLotesAlmacenDespacho(Material, Almacen);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpGet]
        [ActionName("SD_ListarOrigenDestinoDespacho")]
        public IHttpActionResult SD_ListarOrigenDestinoDespacho(int Tipo)
        {
            try
            {
                DataTable dt = Instancia.getListarOrigenDestinoDespacho(Tipo, JWT.IdEmpresa);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpGet]
        [ActionName("SD_RptDespacho")]
        public IHttpActionResult PP_RptOrdenTransporte(int prmintSociedad, int prmintTipoMaterial, int prmintGrupoArticulo, int prmintMaterial, int prmintCliente, int prmintOrgVta, string prmstrEstado, DateTime prmdateFechaInicio, DateTime prmdateFechaFin)
        {
            try
            {
                DataTable dt = Instancia.GetRptDespacho(prmintSociedad, prmintTipoMaterial, prmintGrupoArticulo, prmintMaterial, prmintCliente, prmintOrgVta, prmstrEstado, prmdateFechaInicio, prmdateFechaFin);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }
    }
}
