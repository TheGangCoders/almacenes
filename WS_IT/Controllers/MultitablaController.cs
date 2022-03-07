using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;

using Shared.Controllers;
using Shared.Helpers;
using System.Web.Http;
using Shared.Util;
using Shared.Token;

using WS_IT.Models;
using WS_IT.DTOs;
using WS_IT.Constants;
using WS_IT.Util;

namespace WS_IT.Controllers
{
    [CorsOrigin]
    [Authorize]
    public class MultitablaController : BaseController
    {
        [HttpGet]
        [ActionName("GetMultitabla")]
        public IHttpActionResult GetMultitabla()
        {
            try
            {
                DataTable dt = MultitablaModel.Instancia.GetMultitabla(JWT.IdEmpresa);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleError(ex));
            }
        }

        [HttpGet]
        [ActionName("GetModulos")]
        public IHttpActionResult GetModulos()
        {
            try
            {
                DataTable dt = MultitablaModel.Instancia.GetModulos();
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleError(ex));
            }
        }

        [HttpGet]
        [ActionName("GetDetailMultitabla")]
        public IHttpActionResult GetDetailMultitabla(string prmstrTabla)
        {
            try
            {
                DataTableCollection dt = MultitablaModel.Instancia.GetDetailMultitabla(prmstrTabla);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleError(ex));
            }
        }

        [HttpPost]
        [ActionName("SaveUpdate_Multitabla")]
        public IHttpActionResult SaveUpdate_Multitabla(dynamic obj)
        {
            try
            {
                int prmintOpcion = obj.Opcion;
                string idTabla = obj.idTabla;
                string prmstrModulo = obj.Modulo;
                //string newidTabla = obj.newIdTabla;
                int idEmpresa = JWT.IdEmpresa;
                string Nombre = obj.Nombre;
                string Descripcion = obj.Descripcion;
                string xml = obj.strXML;
                string ussername = JWT.Login;

                int dt = MultitablaModel.Instancia.SaveUpdate_Multitabla(prmintOpcion,idTabla, prmstrModulo,/*newidTabla, */idEmpresa, Nombre, Descripcion, xml, ussername);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpPost]
        [ActionName("ChangeStatus_Multitabla")]
        public IHttpActionResult ChangeStatus_Multitabla(dynamic obj)
        {
            try
            {
                string prmstrTabla = obj.prmstrTabla;
                Boolean prmbitActivo = obj.prmbitActivo;
                string prmstrUsuario = JWT.Login;

                int dt = MultitablaModel.Instancia.ChangeStatus_Multitabla(prmstrTabla, prmbitActivo, prmstrUsuario);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpPost]
        [ActionName("GetMultitablaByStatus")]
        public IHttpActionResult GetMultitablaByStatus(dynamic obj)
        {
            try
            {
                int prmintActivo = obj.prmintActivo;
                int prmstrTabla = JWT.IdEmpresa;

                DataTable dt = MultitablaModel.Instancia.GetMultitablaByStatus(prmstrTabla, prmintActivo);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }
    }
}