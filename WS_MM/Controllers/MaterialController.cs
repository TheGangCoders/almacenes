using System;
using System.Data;
using static WS_MM.Models.MaterialModelDAL;
using WS_MM.Models;
using Shared.Controllers;
using Shared.Helpers;
using System.Web.Http;
using Shared.Util;
using Newtonsoft.Json;
using System.Xml;
using System.Linq;

namespace WS_MM.Controllers
{
    [CorsOrigin]
    [Authorize]
    public class MaterialController : BaseController
    {
        [HttpGet]
        [ActionName("PMMM_dataModalMaterial")]
        public IHttpActionResult PMMM_dataModalMaterial(int prmintMaterial)
        {
            try
            {
                DataTableCollection dt = Instancia.dataModalMaterial(prmintMaterial);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleError(ex));
            }
        }

        [HttpGet]
        [ActionName("PMMM_GetMaterial")]
        public IHttpActionResult PMMM_GetMaterial(int prmintTipoMaterial, int prmintGrupoArticulo, Boolean? prmbitStatus)
        {
            try
            {
                DataTable dt = Instancia.GetMaterial(JWT.IdEmpresa, prmintTipoMaterial, prmintGrupoArticulo, prmbitStatus);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpPost]
        [ActionName("PMMM_GetReporteStockMaterial")]
        public IHttpActionResult PMMM_GetReporteStockMaterial(dynamic obj)
        {
            int prmintSociedad = obj.prmintSociedad;
            int prmintTipoMaterial = obj.prmintTipoMaterial;
            int prmintGrupoArticulo = obj.prmintGrupoArticulo;
            int prmintMaterial = obj.prmintMaterial;
            string prmstrLote = obj.prmstrLote;
            int prmintCentro = obj.prmintCentro;
            int prmintAlmacen = obj.prmintAlmacen;
            int prmintTipo = obj.prmintTipo;
            string FechaHasta = obj.prmFechaLimite;
            int pkUser = JWT.IdUsuario;
            try
            {
                DataTable dt = Instancia.GetReporteStockMaterial(prmintSociedad, prmintTipoMaterial, prmintGrupoArticulo, prmintMaterial, prmstrLote,prmintCentro,prmintAlmacen, prmintTipo, FechaHasta, pkUser);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpGet]
        [ActionName("PMMM_getMateriales_List")]
        public IHttpActionResult PMMM_getMateriales_List(int prmintTipoMaterial, Boolean? prmbitStatus)
        {
            try
            {
                DataTable dt = Instancia.getMateriales_List(JWT.IdEmpresa, prmintTipoMaterial, prmbitStatus);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpGet]
        [ActionName("PMMM_GetMaterialByCodigo")]
        public IHttpActionResult PMMM_GetMaterialByCodigo(String prmstrCodigo)
        {
            try
            {
                DataTable dt = Instancia.getMateriaByCodigo(JWT.IdEmpresa, prmstrCodigo);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpGet]
        [ActionName("PMMM_getClaseMaterial")]
        public IHttpActionResult PMMM_getClaseMaterial()
        {
            try
            {
                DataTable dt = Instancia.getClaseMaterial(JWT.IdEmpresa);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpGet]
        [ActionName("PMMM_getClaseMaterial_REAL")]
        public IHttpActionResult PMMM_getClaseMaterial_REAL()
        {
            try
            {
                DataTable dt = Instancia.getClaseMaterial_REAL(JWT.IdEmpresa);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpPost]
        [ActionName("PMMM_GuardarDatosGenerales")]
        public IHttpActionResult PMMM_GuardarDatosGenerales(dynamic obj)
        {
            try
            {
                int idMaterial = obj.idMaterial;
                int idTipoMaterial = obj.idTipoMaterial;
                int idGrupoArticulo = obj.idGrupoArticulo;
                string idClase = obj.idClase;
                string codigo = obj.codigo;
                string descripcion = obj.descripcion;
                int idUnidadMedida = obj.idUnidadMedida;
                string pesoNeto = obj.pesoNeto;
                string pesoBruto = obj.pesoBruto;
                string idUnidadMedidaPeso = obj.idUnidadMedidaPeso;
                string volumen = obj.volumen;
                bool productoControlado = obj.productoControlado;
                bool inafecto = obj.inafecto;
                string partidaArancelaria = obj.partidaArancelaria;
                bool activo = obj.activo;
                Boolean DUA = obj.DUA_Importacion;
                var caracteristicas = JsonConvert.SerializeObject(obj.caracteristicas);
                XmlNode caracteristicasXmlNode = JsonConvert.DeserializeXmlNode("{\"row\":" + caracteristicas + "}", "caracteristicas");
                string caracteristicasXml = caracteristicasXmlNode.InnerXml;
                
                DataTable dt = Instancia.GuardarDatosGenerales(idMaterial, JWT.IdEmpresa, idTipoMaterial, idGrupoArticulo, idClase, codigo, descripcion,
                                                idUnidadMedida, pesoNeto, pesoBruto, idUnidadMedidaPeso, volumen, productoControlado, inafecto, partidaArancelaria, activo, JWT.Login, caracteristicasXml, DUA);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleError(ex));
            }
        }

        [HttpPost]
        [ActionName("PMMM_GuardarVentas")]
        public IHttpActionResult PMMM_GuardarVentas(dynamic obj)
        {
            try
            {
                int idMaterialOrgVentas = obj.idMaterialOrgVentas;
                int idMaterial = obj.idMaterial;
                int idOrgVentas = obj.idOrgVentas;
                int idUnidadMedidaVenta = obj.idUnidadMedidaVenta;
                int idCentroSuministrador = obj.idCentroSuministrador;
                bool habilitadoVenta = obj.habilitadoVenta;
                bool activo = obj.activo;
                
                var nombresComerciales = JsonConvert.SerializeObject(obj.nombresComerciales);
                var factores = JsonConvert.SerializeObject(obj.factores);
                XmlNode nombresComercialesXmlNode = JsonConvert.DeserializeXmlNode("{\"row\":" + nombresComerciales + "}", "nombresComerciales");
                string nombresComercialesXml = nombresComercialesXmlNode.InnerXml;

                XmlNode factoresXmlNode = JsonConvert.DeserializeXmlNode("{\"row\":" + factores + "}", "factores");
                string factoresXml = factoresXmlNode.InnerXml;

                DataTable dt = Instancia.GuardarVentas(idMaterialOrgVentas, JWT.IdEmpresa, idMaterial, idOrgVentas, idUnidadMedidaVenta,
                                                idCentroSuministrador, habilitadoVenta, activo, JWT.Login, nombresComercialesXml, factoresXml);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleError(ex));
            }
        }
        
        [HttpPost]
        [ActionName("PMMM_GuardarLogistica")]
        public IHttpActionResult PMMM_GuardarLogistica(dynamic obj)
        {
            try
            {
                int idMaterialCentro = obj.idMaterialCentro;
                int idMaterial = obj.idMaterial;
                int idCentro = obj.idCentro;
                int idUnidadMedidaCompra = obj.idUnidadMedidaCompra;
                int idUnidadMedidaSalida = obj.idUnidadMedidaSalida;
                decimal stockMinimo = obj.stockMinimo;
                decimal stockMaximo = obj.stockMaximo;
                int tiempoEntrada = obj.tiempoEntrada;
                int plazoEntrega = obj.plazoEntrega;
                string claseAprovisionamiento = obj.claseAprovisionamiento;
                string centroBeneficio = obj.centroBeneficio;
                string planificador = obj.planificador;
                int almacenProduccion = obj.almacenProduccion;
                bool activo = obj.activo;
                
                var almacenes = JsonConvert.SerializeObject(obj.almacenes);
                XmlNode almacenesXmlNode = JsonConvert.DeserializeXmlNode("{\"row\":" + almacenes + "}", "almacenes");
                string almacenesXml = almacenesXmlNode.InnerXml;
                
                DataTable dt = Instancia.GuardarLogistica(idMaterialCentro, JWT.IdEmpresa, idMaterial, idCentro, idUnidadMedidaCompra, idUnidadMedidaSalida,
                                stockMinimo, stockMaximo, tiempoEntrada, plazoEntrega, claseAprovisionamiento, centroBeneficio, planificador, almacenProduccion, activo, JWT.Login, almacenesXml);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleError(ex));
            }
        }
        
        [HttpPost]
        [ActionName("PMMM_GuardarContabilidad")]
        public IHttpActionResult PMMM_GuardarContabilidad(dynamic obj)
        {
            try
            {
                int idMaterial = obj.idMaterial;
                int idOrgVenta = obj.idOrgVenta;
                int idMaterialOrgVentas = obj.idMaterialOrgVentas;
                int idRetencion = obj.idRetencion;
                string tipoExistencia = obj.tipoExistencia;
                DateTime fechaClave = obj.fechaClave;
                
                var precios = JsonConvert.SerializeObject(obj.precios);
                XmlNode preciosXmlNode = JsonConvert.DeserializeXmlNode("{\"row\":" + precios + "}", "precios");
                string preciosXml = preciosXmlNode.InnerXml;
                
                DataTable dt = Instancia.GuardarContabilidad(idMaterial, idOrgVenta, idMaterialOrgVentas, JWT.IdEmpresa, idRetencion,
                                tipoExistencia, fechaClave, JWT.Login, preciosXml);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleError(ex));
            }
        }

        [HttpGet]
        [ActionName("PMMM_GetDataMaterialById")]
        public IHttpActionResult PMMM_GetDataMaterialById(int prmId)
        {
            try
            {
                DataTable dt = Instancia.getDataMateriaByIdMaterial(JWT.IdEmpresa, prmId);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpGet]
        [ActionName("PMMM_GetDataMaterialCaracteristicaByIdMaterial")]
        public IHttpActionResult PMMM_GetDataMaterialCaracteristicaByIdMaterial(int prmIdMaterial)
        {
            try
            {
                DataTable dt = Instancia.getDataMateriaCaracteristicaByIdMaterial(JWT.IdEmpresa, prmIdMaterial);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpGet]
        [ActionName("PMMM_GetDataMaterialVentasByIdMaterial")]
        public IHttpActionResult PMMM_GetDataMaterialVentasByIdMaterial(int prmIdMaterial, int prmSociedad, int prmOrgVentas)
        {
            try
            {
                DataTable dt = Instancia.getDataMaterialVentasByIdMaterial(JWT.IdEmpresa, prmIdMaterial, prmSociedad, prmOrgVentas);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpGet]
        [ActionName("PMMM_GetDataMaterialVentasByIdMaterial_RE")]
        public IHttpActionResult PMMM_GetDataMaterialVentasByIdMaterial_RE(int prmIdMaterial, int prmSociedad, int prmOrgVentas)
        {
            try
            {
                DataTableCollection dt = Instancia.getDataMaterialVentasByIdMaterial_re(JWT.IdEmpresa, prmIdMaterial, prmSociedad, prmOrgVentas);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpGet]
        [ActionName("PMMM_GetDataContabilidadById_RE")]
        public IHttpActionResult PMMM_GetDataContabilidadById_RE(int prmIdMaterial, int prmSociedad, int prmOrgVentas)
        {
            try
            {
                DataTableCollection dt = Instancia.getDataContabilidadByIdMaterial_re(JWT.IdEmpresa, prmIdMaterial, prmSociedad, prmOrgVentas);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpGet]
        [ActionName("PMMM_GetDataLogistiById_RE")]
        public IHttpActionResult PMMM_GetDataLogistiById_RE(int Material, int Centro)
        {
            try
            {
                DataTableCollection dt = Instancia.GetDataLogistiById_RE(JWT.IdEmpresa, Material, Centro);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleError(ex));
            }
        }





        [HttpPost]
        [ActionName("PMMM_Eliminar")]
        public IHttpActionResult PMMM_Eliminar([FromBody] dynamic request)
        {
            try
            {
                int Material = request.Material;
                DataTable dt = MaterialModelDAL.Instancia.Eliminar(Material, JWT.IdEmpresa, JWT.Login);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleError(ex));
            }
        }

        [HttpGet]
        [ActionName("PMMM_enable_disable_Material")]
        public IHttpActionResult PMMM_enable_disable_Material(int idMaterial, Boolean future_status)
        {
            try
            {
                DataTable dt = MaterialModelDAL.Instancia.enable_disable_Material(idMaterial, future_status, JWT.Login);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleError(ex));
            }
        }

        [HttpGet]
        [ActionName("PMMM_GetDataCaracteristicaByTipoMaterial")]
        public IHttpActionResult PMMM_GetDataCaracteristicaByTipoMaterial(int prmIdTipoMaterial)
        {
            try
            {
                DataTable dt = Instancia.getDataCaracteristicaByTipoMaterial(JWT.IdEmpresa, prmIdTipoMaterial);
                var eval = (from c in dt.AsEnumerable()
                            select new
                            {
                                TipoMaterialCaracteristica = c["TipoMaterialCaracteristica"],
                                TipoMaterial = c["TipoMaterial"],
                                Caracteristica = c["Caracteristica"],
                                CaracteristicaNombre = c["CaracteristicaNombre"],
                                TipoValor = c["TipoValor"],
                                Maestro = c["Maestro"],
                                Obligatorio = c["Obligatorio"],
                                Activo = c["Activo"],

                                array_valores = (from dd in dt.AsEnumerable()
                                                where dd.Field<Int32>("TipoMaterialCaracteristica").Equals(c["TipoMaterialCaracteristica"]) &&
                                                dd.Field<Int32>("TipoMaterial").Equals(c["TipoMaterial"]) &&
                                                dd.Field<Int32>("Caracteristica").Equals(c["Caracteristica"]) &&
                                                dd.Field<String>("TipoValor").Equals(c["TipoValor"])
                                                orderby dd["CaracteristicaValor"] ascending
                                                select new
                                                {
                                                    CaracteristicaValor = dd["CaracteristicaValor"],
                                                    DescripcionValor = dd["DescripcionValor"]
                                                }).GroupBy(o => new { o.CaracteristicaValor }).Select(o => o.First())
                            }).GroupBy(o => new { o.TipoMaterialCaracteristica }).Select(o => o.First());



                return Ok(eval);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpGet]
        [ActionName("PMMM_Buscar")]
        public IHttpActionResult PMMM_Buscar(int Activo, string Codigo, string Query)
        {
            try
            {
                DataTable dt = MaterialModelDAL.Instancia.Buscar(JWT.IdEmpresa, Activo, Codigo, Query);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleError(ex));
            }
        }

        [HttpPost]
        [ActionName("PMMM_GetReporteMaterial")]
        public IHttpActionResult PMMM_GetReporteMaterial(dynamic obj)
        {
            int prmintTipoReporte = obj.prmintTipoReporte;
            int prmintTipoMaterial = obj.prmintTipoMaterial;
            int prmintGrupoArticulo = obj.prmintGrupoArticulo;
            int prmintMaterial = obj.prmintMaterial;
            int prmintCentro = obj.prmintCentro;

            try
            {
                DataTable dt = Instancia.GetReporteMaterial(JWT.IdEmpresa, prmintTipoReporte, prmintTipoMaterial, prmintGrupoArticulo, prmintMaterial, prmintCentro);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpGet]
        [ActionName("MM_listMateriales_paraOC")]
        public IHttpActionResult MM_listMateriales_paraOC(string CodMaterial, string Material, string Calidad, string TipoMaterial, string GrupoArticulo)
        {
            try
            {
                DataTable dt = Instancia.listMateriales_paraOC(JWT.IdEmpresa, CodMaterial, Material, Calidad, TipoMaterial, GrupoArticulo);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpGet]
        [ActionName("MM_ListarCalidadMaterial")]
        public IHttpActionResult MM_ListarCalidadMaterial()
        {
            try
            {
                DataTable dt = Instancia.ListarCalidadMaterial(JWT.IdEmpresa);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }


        [HttpGet]
        [ActionName("PMMM_getEquivalencia_List")]
        public IHttpActionResult PMMM_getEquivalencia_List(int idMaterial)
        {
            try
            {
                DataTable dt = Instancia.getEquivalencia_List(JWT.IdEmpresa, idMaterial);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpGet]
        [ActionName("PMMM_getEquivalencia_Data_byMaterial")]
        public IHttpActionResult PMMM_getEquivalencia_Data_byMaterial(int idMaterial, int idBase, int idEquivalente)
        {
            try
            {
                DataTable dt = Instancia.getEquivalencia_Data_byMaterial(JWT.IdEmpresa, idMaterial, idBase, idEquivalente);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }


        [HttpPost]
        [ActionName("PMMM_saveUpdateEquivalencia")]
        public IHttpActionResult PMMM_saveUpdateEquivalencia(dynamic obj)
        {

            string XML = obj.prmXML;
            int idMaterial = obj.idMaterial;
            try
            {
                DataTable dt = Instancia.saveUpdateEquivalencia(JWT.IdEmpresa,idMaterial,JWT.Login, XML);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

    }
}
