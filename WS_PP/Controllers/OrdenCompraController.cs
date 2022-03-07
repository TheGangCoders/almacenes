using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using static WS_PP.Models.OrdenCompraModelDAL;
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
    public class OrdenCompraController : BaseController
    {
        [HttpGet]
        [ActionName("getFromMultitabla")]
        public IHttpActionResult getFromMultitabla(string prmstrTabla, int prmintEmpresa)
        {
            try
            {
                DataTable dt = Instancia.getFromMultitabla(prmstrTabla, prmintEmpresa);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        
        [HttpGet]
        [ActionName("PP_getFormaValorizacion")]
        public IHttpActionResult PP_getFormaValorizacion()
        {
            try
            {
                DataTable dt = Instancia.getFormaValorizacion(JWT.IdEmpresa);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpGet]
        [ActionName("PP_getEstadoOrdenCompra")]
        public IHttpActionResult PP_getEstadoOrdenCompra()
        {
            try
            {
                DataTable dt = Instancia.getEstadoOrdenCompra(JWT.IdEmpresa);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }
        

        [HttpGet]
        [ActionName("PP_getOrdenCompra_List")]
        public IHttpActionResult PP_getOrdenCompra_List(string FechaInicio, string FechaFin, string Estado)
        {
            try
            {
                DataTable dt = Instancia.getOrdenCompra_List(FechaInicio, FechaFin, Estado);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpGet]
        [ActionName("PP_getOrdenCompraListMigo")]
        public IHttpActionResult PP_getOrdenCompraListMigo(string FechaInicio, string FechaFin)
        {
            try
            {
                DataTable dt = Instancia.GetOrdenCompraListMigo(FechaInicio, FechaFin, JWT.IdEmpresa, JWT.IdSociedad);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpGet]
        [ActionName("PP_getOrdenCompra_Data")]
        public IHttpActionResult PP_getOrdenCompra_Data(int prmintOrdenCompra)
        {
            try
            {
                DataTableCollection dt = Instancia.getOrdenCompra_Data(prmintOrdenCompra);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpPost]
        [ActionName("PP_saveUpdate_OrdenCompra")]
        public IHttpActionResult PP_saveUpdate_OrdenCompra(dynamic obj)
        {
            try
            {
                int idOrden = obj.OrdenCompra;
                int idSociedad = JWT.IdSociedad;
                int idEmpresa = JWT.IdEmpresa;
                int idProveedor = obj.Proveedor;
                Boolean IncluyeIgv = obj.IncluyeIgv;
                Boolean IncluyeRenta = obj.IncluyeRenta;
                Boolean Maquila = obj.Maquila;
                string codigo = obj.CodigoCotizacion;
                string Valorizacion = obj.Valorizacion;
                string tipoDocumento = obj.TipoDocumento;
                string formaPago = obj.FormaPago;
                string fechaEmision = obj.FechaEmision;
                string fechaEntrega = obj.FechaEntrega;
                string tipoMoneda = obj.TipoMoneda;
                string tasaCambio = obj.TasaCambio;
                string subTotal = obj.SubTotal;
                string descuento = obj.Descuento;
                string igvPorcentual = obj.IgvPorcentaje;
                string rentaPorcentual = obj.RentaPorcentaje; //
                string igv = obj.Igv; // este valor tambien funcionara como Renta cuanto IncluyeRenta=true y tipoDocumento='0003'
                string total = obj.Total;
                string xml = obj.strXML;
                string xml_MAQUILA = obj.strXML_MAQUILA;
                string ussername = obj.usser;
                string Estado = obj.Estado;

                string FechaInicio = obj.FechaInicio;
                string FechaFin = obj.FechaFin;

                string centro = obj.centro;
                string almacen = obj.almacen;
                string Nave = obj.Nave;
                string Observacion = obj.Observacion;
                string AlmacenDestino = obj.AlmacenDestino;
                string DireccionDestino = obj.DireccionDestino;
                int Area = obj.Area;




                int dt = Instancia.saveUpdate_OrdenCompra(idOrden, idSociedad, idEmpresa, idProveedor, IncluyeIgv, IncluyeRenta, Maquila, codigo, Valorizacion,
                                                        tipoDocumento, formaPago, fechaEmision, fechaEntrega, tipoMoneda, tasaCambio,
                                                        subTotal, descuento, igvPorcentual, rentaPorcentual, igv, total, centro, almacen, 
                                                        Nave, xml, xml_MAQUILA, ussername, Estado, FechaInicio,FechaFin, Observacion,
                                                        AlmacenDestino, DireccionDestino, Area);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpPost]
        [ActionName("PP_enableDisable_OrdenCompra")]
        public IHttpActionResult PP_enableDisable_OrdenCompra(dynamic obj)
        {
            try
            {
                int idOrden = obj.prmintOrdenCompra;
                Boolean bitStatus = obj.prmbitActivo;
                string Status = obj.prmstrEstado;
                string usuario = obj.prmstrUsuario;
                string idUsuario = obj.prmintUsuario;

                int dt = Instancia.enableDisable_OrdenCompra(idOrden, bitStatus, Status, usuario, idUsuario);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        
        [HttpPost]
        [ActionName("PP_generarOrdenProduccion_byOC")]
        public IHttpActionResult PP_generarOrdenProduccion_byOC(dynamic obj)
        {
            try
            {
                int idOrden = obj.OrdenCompra;
                string usuario = obj.usser;
                DataTable dt = Instancia.generarOrdenProduccion_byOC(idOrden,JWT.IdEmpresa , JWT.IdSociedad, usuario);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpPost]
        [ActionName("PP_liberar_masivo_OrdenCompra")]
        public IHttpActionResult PP_liberar_masivo_OrdenCompra(dynamic obj)
        {
            try
            {
                string XML = obj.param_xml;
                string status = obj.param_status;
                int dt = Instancia.liberar_masivo_OrdenCompra(XML, JWT.Login,JWT.IdUsuario, status);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpGet]
        [ActionName("getTasaActual")]
        public IHttpActionResult getTasaActual(string prmintEmpresa, int year, int mes, int dia, string fecha, string usuario)
        {

            try
            {
                //DataTable dt = Instancia.obtenerDatos_TASA_SUNAT(dia, mes, year);
                string sCompra = "0"; string sVenta = "0";
                //if (dt.Rows.Count > 0) {
                //    sCompra = (from DataRow dr in dt.AsEnumerable()
                //                      where Convert.ToString(dr["Dia"]) == dia.ToString()
                //                      select Convert.ToString(dr["Compra"])).FirstOrDefault();
                //    sVenta = (from DataRow dr in dt.AsEnumerable()
                //                     where Convert.ToString(dr["Dia"]) == dia.ToString()
                //                     select Convert.ToString(dr["Venta"])).FirstOrDefault();
                //}
                //se verifica posible inserción (DOLAR ONLY) y la tasa actual DE TODAS LAS MONEDAS
                DataTableCollection dt2 = Instancia.getTasaActual(JWT.IdEmpresa, sCompra, sVenta, fecha, usuario);
                return Ok(dt2); //debe retornar la tasa actual de cambio de todas las monedas
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpGet]
        [ActionName("PP_getOrdenCompra_List_Maquila")]
        public IHttpActionResult PP_getOrdenCompra_List_Maquila(string FechaInicio, string FechaFin, string Estado)
        {
            try
            {
                DataTable dt = Instancia.getOrdenCompra_List_Maquila(FechaInicio, FechaFin, Estado);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpGet]
        [ActionName("PP_ListarLotesAlmacenDespacho")]
        public IHttpActionResult PP_ListarLotesAlmacenDespacho(int Material, int Almacen)
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
        [ActionName("PP_ListarRecetasActividad")]
        public HttpResponseMessage PP_ListarRecetasActividad()
        {
            try
            {
                DataTable dt = Instancia.getRecetasActividad(JWT.IdEmpresa);
                var obj = (from c in dt.AsEnumerable()
                           select new
                           {
                               Receta = Convert.ToInt64(c["Receta"]),
                               CodReceta = c["CodReceta"].ToString(),
                               DescReceta = c["DescReceta"].ToString(),
                               DescCentro = c["DescCentro"].ToString(),
                               DescNave = c["DescNave"].ToString(),
                               UnidadReceta = c["UnidadReceta"].ToString(),

                               Actividades = (from d in dt.AsEnumerable()
                                                where Convert.ToInt64(c["Receta"]).Equals(Convert.ToInt64(d["Receta"]))
                                                select new
                                                {
                                                    Actividad = Convert.ToInt64(d["Actividad"]),
                                                    Receta = Convert.ToInt64(d["Receta"]),
                                                    Recurso = Convert.ToInt64(d["Recurso"]),
                                                    CodReceta = d["CodReceta"].ToString(),
                                                    DescActividad = d["DescActividad"].ToString(),
                                                }).GroupBy(d => new { d.Actividad }).Select(d => d.First()),                                            
                           }).GroupBy(c => new { c.Receta }).Select(c => c.First());

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
        [ActionName("PP_GetDatosOrdenCompraMaquila")]
        public HttpResponseMessage PP_GetDatosOrdenCompraMaquila(int OrdenMaquila)
        {
            try
            {
                DataTable dt = Instancia.getDatosOrdenCompraMaquila(OrdenMaquila);
                var obj = (from c in dt.AsEnumerable()
                           select new
                           {
                               OrdenCompra = Convert.ToInt64(c["OrdenCompra"]),
                               TipoMaquila = c["TipoMaquila"].ToString(),
                               Proveedor = Convert.ToInt64(c["Proveedor"]),
                               DescProveedor = c["DescProveedor"].ToString(),
                               RUCProveedor = c["RUCProveedor"].ToString(),
                               DirecProveedor = c["DirecProveedor"].ToString(),                               
                               CodigoCotizacion = c["CodigoCotizacion"].ToString(),
                               TipoDocumento = c["TipoDocumento"].ToString(),
                               FormaPago = c["FormaPago"].ToString(),
                               TipoMoneda = c["TipoMoneda"].ToString(),
                               IncluyeIgv = Convert.ToBoolean(c["IncluyeIgv"].ToString()),
                               IncluyeRenta = Convert.ToBoolean(c["IncluyeRenta"].ToString()),
                               FechaEmision = Convert.ToDateTime(c["FechaEmision"]).ToString("yyyy-MM-dd HH:mm:ss"),
                               FechaEntrega = Convert.ToDateTime(c["FechaEntrega"]).ToString("yyyy-MM-dd HH:mm:ss"),
                               FechaInicioVigencia = Convert.ToDateTime(c["FechaInicioVigencia"]).ToString("yyyy-MM-dd HH:mm:ss"),
                               FechaFinVigencia = Convert.ToDateTime(c["FechaFinVigencia"]).ToString("yyyy-MM-dd HH:mm:ss"),

                               LugarEntrega = Convert.ToInt64(c["LugarEntrega"]),
                               DireccionEntrega = c["DireccionEntrega"].ToString(),
                               Centro = Convert.ToInt64(c["Centro"].ToString()),
                               Almacen = Convert.ToInt64(c["Almacen"].ToString()),
                               Nave = Convert.ToInt64(c["Nave"].ToString()),

                               SubTotal = Convert.ToDecimal(c["SubTotal"].ToString()),
                               Descuento = Convert.ToDecimal(c["Descuento"].ToString()),
                               IgvPorcentaje = Convert.ToDecimal(c["IgvPorcentaje"].ToString()),
                               Igv = Convert.ToDecimal(c["Igv"].ToString()),
                               RentaPorcentaje = Convert.ToDecimal(c["RentaPorcentaje"].ToString()),
                               Renta = Convert.ToDecimal(c["Renta"].ToString()),
                               Total = Convert.ToDecimal(c["Total"].ToString()),
                               Estado = c["Estado"].ToString(),
                               
                               DetalleServicios = (from d in dt.AsEnumerable()
                                              where Convert.ToInt32(d["DOrdenCompra"]) > 0 &&  Convert.ToInt64(c["OrdenCompra"]).Equals(Convert.ToInt64(d["OrdenCompra"]))
                                              select new
                                              {
                                                  DOrdenCompra = Convert.ToInt64(d["DOrdenCompra"]),
                                                  OrdenCompra = Convert.ToInt64(d["OrdenCompra"]),
                                                  Cantidad = Convert.ToDecimal(d["CantidadServicio"].ToString()),
                                                  PrecioUnitario = Convert.ToDecimal(d["PrecioServicio"].ToString()),
                                                  DescuentoPrecio = Convert.ToDecimal(d["DescuentoServicio"].ToString()),
                                                  PrecioTotal = Convert.ToDecimal(d["TotalServicio"].ToString()),
                                                  UnidadMedida = Convert.ToInt64(d["UnidadServicio"]),
                                                  DescUnidadMedida = d["DescUnidadMedida"].ToString(),
                                                  Receta = Convert.ToInt64(d["Receta"]),
                                                  Recurso = Convert.ToInt64(d["Recurso"]),
                                                  Actividad = Convert.ToInt64(d["Actividad"]),
                                                  DescripcionAdicional = d["DescripcionAdicional"].ToString(),
                                                  CodReceta = d["CodReceta"].ToString(),
                                                  DescActividad = d["DescActividad"].ToString(),
                                              }).GroupBy(d => new { d.DOrdenCompra }).Select(d => d.First()),                      

                               DetalleMaterialesEntregados = (from m in dt.AsEnumerable()
                                                   where Convert.ToInt32(m["DOrdenEntregado"]) > 0 && Convert.ToInt32(m["Entregado"]) == 1 && Convert.ToInt64(c["OrdenCompra"]).Equals(Convert.ToInt64(m["OrdenCompra"]))
                                                   select new
                                                   {
                                                       DDetalleMaterialEntregado = Convert.ToInt64(m["DOrdenEntregado"]),
                                                       Material = Convert.ToInt64(m["MaterialEntregado"]),
                                                       CodMaterialEntregado = m["CodMaterialEntregado"].ToString(),
                                                       DescMaterialEntregado = m["DescMaterialEntregado"].ToString(),
                                                       CantidadEntregado = Convert.ToInt64(m["CantidadEntregado"]),
                                                       UnidadMedida = Convert.ToInt64(m["UnidadEntregado"].ToString()),
                                                       Almacen = Convert.ToInt64(m["Almacen"]),
                                                       AlmacenEntregado = m["AlmacenEntregado"].ToString(),
                                                       DescUnidadMedidaEntregado = m["DescUnidadMedidaEntregado"].ToString(),                                                       
                                                       LoteEntregado = m["LoteEntregado"].ToString(),
                                                   }).GroupBy(m => new { m.DDetalleMaterialEntregado }).Select(m => m.First()),

                               DetalleMaterialesproducidos = (from n in dt.AsEnumerable()
                                                   where Convert.ToInt32(n["DOrdenProducido"]) > 0 && Convert.ToInt32(n["Producido"]) == 1 && Convert.ToInt64(c["OrdenCompra"]).Equals(Convert.ToInt64(n["OrdenCompra"]))
                                                   select new
                                                   {
                                                       DDetalleMaterialProducido = Convert.ToInt64(n["DOrdenProducido"]),
                                                       Material = Convert.ToInt64(n["MaterialProducido"]),
                                                       CodMaterialProducidos = n["CodMaterialProducidos"].ToString(),
                                                       DescMaterialProducidos = n["DescMaterialProducidos"].ToString(),
                                                       UnidadMedida = Convert.ToInt64(n["UnidadProducido"]),
                                                       DescUnidadMedidaProducidos = n["DescUnidadMedidaProducidos"].ToString(),
                                                       CantidadProducidos = Convert.ToDecimal(n["CantidadProducido"].ToString()),                                                      
                                                   }).GroupBy(n => new { n.DDetalleMaterialProducido }).Select(n => n.First()),
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



        [HttpPost]
        [ActionName("PPPM_SaveUpdateOrdenCompraMaquila")]
        public HttpResponseMessage PPPM_SaveUpdateOrdenCompraMaquila(dynamic obj)
        {
            try
            {
                string xml = "<?xml version='1.0' encoding='ISO-8859-1'?><root>";
                string xmlDOrdenCompra = "<?xml version='1.0' encoding='ISO-8859-1'?><root>";
                string xmlDOrdenCompraMaterial = "<?xml version='1.0' encoding='ISO-8859-1'?><root>";
                xml += "<OrdenCompra ";
                xml += "OrdenCompra ='" + obj.OrdenCompra + "' ";
                xml += "Empresa ='" + JWT.IdEmpresa + "' ";
                xml += "Sociedad ='" + JWT.IdSociedad + "' ";
                xml += "Correlativo ='" + obj.Correlativo + "' ";
                xml += "Proveedor ='" + obj.Proveedor + "' ";
                xml += "CodigoCotizacion ='" + obj.CodigoCotizacion + "' ";
                xml += "TipoDocumento ='" + obj.TipoDocumento + "' ";
                xml += "FormaPago ='" + obj.FormaPago + "' ";
                xml += "FormaValorizar ='" + obj.FormaValorizar + "' ";
                xml += "FechaEmision ='" + obj.FechaEmision + "' ";
                xml += "FechaEntrega ='" + obj.FechaEntrega + "' ";
                xml += "TipoMoneda ='" + obj.TipoMoneda + "' ";
                xml += "TasaCambio ='" + obj.TasaCambio.ToString(System.Globalization.CultureInfo.InvariantCulture) + "' ";
                xml += "IncluyeIgv ='" + obj.IncluyeIgv + "' ";
                xml += "IncluyeRenta ='" + obj.IncluyeRenta + "' ";
                xml += "Maquila ='" + obj.Maquila + "' ";
                xml += "Centro ='" + obj.Centro + "' ";
                xml += "Nave ='" + obj.Nave + "' ";
                xml += "Almacen ='" + obj.Almacen + "' ";
                xml += "FechaInicioVigencia ='" + obj.FechaInicioVigencia + "' ";
                xml += "FechaFinVigencia ='" + obj.FechaFinVigencia + "' ";
                xml += "SubTotal ='" + obj.SubTotal.ToString(System.Globalization.CultureInfo.InvariantCulture) + "' ";
                xml += "Descuento ='" + obj.Descuento.ToString(System.Globalization.CultureInfo.InvariantCulture) + "' ";
                xml += "IgvPorcentaje ='" + obj.IgvPorcentaje.ToString(System.Globalization.CultureInfo.InvariantCulture) + "' ";
                xml += "Igv ='" + obj.Igv.ToString(System.Globalization.CultureInfo.InvariantCulture) + "' ";
                xml += "RentaPorcentaje ='" + obj.RentaPorcentaje.ToString(System.Globalization.CultureInfo.InvariantCulture) + "' ";
                xml += "Renta ='" + obj.Renta.ToString(System.Globalization.CultureInfo.InvariantCulture) + "' ";
                xml += "Total ='" + obj.Total.ToString(System.Globalization.CultureInfo.InvariantCulture) + "' ";
                xml += "Observacion ='" + obj.Observacion + "' ";
                xml += "AlmacenDestino ='" + obj.AlmacenDestino + "' ";
                xml += "DireccionDestino ='" + obj.DireccionDestino + "' ";
                xml += "Estado ='" + obj.Estado + "' ";
                xml += "UsuarioLibera ='" + obj.UsuarioLibera + "' ";
                xml += "Activo ='" + obj.Activo + "' />";
                foreach (dynamic doc in obj.DOrdenCompra)
                {
                    xmlDOrdenCompra += "<DOrdenCompra ";
                    xmlDOrdenCompra += "DOrdenCompra = '" + doc.DOrdenCompra + "' ";
                    xmlDOrdenCompra += "Empresa = '" + JWT.IdEmpresa + "' ";
                    xmlDOrdenCompra += "OrdenCompra = '" + doc.OrdenCompra + "' ";
                    xmlDOrdenCompra += "Cantidad = '" + doc.Cantidad.ToString(System.Globalization.CultureInfo.InvariantCulture) + "' ";
                    xmlDOrdenCompra += "PrecioUnitario = '" + doc.PrecioUnitario.ToString(System.Globalization.CultureInfo.InvariantCulture) + "' ";
                    xmlDOrdenCompra += "DescuentoPrecio = '" + doc.DescuentoPrecio.ToString(System.Globalization.CultureInfo.InvariantCulture) + "' ";
                    xmlDOrdenCompra += "PrecioTotal = '" + doc.PrecioTotal.ToString(System.Globalization.CultureInfo.InvariantCulture) + "' ";
                    xmlDOrdenCompra += "UnidadMedida = '" + doc.UnidadMedida + "' ";
                    xmlDOrdenCompra += "Receta = '" + doc.Receta + "' ";
                    xmlDOrdenCompra += "Recurso = '" + doc.Recurso + "' ";
                    xmlDOrdenCompra += "Actividad = '" + doc.Actividad + "' ";
                    xmlDOrdenCompra += "DescripcionAdicional = '" + doc.DescripcionAdicional + "' ";
                    xmlDOrdenCompra += "Activo = '" + doc.Activo + "' />";
                }
                foreach (dynamic docm in obj.DOrdenCompraMaterial)
                {
                    xmlDOrdenCompraMaterial += "<DOrdenCompraMaterial ";
                    xmlDOrdenCompraMaterial += "DOrdenCompraMaterial = '" + docm.DOrdenCompraMaterial + "' ";
                    xmlDOrdenCompraMaterial += "OrdenCompra = '" + docm.OrdenCompra + "' ";
                    xmlDOrdenCompraMaterial += "Empresa = '" + JWT.IdEmpresa + "' ";
                    xmlDOrdenCompraMaterial += "Material = '" + docm.Material + "' ";
                    xmlDOrdenCompraMaterial += "Cantidad = '" + docm.Cantidad.ToString(System.Globalization.CultureInfo.InvariantCulture) + "' ";
                    xmlDOrdenCompraMaterial += "UnidadMedida = '" + docm.UnidadMedida + "' ";
                    xmlDOrdenCompraMaterial += "Almacen = '" + docm.Almacen + "' ";
                    xmlDOrdenCompraMaterial += "Lote = '" + docm.Lote + "' ";
                    xmlDOrdenCompraMaterial += "Entregado = '" + docm.Entregado + "' ";
                    xmlDOrdenCompraMaterial += "Producido = '" + docm.Producido + "' ";
                    xmlDOrdenCompraMaterial += "Activo = '" + docm.Activo + "' />";
                }
                xml += "</root>";
                xmlDOrdenCompra += "</root>";
                xmlDOrdenCompraMaterial += "</root>";
                int res = Instancia.SaveUpdateOrdenCompraMaquila(JWT.Login, xml, xmlDOrdenCompra, xmlDOrdenCompraMaterial);
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
                    mensaje = "El recurso ya se encuentra registrado.";
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

        [HttpPost]
        [ActionName("PPPM_LiberarOrdenCompraMaquila")]
        public HttpResponseMessage PPPM_LiberarOrdenCompraMaquila(dynamic obj)
        {
            try
            {
                int UsuarioLibera = obj.UsuarioLibera;
                string xmlOrdenCompra = "<?xml version='1.0' encoding='ISO-8859-1'?><root>";
                foreach (dynamic oc in obj.OrdenCompra)
                {
                    xmlOrdenCompra += "<OrdenCompra ";
                    xmlOrdenCompra += "OrdenCompra = '" + oc.OrdenCompra + "' />";
                }
                xmlOrdenCompra += "</root>";
                int res = Instancia.LiberarOrdenCompraMaquila(JWT.IdEmpresa, UsuarioLibera, JWT.Login, xmlOrdenCompra);
                string mensaje = "";
                bool success = false;
                if (res > 0)
                {
                    success = true;
                    mensaje = "Se liberó correctamente.";
                }
                if (res == -1)
                {
                    success = true;
                    mensaje = "Revisar stock.";
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

        [HttpPost]
        [ActionName("PPPM_CerrarOrdenCompraMaquila")]
        public HttpResponseMessage PPPM_CerrarOrdenCompraMaquila(dynamic obj)
        {
            try
            {
                int UsuarioCierre = obj.UsuarioLibera;
                string xmlOrdenCompra = "<?xml version='1.0' encoding='ISO-8859-1'?><root>";
                foreach (dynamic oc in obj.OrdenCompra)
                {
                    xmlOrdenCompra += "<OrdenCompra ";
                    xmlOrdenCompra += "OrdenCompra = '" + oc.OrdenCompra + "' />";
                }
                xmlOrdenCompra += "</root>";
                int res = Instancia.CerrarOrdenCompraMaquila(JWT.IdEmpresa, UsuarioCierre, JWT.Login, xmlOrdenCompra);
                string mensaje = "";
                bool success = false;
                if (res > 0)
                {
                    success = true;
                    mensaje = "Se cerró correctamente.";
                }
                if (res == -1)
                {
                    success = true;
                    mensaje = "Tiene ordenes de producción sin cerrar.";
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

        [HttpPost]
        [ActionName("PPPM_RevertirOrdenCompraMaquila")]
        public HttpResponseMessage PPPM_RevertirOrdenCompraMaquila(dynamic obj)
        {
            try
            {
                string Usuario = JWT.Login;
                int OrdenCompra = obj.OrdenMaquila;
              
                int res = Instancia.update_estatus_OrdenCompra(OrdenCompra, Usuario, "M");
                string mensaje = "";
                bool success = false;
                if (res > 0)
                {
                    success = true;
                    mensaje = "Se cerró correctamente.";
                }
                if (res == -1)
                {
                    success = true;
                    mensaje = "Tiene ordenes de producción sin cerrar.";
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

        [HttpPost]
        [ActionName("PPPM_AnularOrdenCompraMaquila")]
        public HttpResponseMessage PPPM_AnularOrdenCompraMaquila(dynamic obj)
        {
            try { 
                int OrdenCompra = obj.idOrdenCompra;
                int res = Instancia.AnularOrdenCompraMaquila(JWT.IdEmpresa, OrdenCompra, JWT.Login);
                string mensaje = "";
                bool success = false;
                if (res > 0)
                {
                    success = true;
                    mensaje = "Se anuló correctamente.";
                }
                if (res == -1)
                {
                    success = true;
                    mensaje = "No se pudo anular correctamente.";
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
        [ActionName("PPPM_GetDatosNotificacionConsumoMaquila")]
        public IHttpActionResult PPPM_GetDatosNotificacionConsumoMaquila(int OrdenMaquila)
        {
            try
            {
                DataTableCollection dt = Instancia.GetDatosNotificacionConsumoMaquila(OrdenMaquila);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleError(ex));
            }
        }

        [HttpPost]
        [ActionName("PPPM_SaveUpdateNotificacionConsumoMaquila")]
        public HttpResponseMessage PPPM_SaveUpdateNotificacionConsumoMaquila(dynamic obj)
        {
            try
            {
                int OrdenCompra = obj.OrdenCompra;
                string FechaDocumento = obj.FechaDocumento;
                string FechaContabilizacion = obj.FechaContabilizacion;

                string xmlMaterialesEntregados = "<?xml version='1.0' encoding='ISO-8859-1'?><root>";
                foreach (dynamic me in obj.MaterialesEntregados)
                {
                    xmlMaterialesEntregados += "<MaterialesEntregados ";
                    xmlMaterialesEntregados += "Empresa = '" + JWT.IdEmpresa + "' ";
                    xmlMaterialesEntregados += "DOrdenCompraMaterial = '" + me.DOrdenCompraMaterial + "' ";
                    xmlMaterialesEntregados += "Material = '" + me.Material + "' ";
                    xmlMaterialesEntregados += "Cantidad = '" + me.Cantidad.ToString(System.Globalization.CultureInfo.InvariantCulture) + "' ";
                    xmlMaterialesEntregados += "Merma = '" + me.Merma.ToString(System.Globalization.CultureInfo.InvariantCulture) + "' ";
                    xmlMaterialesEntregados += "CantidadReal = '" + me.CantidadReal.ToString(System.Globalization.CultureInfo.InvariantCulture) + "' ";
                    xmlMaterialesEntregados += "UnidadMedida = '" + me.UnidadMedida + "' ";
                    xmlMaterialesEntregados += "Restringido = '" + me.Restringido + "' ";
                    xmlMaterialesEntregados += "Almacen = '" + me.Almacen + "' ";
                    xmlMaterialesEntregados += "Lote = '" + me.Lote + "' />";
                }
                xmlMaterialesEntregados += "</root>";
                int res = Instancia.SaveUpdateNotificacionConsumoMaquila(JWT.IdUsuario, JWT.Login, JWT.IdEmpresa, JWT.IdSociedad, OrdenCompra, FechaDocumento, FechaContabilizacion, xmlMaterialesEntregados);
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
                    mensaje = "La notificación ya se encuentra registrada.";
                }

                if (res == -2)
                {
                    success = true;
                    mensaje = "Algunos productos no tienen stock.";
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
        [ActionName("PP_getOrdenCompraMaquila_Data")]
        public IHttpActionResult PP_getOrdenCompraMaquila_Data(int prmintOrdenCompra)
        {
            try
            {
                DataTableCollection dt = Instancia.getOrdenCompraMaquila_Data(prmintOrdenCompra);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpGet]
        [ActionName("PP_RptIngresoOrdenCompra")]
        public IHttpActionResult PP_RptIngresoOrdenCompra(int prmintSociedad, int prmintTipoMaterial, int prmintGrupoArticulo, int prmintMaterial, int prmintProveedor, string prmstrEstado, DateTime prmdateFechaInicio, DateTime prmdateFechaFin)
        {
            try
            {
                DataTable dt = Instancia.GetRptIngresoOrdenCompra(prmintSociedad, prmintTipoMaterial, prmintGrupoArticulo, prmintMaterial, prmintProveedor, prmstrEstado, prmdateFechaInicio, prmdateFechaFin);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpGet]
        [ActionName("PP_RptServicioMaquila")]
        public IHttpActionResult PP_RptServicioMaquila(int prmintSociedad, int prmintTipoMaterial, int prmintGrupoArticulo, int prmintMaterial, int prmintProveedor, string prmstrEstado, DateTime prmdateFechaInicio, DateTime prmdateFechaFin)
        {
            try
            {
                DataTable dt = Instancia.GetRptServicioMaquila(prmintSociedad, prmintTipoMaterial, prmintGrupoArticulo, prmintMaterial, prmintProveedor, prmstrEstado, prmdateFechaInicio, prmdateFechaFin);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpGet]
        [ActionName("PP_RptOrdenTransporte")]
        public IHttpActionResult PP_RptOrdenTransporte(int prmintSociedad, int prmintTipoMaterial, int prmintGrupoArticulo, int prmintMaterial, int prmintProveedor, int prmintOrigen, int prmintDestino, string prmstrEstado, DateTime prmdateFechaInicio, DateTime prmdateFechaFin)
        {
            try
            {
                DataTable dt = Instancia.GetRptOrdenTransporte(prmintSociedad, prmintTipoMaterial, prmintGrupoArticulo, prmintMaterial, prmintProveedor, prmintOrigen, prmintDestino, prmstrEstado, prmdateFechaInicio, prmdateFechaFin);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpPost]
        [ActionName("PP_update_estatus_OrdenCompra")]
        public IHttpActionResult PP_update_estatus_OrdenCompra(dynamic obj)
        {
            try
            {
                int idOrden = obj.prmintOrdenCompra;
                string usuario = obj.prmstrUsuario;

                int dt = Instancia.update_estatus_OrdenCompra(idOrden, usuario, "N");
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpGet]
        [ActionName("PPPM_GetAreasOC")]
        public IHttpActionResult PPPM_GetAreasOC()
        {
            try
            {
                DataTable dt = Instancia.GetAreasOC();
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleError(ex));
            }
        }

    }
}
