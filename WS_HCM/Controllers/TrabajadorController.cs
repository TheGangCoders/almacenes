using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

using Shared.Controllers;
using Shared.Helpers;
using System.Web.Http;
using Shared.Util;

using static WS_HCM.Models.TrabajadorModel;

namespace WS_HCM.Controllers
{
    [CorsOrigin]
    [Authorize]
    public class TrabajadorController : BaseController
    {
        [HttpPost]
        [ActionName("GetTrabajadorByFilters")]
        public IHttpActionResult GetTrabajadorByFilters(dynamic obj)
        {
            try
            {
                int prmintSociedad = obj.prmintSociedad;
                int prmintActivo = obj.prmintActivo;
                int prmintEmpresa = JWT.IdEmpresa;

                DataTable dt = Instancia.GetTrabajadorByFilters(prmintSociedad, prmintActivo, prmintEmpresa);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpGet]
        [ActionName("GetTrabajadorById")]
        public IHttpActionResult GetTrabajadorById(string prmstrTrabajador)
        {
            try
            {
                DataTableCollection dt = Instancia.GetTrabajadorById(prmstrTrabajador);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleError(ex));
            }
        }

        [HttpPost]
        [ActionName("SaveUpdate_Trabajador")]
        public IHttpActionResult SaveUpdate_Trabajador(dynamic obj)
        {
            try
            {
                int prmintOpcion = obj.Opcion;
                string idTrabajador = obj.NroPersonal;
                int prmintSociedad = obj.Sociedad;
                string prmstrDocIdentidad = obj.DocIdentidad;
                string prmstrNombres = obj.Nombres;
                string prmstrApePaterno = obj.ApePaterno;
                string prmstrApeMaterno = obj.ApeMaterno;
                string prmstrCorreo = obj.Correo;
                string prmstrTelefono = obj.Telefono;
                string prmstrDireccion = obj.Direccion;
                DateTime prmdateFechaNacimiento = obj.FechaNacimiento;
                DateTime prmdateFechaIngreso = obj.FechaIngreso;
                DateTime prmdateFechaCese = obj.FechaCese;
                string prmstrGenero = obj.Genero;
                int prmintRoot = obj.Root;
                string xml = obj.strXML;
                string ussername = JWT.Login;

                int dt = Instancia.SaveUpdate_Trabajador(prmintOpcion, idTrabajador, prmintSociedad, prmstrDocIdentidad, prmstrNombres, prmstrApePaterno, prmstrApeMaterno,
                    prmstrCorreo, prmstrTelefono, prmstrDireccion, prmdateFechaNacimiento, prmdateFechaIngreso, prmdateFechaCese, prmstrGenero, prmintRoot, xml, ussername);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpPost]
        [ActionName("ChangeStatus_Trabajador")]
        public IHttpActionResult ChangeStatus_Trabajador(dynamic obj)
        {
            try
            {
                string prmstrTrabajador = obj.prmstrTrabajador;
                Boolean prmbitActivo = obj.prmbitActivo;
                string prmstrUsuario = JWT.Login;

                int dt = Instancia.ChangeStatus_Trabajador(prmstrTrabajador, prmbitActivo, prmstrUsuario);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }
    }
}