using System;
using System.Data;

using WS_MM.Models;
using static WS_MM.Models.ProveedorModel;
using Shared.Controllers;
using Shared.Helpers;
using System.Web.Http;
using Shared.Util;
using Newtonsoft.Json;
using System.Xml;

namespace WS_MM.Controllers
{
    [CorsOrigin]
    [Authorize]
    public class ProveedorSociedadController : BaseController
    {

        [HttpPost]
        [ActionName("PMMM_Guardar")]
        public IHttpActionResult Guardar([FromBody] dynamic request)
        {
            try
            {
                int ProveedorSociedad = request.ProveedorSociedad;
                int Proveedor = request.Proveedor;
                int Sociedad = request.Sociedad;
                string FormaPago = request.FormaPago;
                string CuentaAcreedor = request.CuentaAcreedor;
                string Idioma = request.Idioma;
                string Moneda = request.Moneda;

                var contactos = JsonConvert.SerializeObject(request.Contactos);
                XmlNode contactosXmlNode = JsonConvert.DeserializeXmlNode("{\"row\":" + contactos + "}", "contactos");
                string contactosXml = contactosXmlNode.InnerXml;

                DataTable dt = ProveedorSociedadModel.Instancia.Guardar(ProveedorSociedad, JWT.IdEmpresa, Proveedor, Sociedad,
                                                                FormaPago, CuentaAcreedor, Idioma, Moneda,
                                                                JWT.Login, contactosXml);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleError(ex));
            }
        }

        [HttpGet]
        [ActionName("PMMM_Obtener")]
        public IHttpActionResult PMMM_Obtener(int Proveedor, int Sociedad)
        {
            try
            {
                DataTable dt = ProveedorSociedadModel.Instancia.Obtener(JWT.IdEmpresa, Proveedor, Sociedad);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleError(ex));
            }
        }
    }
}
