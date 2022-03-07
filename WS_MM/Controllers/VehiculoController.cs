using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;

using static WS_MM.Models.VehiculoModel;
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
    public class VehiculoController : BaseController
    {
        [HttpGet]
        [ActionName("MM_GetVehiculo")]
        public IHttpActionResult MM_GetVehiculo(int? prmintActivo)
        {
            try
            {
                DataTable dt = Instancia.GetVehiculo(JWT.IdEmpresa, prmintActivo);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpPost]
        [ActionName("MM_SaveUpdate_Vehiculo")]
        public IHttpActionResult MM_SaveUpdate_Vehiculo(dynamic obj)
        {
            try
            {
                int prmintVehiculo = obj.Vehiculo;
                int prmintProveedor = obj.Proveedor;
                string prmstrPlaca = obj.Placa;
                string prmstrMarca = obj.Marca;
                string prmstrModelo = obj.Modelo;
                string prmstrCapacidad = obj.Capacidad;
                string prmstrConstancia = obj.Constancia;
                int prmintActivo = obj.Activo;

                int idEmpresa = JWT.IdEmpresa;
                string username = JWT.Login;

                int dt = Instancia.SaveUpdate_Vehiculo(prmintVehiculo, prmintProveedor, prmstrPlaca, prmstrMarca, prmstrModelo, prmstrCapacidad, prmstrConstancia, prmintActivo, idEmpresa, username);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpGet]
        [ActionName("MM_Anular_Vehiculo")]
        public IHttpActionResult MM_Anular_Vehiculo(int prmintVehiculo)
        {
            try
            {
                string prmstrUsuario = JWT.Login;

                int dt = Instancia.Anular_Vehiculo(prmintVehiculo, prmstrUsuario);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }
    }
}