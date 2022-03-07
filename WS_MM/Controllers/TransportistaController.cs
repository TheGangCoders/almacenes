using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;

using static WS_MM.Models.TransportistaModel;
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
    public class TransportistaController : BaseController
    {
        [HttpGet]
        [ActionName("PMMM_GetTransportista")]
        public HttpResponseMessage PMMM_GetTransportista(int prmintEmpresa, Boolean? prmbitActivo)
        {
            try
            {
                DataTable dt = Instancia.GetTransportista(prmintEmpresa, prmbitActivo);

                var obj = (from c in dt.AsEnumerable()
                           select new
                           {
                               Proveedor = Convert.ToInt64(c["Proveedor"]),
                               NombreComercial = c["NombreComercial"].ToString(),
                               RazonSocial = c["RazonSocial"].ToString(),
                               Pais = Convert.ToInt32(c["Pais"]),
                               NombrePais = c["NombrePais"].ToString(),
                               TipoDocumento = c["TipoDocumento"].ToString(),
                               DescTipoDocumento = c["DescTipoDocumento"].ToString(),
                               Documento = c["Documento"].ToString(),
                               Direccion = c["Direccion"].ToString(),
                               Telefono1 = c["Telefono1"].ToString(),
                               Telefono2 = c["Telefono2"].ToString(),
                               Telefono3 = c["Telefono3"].ToString(),
                               Celular = c["Celular"].ToString(),
                               Email = c["Email"].ToString(),
                               Fax = c["Fax"].ToString(),
                               ActProveedor = c["ActProveedor"].ToString(),

                               Vehiculos = (from d in dt.AsEnumerable()
                                               where Convert.ToInt64(c["Proveedor"]).Equals(Convert.ToInt64(d["Proveedor"]))                                              
                                               select new
                                               {
                                                   Vehiculo = Convert.ToInt64(d["Vehiculo"]),
                                                   Placa = d["Placa"].ToString(),
                                                   Marca = d["Marca"].ToString(),
                                                   Modelo = d["Modelo"].ToString(),
                                                   Capacidad = d["Capacidad"].ToString(),
                                                   ActVehiculo = Convert.ToInt32(d["ActVehiculo"]),                                                 
                                               }).GroupBy(d => new { d.Vehiculo }).Select(d => d.First()),

                               Conductores = (from d in dt.AsEnumerable()
                                            where Convert.ToInt64(c["Proveedor"]).Equals(Convert.ToInt64(d["Proveedor"]))
                                            select new
                                            {
                                                Conductor = Convert.ToInt64(d["Conductor"]),
                                                NombresConductor = d["NombresConductor"].ToString(),
                                                DocConductor = d["DocConductor"].ToString(),
                                                Licencia = d["Licencia"].ToString(),
                                                ActConductor = Convert.ToInt32(d["ActConductor"]),                                                                                                
                                            }).GroupBy(d => new { d.Conductor }).Select(d => d.First()),
                           }).GroupBy(c => new { c.Proveedor }).Select(c => c.First());

                var httpResponseMessage = Request.CreateResponse<IEnumerable<Object>>(HttpStatusCode.OK, obj);
                httpResponseMessage.Headers.Add("Access-Control-Allow-Origin", "*");

                return httpResponseMessage;
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }
    }
}
