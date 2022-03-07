using System;
using System.Data;

using WS_IT.Models;
using Shared.Controllers;
using Shared.Helpers;
using System.Web.Http;
using Shared.Util;


namespace WS_IT.Controllers
{
    [CorsOrigin]
    [Authorize]
    public class ClaseDocVtaOrgVtaController : BaseController
    {
        [HttpGet]
        [ActionName("IT_getClaseDocOrganizacionVta_list")]
        public IHttpActionResult IT_getClaseDocOrganizacionVta_list()
        {
            try
            {
                DataTable dt = ClaseDocVtaOrgVtaModel.Instancia.getClaseDocOrganizacionVta_list(JWT.IdEmpresa);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleError(ex));
            }
        }

        [HttpGet]
        [ActionName("IT_getClaseDocVta_list")]
        public IHttpActionResult IT_getClaseDocVta_list(Boolean ?prmbitStatus)
        {
            try
            {
                DataTable dt = ClaseDocVtaOrgVtaModel.Instancia.getClaseDocVta_list(JWT.IdEmpresa, prmbitStatus);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleError(ex));
            }
        }


        [HttpPost]
        [ActionName("IT_enable_disableClaseDocOrganizacionVta")]
        public IHttpActionResult IT_enable_disableClaseDocOrganizacionVta(dynamic obj)
        {
            string usser = JWT.Login;
            int id = obj.idClase_OrgVta;
            Boolean status = obj.future_status;
            try
            {
                int dt = ClaseDocVtaOrgVtaModel.Instancia.enable_disableClaseDocOrganizacionVta(id, status, usser);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleError(ex));
            }
        }



        [HttpPost]
        [ActionName("IT_saveUpdate_ClaseDocOrganizacionVta")]
        public IHttpActionResult IT_saveUpdate_ClaseDocOrganizacionVta(dynamic obj)
        {
            int id = obj.idClase_OrgVta;
            int idClase = obj.idClaseDoc;
            int idOrg = obj.idOrg;
            string usser = JWT.Login;
            try
            {
                int dt = ClaseDocVtaOrgVtaModel.Instancia.saveUpdate_ClaseDocOrganizacionVta(id, idClase, idOrg, usser);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleError(ex));
            }
        }



    }
}
