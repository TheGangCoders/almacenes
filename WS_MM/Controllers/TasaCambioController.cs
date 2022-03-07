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

using WS_MM.Models;

namespace WS_MM.Controllers
{
    [CorsOrigin]
    [Authorize]
    public class TasaCambioController : BaseController
    {
        [HttpGet]
        [ActionName("GetTasaCambio")]
        public IHttpActionResult GetTasaCambio()
        {
            try
            {
                DataTable dt = TasaCambioModel.Instancia.GetTasaCambio(JWT.IdEmpresa);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleError(ex));
            }
        }

        [HttpGet]
        [ActionName("GetTasaCambioById")]
        public IHttpActionResult GetTasaCambioById(int prmintTasa)
        {
            try
            {
                DataTable dt = TasaCambioModel.Instancia.GetTasaCambioById(prmintTasa);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleError(ex));
            }
        }

        [HttpPost]
        [ActionName("GetTasaCambioByFilters")]
        public IHttpActionResult GetTasaCambioByFilters(dynamic obj)
        {
            try
            {                
                string prmstrMorigen = obj.prmstrMorigen;
                string prmstrDestino = obj.prmstrDestino;
                int prmintActivo = obj.prmintActivo;
                int prmintEmpresa = JWT.IdEmpresa;
                DateTime prmdtFdesde = obj.prmdtFdesde;
                DateTime prmdtFhasta = obj.prmdtFhasta;

                DataTable dt = TasaCambioModel.Instancia.GetTasaCambioByFilters(prmstrMorigen, prmstrDestino, prmintActivo, prmintEmpresa, prmdtFdesde, prmdtFhasta);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpPost]
        [ActionName("ChangeStatus_TasaCambio")]
        public IHttpActionResult ChangeStatus_TasaCambio(dynamic obj)
        {
            try
            {
                int prmintTasa = obj.prmintTasa;
                Boolean prmbitActivo = obj.prmbitActivo;
                string prmstrUsuario = JWT.Login;

                int dt = TasaCambioModel.Instancia.ChangeStatus_TasaCambio(prmintTasa, prmbitActivo, prmstrUsuario);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpPost]
        [ActionName("SaveUpdate_TasaCambio")]
        public IHttpActionResult SaveUpdate_TasaCambio(dynamic obj)
        {
            try
            {
                int idTasa = obj.idTasa;
                int idEmpresa = JWT.IdEmpresa;
                DateTime prmdateFecha = obj.Fecha;
                string prmstrMOrigen = obj.MonedaOrigen;
                string prmstrMDestino = obj.MonedaDestino;
                Decimal prmdecValorCompra = obj.ValorCompra;
                Decimal prmdecValorVenta = obj.ValorVenta;
                string ussername = JWT.Login;

                int dt = TasaCambioModel.Instancia.SaveUpdate_TasaCambio(idTasa, idEmpresa, prmdateFecha, prmstrMOrigen, prmstrMDestino, prmdecValorCompra, prmdecValorVenta, ussername);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }
    }
}