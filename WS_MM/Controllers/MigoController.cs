using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;

using static WS_MM.Models.MigoModel;
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
    public class MigoController : BaseController
    {
        [HttpGet]
        [ActionName("PMMM_GetTipoMovimiento")]
        public HttpResponseMessage PMMM_GetTipoMovimiento(int prmintEmpresa, Boolean? prmbitActivo)
        {
            try
            {
                DataTable dt = Instancia.GetTipoMovimiento(prmintEmpresa, prmbitActivo);

                var obj = (from c in dt.AsEnumerable()
                           select new
                           {
                               TipoMovimiento = Convert.ToInt64(c["TipoMovimiento"]),
                               NombreTipoMovimiento = c["NombreTipoMovimiento"].ToString(),
                               Movimiento = Convert.ToInt64(c["Movimiento"]),
                               CodMovimiento = c["CodMovimiento"].ToString(),

                               TipoDocumento = (from d in dt.AsEnumerable()
                                                where Convert.ToInt64(c["TipoMovimiento"]).Equals(Convert.ToInt64(d["TipoMovimiento"]))
                                                select new
                                                {
                                                    TipoMovimientoTipoDoc = Convert.ToInt64(d["TipoMovimientoTipoDoc"]),
                                                    TipoDocumento = Convert.ToInt64(d["TipoDocumento"]),
                                                    NombreDocumento = d["NombreDocumento"].ToString(),
                                                    CodDocumento = d["CodDocumento"].ToString(),
                                                    ActTipoMovTipoDoc = Convert.ToInt32(d["ActTipoMovTipoDoc"]),


                                                    Componentes = (from f in dt.AsEnumerable()
                                                                   where Convert.ToInt64(d["TipoMovimientoTipoDoc"]).Equals(Convert.ToInt64(f["TipoMovimientoTipoDoc"]))
                                                                   select new
                                                                   {
                                                                       ConfiguracionMigo = Convert.ToInt64(f["ConfiguracionMigo"]),
                                                                       Campo = f["Campo"].ToString(),
                                                                       Visible = Convert.ToBoolean(f["Visible"]),
                                                                   }).GroupBy(f => new { f.ConfiguracionMigo }).Select(f => f.First()),



                                                }).GroupBy(d => new { d.TipoMovimientoTipoDoc }).Select(d => d.First()),



                           }).GroupBy(c => new { c.TipoMovimiento }).Select(c => c.First());

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
        [ActionName("PMMM_GetCabeceraDetalleDocumento")]
        public HttpResponseMessage PMMM_GetCabeceraDetalleDocumento(string prmstrTipoMovimiento, string prmstrTipoDocumento, string prmstrDocumento)
        {
            try
            {
                DataTable dt = Instancia.GetCabeceraDetalleDocuemento(prmstrTipoMovimiento, prmstrTipoDocumento, prmstrDocumento, JWT.IdEmpresa);

                string Respuesta = dt.Rows[0]["Respuesta"].ToString();

                if (Respuesta == "0")
                {
                    var obj = (from c in dt.AsEnumerable()
                               select new
                               {
                                   IDDocumento = Convert.ToInt64(c["IDDocumento"]),
                                   DocumentoMaterial = Convert.ToInt64(c["DocumentoMaterial"]),
                                   GuiaRemision = Convert.ToInt64(c["GuiaRemision"]),
                                   OrdenCompra = Convert.ToInt64(c["OrdenCompra"]),
                                   OrdenProduccion = Convert.ToInt64(c["OrdenProduccion"]),
                                   OrdenTransporte = Convert.ToInt64(c["OrdenTransporte"]),
                                   EmiteGuiaRemision = Convert.ToInt64(c["EmiteGuiaRemision"]),
                                   AlmacenOrigen = Convert.ToInt64(c["AlmacenOrigen"]),
                                   AlmacenDestino = Convert.ToInt64(c["AlmacenDestino"]),
                                   Transportista = Convert.ToInt64(c["Transportista"]),
                                   Conductor = Convert.ToInt64(c["Conductor"]),
                                   Vehiculo = Convert.ToInt64(c["Vehiculo"]),



                                   Codigo = c["Codigo"].ToString(),
                                   CodMaterial = c["CodMaterial"].ToString(),
                                   CabDescMaterial = c["CabDescMaterial"].ToString(),
                                   Respuesta = c["Respuesta"].ToString(),
                                   TipoMoneda = c["TipoMoneda"].ToString(),
                                   DesTipoMoneda = c["DesTipoMoneda"].ToString(),
                                   

                                   PedidoVenta = c["PedidoVenta"].ToString(),
                                   Posicion = c["Posicion"].ToString(),
                                   TasaCambio = Convert.ToDecimal(c["TasaCambio"].ToString()),
                                   
                                   ConceptoDocumento = c["ConceptoDocumento"],

                                   IgvPorcentaje = Convert.ToDecimal(c["IgvPorcentaje"].ToString()),
                                   IncluyeIgv = Convert.ToBoolean(c["IncluyeIgv"].ToString()),
                                   Igv = Convert.ToDecimal(c["Igv"].ToString()),

                                   RentaPorcentaje = Convert.ToDecimal(c["RentaPorcentaje"].ToString()),
                                   IncluyeRenta = Convert.ToBoolean(c["IncluyeRenta"].ToString()),
                                   Renta = Convert.ToDecimal(c["Renta"].ToString()),



                                   SubTotal = Convert.ToDecimal(c["SubTotal"].ToString()),
                                   DtoCabecera = Convert.ToDecimal(c["DtoCabecera"].ToString()),
                                   
                                   
                                   Total = Convert.ToDecimal(c["Total"].ToString()),

                                   Detalles = (from d in dt.AsEnumerable()
                                               where Convert.ToInt64(c["IDDocumento"]).Equals(Convert.ToInt64(d["IDDocumento"]))
                                               select new
                                               {
                                                   DetalleId = Convert.ToInt64(d["DetalleId"]),
                                                   DMigo = 0,
                                                   Material = Convert.ToInt64(d["Material"].ToString()),
                                                   ProductoControlado = Convert.ToInt64(d["ProductoControlado"]),
                                                   Restringido = Convert.ToInt64(d["Restringido"]),
                                                   Inafecto = Convert.ToInt64(d["Inafecto"]),
                                                   CodDetaMaterial = d["CodDetaMaterial"].ToString(),
                                                   DescMaterial = d["DescMaterial"].ToString(),
                                                   //Cantidad = d["Cantidad"].ToString(),
                                                   DetallePrecioUnitario = Convert.ToDecimal(d["DetallePrecioUnitario"].ToString()),
                                                   PrecioUnitarioEntrada = Convert.ToDecimal(d["PrecioUnitarioEntrada"].ToString()),
                                                   DetalleTotal = Convert.ToDecimal(d["DetalleTotal"].ToString()),
                                                   DetalleDescuento = Convert.ToDecimal(d["DetalleDescuento"].ToString()),
                                                   //UnidadMedida = Convert.ToInt64(d["UnidadMedida"]),
                                                   DescUnidadMedida = d["DetalleDescUnidadAlmacen"].ToString(),
                                                   UnidadAgricola = d["UnidadAgricola"].ToString() == "" ? null : d["UnidadAgricola"],
                                                   DescUnidadAgricola = d["DescUnidadAgricola"].ToString() == "" ? null : d["DescUnidadAgricola"].ToString(),
                                                   Modulo = d["Modulo"].ToString() == "" ? null : d["Modulo"],
                                                   DescModulo = d["DescModulo"].ToString() == "" ? null : d["DescModulo"].ToString(),
                                                   Turno = d["Turno"].ToString() == "" ? null : d["Turno"],
                                                   DescTurno = d["DescTurno"].ToString() == "" ? null : d["DescTurno"].ToString(),
                                                   DetalleFechaCosecha = d["DetalleFechaCosecha"].ToString() == "" ? null : d["DetalleFechaCosecha"].ToString(),
                                                   DetalleCantidadEntrada = Convert.ToDecimal(d["DetalleCantidadEntrada"].ToString()),
                                                   DetalleUnidadEntrada = d["DetalleUnidadEntrada"].ToString() == "" ? 0 : Convert.ToInt64(d["DetalleUnidadEntrada"]),
                                                   Almacen = d["Almacen"].ToString() == "" ? 0 : Convert.ToInt64(d["Almacen"]),
                                                   DescAlmacen = d["DescAlmacen"].ToString(),
                                                   Centro = d["DescCentro"].ToString(),

                                                   PedidoVenta = d["PedidoVenta"].ToString(),
                                                   Posicion = d["Posicion"].ToString(),

                                                   MotivoTraslado = d["MotivoTraslado"].ToString(),
                                                   nombreMotivoTraslado = d["nombreMotivoTraslado"].ToString(),

                                                   ValorUnitario = Convert.ToDecimal(d["ValorUnitario"].ToString()),
                                                   PrecioUnitario = Convert.ToDecimal(d["PrecioUnitario"].ToString()),
                                                   DescuentoPrecio = Convert.ToDecimal(d["DescuentoPrecio"].ToString()),
                                                   DescuentoValor = Convert.ToDecimal(d["DescuentoValor"].ToString()),
                                                   ValorTotal = Convert.ToDecimal(d["ValorTotal"].ToString()),
                                                   PrecioTotal = Convert.ToDecimal(d["PrecioTotal"].ToString()),
                                                   Merma = Convert.ToDecimal(d["Merma"].ToString()),
                                                   CantidadReal = Convert.ToDecimal(d["CantidadReal"].ToString()),
                                                   CantidadRecibido = Convert.ToDecimal(d["CantidadRecibido"].ToString()),
                                                   FactorEquivalenciaKg = Convert.ToDecimal(d["FactorEquivalenciaKg"].ToString()),
                                                   CantidadKg = Convert.ToDecimal(d["CantidadKg"].ToString()),
                                                   FactorDistribucion = Convert.ToDecimal(d["FactorDistribucion"].ToString()),
                                                   FactorDistribucionKg = Convert.ToDecimal(d["FactorDistribucionKg"].ToString()),                                                   
                                                   Lote = d["Lote"].ToString() == "" ? null : d["Lote"].ToString(),                                                
                                                   DetalleCantidadAlmacen = d["DetalleCantidadAlmacen"].ToString() == "" ? 0 : Convert.ToDecimal(d["DetalleCantidadAlmacen"]),
                                                   // DetalleCantidadAlmacen = Convert.ToDecimal(d["DetalleCantidadAlmacen"].ToString()),
                                                   DetalleUnidadAlmacen = d["DetalleUnidadAlmacen"].ToString() == "" ? 0 : Convert.ToInt64(d["DetalleUnidadAlmacen"]),
                                                   DetalleDescUnidadAlmacen = d["DetalleDescUnidadAlmacen"].ToString(),
                                                   DetalleDescUnidadEntrada = d["DetalleDescUnidadEntrada"].ToString(),
                                                   OrdenProduccion = d["Codigo"].ToString() == "" ? null : d["Codigo"].ToString(),
                                                   DetalleFechaFabricacion = d["DetalleFechaFabricacion"].ToString() == "" ? null : d["DetalleFechaFabricacion"].ToString(),
                                                   DetalleFechaVencimiento = d["DetalleFechaVencimiento"].ToString() == "" ? null : d["DetalleFechaVencimiento"].ToString(),
                                               }).GroupBy(d => new { d.DetalleId }).Select(d => d.First()),
                               }).GroupBy(c => new { c.IDDocumento }).Select(c => c.First());

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
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpPost]
        [ActionName("PMMM_SaveUpdateMigo")]
        public HttpResponseMessage PMMM_SaveUpdateMigo(dynamic obj)
        {
            try
            {
                string xml = "<?xml version='1.0' encoding='ISO-8859-1'?><root>";
                string xmlDMigo = "<?xml version='1.0' encoding='ISO-8859-1'?><root>";


                xml += "<Migo ";
                xml += "Migo ='" + obj.Migo + "' ";
                xml += "Empresa ='" + JWT.IdEmpresa + "' ";
                xml += "Sociedad ='" + JWT.IdSociedad + "' ";
                xml += "Correlativo ='" + obj.Correlativo + "' ";
                xml += "TipoMovimientoTipoDoc ='" + obj.TipoMovimientoTipoDoc + "' ";
                xml += "Movimiento ='" + obj.Movimiento + "' ";
                xml += "OrdenCompra ='" + obj.OrdenCompra + "' ";
                xml += "OrdenProduccion ='" + obj.OrdenProduccion + "' ";
                xml += "OrdenTransporte ='" + obj.OrdenTransporte + "' ";
                xml += "GuiaRemision ='" + obj.GuiaRemision + "' ";

                xml += "DocumentoMaterial ='" + obj.DocumentoMaterial + "' ";
                xml += "FechaDocumento ='" + obj.FechaDocumento + "' ";
                xml += "FechaContabilizacion ='" + obj.FechaContabilizacion + "' ";
                xml += "IncluyeIgv ='" + obj.IncIGV + "' ";
                xml += "IncluyeRenta ='" + obj.IncRenta + "' ";
                xml += "ConceptoDocumento ='" + obj.ConceptoDocumento + "' ";
                xml += "SerieGuia ='" + obj.SerieGuia + "' ";
                xml += "NumeroGuia ='" + obj.NumeroGuia + "' ";
                xml += "TasaCambio ='" + obj.TipoCambio + "' ";
                xml += "AlmacenOrigen ='" + obj.AlmacenOrigen + "' ";
                xml += "AlmacenDestino ='" + obj.AlmacenDestino + "' ";
                xml += "Transportista ='" + obj.Transportista + "' ";
                xml += "Vehiculo ='" + obj.Vehiculo + "' ";
                xml += "Conductor ='" + obj.Conductor + "' ";
                xml += "Flete ='" + obj.Flete.ToString(System.Globalization.CultureInfo.InvariantCulture) + "' ";
                xml += "SubTotal ='" + obj.SubTotal.ToString(System.Globalization.CultureInfo.InvariantCulture) + "' ";
                xml += "Descuento ='" + obj.Descuento.ToString(System.Globalization.CultureInfo.InvariantCulture) + "' ";
                xml += "IgvPorcentaje ='" + obj.IgvPorcentaje.ToString(System.Globalization.CultureInfo.InvariantCulture) + "' ";
                xml += "RentaPorcentaje ='" + obj.RentaPorcentaje.ToString(System.Globalization.CultureInfo.InvariantCulture) + "' ";                
                xml += "Igv ='" + obj.Igv.ToString(System.Globalization.CultureInfo.InvariantCulture) + "' ";
                xml += "Renta ='" + obj.Renta.ToString(System.Globalization.CultureInfo.InvariantCulture) + "' ";                
                xml += "Total ='" + obj.Total.ToString(System.Globalization.CultureInfo.InvariantCulture) + "' ";
                xml += "Estado ='" + obj.Estado + "' ";
                xml += "Anulado ='" + obj.Anulado + "' />";

                foreach (dynamic dm in obj.DMigo)
                {
                    xmlDMigo += "<DMigo ";
                    xmlDMigo += "DMigo = '" + dm.DMigo + "' ";
                    xmlDMigo += "DOrdenCompra = '" + dm.DetalleOrdenCompra + "' ";
                    xmlDMigo += "DOrdenProduccion = '" + dm.DetalleOrdenProduccion + "' ";
                    xmlDMigo += "Migo = '" + dm.Migo + "' ";
                    xmlDMigo += "Empresa = '" + JWT.IdEmpresa + "' ";
                    xmlDMigo += "Material = '" + dm.Material + "' ";
                    xmlDMigo += "Almacen = '" + dm.Almacen + "' ";
                    xmlDMigo += "UnidadAgricola = '" + dm.UnidadAgricola + "' ";
                    xmlDMigo += "Modulo = '" + dm.Modulo + "' ";
                    xmlDMigo += "Campo = '" + dm.Campo + "' ";
                    xmlDMigo += "PrecioUnitario = '" + dm.PrecioUnitario.ToString(System.Globalization.CultureInfo.InvariantCulture) + "' ";
                    // xmlDMigo += "Descuento = '" + dm.Descuento.ToString(System.Globalization.CultureInfo.InvariantCulture) + "' ";

                    if (dm.CantidadEntrada == null || dm.CantidadEntrada.ToString() == "" || dm.CantidadEntrada.ToString() == "0")
                    {
                        xmlDMigo += "CantidadEntrada = '" + dm.CantidadAlmacen.ToString(System.Globalization.CultureInfo.InvariantCulture) + "' ";
                        xmlDMigo += "UnidadMedidaEntrada = '" + dm.UnidadMedidaAlmacen + "' ";
                    }
                    else
                    {
                        xmlDMigo += "CantidadEntrada = '" + dm.CantidadEntrada.ToString(System.Globalization.CultureInfo.InvariantCulture) + "' ";
                        xmlDMigo += "UnidadMedidaEntrada = '" + dm.UnidadMedidaEntrada + "' ";
                    }

                    //xmlDMigo += "CantidadEntrada = '" + dm.CantidadEntrada.ToString(System.Globalization.CultureInfo.InvariantCulture) + "' ";
                    //xmlDMigo += "UnidadMedidaEntrada = '" + dm.UnidadMedidaEntrada + "' ";
                    xmlDMigo += "PrecioTotal = '" + dm.PrecioTotal.ToString(System.Globalization.CultureInfo.InvariantCulture) + "' ";
                    xmlDMigo += "ValorUnitario = '" + dm.ValorUnitario.ToString(System.Globalization.CultureInfo.InvariantCulture) + "' ";
                    xmlDMigo += "DescuentoValor = '" + dm.DescuentoValor.ToString(System.Globalization.CultureInfo.InvariantCulture) + "' ";
                    xmlDMigo += "Restringido = '" + dm.Restringido.ToString(System.Globalization.CultureInfo.InvariantCulture) + "' ";
                    xmlDMigo += "Merma = '" + dm.Merma.ToString(System.Globalization.CultureInfo.InvariantCulture) + "' ";
                    xmlDMigo += "CantidadReal = '" + dm.CantidadReal.ToString(System.Globalization.CultureInfo.InvariantCulture) + "' ";
                    xmlDMigo += "DescuentoPrecio = '" + dm.DescuentoPrecio.ToString(System.Globalization.CultureInfo.InvariantCulture) + "' ";
                    xmlDMigo += "ValorTotal = '" + dm.ValorTotal.ToString(System.Globalization.CultureInfo.InvariantCulture) + "' ";
                    xmlDMigo += "CantidadAlmacen = '" + dm.CantidadAlmacen.ToString(System.Globalization.CultureInfo.InvariantCulture) + "' ";
                    xmlDMigo += "MotivoTraslado = '" + dm.MotivoTraslado + "' ";
                    xmlDMigo += "FactorEquivalenciaKg = '" + dm.FactorEquivalenciaKg.ToString(System.Globalization.CultureInfo.InvariantCulture) + "' ";
                    xmlDMigo += "CantidadKg = '" + dm.CantidadKg.ToString(System.Globalization.CultureInfo.InvariantCulture) + "' ";
                    xmlDMigo += "FactorDistribucion = '" + dm.FactorDistribucion.ToString(System.Globalization.CultureInfo.InvariantCulture) + "' ";
                    xmlDMigo += "FactorDistribucionKg = '" + dm.FactorDistribucionKg.ToString(System.Globalization.CultureInfo.InvariantCulture) + "' ";
                    xmlDMigo += "UnidadMedidaAlmacen = '" + dm.UnidadMedidaAlmacen + "' ";
                    xmlDMigo += "Lote = '" + dm.Lote + "' ";


                    if (dm.FechaCosecha.ToString() != "")
                    {
                        xmlDMigo += "FechaCosecha = '" + dm.FechaCosecha + "' ";
                    }

                    if (dm.FechaFabricacion.ToString() != "")
                    {
                        xmlDMigo += "FechaFabricacion = '" + dm.FechaFabricacion + "' ";
                    }

                    if (dm.FechaVencimiento.ToString() != "")
                    {
                        xmlDMigo += "FechaVencimiento = '" + dm.FechaVencimiento + "' ";
                    }



                    //xmlDMigo += "FechaFabricacion = '" + dm.FechaFabricacion + "' ";
                    //xmlDMigo += "FechaVencimiento = '" + dm.FechaVencimiento + "' ";
                    xmlDMigo += "CentroCosto = '" + dm.CentroCosto + "' ";
                    xmlDMigo += "Campania = '" + dm.Campania + "' ";
                    xmlDMigo += "ClaseValor = '" + dm.ClaseValor + "' ";
                    xmlDMigo += "ItemValor = '" + dm.ItemValor + "' ";
                    xmlDMigo += "Anulado = '" + dm.Anulado + "' />";
                }

                xml += "</root>";
                xmlDMigo += "</root>";

                int res = Instancia.SaveUpdateMigo(JWT.Login, xml, xmlDMigo);
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

                if (res == -3)
                {
                    success = true;
                    mensaje = "El periodo para este documento no esta abierto.";
                }

                if (res == -4)
                {
                    success = true;
                    mensaje = "Uno de los materiales no tiene equivalencia con KG.";
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
        [ActionName("PMMM_GetCabeceraDetalleDocumentoMigo")]
        public HttpResponseMessage PMMM_GetCabeceraDetalleDocumentoMigo(int prmintoDocumentoMigo)
        {
            try
            {
                DataTable dt = Instancia.GetCabeceraDetalleDocumentoMigo(prmintoDocumentoMigo);

                var obj = (from c in dt.AsEnumerable()
                           select new
                           {
                               Migo = Convert.ToInt64(c["Migo"]),
                               TipoMovimiento = Convert.ToInt64(c["TipoMovimiento"]),
                               GuiaRemision = Convert.ToInt64(c["GuiaRemision"]),
                               TipoMovimientoTipoDoc = Convert.ToInt64(c["TipoMovimientoTipoDoc"]),

                               AlmacenOrigen = c["AlmacenOrigen"].ToString() == "" ? 0 : Convert.ToInt64(c["AlmacenOrigen"]),
                               AlmacenDestino = c["AlmacenDestino"].ToString() == "" ? 0 : Convert.ToInt64(c["AlmacenDestino"]),
                               Transportista = c["Transportista"].ToString() == "" ? 0 : Convert.ToInt64(c["Transportista"]),
                               Vehiculo = c["Vehiculo"].ToString() == "" ? 0 : Convert.ToInt64(c["Vehiculo"]),
                               Conductor = c["Conductor"].ToString() == "" ? 0 : Convert.ToInt64(c["Conductor"]),
                               ConceptoDocumento = c["ConceptoDocumento"],
                               CodCabeceraMaterial = c["CodCabeceraMaterial"].ToString(),
                               DescCabeceraMaterial = c["DescCabeceraMaterial"].ToString(),
                               CodDocumento = c["CodDocumento"].ToString(),
                               CorreMigo = c["CorreMigo"].ToString(),
                               Documento = c["Documento"].ToString(),
                               FechaDocumento = c["FechaDocumento"].ToString(),

                               DesTipoMoneda = c["DesTipoMoneda"].ToString(),
                               TasaCambio = Convert.ToDecimal(c["TasaCambio"].ToString()),


                               IncluyeIgv = Convert.ToBoolean(c["IncluyeIgv"].ToString()),
                               IncluyeRenta = Convert.ToBoolean(c["IncluyeRenta"].ToString()),
                               FechaContabilizacion = c["FechaContabilizacion"].ToString(),
                               SubTotal = Convert.ToDecimal(c["SubTotal"].ToString()),
                               IgvPorcentaje = Convert.ToDecimal(c["IgvPorcentaje"].ToString()),
                               RentaPorcentaje = Convert.ToDecimal(c["RentaPorcentaje"].ToString()),
                               Renta = Convert.ToDecimal(c["Renta"].ToString()),
                               Igv = Convert.ToDecimal(c["Igv"].ToString()),
                               Total = Convert.ToDecimal(c["Total"].ToString()),
                               Descuento = Convert.ToDecimal(c["Descuento"].ToString()),
                               Flete = c["Flete"].ToString() == "" ? 0 : Convert.ToDecimal(c["Flete"].ToString()),
                               Estado = c["Estado"].ToString(),
                               SerieGuia = c["SerieGuia"].ToString(),
                               NumeroGuia = c["NumeroGuia"].ToString(),

                               DetallesMigo = (from d in dt.AsEnumerable()
                                               where Convert.ToInt64(c["Migo"]).Equals(Convert.ToInt64(d["Migo"]))
                                               select new
                                               {
                                                   DMigo = Convert.ToInt64(d["DMigo"]),
                                                   Material = Convert.ToInt64(d["Material"].ToString()),
                                                   CodDetaMaterial = d["CodDetaMaterial"].ToString(),
                                                   DescMaterial = d["DescMaterial"].ToString(),
                                                   Cantidad = d["CantidadEntrada"].ToString(),
                                                   OrdenProduccion = d["OrdenProduccion"].ToString(),
                                                   DetallePrecioUnitario = Convert.ToDecimal(d["DetallePrecioUnitario"].ToString()),
                                                   PrecioUnitarioEntrada = Convert.ToDecimal(d["PrecioUnitarioEntrada"].ToString()),
                                                   DetalleTotal = Convert.ToDecimal(d["DetalleTotal"].ToString()),
                                                   DetalleDescuento = Convert.ToDecimal(d["DetalleDescuento"].ToString()),
                                                   UnidadMedida = Convert.ToInt64(d["UnidadMedidaEntrada"]),
                                                   DescUnidadMedida = d["DescUnidadMedida"].ToString(),
                                                   UnidadAgricola = Convert.ToInt64(d["UnidadAgricola"].ToString()) == 0 ? 0 : Convert.ToInt64(d["UnidadAgricola"].ToString()),
                                                   DescUnidadAgricola = d["NombreUnidadAgricola"].ToString() == "" ? null : d["NombreUnidadAgricola"].ToString(),
                                                   Modulo = Convert.ToInt64(d["Modulo"].ToString()) == 0 ? 0 : Convert.ToInt64(d["Modulo"].ToString()),
                                                   DescModulo = d["NombreModulo"].ToString() == "" ? null : d["NombreModulo"].ToString(),
                                                   Turno = Convert.ToInt64(d["Campo"].ToString()) == 0 ? 0 : Convert.ToInt64(d["Campo"].ToString()),
                                                   DescTurno = d["NombreCampo"].ToString() == "" ? null : d["NombreCampo"].ToString(),
                                                   DetalleFechaCosecha = d["FechaCosecha"].ToString() == "" ? null : d["FechaCosecha"].ToString(),
                                                   DetalleCantidadEntrada = Convert.ToDecimal(d["CantidadEntrada"].ToString()),
                                                   DetalleUnidadEntrada = d["UnidadMedidaEntrada"].ToString() == "" ? 0 : Convert.ToInt64(d["UnidadMedidaEntrada"]),
                                                   DetalleDescUnidadEntrada = d["DescUnidadMedida"].ToString(),


                                                   nombreMotivoTraslado = d["nombreMotivoTraslado"].ToString(),
                                                   CantidadKg = Convert.ToDecimal(d["CantidadKg"].ToString()),
                                                   FactorDistribucion = Convert.ToDecimal(d["FactorDistribucion"].ToString()),
                                                   FactorDistribucionKg = Convert.ToDecimal(d["FactorDistribucionKg"].ToString()),


                                                   ValorUnitario = Convert.ToDecimal(d["ValorUnitario"].ToString()),
                                                   PrecioUnitario = Convert.ToDecimal(d["PrecioUnitario"].ToString()),
                                                   DescuentoPrecio = Convert.ToDecimal(d["DescuentoPrecio"].ToString()),
                                                   DescuentoValor = Convert.ToDecimal(d["DescuentoValor"].ToString()),
                                                   ValorTotal = Convert.ToDecimal(d["ValorTotal"].ToString()),
                                                   PrecioTotal = Convert.ToDecimal(d["PrecioTotal"].ToString()),
                                                   Merma = Convert.ToDecimal(d["Merma"].ToString()),
                                                   Almacen = Convert.ToInt64(d["Almacen"].ToString()),
                                                   DescAlmacen = d["DescAlmacen"].ToString() == "" ? null : d["DescAlmacen"].ToString(),
                                                   Centro = d["Centro"].ToString() == "" ? null : d["Centro"].ToString(),
                                                   Lote = d["Lote"].ToString() == "" ? null : d["Lote"].ToString(),
                                                   DetalleCantidadAlmacen = Convert.ToDecimal(d["CantidadAlmacen"].ToString()),
                                                   DetalleUnidadAlmacen = d["UnidadMedidaAlmacen"].ToString() == "" ? 0 : Convert.ToInt64(d["UnidadMedidaAlmacen"]),
                                                   DetalleDescUnidadAlmacen = d["DetalleDescUnidadAlmacen"].ToString(),
                                                   DetalleFechaFabricacion = d["DetalleFechaFabricacion"].ToString() == "" ? null : d["DetalleFechaFabricacion"].ToString(),
                                                   DetalleFechaVencimiento = d["DetalleFechaVencimiento"].ToString() == "" ? null : d["DetalleFechaVencimiento"].ToString(),
                                               }).GroupBy(d => new { d.DMigo }).Select(d => d.First()),
                           }).GroupBy(c => new { c.Migo }).Select(c => c.First());

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
        [ActionName("PMMM_GetEstadoMIGO")]
        public IHttpActionResult PMMM_GetEstadoMIGO()
        {
            try
            {
                DataTable dt = Instancia.GetEstadoMIGO(JWT.IdEmpresa);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpPost]
        [ActionName("PMM_ValidarStockNegativo")]
        public IHttpActionResult PMM_ValidarStockNegativo(dynamic obj)
        {
            try
            {
                string xmlDMigo = "<?xml version='1.0' encoding='ISO-8859-1'?><root>";




                foreach (dynamic dm in obj)
                {
                    xmlDMigo += "<DMigo ";
                    xmlDMigo += "Empresa = '" + JWT.IdEmpresa + "' ";
                    xmlDMigo += "Material = '" + dm.Material + "' ";
                    xmlDMigo += "Almacen = '" + dm.Almacen + "' ";
                    xmlDMigo += "CantidadAlmacen = '" + dm.CantidadAlmacen.ToString(System.Globalization.CultureInfo.InvariantCulture) + "' ";
                    xmlDMigo += "UnidadMedidaAlmacen = '" + dm.UnidadMedidaAlmacen + "' ";
                    xmlDMigo += "Lote = '" + dm.Lote + "' />";
                }

                xmlDMigo += "</root>";

                DataTable dt = Instancia.ValidarStockNegativo(xmlDMigo);
                return Ok(dt);



            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpGet]
        [ActionName("PMMM_GetPorcentajeIGV")]
        public IHttpActionResult PMMM_GetPorcentajeIGV(int? prmbitActivo)
        {
            try
            {
                DataTable dt = Instancia.GetPorcentajeIGV(JWT.IdEmpresa, prmbitActivo);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpGet]
        [ActionName("PMMM_GetDocumentoMaterial")]
        public IHttpActionResult PMMM_GetDocumentoMaterial(string FechaInicio, string FechaFin, string TipoMovimiento, string TipoDocumento)
        {
            try
            {
                DataTable dt = Instancia.GetDocumentoMaterial(FechaInicio, FechaFin, TipoMovimiento, TipoDocumento);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpGet]
        [ActionName("PMMM_GetUnidadMedidaEquivalencia")]
        public IHttpActionResult PMMM_GetUnidadMedidaEquivalencia(int Material, int Almacen)
        {
            try
            {
                DataTable dt = Instancia.GetUnidadMedidaEquivalencia(Material, Almacen);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpGet]
        [ActionName("PMMM_GetDocumentoMaterialMigo")]
        public IHttpActionResult PMMM_GetDocumentoMaterialMigo(string FechaInicio, string FechaFin, string TipoMovimiento, string TipoDocumento)
        {
            try
            {
                DataTable dt = Instancia.GetDocumentoMaterialMigo(FechaInicio, FechaFin, TipoMovimiento, TipoDocumento, JWT.IdEmpresa, JWT.IdSociedad);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpGet]
        [ActionName("PP_getSearchMaterialesMigo")]
        public IHttpActionResult PP_getSearchMateriales(string CodMaterial, string Material, string TipoMaterial, string GrupoArticulo, int? Almacen)
        {
            try
            {
                DataTable dt = Instancia.getSearchMaterialesMigo(JWT.IdEmpresa, CodMaterial, Material, TipoMaterial, GrupoArticulo, Almacen);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpGet]
        [ActionName("PMMM_GetListadoOrdenesTransporteMigo")]
        public IHttpActionResult PMMM_GetListadoOrdenesTransporteMigo(string FechaInicio, string FechaFin)
        {
            try
            {
                DataTable dt = Instancia.GetListadoOrdenesTransporteMigo(FechaInicio, FechaFin, JWT.IdEmpresa, JWT.IdSociedad);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }


        [HttpGet]
        [ActionName("PMMM_GetListarTrasladosAlmacenes")]
        public IHttpActionResult PMMM_GetListarTrasladosAlmacenes(string FechaInicio, string FechaFin, int AlmacenOrigen, int AlmacenDestino)
        {
            try
            {
                DataTable dt = Instancia.GetListarTrasladosAlmacenes(FechaInicio, FechaFin, AlmacenOrigen, AlmacenDestino, JWT.IdEmpresa, JWT.IdSociedad);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }


        [HttpGet]
        [ActionName("PMMM_GetTasaCambioByFechaContabilizacion")]
        public IHttpActionResult PMMM_GetTasaCambioByFechaContabilizacion(string FechaContable, string MonedaOrigen, string MonedaDestino, int OrdenCompra, int OrdenTransporte)
        {
            try
            {
                DataTable dt = Instancia.GetTasaCambioByFechaContabilizacion(FechaContable, MonedaOrigen, MonedaDestino, JWT.IdEmpresa, OrdenCompra, OrdenTransporte);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }
        [HttpGet]
        [ActionName("PMMM_GetCentroCosto")]
        public IHttpActionResult PMMM_GetCentroCosto()
        {
            try
            {
                DataTable dt = Instancia.GetCentroCosto();
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }
        [HttpGet]
        [ActionName("PMMM_GetReservasMigo")]
        public IHttpActionResult PMMM_GetReservasMigo(string FechaInicio, string FechaFin)
        {
            try
            {
                DataTable dt = Instancia.GetReservasMigo(FechaInicio, FechaFin, JWT.IdEmpresa, JWT.IdSociedad);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }
        [HttpGet]
        [ActionName("PMMM_GetCentroCostoCampanyas")]
        public IHttpActionResult PMMM_GetCentroCostoCampanyas()
        {
            try
            {
                DataTable dt = Instancia.GetCentroCostoCampanya();
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpGet]
        [ActionName("PMMM_GetClaseValor")]
        public IHttpActionResult PMMM_GetClaseValor()
        {
            try
            {
                DataTable dt = Instancia.GetClaseValor(JWT.IdEmpresa, JWT.IdSociedad);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpGet]
        [ActionName("PMMM_GetItemValor")]
        public IHttpActionResult PMMM_GetItemValor()
        {
            try
            {
                DataTable dt = Instancia.GetItemValor(JWT.IdEmpresa, JWT.IdSociedad);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpGet]
        [ActionName("PMMM_GetReporteGuiasVentas")]
        public IHttpActionResult PMMM_GetReporteGuiasVentas(string prmdatFechaDesde, string prmdatFechaHasta)
        {
            try
            {
                DataTable dt = Instancia.GetReporteGuiasVentas(prmdatFechaDesde, prmdatFechaHasta, JWT.IdEmpresa, JWT.IdSociedad);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }
    }
}
