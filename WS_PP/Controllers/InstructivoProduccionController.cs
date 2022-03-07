using Newtonsoft.Json;
using Shared.Controllers;
using Shared.Helpers;
using Shared.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using static WS_PP.Models.InstructivoProduccionModel;
namespace WS_PP.Controllers
{
    [CorsOrigin]
    [Authorize]
    public class InstructivoProduccionController : BaseController
    {
        [HttpGet]
        [ActionName("PP_GetInstructivosProduccion")]
        public IHttpActionResult PP_GetInstructivosProduccion(int prmintCentro, int prmintNave, string prmstrMetodoProduccion, int prmintClaseOrden,
                string prmstrEstado, int prmintTipoFecha, string prmstrFechaInicio, string prmstrFechaFin)
        {
            try
            {
                DataTable dt = Instancia.GetInstructivosProduccion(JWT.IdEmpresa, prmintCentro, prmintNave, prmstrMetodoProduccion, prmintClaseOrden, prmstrEstado,
                    prmintTipoFecha, prmstrFechaInicio, prmstrFechaFin);

                return Ok(dt);
            }
            catch (Exception ex)
            {
                //return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [ActionName("PP_GetEstadosInstructivoProduccion")]
        public IHttpActionResult PP_GetEstadosInstructivoProduccion()
        {
            try
            {
                DataTable dt = Instancia.GetEstadosInstructivoProduccion(JWT.IdEmpresa);

                return Ok(dt);
            }
            catch (Exception ex)
            {
                //return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [ActionName("PP_GetLineasProduccion")]
        public IHttpActionResult PP_GetLineasProduccion(int prmintNave)
        {
            try
            {
                DataTable dt = Instancia.GetLineasProduccion(JWT.IdEmpresa, prmintNave);

                return Ok(dt);
            }
            catch (Exception ex)
            {
                //return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [ActionName("PP_GetBuscarMaterialStock")]
        public IHttpActionResult PP_GetBuscarMaterialStock(int prmintTipoMaterial, string prmstrCodigoMaterial, string prmstrDescripcionMaterial)
        {
            try
            {
                DataTable dt = Instancia.GetBuscarMaterialStock(JWT.IdEmpresa, prmintTipoMaterial, prmstrCodigoMaterial, prmstrDescripcionMaterial);

                return Ok(dt);
            }
            catch (Exception ex)
            {
                //return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [ActionName("PP_getBuscarPedidoVenta")]
        public IHttpActionResult PP_getBuscarPedidoVenta()
        {
            try
            {
                DataTable dt = Instancia.GetBuscarPedidoVenta(JWT.IdEmpresa);

                return Ok(dt);
            }
            catch (Exception ex)
            {
                //return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [ActionName("PP_getBuscarOrdenMaquila")]
        public IHttpActionResult PP_getBuscarOrdenMaquila()
        {
            try
            {
                DataTable dt = Instancia.GetBuscarOrdenMaquila(JWT.IdEmpresa);

                return Ok(dt);
            }
            catch (Exception ex)
            {
                //return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [ActionName("PP_SaveUpdateInstructivoProduccion")]
        public IHttpActionResult PP_SaveUpdateInstructivoProduccion(dynamic obj)
        {
            try
            {
                string xml = "<?xml version='1.0' encoding='ISO-8859-1'?><root>";
                string xmlLanzado = "<?xml version='1.0' encoding='ISO-8859-1'?><root>";
                string xmlNotificacionProduccion = "<?xml version='1.0' encoding='ISO-8859-1'?><root>";
                string xmlDistribucionProduccion = "<?xml version='1.0' encoding='ISO-8859-1'?><root>";

                xml += "<InstructivoProduccion ";
                xml += "InstructivoProduccion ='" + obj.InstructivoProduccion + "' ";
                xml += "Empresa ='" + JWT.IdEmpresa + "' ";
                xml += "Sociedad ='" + JWT.IdSociedad + "' ";
                xml += "ClaseOrdenProduccion ='" + obj.ClaseOrdenProduccion + "' ";
                xml += "MetodoProduccion ='" + obj.MetodoProduccion + "' ";
                xml += "Centro ='" + obj.Centro + "' ";
                xml += "Nave ='" + obj.Nave + "' ";
                xml += "Estado ='" + obj.Estado + "' ";
                xml += "FechaInicio ='" + obj.FechaInicio + "' ";
                xml += "FechaFin ='" + obj.FechaFin + "' ";
                xml += "OrdenCompra ='" + obj.OrdenCompra + "' ";
                xml += "PedidoVenta ='" + obj.PedidoVenta + "' ";
                xml += "CantidadDesecho ='" + obj.CantidadDesecho.ToString(System.Globalization.CultureInfo.InvariantCulture) + "' />";

                foreach (dynamic dm in obj.LanzadoProduccion)
                {
                    xmlLanzado += "<LanzadoProduccion ";
                    xmlLanzado += "LanzadoProduccion = '" + dm.LanzadoProduccion + "' ";
                    xmlLanzado += "DOrdenCompraMaterial = '" + dm.DOrdenCompraMaterial + "' ";
                    xmlLanzado += "Empresa = '" + JWT.IdEmpresa + "' ";
                    xmlLanzado += "Sociedad = '" + JWT.IdSociedad + "' ";
                    xmlLanzado += "InstructivoProduccion = '" + obj.InstructivoProduccion + "' ";
                    xmlLanzado += "Material = '" + dm.Material + "' ";
                    xmlLanzado += "Proveedor = '" + dm.Proveedor + "' ";
                    xmlLanzado += "Almacen = '" + dm.Almacen + "' ";
                    xmlLanzado += "UnidadMedida = '" + dm.UnidadMedida + "' ";
                    xmlLanzado += "Lote = '" + dm.Lote + "' ";
                    xmlLanzado += "LineaProduccion = '" + dm.LineaProduccion + "' ";
                    xmlLanzado += "Predio = '" + dm.Predio + "' ";
                    xmlLanzado += "CantidadLanzado = '" + dm.CantidadLanzado.ToString(System.Globalization.CultureInfo.InvariantCulture) + "' />";
                }

                foreach (dynamic dm in obj.NotificacionProduccion)
                {
                    xmlNotificacionProduccion += "<NotificacionProduccion ";
                    xmlNotificacionProduccion += "OrdenProduccion = '" + dm.OrdenProduccion + "' ";
                    xmlNotificacionProduccion += "Migo = '" + dm.Migo + "' ";
                    xmlNotificacionProduccion += "Empresa = '" + JWT.IdEmpresa + "' ";
                    xmlNotificacionProduccion += "Sociedad = '" + JWT.IdSociedad + "' ";
                    xmlNotificacionProduccion += "InstructivoProduccion = '" + obj.InstructivoProduccion + "' ";
                    xmlNotificacionProduccion += "Material = '" + dm.Material + "' ";
                    xmlNotificacionProduccion += "Almacen = '" + dm.Almacen + "' ";
                    xmlNotificacionProduccion += "VersionFabricacion = '" + dm.VersionFabricacion + "' ";
                    xmlNotificacionProduccion += "Receta = '" + dm.Receta + "' ";
                    xmlNotificacionProduccion += "ListaMaterial = '" + dm.ListaMaterial + "' ";
                    xmlNotificacionProduccion += "Lote = '" + dm.LoteProduccion + "' ";
                    xmlNotificacionProduccion += "Cantidad = '" + dm.Cantidad.ToString(System.Globalization.CultureInfo.InvariantCulture) + "' ";
                    xmlNotificacionProduccion += "FechaInicio ='" + obj.FechaInicio + "' ";
                    xmlNotificacionProduccion += "FechaFin ='" + obj.FechaFin + "' ";
                    xmlNotificacionProduccion += "FechaNotificacion ='" + dm.FechaNotificacion + "' ";
                    xmlNotificacionProduccion += "FechaFabricacion ='" + dm.FechaFabricacion + "' ";
                    xmlNotificacionProduccion += "FechaVencimiento ='" + dm.FechaVencimiento + "' ";
                    xmlNotificacionProduccion += "ClaseOrdenProduccion ='" + obj.ClaseOrdenProduccion + "' ";
                    xmlNotificacionProduccion += "MetodoProduccion ='" + obj.MetodoProduccion + "' ";
                    xmlNotificacionProduccion += "UnidadMedida = '" + dm.UnidadMedida + "' />";
                }

                foreach (dynamic dp in obj.DistribucionProduccion)
                {
                    xmlDistribucionProduccion += "<DistribucionProduccion ";
                    xmlDistribucionProduccion += "OrdenProduccion = '" + dp.OrdenProduccion + "' ";
                    xmlDistribucionProduccion += "Migo = '" + dp.Migo + "' ";
                    xmlDistribucionProduccion += "Empresa = '" + JWT.IdEmpresa + "' ";
                    xmlDistribucionProduccion += "Sociedad = '" + JWT.IdSociedad + "' ";
                    xmlDistribucionProduccion += "InstructivoProduccion = '" + obj.InstructivoProduccion + "' ";
                    xmlDistribucionProduccion += "Material = '" + dp.Material + "' ";
                    xmlDistribucionProduccion += "Almacen = '" + dp.Almacen + "' ";
                    xmlDistribucionProduccion += "Lote = '" + dp.LoteProduccion + "' ";
                    xmlDistribucionProduccion += "Cantidad = '" + dp.Cantidad.ToString(System.Globalization.CultureInfo.InvariantCulture) + "' ";
                    xmlDistribucionProduccion += "UnidadMedida = '" + dp.UnidadMedida + "' ";
                    xmlDistribucionProduccion += "UnidadMedidaOriginal = '" + dp.UnidadMedidaOriginal + "' />";
                }

                xml += "</root>";
                xmlLanzado += "</root>";
                xmlNotificacionProduccion += "</root>";
                xmlDistribucionProduccion += "</root>";

                int res = Instancia.SaveUpdateInstructivoProduccion(JWT.Login, xml, xmlLanzado, xmlNotificacionProduccion, xmlDistribucionProduccion);
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

                var IHttpActionResult = Request.CreateResponse(HttpStatusCode.OK, respuesta);

                //var IHttpActionResult = Request.CreateResponse<object>(HttpStatusCode.OK, Grupo);
                IHttpActionResult.Headers.Add("Access-Control-Allow-Origin", "*");

                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                var error = MetodoComun.ObtenerExceptionModel(ex);

                var IHttpActionResult = Request.CreateResponse(HttpStatusCode.BadRequest, error);
                IHttpActionResult.Headers.Add("Access-Control-Allow-Origin", "*");

                return Ok(ex.ToString());
            }
        }

        [HttpGet]
        [ActionName("PP_GetMaterialAlmacenFromInstructivo")]
        public IHttpActionResult PP_GetMaterialAlmacenFromInstructivo(int? prmintCentro, int? prmintMaterial)
        {
            try
            {
                DataTable dt = Instancia.GetMaterialAlmacenFromInstructivo(JWT.IdEmpresa, prmintCentro, prmintMaterial);

                return Ok(dt);
            }
            catch (Exception ex)
            {
                //return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [ActionName("PP_GetDatosInstructivoProduccion")]
        public IHttpActionResult PP_GetDatosInstructivoProduccion(int prmintInstructivoProduccion)
        {
            try
            {
                DataTableCollection dt = Instancia.GetDatosInstructivoProduccion(prmintInstructivoProduccion);
                string json = JsonConvert.SerializeObject(dt, Newtonsoft.Json.Formatting.None);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                //return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [ActionName("PP_GetInstructivosProduccionForConsumoMateriales")]
        public IHttpActionResult PP_GetInstructivosProduccionForConsumoMateriales(int? prmintCentro, int? prmintNave, string prmstrFecha)
        {
            try
            {
                DataTable dt = Instancia.GetInstructivosProduccionForConsumoMateriales(JWT.IdEmpresa, prmintCentro, prmintNave, prmstrFecha);

                return Ok(dt);
            }
            catch (Exception ex)
            {
                //return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [ActionName("PP_GetConsumoMaterialesByInstructivo")]
        public IHttpActionResult PP_GetConsumoMaterialesByInstructivo(int? prmintInstructivoProduccion)
        {
            try
            {
                DataTable dt = Instancia.GetConsumoMaterialesByInstructivo(prmintInstructivoProduccion);

                return Ok(dt);
            }
            catch (Exception ex)
            {
                //return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [ActionName("PP_GetPreDistribucionConsumoMateriaPrima")]
        public IHttpActionResult PP_GetPreDistribucionConsumoMateriaPrima(dynamic obj)
        {
            try
            {
                string xmlLanzado = "<?xml version='1.0' encoding='ISO-8859-1'?><root>";
                string xmlNotificacionProduccion = "<?xml version='1.0' encoding='ISO-8859-1'?><root>";
                string xmlDistribucionProduccion = "<?xml version='1.0' encoding='ISO-8859-1'?><root>";

                int prmintEmpresa = Convert.ToInt32(JWT.IdEmpresa);
                int prmintCentro = Convert.ToInt32(obj.Centro);
                int prmintNave = Convert.ToInt32(obj.Nave);
                string prmstrFecha = Convert.ToString(obj.Fecha);

                foreach (dynamic dm in obj.LanzadoProduccion)
                {
                    xmlLanzado += "<LanzadoProduccion ";
                    xmlLanzado += "LanzadoProduccion = '" + dm.LanzadoProduccion + "' ";
                    xmlLanzado += "DOrdenCompraMaterial = '" + dm.DOrdenCompraMaterial + "' ";
                    xmlLanzado += "Empresa = '" + JWT.IdEmpresa + "' ";
                    xmlLanzado += "Sociedad = '" + JWT.IdSociedad + "' ";
                    xmlLanzado += "InstructivoProduccion = '" + obj.InstructivoProduccion + "' ";
                    xmlLanzado += "Material = '" + dm.Material + "' ";
                    xmlLanzado += "Proveedor = '" + dm.Proveedor + "' ";
                    xmlLanzado += "Almacen = '" + dm.Almacen + "' ";
                    xmlLanzado += "UnidadMedida = '" + dm.UnidadMedida + "' ";
                    xmlLanzado += "Lote = '" + dm.Lote + "' ";
                    xmlLanzado += "LineaProduccion = '" + dm.LineaProduccion + "' ";
                    xmlLanzado += "CantidadLanzado = '" + dm.CantidadLanzado.ToString(System.Globalization.CultureInfo.InvariantCulture) + "' />";
                }

                foreach (dynamic dm in obj.NotificacionProduccion)
                {
                    xmlNotificacionProduccion += "<NotificacionProduccion ";
                    xmlNotificacionProduccion += "OrdenProduccion = '" + dm.OrdenProduccion + "' ";
                    xmlNotificacionProduccion += "Migo = '" + dm.Migo + "' ";
                    xmlNotificacionProduccion += "Empresa = '" + JWT.IdEmpresa + "' ";
                    xmlNotificacionProduccion += "Sociedad = '" + JWT.IdSociedad + "' ";
                    xmlNotificacionProduccion += "InstructivoProduccion = '" + obj.InstructivoProduccion + "' ";
                    xmlNotificacionProduccion += "Material = '" + dm.Material + "' ";
                    xmlNotificacionProduccion += "Almacen = '" + dm.Almacen + "' ";
                    xmlNotificacionProduccion += "VersionFabricacion = '" + dm.VersionFabricacion + "' ";
                    xmlNotificacionProduccion += "Receta = '" + dm.Receta + "' ";
                    xmlNotificacionProduccion += "ListaMaterial = '" + dm.ListaMaterial + "' ";
                    xmlNotificacionProduccion += "Lote = '" + dm.LoteProduccion + "' ";
                    xmlNotificacionProduccion += "Cantidad = '" + dm.Cantidad.ToString(System.Globalization.CultureInfo.InvariantCulture) + "' ";
                    xmlNotificacionProduccion += "FechaInicio ='" + obj.FechaInicio + "' ";
                    xmlNotificacionProduccion += "FechaFin ='" + obj.FechaFin + "' ";
                    xmlNotificacionProduccion += "FechaNotificacion ='" + dm.FechaNotificacion + "' ";
                    xmlNotificacionProduccion += "FechaFabricacion ='" + dm.FechaFabricacion + "' ";
                    xmlNotificacionProduccion += "FechaVencimiento ='" + dm.FechaVencimiento + "' ";
                    xmlNotificacionProduccion += "ClaseOrdenProduccion ='" + obj.ClaseOrdenProduccion + "' ";
                    xmlNotificacionProduccion += "MetodoProduccion ='" + obj.MetodoProduccion + "' ";
                    xmlNotificacionProduccion += "UnidadMedida = '" + dm.UnidadMedida + "' />";
                }

                foreach (dynamic dp in obj.DistribucionProduccion)
                {
                    xmlDistribucionProduccion += "<DistribucionProduccion ";
                    xmlDistribucionProduccion += "OrdenProduccion = '" + dp.OrdenProduccion + "' ";
                    xmlDistribucionProduccion += "Migo = '" + dp.Migo + "' ";
                    xmlDistribucionProduccion += "Empresa = '" + JWT.IdEmpresa + "' ";
                    xmlDistribucionProduccion += "Sociedad = '" + JWT.IdSociedad + "' ";
                    xmlDistribucionProduccion += "InstructivoProduccion = '" + obj.InstructivoProduccion + "' ";
                    xmlDistribucionProduccion += "Material = '" + dp.Material + "' ";
                    xmlDistribucionProduccion += "Almacen = '" + dp.Almacen + "' ";
                    xmlDistribucionProduccion += "Lote = '" + dp.LoteProduccion + "' ";
                    xmlDistribucionProduccion += "Cantidad = '" + dp.Cantidad.ToString(System.Globalization.CultureInfo.InvariantCulture) + "' ";
                    xmlDistribucionProduccion += "UnidadMedida = '" + dp.UnidadMedida + "' ";
                    xmlDistribucionProduccion += "UnidadMedidaOriginal = '" + dp.UnidadMedidaOriginal + "' />";
                }

                xmlLanzado += "</root>";
                xmlNotificacionProduccion += "</root>";
                xmlDistribucionProduccion += "</root>";

                DataTable dt = Instancia.GetPreDistribucionConsumoMateriaPrima(prmintEmpresa, prmintCentro, prmintNave, xmlLanzado, xmlNotificacionProduccion, xmlDistribucionProduccion);

                return Ok(dt);
            }
            catch (Exception ex)
            {
                //return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [ActionName("PP_SaveUpdateConsumoDistribucionLanzado")]
        public IHttpActionResult PP_SaveUpdateConsumoDistribucionLanzado(dynamic obj)
        {
            try
            {
                string xml = "<?xml version='1.0' encoding='ISO-8859-1'?><root>";
                string xmlLanzado = "<?xml version='1.0' encoding='ISO-8859-1'?><root>";
                string xmlNotificacionProduccion = "<?xml version='1.0' encoding='ISO-8859-1'?><root>";
                string xmlDistribucionProduccion = "<?xml version='1.0' encoding='ISO-8859-1'?><root>";

                xml += "<InstructivoProduccion ";
                xml += "InstructivoProduccion ='" + obj.InstructivoProduccion + "' ";
                xml += "Empresa ='" + JWT.IdEmpresa + "' ";
                xml += "Sociedad ='" + JWT.IdSociedad + "' ";
                xml += "ClaseOrdenProduccion ='" + obj.ClaseOrdenProduccion + "' ";
                xml += "MetodoProduccion ='" + obj.MetodoProduccion + "' ";
                xml += "Centro ='" + obj.Centro + "' ";
                xml += "Nave ='" + obj.Nave + "' ";
                xml += "Estado ='" + obj.Estado + "' ";
                xml += "FechaInicio ='" + obj.FechaInicio + "' ";
                xml += "FechaFin ='" + obj.FechaFin + "' ";
                xml += "OrdenCompra ='" + obj.OrdenCompra + "' ";
                xml += "PedidoVenta ='" + obj.PedidoVenta + "' ";
                xml += "CantidadDesecho ='" + obj.CantidadDesecho.ToString(System.Globalization.CultureInfo.InvariantCulture) + "' />";

                foreach (dynamic dm in obj.LanzadoProduccion)
                {
                    xmlLanzado += "<LanzadoProduccion ";
                    xmlLanzado += "LanzadoProduccion = '" + dm.LanzadoProduccion + "' ";
                    xmlLanzado += "DOrdenCompraMaterial = '" + dm.DOrdenCompraMaterial + "' ";
                    xmlLanzado += "Empresa = '" + JWT.IdEmpresa + "' ";
                    xmlLanzado += "Sociedad = '" + JWT.IdSociedad + "' ";
                    xmlLanzado += "InstructivoProduccion = '" + obj.InstructivoProduccion + "' ";
                    xmlLanzado += "Material = '" + dm.Material + "' ";
                    xmlLanzado += "Proveedor = '" + dm.Proveedor + "' ";
                    xmlLanzado += "Almacen = '" + dm.Almacen + "' ";
                    xmlLanzado += "UnidadMedida = '" + dm.UnidadMedida + "' ";
                    xmlLanzado += "Lote = '" + dm.Lote + "' ";
                    xmlLanzado += "LineaProduccion = '" + dm.LineaProduccion + "' ";
                    xmlLanzado += "CantidadLanzado = '" + dm.CantidadLanzado.ToString(System.Globalization.CultureInfo.InvariantCulture) + "' />";
                }

                foreach (dynamic dm in obj.NotificacionProduccion)
                {
                    xmlNotificacionProduccion += "<NotificacionProduccion ";
                    xmlNotificacionProduccion += "OrdenProduccion = '" + dm.OrdenProduccion + "' ";
                    xmlNotificacionProduccion += "Migo = '" + dm.Migo + "' ";
                    xmlNotificacionProduccion += "Empresa = '" + JWT.IdEmpresa + "' ";
                    xmlNotificacionProduccion += "Sociedad = '" + JWT.IdSociedad + "' ";
                    xmlNotificacionProduccion += "InstructivoProduccion = '" + obj.InstructivoProduccion + "' ";
                    xmlNotificacionProduccion += "Material = '" + dm.Material + "' ";
                    xmlNotificacionProduccion += "Almacen = '" + dm.Almacen + "' ";
                    xmlNotificacionProduccion += "VersionFabricacion = '" + dm.VersionFabricacion + "' ";
                    xmlNotificacionProduccion += "Receta = '" + dm.Receta + "' ";
                    xmlNotificacionProduccion += "ListaMaterial = '" + dm.ListaMaterial + "' ";
                    xmlNotificacionProduccion += "Lote = '" + dm.LoteProduccion + "' ";
                    xmlNotificacionProduccion += "Cantidad = '" + dm.Cantidad.ToString(System.Globalization.CultureInfo.InvariantCulture) + "' ";
                    xmlNotificacionProduccion += "FechaInicio ='" + obj.FechaInicio + "' ";
                    xmlNotificacionProduccion += "FechaFin ='" + obj.FechaFin + "' ";
                    xmlNotificacionProduccion += "FechaNotificacion ='" + dm.FechaNotificacion + "' ";
                    xmlNotificacionProduccion += "FechaFabricacion ='" + dm.FechaFabricacion + "' ";
                    xmlNotificacionProduccion += "FechaVencimiento ='" + dm.FechaVencimiento + "' ";
                    xmlNotificacionProduccion += "ClaseOrdenProduccion ='" + obj.ClaseOrdenProduccion + "' ";
                    xmlNotificacionProduccion += "MetodoProduccion ='" + obj.MetodoProduccion + "' ";
                    xmlNotificacionProduccion += "UnidadMedida = '" + dm.UnidadMedida + "' />";
                }

                foreach (dynamic dp in obj.DistribucionProduccion)
                {
                    xmlDistribucionProduccion += "<DistribucionProduccion ";
                    xmlDistribucionProduccion += "OrdenProduccion = '" + dp.OrdenProduccion + "' ";
                    xmlDistribucionProduccion += "Migo = '" + dp.Migo + "' ";
                    xmlDistribucionProduccion += "Empresa = '" + JWT.IdEmpresa + "' ";
                    xmlDistribucionProduccion += "Sociedad = '" + JWT.IdSociedad + "' ";
                    xmlDistribucionProduccion += "InstructivoProduccion = '" + obj.InstructivoProduccion + "' ";
                    xmlDistribucionProduccion += "Material = '" + dp.Material + "' ";
                    xmlDistribucionProduccion += "Almacen = '" + dp.Almacen + "' ";
                    xmlDistribucionProduccion += "Lote = '" + dp.LoteProduccion + "' ";
                    xmlDistribucionProduccion += "Cantidad = '" + dp.Cantidad.ToString(System.Globalization.CultureInfo.InvariantCulture) + "' ";
                    xmlDistribucionProduccion += "UnidadMedida = '" + dp.UnidadMedida + "' ";
                    xmlDistribucionProduccion += "UnidadMedidaOriginal = '" + dp.UnidadMedidaOriginal + "' />";
                }

                xml += "</root>";
                xmlLanzado += "</root>";
                xmlNotificacionProduccion += "</root>";
                xmlDistribucionProduccion += "</root>";

                int res = Instancia.SaveUpdateConsumoDistribucionLanzado(JWT.Login, xml, xmlLanzado, xmlNotificacionProduccion, xmlDistribucionProduccion);
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
                    mensaje = "Ya existe un registro de consumo de materiales en esta fecha, nave y alamacén.";
                }

                if (res == -2)
                {
                    success = true;
                    mensaje = "Existen materiales sin stock.";
                }

                if (res == -3)
                {
                    success = true;
                    mensaje = "El periodo contable de este documento no está abierto.";
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

                var IHttpActionResult = Request.CreateResponse(HttpStatusCode.OK, respuesta);

                //var IHttpActionResult = Request.CreateResponse<object>(HttpStatusCode.OK, Grupo);
                IHttpActionResult.Headers.Add("Access-Control-Allow-Origin", "*");

                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                var error = MetodoComun.ObtenerExceptionModel(ex);

                var IHttpActionResult = Request.CreateResponse(HttpStatusCode.BadRequest, error);
                IHttpActionResult.Headers.Add("Access-Control-Allow-Origin", "*");

                return Ok(ex.ToString());
            }
        }

        [HttpPost]
        [ActionName("PP_UpdateEstadoInstructivoProduccion")]
        public IHttpActionResult PP_UpdateEstadoInstructivoProduccion(dynamic obj)
        {
            try
            {
                int idInstructivoProduccion = obj.InstructivoProduccion;
                string Estado = obj.Estado;
                string UsuarioRegistro = obj.Usuario;


                int dt = Instancia.updateEstadoOrdenProduccion(idInstructivoProduccion, Estado, UsuarioRegistro);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                //return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [ActionName("PP_GetLanzadoNotificacionByOrdenCompra")]
        public IHttpActionResult PP_GetLanzadoNotificacionByOrdenCompra(int prmintOrdenCompra)
        {
            try
            {
                DataTableCollection dt = Instancia.GetLanzadoNotificacionByOrdenCompra(prmintOrdenCompra);
                string json = JsonConvert.SerializeObject(dt, Newtonsoft.Json.Formatting.None);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                //return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [ActionName("PP_CerrarInstructivoProduccion")]
        public IHttpActionResult PP_CerrarInstructivoProduccion(dynamic obj)
        {
            try
            {
                string xml = "<?xml version='1.0' encoding='ISO-8859-1'?><root>";
                string xmlLanzado = "<?xml version='1.0' encoding='ISO-8859-1'?><root>";
                string xmlNotificacionProduccion = "<?xml version='1.0' encoding='ISO-8859-1'?><root>";
                string xmlDistribucionProduccion = "<?xml version='1.0' encoding='ISO-8859-1'?><root>";

                xml += "<InstructivoProduccion ";
                xml += "InstructivoProduccion ='" + obj.InstructivoProduccion + "' ";
                xml += "Empresa ='" + JWT.IdEmpresa + "' ";
                xml += "Sociedad ='" + JWT.IdSociedad + "' ";
                xml += "ClaseOrdenProduccion ='" + obj.ClaseOrdenProduccion + "' ";
                xml += "MetodoProduccion ='" + obj.MetodoProduccion + "' ";
                xml += "Centro ='" + obj.Centro + "' ";
                xml += "Nave ='" + obj.Nave + "' ";
                xml += "Estado ='" + obj.Estado + "' ";
                xml += "FechaInicio ='" + obj.FechaInicio + "' ";
                xml += "FechaFin ='" + obj.FechaFin + "' ";
                xml += "OrdenCompra ='" + obj.OrdenCompra + "' ";
                xml += "PedidoVenta ='" + obj.PedidoVenta + "' ";
                xml += "CantidadDesecho ='" + obj.CantidadDesecho.ToString(System.Globalization.CultureInfo.InvariantCulture) + "' />";

                foreach (dynamic dm in obj.LanzadoProduccion)
                {
                    xmlLanzado += "<LanzadoProduccion ";
                    xmlLanzado += "LanzadoProduccion = '" + dm.LanzadoProduccion + "' ";
                    xmlLanzado += "DOrdenCompraMaterial = '" + dm.DOrdenCompraMaterial + "' ";
                    xmlLanzado += "Empresa = '" + JWT.IdEmpresa + "' ";
                    xmlLanzado += "Sociedad = '" + JWT.IdSociedad + "' ";
                    xmlLanzado += "InstructivoProduccion = '" + obj.InstructivoProduccion + "' ";
                    xmlLanzado += "Material = '" + dm.Material + "' ";
                    xmlLanzado += "Proveedor = '" + dm.Proveedor + "' ";
                    xmlLanzado += "Almacen = '" + dm.Almacen + "' ";
                    xmlLanzado += "UnidadMedida = '" + dm.UnidadMedida + "' ";
                    xmlLanzado += "Lote = '" + dm.Lote + "' ";
                    xmlLanzado += "LineaProduccion = '" + dm.LineaProduccion + "' ";
                    xmlLanzado += "CantidadLanzado = '" + dm.CantidadLanzado.ToString(System.Globalization.CultureInfo.InvariantCulture) + "' />";
                }

                foreach (dynamic dm in obj.NotificacionProduccion)
                {
                    xmlNotificacionProduccion += "<NotificacionProduccion ";
                    xmlNotificacionProduccion += "OrdenProduccion = '" + dm.OrdenProduccion + "' ";
                    xmlNotificacionProduccion += "Migo = '" + dm.Migo + "' ";
                    xmlNotificacionProduccion += "Empresa = '" + JWT.IdEmpresa + "' ";
                    xmlNotificacionProduccion += "Sociedad = '" + JWT.IdSociedad + "' ";
                    xmlNotificacionProduccion += "InstructivoProduccion = '" + obj.InstructivoProduccion + "' ";
                    xmlNotificacionProduccion += "Material = '" + dm.Material + "' ";
                    xmlNotificacionProduccion += "Almacen = '" + dm.Almacen + "' ";
                    xmlNotificacionProduccion += "VersionFabricacion = '" + dm.VersionFabricacion + "' ";
                    xmlNotificacionProduccion += "Receta = '" + dm.Receta + "' ";
                    xmlNotificacionProduccion += "ListaMaterial = '" + dm.ListaMaterial + "' ";
                    xmlNotificacionProduccion += "Lote = '" + dm.LoteProduccion + "' ";
                    xmlNotificacionProduccion += "Cantidad = '" + dm.Cantidad.ToString(System.Globalization.CultureInfo.InvariantCulture) + "' ";
                    xmlNotificacionProduccion += "FechaInicio ='" + obj.FechaInicio + "' ";
                    xmlNotificacionProduccion += "FechaFin ='" + obj.FechaFin + "' ";
                    xmlNotificacionProduccion += "FechaNotificacion ='" + dm.FechaNotificacion + "' ";
                    xmlNotificacionProduccion += "FechaFabricacion ='" + dm.FechaFabricacion + "' ";
                    xmlNotificacionProduccion += "FechaVencimiento ='" + dm.FechaVencimiento + "' ";
                    xmlNotificacionProduccion += "ClaseOrdenProduccion ='" + obj.ClaseOrdenProduccion + "' ";
                    xmlNotificacionProduccion += "MetodoProduccion ='" + obj.MetodoProduccion + "' ";
                    xmlNotificacionProduccion += "UnidadMedida = '" + dm.UnidadMedida + "' />";
                }

                foreach (dynamic dp in obj.DistribucionProduccion)
                {
                    xmlDistribucionProduccion += "<DistribucionProduccion ";
                    xmlDistribucionProduccion += "OrdenProduccion = '" + dp.OrdenProduccion + "' ";
                    xmlDistribucionProduccion += "Migo = '" + dp.Migo + "' ";
                    xmlDistribucionProduccion += "Empresa = '" + JWT.IdEmpresa + "' ";
                    xmlDistribucionProduccion += "Sociedad = '" + JWT.IdSociedad + "' ";
                    xmlDistribucionProduccion += "InstructivoProduccion = '" + obj.InstructivoProduccion + "' ";
                    xmlDistribucionProduccion += "Material = '" + dp.Material + "' ";
                    xmlDistribucionProduccion += "Almacen = '" + dp.Almacen + "' ";
                    xmlDistribucionProduccion += "Lote = '" + dp.LoteProduccion + "' ";
                    xmlDistribucionProduccion += "Cantidad = '" + dp.Cantidad.ToString(System.Globalization.CultureInfo.InvariantCulture) + "' ";
                    xmlDistribucionProduccion += "UnidadMedida = '" + dp.UnidadMedida + "' ";
                    xmlDistribucionProduccion += "UnidadMedidaOriginal = '" + dp.UnidadMedidaOriginal + "' />";
                }

                xml += "</root>";
                xmlLanzado += "</root>";
                xmlNotificacionProduccion += "</root>";
                xmlDistribucionProduccion += "</root>";

                int res = Instancia.CerrarInstructivoProduccion(JWT.Login, xml, xmlLanzado, xmlNotificacionProduccion, xmlDistribucionProduccion);
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
                    mensaje = "Ya existe un registro de consumo de materiales en esta fecha, nave y alamacén.";
                }

                if (res == -2)
                {
                    success = true;
                    mensaje = "Existen materiales sin stock.";
                }

                if (res == -3)
                {
                    success = true;
                    mensaje = "El periodo contable de este documento no está abierto.";
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

                var IHttpActionResult = Request.CreateResponse(HttpStatusCode.OK, respuesta);

                //var IHttpActionResult = Request.CreateResponse<object>(HttpStatusCode.OK, Grupo);
                IHttpActionResult.Headers.Add("Access-Control-Allow-Origin", "*");

                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                var error = MetodoComun.ObtenerExceptionModel(ex);

                var IHttpActionResult = Request.CreateResponse(HttpStatusCode.BadRequest, error);
                IHttpActionResult.Headers.Add("Access-Control-Allow-Origin", "*");

                return Ok(ex.ToString());
            }
        }

        [HttpGet]
        [ActionName("PP_GetProveedoresList")]
        public IHttpActionResult PP_GetProveedoresList()
        {
            try
            {
                DataTable dt = Instancia.GetProveedoresList();
                return Ok(dt);
            }
            catch (Exception ex)
            {
                //return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [ActionName("PP_GetPredioList")]
        public IHttpActionResult PP_GetPredioList(int prmIntProveedor)
        {
            try
            {
                DataTable dt = Instancia.GetPredioList(prmIntProveedor);
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
