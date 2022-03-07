using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;

using static WS_SD.Models.CategoriaClienteModel;
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
    public class CategoriaClienteController : BaseController
    {
        [HttpGet]
        [ActionName("SD_GetCategoriaCliente")]
        public IHttpActionResult SD_GetCategoriaCliente(int? prmintActivo)
        {
            try
            {
                DataTable dt = Instancia.GetCategoriaCliente(JWT.IdEmpresa, prmintActivo);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpPost]
        [ActionName("SD_SaveUpdate_CategoriaCliente")]
        public IHttpActionResult SD_SaveUpdate_CategoriaCliente(dynamic obj)
        {
            try
            {
                int prmintCategoriaCliente = obj.CategoriaCliente;
                string prmstrCodigo = obj.Codigo;
                string prmstrDescripcion = obj.Descripcion;
                int prmintActivo = obj.Activo;

                int idEmpresa = JWT.IdEmpresa;
                string ussername = JWT.Login;

                int dt = Instancia.SaveUpdate_CategoriaCliente(prmintCategoriaCliente, prmstrCodigo, prmstrDescripcion, prmintActivo, idEmpresa, ussername);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpGet]
        [ActionName("SD_Anular_CategoriaCliente")]
        public IHttpActionResult SD_Anular_CategoriaCliente(int prmintCategoriaCliente)
        {
            try
            {
                string prmstrUsuario = JWT.Login;
                    
                int dt = Instancia.Anular_CategoriaCliente(prmintCategoriaCliente, prmstrUsuario);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }
    }
}