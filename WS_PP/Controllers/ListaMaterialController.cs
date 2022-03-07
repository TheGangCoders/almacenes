using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using static WS_PP.Models.ListaMaterialDAL;
using System.Web;
using System.Security.Cryptography;
using Shared.Controllers;
using Shared.Helpers;
using System.Web.Http;
using Shared.Util;
using Shared.Token;
using Newtonsoft.Json;

namespace WS_PP.Controllers
{
    [CorsOrigin]
    [Authorize]
    public class ListaMaterialController : BaseController
    {
        [HttpGet]
        [ActionName("PP_getListaMaterial_List")]
        public IHttpActionResult PP_getListaMaterial_List(int prmintCentro, int prmbitActivo, int prmintMaterial)
        {
            try
            {
                DataTable dt = Instancia.getListaMaterial_List(prmintCentro, prmbitActivo, JWT.IdEmpresa, prmintMaterial);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpGet]
        [ActionName("PP_getListaMaterial_Data")]
        public IHttpActionResult PP_getListaMaterial_Data(int prmintLista)
        {
            try
            {
                DataTableCollection dt = Instancia.getListaMaterial_Data(prmintLista);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpGet]
        [ActionName("PP_getCabeceraListaMaterial_list")]
        public IHttpActionResult PP_getCabeceraListaMaterial_list(string Centro, string TipoMaterial, string GrupoArticulo, string CodigoMaterial, string Material)
        {
            try
            {
                DataTable dt = Instancia.getCabeceraListaMaterial_list(Centro, TipoMaterial, GrupoArticulo, CodigoMaterial, Material);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        


        [HttpPost]
        [ActionName("PP_saveUpdate_ListaMaterial")]
        public IHttpActionResult PP_saveUpdate_ListaMaterial(dynamic obj)
        {
            try
            {
                int idLista = obj.idLista;
                int idEmpresa = JWT.IdEmpresa;
                int idCentro = obj.Centro;
                int idMaterial = obj.Material;
                int idUnidadMedida = obj.UnidadMedida;
                string CantidadBase = obj.CantidadBase;
                string Nombre = obj.Nombre;
                string xml = obj.strXML;
                string ussername = JWT.Login;

                int dt = Instancia.saveUpdate_ListaMaterial(idLista, idEmpresa, idCentro, idMaterial, idUnidadMedida, CantidadBase, Nombre, xml, ussername);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpPost]
        [ActionName("PP_enableDisable_ListaMaterial")]
        public IHttpActionResult PP_enableDisable_ListaMaterial(dynamic obj)
        {
            try
            {
                int ID = obj.prmintLista;
                Boolean bitStatus = obj.prmbitActivo;
                string usuario = obj.prmstrUsuario;

                int dt = Instancia.enableDisable_ListaMaterial(ID, usuario, bitStatus);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }


        [HttpGet]
        [ActionName("PP_listMateriales_byCentro")]
        public IHttpActionResult PP_listMateriales_byCentro(string CodMaterial, string Material, string Centro, string TipoMaterial, string GrupoArticulo, string Calidad)
        {
            try
            {
                DataTable dt = Instancia.listMateriales_byCentro(JWT.IdEmpresa, CodMaterial, Material, Centro, TipoMaterial, GrupoArticulo, Calidad);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpGet]
        [ActionName("PP_searchListaMateriales")]
        public IHttpActionResult PP_searchListaMateriales(int Material, int Centro)
        {
            try
            {
                DataTable dt = Instancia.searchListaMateriales(JWT.IdEmpresa, Material, Centro);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }


        [HttpGet]
        [ActionName("PP_compararListaMateriales")]
        public IHttpActionResult PP_compararListaMateriales(int lista1, int lista2)
        {
            try
            {
                DataTable dt = Instancia.compararListaMateriales(lista1, lista2);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }

        [HttpGet]
        [ActionName("PP_getMaterialByCodigo")]
        public IHttpActionResult PP_getMaterialByCodigo(string codigo, int centro)
        {
            try
            {
                DataTable dt = Instancia.getMaterialByCodigo(JWT.IdEmpresa, centro, codigo);
                return Ok(dt);
            }
            catch (Exception ex)
            {
                return BadRequest(MetodoComun.ObtenerDetalleErrorDynamic(ex));
            }
        }





    }
}
