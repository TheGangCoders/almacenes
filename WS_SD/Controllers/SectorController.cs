using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;

using static WS_SD.Models.SectorModel;
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
    public class SectorController : BaseController
    {
        [HttpGet]
        [ActionName("SD_GetSector")]
        public IHttpActionResult SD_GetSector(int? prmintActivo)
        {
            try
            {
                DataTable dt = Instancia.GetSector(JWT.IdEmpresa, prmintActivo);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpPost]
        [ActionName("SD_SaveUpdate_Sector")]
        public IHttpActionResult SD_SaveUpdate_Sector(dynamic obj)
        {
            try
            {
                int prmintSector = obj.Sector;
                string prmstrNombre= obj.Nombre;

                int idEmpresa = JWT.IdEmpresa;
                string ussername = JWT.Login;

                int dt = Instancia.SaveUpdate_Sector(prmintSector, prmstrNombre, idEmpresa, ussername);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpGet]
        [ActionName("SD_ChangeStatus_Sector")]
        public IHttpActionResult ChangeStatus_Sector(int prmintSector, Boolean prmbitActivo)
        {
            try
            {
                string prmstrUsuario = JWT.Login;

                int dt = Instancia.ChangeStatus_Sector(prmintSector, prmbitActivo, prmstrUsuario);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }
    }
}