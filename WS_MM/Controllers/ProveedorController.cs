using System;
using System.Data;

using WS_MM.Models;
using static WS_MM.Models.ProveedorModel;
using Shared.Controllers;
using Shared.Helpers;
using System.Web.Http;
using Shared.Util;
using Newtonsoft.Json;
using System.Linq;
using System.Xml;

namespace WS_MM.Controllers
{
    [CorsOrigin]
    [Authorize]
    public class ProveedorController : BaseController
    {
        [HttpGet]
        [ActionName("PMMM_GetProveedores")]
        public IHttpActionResult PMMM_GetProveedores(string prmintEmpresa, string prmstrGrupoProveedor, Boolean? prmbitActivo)
        {
            try
            {
                DataTable dt = Instancia.GetProveedores(JWT.IdEmpresa, prmstrGrupoProveedor, prmbitActivo);
                string json = JsonConvert.SerializeObject(dt, Newtonsoft.Json.Formatting.None);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpPost]
        [ActionName("PMMM_Guardar")]
        public IHttpActionResult Guardar([FromBody] dynamic request)
        {
            try
            {
                int Proveedor = request.Proveedor;
                string CodProveedor = request.CodProveedor;
                int GrupoProveedor = request.GrupoProveedor;
                string GrupoCuenta = request.GrupoCuenta;
                int Pais = request.Pais;
                string CondicionBusqueda = request.CondicionBusqueda;
                string RazonSocial = request.RazonSocial;
                string NombreComercial = request.NombreComercial;
                string TipoDocumento = request.TipoDocumento;
                string Documento = request.Documento;
                string Direccion = request.Direccion;
                string Telefono1 = request.Telefono1;
                string Telefono2 = request.Telefono2;
                string Telefono3 = request.Telefono3;
                string Celular = request.Celular;
                string Email = request.Email;
                string Observacion = request.Observacion;
                string Fax = request.Fax;
                bool Activo = request.Activo;

                DataTable dt = ProveedorModel.Instancia.Guardar(Proveedor, JWT.IdEmpresa, CodProveedor, GrupoProveedor, GrupoCuenta,
                                                                Pais, CondicionBusqueda, RazonSocial, NombreComercial, 
                                                                TipoDocumento, Documento, Direccion, Telefono1, 
                                                                Telefono2, Telefono3, Celular, Email,
                                                                Observacion, Fax, Activo, JWT.Login);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleError(ex));
            }
        }

        [HttpPost]
        [ActionName("PMMM_ActualizarActivo")]
        public IHttpActionResult PMMM_ActualizarActivo([FromBody] dynamic request)
        {
            try
            {
                int Proveedor = request.Proveedor;
                bool Activo = request.Activo;
                DataTable dt = ProveedorModel.Instancia.ActualizarActivo(Proveedor, JWT.IdEmpresa, Activo, JWT.Login);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleError(ex));
            }
        }

