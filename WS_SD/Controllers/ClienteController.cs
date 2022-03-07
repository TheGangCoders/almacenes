using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using static WS_SD.Models.ClienteModel;
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
    public class ClienteController : BaseController
    {
        [HttpGet]
        [ActionName("SD_getCategoriaCliente_list")]
        public IHttpActionResult SD_getCategoriaCliente_list()
        {
            try
            {
                DataTable dt = Instancia.getCategoriaCliente_list(JWT.IdEmpresa);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }
        
        [HttpGet]
        [ActionName("SD_getCanal_list")]
        public IHttpActionResult SD_getCanal_list(int status)
        {
            try
            {
                DataTable dt = Instancia.getCanal_list(JWT.IdEmpresa, status);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpGet]
        [ActionName("SD_getSector_list")]
        public IHttpActionResult SD_getSector_list(int status)
        {
            try
            {
                DataTable dt = Instancia.getSector_list(JWT.IdEmpresa, status);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }


        

        [HttpGet]
        [ActionName("SD_getCliente_list")]
        public IHttpActionResult SD_getCliente_list(int prmintCategoriaCliente, int prmintStatus)
        {
            try
            {
                DataTable dt = Instancia.getCliente_list(JWT.IdEmpresa, prmintCategoriaCliente, prmintStatus);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpPost]
        [ActionName("SD_enable_disableCliente")]
        public IHttpActionResult SD_enable_disableCliente(dynamic obj)
        {
            try
            {
                int idCliente = obj.idCliente;
                int future_status = obj.status;
                int dt = Instancia.enable_disableCliente(idCliente, future_status, JWT.Login);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpGet]
        [ActionName("SD_getTipoDireccion_list")]
        public IHttpActionResult SD_getTipoDireccion_list()
        {
            try
            {
                DataTable dt = Instancia.getTipoDireccionList(JWT.IdEmpresa);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpGet]
        [ActionName("SD_getOrganizacionVentas_list")]
        public IHttpActionResult SD_getOrganizacionVentas_list(int status)
        {
            try
            {
                DataTable dt = Instancia.getOrganizacionVentas_list(JWT.IdEmpresa, status);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }


        [HttpGet]
        [ActionName("SD_getCondicionPago_list")]
        public IHttpActionResult SD_getCondicionPago_list(int status)
        {
            try
            {
                DataTable dt = Instancia.getCondicionPago_list(JWT.IdEmpresa, status);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpGet]
        [ActionName("SD_getGrupoCliente_list")]
        public IHttpActionResult SD_getGrupoCliente_list(int status)
        {
            try
            {
                DataTable dt = Instancia.getGrupoCliente_list(JWT.IdEmpresa, status);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpGet]
        [ActionName("SD_RptClientes_General")]
        public IHttpActionResult SD_RptClientes_General(string prmstrCodigo, string prmstrNombre, Boolean prmbitStatus)
        {
            try
            {
                DataTable dt = Instancia.RptClientes_General(prmstrCodigo, prmstrNombre, prmbitStatus);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }
        [HttpGet]
        [ActionName("SD_RptClientes_Organizacion")]
        public IHttpActionResult SD_RptClientes_Organizacion(string prmstrCodigo, string prmstrNombre, string prmstrSociedad, Boolean prmbitStatus)
        {
            try
            {
                DataTable dt = Instancia.RptClientes_Organizacion(prmstrCodigo, prmstrNombre, prmstrSociedad, prmbitStatus);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }
        [HttpGet]
        [ActionName("SD_RptClientes_ContactosxSector")]
        public IHttpActionResult SD_RptClientes_ContactosxSector(string prmstrCodigo, string prmstrNombre, string prmstrSociedad, Boolean prmbitStatus)
        {
            try
            {
                DataTable dt = Instancia.RptClientes_ContactosxSector(prmstrCodigo, prmstrNombre, prmstrSociedad, prmbitStatus);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }
        [HttpGet]
        [ActionName("SD_RptClientes_ClientesxSector")]
        public IHttpActionResult SD_RptClientes_ClientesxSector(string prmstrCodigo, string prmstrNombre, string prmstrSociedad, Boolean prmbitStatus)
        {
            try
            {
                DataTable dt = Instancia.RptClientes_ClientesxSector(prmstrCodigo, prmstrNombre, prmstrSociedad, prmbitStatus);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpGet]
        [ActionName("SD_getCliente_data")]
        public IHttpActionResult SD_getCliente_data(int prmintCliente)
        {
            try
            {
                DataTableCollection dt = Instancia.getCliente_data(prmintCliente);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }


        [HttpPost]
        [ActionName("SD_saveUpdate_Cliente")]
        public IHttpActionResult SD_saveUpdate_Cliente(dynamic obj)
        {
            try
            {
                int prmintCliente = obj.idCliente;
                int prmintEmpresa = JWT.IdEmpresa;
                int prmintCategoriaCliente = obj.CategoriaCliente;

                string prmstrCodCliente = obj.CodCliente;
                string prmstrRazonSocial = obj.RazonSocial;
                string prmstrRazonComercial = obj.RazonComercial;
                string prmstrTipoDocumento = obj.TipoDocumento;
                string prmstrNroDocumento = obj.NroDocumento;
                string prmstrTipoDireccion = obj.TipoDireccion;
                string prmstrDireccion = obj.Direccion;
                string prmstrTelefono = obj.Telefono;
                string prmstrCelular = obj.Celular;
                string prmstrEmail = obj.Email;
                string prmstrCuentaContable = obj.CuentaContable;

                int prmintPais = obj.Pais;
                int? prmintDepartamento = obj.Departamento;
                int? prmintProvincia = obj.Provincia;

                string prmstrIdioma = obj.Idioma;
                int prmintOrgVenta = obj.OrgVenta;
                string prmstrMoneda = obj.Moneda;
                int prmstrCondicionPago = obj.CondicionPago;

                int prmstrGrupoCliente = obj.GrupoCliente;
                string prmxmlCliente_Sector = obj.xmlCliente_Sector;
                string prmxmlContacto_Cliente = obj.xmlContacto_Cliente;

                int? UnidadMedida = obj.UnidadMedida;
                string prmstrObs = obj.Observaciones;

                string prmUsser = JWT.Login;

                int dt = Instancia.saveUpdate_Cliente( prmintCliente, prmintEmpresa, prmintCategoriaCliente, prmstrCodCliente,
                                            prmstrRazonSocial, prmstrRazonComercial, prmstrTipoDocumento, prmstrNroDocumento,
                                            prmstrTipoDireccion, prmstrDireccion, prmstrTelefono, prmstrCelular, prmstrEmail,
                                            prmstrCuentaContable, prmintPais, prmintDepartamento, prmintProvincia, prmstrIdioma,
                                            prmintOrgVenta, prmstrMoneda, prmstrCondicionPago, prmstrGrupoCliente,
                                            prmxmlCliente_Sector,
                                            prmxmlContacto_Cliente, prmUsser, UnidadMedida,prmstrObs);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }





    }
}
