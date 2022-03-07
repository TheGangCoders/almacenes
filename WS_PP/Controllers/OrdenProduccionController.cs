using System;
using System.Data;
using static WS_PP.Models.OrdenProduccionModelDAL;
using Shared.Controllers;
using Shared.Helpers;
using System.Web.Http;
using Shared.Util;
using Shared.Token;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net;
using System.Linq;
using System.Collections.Generic;

namespace WS_PP.Controllers
{
    [CorsOrigin]
    [Authorize]
    public class OrdenProduccionController : BaseController
    {
        [HttpGet]
        [ActionName("PP_getOrdenProduccion_List")]
        public IHttpActionResult PP_getOrdenProduccion_List(string FechaInicio, string FechaFin, string Estado, int Tipo)
        {
            try
            {
                DataTable dt = Instancia.getOrdenProduccion_List(FechaInicio, FechaFin, Estado, Tipo, JWT.IdEmpresa);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpGet]
        [ActionName("PP_getEstadoOrdenProduccion")]
        public IHttpActionResult PP_getEstadoOrdenProduccion()
        {
            try
            {
                DataTable dt = Instancia.getEstadoOrdenProduccion(JWT.IdEmpresa);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpGet]
        [ActionName("PP_getTipoOrdenProduccion")]
        public IHttpActionResult PP_getTipoOrdenProduccion()
        {
            try
            {
                DataTable dt = Instancia.getTipoOrdenProduccion(JWT.IdEmpresa);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }


        [HttpGet]
        [ActionName("PP_getVersionNave")]
        public IHttpActionResult PP_getVersionNave(int prmintCentro, int prmintNave, int prmintMaterial, int prmintUnidadBase)
        {
            try
            {
                DataTable dt = Instancia.getVersionNave(JWT.IdEmpresa, prmintCentro, prmintNave, prmintMaterial, prmintUnidadBase);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpGet]
        [ActionName("PP_getDatosAdicionalesMaterial")]
        public IHttpActionResult PP_getDatosAdicionalesMaterial(int prmintCentro, int prmintMaterial)
        {
            try
            {
                DataTable dt = Instancia.getDatosAdicionalesMaterial(JWT.IdEmpresa, prmintCentro, prmintMaterial);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpGet]
        [ActionName("PP_getCentroBeneficioMaterial")]
        public IHttpActionResult PP_getCentroBeneficioMaterial(int prmintCentro, int prmintMaterial, int prmintAlmacen)
        {
            try
            {
                DataTable dt = Instancia.getCentroBeneficioMaterial(JWT.IdEmpresa, prmintCentro, prmintMaterial, prmintAlmacen);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpGet]
        [ActionName("PP_getPlanificador")]
        public IHttpActionResult PP_getPlanificador()
        {
            try
            {
                DataTable dt = Instancia.getPlanificador(JWT.IdEmpresa);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpGet]
        [ActionName("PP_getDetalleListaMaterial")]
        public IHttpActionResult PP_getDetalleListaMaterial(int prmintListaMaterial, float prmfltCantidadOrden, int prmintOrdenProduccion, int prmintVersion)
        {
            try
            {
                DataTable dt = Instancia.getDetalleListaMaterial(prmintListaMaterial, prmfltCantidadOrden, prmintOrdenProduccion, prmintVersion);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpGet]
        [ActionName("PP_getPosicionPedidoVenta")]
        public IHttpActionResult PP_getPosicionPedidoVenta(int prmintMaterial)
        {
            try
            {
                DataTable dt = Instancia.getPosicionPedidoVenta(prmintMaterial);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpPost]
        [ActionName("PP_postSaveUpdateOrdenProduccion")]
        public HttpResponseMessage PP_postSaveUpdateOrdenProduccion(dynamic obj)
        {
            try
            {
                string xml = "<?xml version='1.0' encoding='ISO-8859-1'?><root>";
                string xmlDOrden = "<?xml version='1.0' encoding='ISO-8859-1'?><root>";

                xml += "<OrdenProduccion ";
                xml += "OrdenProduccion ='" + obj.OrdenProduccion + "' ";
                xml += "Empresa ='" + JWT.IdEmpresa + "' ";
                xml += "Sociedad ='" + JWT.IdSociedad + "' ";
                xml += "Material ='" + obj.Material + "' ";
                xml += "ClaseOrden ='" + obj.ClaseOrdenProduccion + "' ";
                xml += "Almacen ='" + obj.Almacen + "' ";
                xml += "Planificador ='" + obj.Planificador + "' ";
                xml += "VersionFabricacion ='" + obj.VersionFabricacion + "' ";
                xml += "Receta ='" + obj.Receta + "' ";
                xml += "DPedidoVenta ='" + obj.DPedidoVenta + "' ";
                xml += "CantidadOrden ='" + obj.CantidadOrden.ToString(System.Globalization.CultureInfo.InvariantCulture) + "' ";
                xml += "FechaInicioExtremo ='" + obj.FechaInicioExtremo + "' ";
                xml += "FechaFinExtremo ='" + obj.FechaFinExtremo + "' ";
                xml += "FechaInicio ='" + obj.FechaInicio + "' ";
                xml += "FechaFin ='" + obj.FechaFin + "' ";
                xml += "Dias ='" + obj.Dias + "' ";
                xml += "CantidadRechazada ='" + obj.CantidadRechazada + "' ";
                xml += "CantidadConfirmada ='" + obj.CantidadConfirmada + "' ";
                xml += "CentroBeneficio ='" + obj.CentroBeneficio + "' ";
                xml += "MetodoProduccion ='" + obj.MetodoProduccion + "' ";
                xml += "Estado ='" + obj.Estado + "' />";

                foreach (dynamic dm in obj.Detalle)
                {
                    xmlDOrden += "<DOrdenProduccion ";
                    xmlDOrden += "DOrdenProduccion = '" + dm.DOrdenProduccion + "' ";
                    xmlDOrden += "Material = '" + dm.idMaterial + "' ";
                    xmlDOrden += "Empresa = '" + JWT.IdEmpresa + "' ";
                    xmlDOrden += "OrdenProduccion = '" + dm.OrdenProduccion + "' ";
                    xmlDOrden += "Cantidad = '" + dm.Cantidad.ToString(System.Globalization.CultureInfo.InvariantCulture) + "' ";
                    xmlDOrden += "UnidadMedida = '" + dm.UnidadMedida + "' />";
                }

                xml += "</root>";
                xmlDOrden += "</root>";

                int res = Instancia.SaveUpdateOrdenProduccion(JWT.Login, xml, xmlDOrden);
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
                    mensaje = "La numeración ya no esta activa. Por favor validar.";
                }

                if (res == -2)
                {
                    success = true;
                    mensaje = "No existe una unidad de medida configurada para el almacen seleccionado.";
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
        [ActionName("PP_getListadoOrdenProduccion")]
        public IHttpActionResult PP_getListadoOrdenProduccion(string prmstrFecha1, string prmstrFecha2, int prmintCentro, int prmintNave, int prmintClase)
        {
            try
            {
                DataTable dt = Instancia.getListadoOrdenProduccion(prmstrFecha1, prmstrFecha2, prmintCentro, prmintNave, prmintClase);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpGet]
        [ActionName("PP_getgetDatosOrdenProduccion")]
        public HttpResponseMessage PP_getgetDatosOrdenProduccion(int prmintOrdenProducion)
        {
            try
            {
                DataTable dt = Instancia.GetDatosOrdenProduccion(prmintOrdenProducion);

                var obj = (from c in dt.AsEnumerable()
                           select new
                           {
                               OrdenProduccion = Convert.ToInt64(c["OrdenProduccion"]),
                               Correlativo = c["Correlativo"].ToString(),
                               ClaseOrdenProduccion = Convert.ToInt64(c["ClaseOrdenProduccion"]),
                               Almacen = Convert.ToInt64(c["Almacen"]),
                               Nave = Convert.ToInt64(c["Nave"]),
                               Centro = Convert.ToInt64(c["Centro"]),
                               Material = Convert.ToInt64(c["Material"]),
                               CodMaterial = c["CodMaterial"].ToString(),

                               UnidadMedida = Convert.ToInt64(c["UnidadCabecera"]),
                               DesUnidadMedida = c["DescUnidadCabecera"].ToString(),

                               DescMaterial = c["DescMaterial"].ToString(),
                               Planificador = c["Planificador"].ToString(),
                               VersionFabricacion = Convert.ToInt64(c["VersionFabricacion"]),
                               DPedidoVenta = Convert.ToInt64(c["DPedidoVenta"]),
                               OrdenCompra = Convert.ToInt64(c["OrdenCompra"]),
                               Posicion = c["Posicion"].ToString(),
                               PedidoVenta = c["PedidoVenta"].ToString(),
                               DescUnidadMedida = c["DescUnidadMedida"].ToString(),

                               CantidadConfirmada = Convert.ToDecimal(c["CantidadConfirmada"].ToString()),
                               CantidadRechazada = Convert.ToDecimal(c["CantidadRechazada"].ToString()),

                               FechaNotificadaMin = c["FechaNotificadaMin"].ToString(),
                               FechaNotificadaMax = c["FechaNotificadaMax"].ToString(),

                               FechaLiberacion = c["FechaLiberacion"].ToString(),
                               FechaCierre = c["FechaCierre"].ToString(),


                               CantidadNotificada = Convert.ToDecimal(c["CantidadNotificada"].ToString()),
                               CantidadOrden = Convert.ToDecimal(c["CantidadOrden"].ToString()),
                               FechaInicioExtremo = c["FechaInicioExtremo"].ToString(),
                               FechaFinExtremo = c["FechaFinExtremo"].ToString(),
                               FechaInicio = c["FechaInicio"].ToString(),
                               FechaFin = c["FechaFin"].ToString(),
                               MetodoProduccion = c["MetodoProduccion"].ToString(),
                               Estado = c["Estado"].ToString(),

                               Detalles = (from d in dt.AsEnumerable()
                                           where Convert.ToInt64(c["OrdenProduccion"]).Equals(Convert.ToInt64(d["OrdenProduccion"]))
                                           select new
                                           {
                                               DOrdenProduccion = Convert.ToInt64(d["DOrdenProduccion"]),
                                               OrdenProduccion = Convert.ToInt64(d["OrdenProduccion"]),
                                               idMaterial = Convert.ToInt64(d["DetMaterial"].ToString()),
                                               UnidadMedida = Convert.ToInt64(d["UnidadMedida"].ToString()),
                                               Cantidad = Convert.ToDecimal(c["DetCantidad"].ToString()),

                                           }).GroupBy(d => new { d.DOrdenProduccion }).Select(d => d.First()),
                           }).GroupBy(c => new { c.OrdenProduccion }).Select(c => c.First());

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
        [ActionName("PP_postUpdateEstadoOrdenProduccion")]
        public IHttpActionResult PP_postUpdateEstadoOrdenProduccion(dynamic obj)
        {
            try
            {
                int idOrden = obj.OrdenProduccion;
                string Estado = obj.Estado;
                string UsuarioRegistro = obj.Usuario;


                int dt = Instancia.postUpdateEstadoOrdenProduccion(idOrden, Estado, UsuarioRegistro);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpGet]
        [ActionName("PP_getReporteProduccion")]
        public IHttpActionResult PP_getReporteProduccion(int prmintMaterial, int prmintCentro, int prmintAlmacen, int prmintClaseOP, string prmstrEstado, string prmstrTipoRpt, DateTime prmdatFechaInicio, DateTime prmdatFechaFin)
        {
            try
            {
                DataTable dt = Instancia.getReporteProduccion(prmintMaterial, prmintCentro, prmintAlmacen, prmintClaseOP, prmstrEstado, prmstrTipoRpt, prmdatFechaInicio, prmdatFechaFin);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpGet]
        [ActionName("PP_getListadoOrdenProduccionMigo")]
        public IHttpActionResult PP_getListadoOrdenProduccionMigo(string prmstrFecha1, string prmstrFecha2, int prmintCentro, int prmintNave, int prmintClase, int prmintTipo)
        {
            try
            {
                DataTable dt = Instancia.getListadoOrdenProduccionMigo(prmstrFecha1, prmstrFecha2, prmintCentro, prmintNave, prmintClase, prmintTipo, JWT.IdEmpresa, JWT.IdSociedad);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }
    }
}