        [HttpGet]
        [ActionName("PMMM_Obtener")]
        public IHttpActionResult PMMM_Obtener(int Proveedor)
        {
            try
            {
                DataTable dt = ProveedorModel.Instancia.Obtener(Proveedor, JWT.IdEmpresa);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleError(ex));
            }
        }

        [HttpGet]
        [ActionName("PMMM_Listar")]
        public IHttpActionResult PMMM_Listar(int GrupoProveedor, int Activo)
        {
            try
            {
                DataTable dt = ProveedorModel.Instancia.Listar(JWT.IdEmpresa, GrupoProveedor, Activo);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleError(ex));
            }
        }

        [HttpGet]
        [ActionName("PMMM_Buscar")]
        public IHttpActionResult PMMM_Buscar(int GrupoProveedor, int Activo, string CondicionBusqueda, string Query)
        {
            try
            {
                DataTable dt = ProveedorModel.Instancia.Buscar(JWT.IdEmpresa, GrupoProveedor, Activo, CondicionBusqueda, Query);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleError(ex));
            }
        }

        [HttpGet]
        [ActionName("MM_getPredioProveedor")]
        public IHttpActionResult MM_getPredioProveedor(int Proveedor)
        {
            try
            {
                DataTable dt = ProveedorModel.Instancia.getPredioProveedor(Proveedor);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleError(ex));
            }
        }


        [HttpPost]
        [ActionName("MM_saveUpdatePredioProveedor")]
        public IHttpActionResult MM_saveUpdatePredioProveedor(dynamic obj)
        {
            try
            {
                XmlDocument XML = new XmlDocument();
                XmlDeclaration xmldecl = XML.CreateXmlDeclaration("1.0", "ISO-8859-1", null);
                XML.InsertBefore(xmldecl, XML.DocumentElement);
                XML.AppendChild(XML.CreateElement("ROOT"));
                int Proveedor = obj.Proveedor;
                foreach (var c in obj.Predios)
                {
                    XmlElement xmlPredios = default(XmlElement);
                    xmlPredios = XML.CreateElement("Predios");
                    xmlPredios.SetAttribute("Proveedor", c.Proveedor.ToString());
                    xmlPredios.SetAttribute("TipoPredio", c.TipoPredio.ToString());
                    xmlPredios.SetAttribute("Departamento", c.Departamento.ToString());
                    xmlPredios.SetAttribute("Provincia", c.Provincia.ToString());
                    xmlPredios.SetAttribute("Vereda", c.Vereda.ToString());
                    xmlPredios.SetAttribute("Nombre", c.Nombre.ToString());
                    xmlPredios.SetAttribute("Direccion", c.Direccion.ToString());
                    XML.DocumentElement.AppendChild(xmlPredios);
                }
                string XML_PREDIOS = XML.OuterXml;
                DataTable dt = ProveedorModel.Instancia.saveUpdatePredioProveedor(Proveedor, XML_PREDIOS, JWT.Login);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleError(ex));
            }
        }


        [HttpGet]
        [ActionName("PMMM_Listar_Acopiadores")]
        public IHttpActionResult PMMM_Listar_Acopiadores()
        {
            try
            {
                DataTable dt = ProveedorModel.Instancia.Listar_Acopiadores(JWT.IdEmpresa);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleError(ex));
            }
        }


        [HttpGet]
        [ActionName("MM_getProveedorAcopiador_List")]
        public IHttpActionResult MM_getProveedorAcopiador_List(int Proveedor)
        {
            try
            {
                DataTable dt = ProveedorModel.Instancia.getProveedorAcopiador_List(Proveedor);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleError(ex));
            }
        }

        [HttpPost]
        [ActionName("MM_saveUpdateProveedorAcopio")]
        public IHttpActionResult MM_saveUpdateProveedorAcopio(dynamic obj)
        {
            try
            {
                XmlDocument XML = new XmlDocument();
                XmlDeclaration xmldecl = XML.CreateXmlDeclaration("1.0", "ISO-8859-1", null);
                XML.InsertBefore(xmldecl, XML.DocumentElement);
                XML.AppendChild(XML.CreateElement("ROOT"));
                int Proveedor = obj.Proveedor;
                foreach (var c in obj.Acopios)
                {
                    XmlElement xmlAcopios = default(XmlElement);
                    xmlAcopios = XML.CreateElement("Acopiadores");
                    xmlAcopios.SetAttribute("Acopiador", c.Acopiador.ToString());
                    xmlAcopios.SetAttribute("Activo", c.Activo.ToString());
                    xmlAcopios.SetAttribute("FinVigencia", c.FinVigencia.ToString());
                    xmlAcopios.SetAttribute("InicioVigencia", c.InicioVigencia.ToString());
                    xmlAcopios.SetAttribute("Proveedor", c.Proveedor.ToString());
                    XML.DocumentElement.AppendChild(xmlAcopios);
                }
                string XML_PREDIOS = XML.OuterXml;
                DataTable dt = ProveedorModel.Instancia.saveUpdateProveedorAcopio(Proveedor, XML_PREDIOS, JWT.Login);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleError(ex));
            }
        }

        [HttpGet]
        [ActionName("PMMM_getTipoCertificacion_list")]
        public IHttpActionResult PMMM_getTipoCertificacion_list()
        {
            try
            {
                DataTable dt = ProveedorModel.Instancia.getTipoCertificacion_list(JWT.IdEmpresa);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleError(ex));
            }
        }
        [HttpGet]
        [ActionName("MM_getProveedorCertificaciones_list")]
        public IHttpActionResult MM_getProveedorCertificaciones_list(int Proveedor)
        {
            try
            {
                DataTable dt = ProveedorModel.Instancia.getProveedorCertificaciones_list(Proveedor);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleError(ex));
            }
        }
        [HttpPost]
        [ActionName("MM_saveUpdateProveedorCertificado")]
        public IHttpActionResult MM_saveUpdateProveedorCertificado(dynamic obj)
        {
            try
            {
                XmlDocument XML = new XmlDocument();
                XmlDeclaration xmldecl = XML.CreateXmlDeclaration("1.0", "ISO-8859-1", null);
                XML.InsertBefore(xmldecl, XML.DocumentElement);
                XML.AppendChild(XML.CreateElement("ROOT"));
                int Proveedor = obj.Proveedor;
                foreach (var c in obj.Certificados)
                {
                    XmlElement xmlCertif = default(XmlElement);
                    xmlCertif = XML.CreateElement("Certificaciones");
                    xmlCertif.SetAttribute("ProveedorCertificado", c.ProveedorCertificado.ToString());
                    xmlCertif.SetAttribute("ProveedorPredio", c.ProveedorPredio.ToString());
                    xmlCertif.SetAttribute("TipoCertificacion", c.TipoCertificacion.ToString());
                    xmlCertif.SetAttribute("Nombre", c.Nombre.ToString());
                    xmlCertif.SetAttribute("Activo", c.Activo.ToString());
                    xmlCertif.SetAttribute("FinVigencia", c.FinVigencia.ToString());
                    xmlCertif.SetAttribute("InicioVigencia", c.InicioVigencia.ToString());
                    xmlCertif.SetAttribute("Proveedor", c.Proveedor.ToString());
                    xmlCertif.SetAttribute("NombreArchivo", c.NombreArchivo.ToString());
                    xmlCertif.SetAttribute("RutaArchivo", c.RutaArchivo.ToString());
                    XML.DocumentElement.AppendChild(xmlCertif);
                }
                string XML_CERTIFICADOS = XML.OuterXml;
                DataTable dt = ProveedorModel.Instancia.saveUpdateProveedorCertificado(Proveedor, XML_CERTIFICADOS, JWT.Login);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleError(ex));
            }
        }

        [HttpGet]
        [ActionName("PMMM_GetAgricultoresPredios")]
        public IHttpActionResult PMMM_GetAgricultoresPredios()
        {
            try
            {
                DataTable dt = ProveedorModel.Instancia.GetAgricultoresPredios(JWT.IdEmpresa);

                var obj = (from c in dt.AsEnumerable()
                           select new
                           {
                               Proveedor = Convert.ToInt32(c["Proveedor"]),
                               RazonSocial = c["RazonSocial"].ToString(),
                               GrupoProveedor = Convert.ToInt32(c["GrupoProveedor"]),
                               NombreGrupoProveedor = c["NombreGrupoProveedor"].ToString(),
                               Predios = (from v in dt.AsEnumerable()
                                          where Convert.ToInt32(c["Proveedor"]).Equals(Convert.ToInt32(v["Proveedor"]))
                                          select new
                                          {
                                              ProveedorPredio = Convert.ToInt32(v["ProveedorPredio"]),
                                              NombrePredio = v["NombrePredio"].ToString(),
                                              Departamento = v["Departamento"].ToString(),
                                              Vereda = v["Vereda"].ToString()
                                          }).GroupBy(v => new { v.ProveedorPredio }).Select(v => v.First())
                           }).GroupBy(c => new { c.Proveedor }).Select(c => c.First());

                //var httpResponseMessage = Request.CreateResponse<IEnumerable<Object>>(HttpStatusCode.OK, obj);
                //httpResponseMessage.Headers.Add("Access-Control-Allow-Origin", "*");

                //return httpResponseMessage;
                return Ok(obj);
            }
            catch (Exception ex)
            {
                //return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [ActionName("MM_getTipoPredio_list")]
        public IHttpActionResult MM_getTipoPredio_list(int Activo)
        {
            try
            {
                DataTable dt = ProveedorModel.Instancia.getTipoPredio_list(JWT.IdEmpresa, Activo);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleError(ex));
            }
        }
    }
}
