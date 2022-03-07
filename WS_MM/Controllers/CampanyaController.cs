using System;
using System.Data;

using static WS_MM.Models.CampanyaModel;
using WS_MM.Models;
using Shared.Controllers;
using Shared.Helpers;
using System.Web.Http;
using Shared.Util;
using Shared.Token;
using Newtonsoft.Json;
using System.Xml;

namespace WS_MM.Controllers
{
    [CorsOrigin]
    [Authorize]
    public class CampanyaController : BaseController
    {
        [HttpGet]
        [ActionName("PMMM_getFundoLst")]
        public IHttpActionResult PMMM_getFundoLst(string prmEmpresa, string prmStatus)
        {
            try
            {
                DataTable dt = Instancia.getFundoLst(prmEmpresa, prmStatus);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpGet]
        [ActionName("PMMM_getModuloByFundo")]
        public IHttpActionResult PMMM_getModuloByFundo(string prmFundo, string prmStatus)
        {
            try
            {
                DataTable dt = Instancia.getModuloByFundo(prmFundo, prmStatus);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpGet]
        [ActionName("PMMM_getTurnoByModulo")]
        public IHttpActionResult PMMM_getTurnoByModulo(string prmModulo, string prmStatus)
        {
            try
            {
                DataTable dt = Instancia.getTurnoByModulo(prmModulo, prmStatus);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }




        [HttpGet]
        [ActionName("PMMM_getCampanya_list")]
        public IHttpActionResult PMMM_getCampanya_list(string prmStatus, string prmFundo, string prmModulo, string prmTurno, string prmEmpresa)
        {
            try
            {
                DataTable dt = Instancia.getCampanya_list(prmStatus, prmFundo, prmModulo, prmTurno, prmEmpresa);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpPost]
        [ActionName("PMMM_saveUpdateCampanya")]
        public IHttpActionResult PMMM_saveUpdateCampanya(dynamic obj)
        {
            try
            {
                int ID=obj.ID;
                int Turno=obj.Turno;
                string codigo = obj.Codigo;
                string inicioCamp = obj.InicioCampanya;
                string finCamp = obj.FinCampanya;
                string inicioCose = obj.InicioCosecha;
                string finCose = obj.FinCosecha;
                int EMPRESA = JWT.IdEmpresa;
                string USER = JWT.Login;

                DataTable dt = Instancia.saveUpdateCampanya(ID, Turno, codigo, inicioCamp, finCamp, inicioCose,finCose,EMPRESA,USER);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpPost]
        [ActionName("PMMM_enableDisableCampanya")]
        public IHttpActionResult PMMM_enableDisableCampanya(dynamic obj)
        {
            try
            {
                int ID = obj.ID;
                bool future_status = obj.Future_Status;
                DataTable dt = Instancia.enableDisableCampanya(ID, future_status,JWT.Login);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }


        
    }
}
