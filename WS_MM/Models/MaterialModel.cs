using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Net;

namespace WS_MM.Models
{
    public class MaterialModelDAL
    {
        private static readonly MaterialModelDAL _instancia = new MaterialModelDAL();

        public static MaterialModelDAL Instancia
        {
            get { return MaterialModelDAL._instancia; }
        }

        
        public DataTableCollection dataModalMaterial(int prmintMaterial)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspMM_dataModalMaterial");
            db.AddInParameter(cmd, "@prmintMaterial", DbType.Int32, prmintMaterial);

            try
            {
                return db.ExecuteDataSet(cmd).Tables;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public DataTable GetMaterial(int empresa, int tipoMat, int grupoArticulo, Boolean? status)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspMM_GetMaterial");
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int32, empresa);
            db.AddInParameter(cmd, "@prmintTipoMaterial", DbType.Int32, tipoMat);
            db.AddInParameter(cmd, "@prmintGrupoArticulo", DbType.Int32, grupoArticulo);
            db.AddInParameter(cmd, "@prmbitStatus", DbType.Boolean, status);

            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetReporteStockMaterial(int prmintSociedad, int prmintTipoMaterial, int prmintGrupoArticulo,
            int prmintMaterial, string prmstrLote, int prmintCentro, int prmintAlmacen, int prmintTipoReporte, string FechaLimite, int pkUser)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspMM_getReporteStockMaterial");
            db.AddInParameter(cmd, "@prmintSociedad", DbType.Int32, prmintSociedad);
            db.AddInParameter(cmd, "@prmintTipoMaterial", DbType.Int32, prmintTipoMaterial);
            db.AddInParameter(cmd, "@prmintGrupoArticulo", DbType.Int32, prmintGrupoArticulo);
            db.AddInParameter(cmd, "@prmintMaterial", DbType.Int32, prmintMaterial);
            db.AddInParameter(cmd, "@prmstrLote", DbType.String, prmstrLote);
            db.AddInParameter(cmd, "@prmintCentro", DbType.Int32, prmintCentro);
            db.AddInParameter(cmd, "@prmintAlmacen", DbType.Int32, prmintAlmacen);
            db.AddInParameter(cmd, "@prmintTipo", DbType.Int32, prmintTipoReporte);
            db.AddInParameter(cmd, "@prmdateFechaHasta", DbType.String, FechaLimite);
            db.AddInParameter(cmd, "@prmintUsuarioSesion", DbType.Int32, pkUser);
            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable getMateriales_List(int empresa,int tipoMat, Boolean? status)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspMM_getMateriales_List");
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int32, empresa);
            db.AddInParameter(cmd, "@prmintTipoMaterial", DbType.Int32, tipoMat);
            db.AddInParameter(cmd, "@prmbitStatus", DbType.Boolean, status);

            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable getMateriaByCodigo(int empresa, string codigo)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspMM_getMaterialByCodigo");
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int32, empresa);
            db.AddInParameter(cmd, "@prmstrCodigo", DbType.String, codigo);
            
            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable getClaseMaterial(int empresa)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("[uspMM_getClaseMaterial]");
            db.AddInParameter(cmd, "@prmEmpresa", DbType.Int32, empresa);

            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable getClaseMaterial_REAL(int empresa)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspMM_getClaseMaterial_REAL");
            db.AddInParameter(cmd, "@prmEmpresa", DbType.Int32, empresa);

            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        
        public DataTable GuardarDatosGenerales(int idMaterial, int idEmpresa, int idTipoMaterial, int idGrupoArticulo, string idClase,
                                string codigo, string descripcion, int idUnidadMedida, string pesoNeto, string pesoBruto,string idUnidadMedidaPeso, string volumen, 
                                bool productoControlado, bool inafecto,string partidaArancelaria, bool activo, string login, string caracteristicasXml, Boolean DUA)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspMM_GuardarMaterialDatosGenerales");
            db.AddInParameter(cmd, "@prmintMaterial", DbType.Int32, idMaterial);
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int32, idEmpresa);
            db.AddInParameter(cmd, "@prmintTipoMaterial", DbType.Int32, idTipoMaterial);
            db.AddInParameter(cmd, "@prmintGrupoArticulo", DbType.Int32, idGrupoArticulo);
            db.AddInParameter(cmd, "@prmstrClase", DbType.String, idClase);
            db.AddInParameter(cmd, "@prmstrCodigo", DbType.String, codigo);
            db.AddInParameter(cmd, "@prmstrDescripcion", DbType.String, descripcion);
            db.AddInParameter(cmd, "@prmintUnidadMedida", DbType.Int32, idUnidadMedida);
            db.AddInParameter(cmd, "@prmdecPesoNeto", DbType.Decimal, pesoNeto);
            db.AddInParameter(cmd, "@prmdecPesoBruto", DbType.Decimal, pesoBruto);
            db.AddInParameter(cmd, "@prmintUnidadMedidaPeso", DbType.Int32, idUnidadMedidaPeso);
            db.AddInParameter(cmd, "@prmdecvolumen", DbType.Decimal, volumen);
            db.AddInParameter(cmd, "@prmbitProductoControlado", DbType.Boolean, productoControlado);
            db.AddInParameter(cmd, "@prmbitinafecto", DbType.Boolean, inafecto);
            db.AddInParameter(cmd, "@prmstrPartidaArancelaria", DbType.String, partidaArancelaria);
            db.AddInParameter(cmd, "@prmbitActivo", DbType.Boolean, activo);

            db.AddInParameter(cmd, "@prmbitDUA", DbType.Boolean, DUA);


            db.AddInParameter(cmd, "@prmstrLogin", DbType.String, login);
            db.AddInParameter(cmd, "@prmxmlCaracteristicas", DbType.Xml, caracteristicasXml);
            
            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GuardarVentas(int idMaterialOrgVentas, int idEmpresa, int idMaterial, int idOrgVentas, int idUnidadMedidaVenta,
                                int idCentroSuministrador, bool habilitadoVenta, bool activo, string login, string nombresComercialesXml, string factoresXml)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspMM_GuardarMaterialVentas");
            db.AddInParameter(cmd, "@prmintMaterialOrgVentas", DbType.Int32, idMaterialOrgVentas);
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int32, idEmpresa);
            db.AddInParameter(cmd, "@prmintMaterial", DbType.Int32, idMaterial);
            db.AddInParameter(cmd, "@prmintOrgVentas", DbType.Int32, idOrgVentas);
            db.AddInParameter(cmd, "@prmintUnidadMedidaVenta", DbType.Int32, idUnidadMedidaVenta);
            db.AddInParameter(cmd, "@prmintCentroSuministrador", DbType.Int32, idCentroSuministrador);
            db.AddInParameter(cmd, "@prmbitHabilitadoVenta", DbType.Boolean, habilitadoVenta);
            db.AddInParameter(cmd, "@prmbitActivo", DbType.Boolean, activo);
            db.AddInParameter(cmd, "@prmstrLogin", DbType.String, login);
            db.AddInParameter(cmd, "@prmxmlNombresComerciales", DbType.Xml, nombresComercialesXml);
            db.AddInParameter(cmd, "@prmxmlfactores", DbType.Xml, factoresXml);

            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GuardarLogistica(int idMaterialCentro, int idEmpresa, int idMaterial, int idCentro, int idUnidadMedidaCompra, int idUnidadMedidaSalida,
                                decimal stockMinimo, decimal stockMaximo, int tiempoEntrada, int plazoEntrega, string claseAprovisionamiento, string centroBeneficio,
                                string planificador, int almacenProduccion, bool activo, string login, string almacenesXml)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspMM_GuardarMaterialLogistica");
            db.AddInParameter(cmd, "@prmintMaterialCentro", DbType.Int32, idMaterialCentro);
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int32, idEmpresa);
            db.AddInParameter(cmd, "@prmintMaterial", DbType.Int32, idMaterial);
            db.AddInParameter(cmd, "@prmintCentro", DbType.Int32, idCentro);
            db.AddInParameter(cmd, "@prmintUnidadMedidaCompra", DbType.Int32, idUnidadMedidaCompra);
            db.AddInParameter(cmd, "@prmintUnidadMedidaSalida", DbType.Int32, idUnidadMedidaSalida);
            db.AddInParameter(cmd, "@prmdecStockMinimo", DbType.Decimal, stockMinimo);
            db.AddInParameter(cmd, "@prmdecStockMaximo", DbType.Decimal, stockMaximo);
            db.AddInParameter(cmd, "@prmintTiempoEntrada", DbType.Int32, tiempoEntrada);
            db.AddInParameter(cmd, "@prmintPlazoEntrega", DbType.Int32, plazoEntrega);
            db.AddInParameter(cmd, "@prmstrClaseAprovisionamiento", DbType.String, claseAprovisionamiento);
            db.AddInParameter(cmd, "@prmstrCentroBeneficio", DbType.String, centroBeneficio);
            db.AddInParameter(cmd, "@prmstrPlanificador", DbType.String, planificador);
            db.AddInParameter(cmd, "@prmintAlmacenProduccion", DbType.Int32, almacenProduccion);
            db.AddInParameter(cmd, "@prmbitActivo", DbType.Boolean, activo);
            db.AddInParameter(cmd, "@prmstrLogin", DbType.String, login);
            db.AddInParameter(cmd, "@prmxmlAlmacenes", DbType.Xml, almacenesXml);
            
            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public DataTable GuardarContabilidad(int idMaterial, int idOrgVenta, int idMaterialOrgVentas, int idEmpresa, int idRetencion,
                        string tipoExistencia, DateTime fechaClave, string login, string preciosXml)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspMM_GuardarMaterialContabilidad");
            db.AddInParameter(cmd, "@prmintMaterial", DbType.Int32, idMaterial);
            db.AddInParameter(cmd, "@prmintOrgVenta", DbType.Int32, idOrgVenta);
            db.AddInParameter(cmd, "@prmintMaterialOrgVentas", DbType.Int32, idMaterialOrgVentas);
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int32, idEmpresa);
            db.AddInParameter(cmd, "@prmintRetencion", DbType.Int32, idRetencion);
            db.AddInParameter(cmd, "@prmstrTipoExistencia", DbType.String, tipoExistencia);
            db.AddInParameter(cmd, "@prmdatFechaClave", DbType.Date, fechaClave);
            db.AddInParameter(cmd, "@prmstrLogin", DbType.String, login);
            db.AddInParameter(cmd, "@prmxmlPrecios", DbType.Xml, preciosXml);
            
            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable getDataMateriaByIdMaterial(int empresa, int id)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspMM_getMaterialById");
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int32, empresa);
            db.AddInParameter(cmd, "@prmId", DbType.Int32, id);

            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable getDataMateriaCaracteristicaByIdMaterial(int empresa, int idMaterial)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspMM_getMaterialCaracteristicaByMaterialId");
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int32, empresa);
            db.AddInParameter(cmd, "@prmIdMaterial", DbType.Int32, idMaterial);

            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable getDataMaterialVentasByIdMaterial(int empresa, int idMaterial, int sociedad, int orgVentas)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspMM_getMaterialVentasByMaterialId");
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int32, empresa);
            db.AddInParameter(cmd, "@prmIdMaterial", DbType.Int32, idMaterial);
            db.AddInParameter(cmd, "@prmSociedad", DbType.Int32, sociedad);
            db.AddInParameter(cmd, "@prmOrgVentas", DbType.Int32, orgVentas);

            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public DataTableCollection getDataMaterialVentasByIdMaterial_re(int empresa, int idMaterial, int sociedad, int orgVentas)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspMM_getMaterialVentasByMaterialId");
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int32, empresa);
            db.AddInParameter(cmd, "@prmIdMaterial", DbType.Int32, idMaterial);
            db.AddInParameter(cmd, "@prmSociedad", DbType.Int32, sociedad);
            db.AddInParameter(cmd, "@prmOrgVentas", DbType.Int32, orgVentas);

            try
            {
                return db.ExecuteDataSet(cmd).Tables;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public DataTableCollection getDataContabilidadByIdMaterial_re(int empresa, int idMaterial, int sociedad, int orgVentas)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspMM_getDataContabilidadByIdMaterial");
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int32, empresa);
            db.AddInParameter(cmd, "@prmIdMaterial", DbType.Int32, idMaterial);
            db.AddInParameter(cmd, "@prmSociedad", DbType.Int32, sociedad);
            db.AddInParameter(cmd, "@prmOrgVentas", DbType.Int32, orgVentas);

            try
            {
                return db.ExecuteDataSet(cmd).Tables;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public DataTableCollection GetDataLogistiById_RE(int prmintEmpresa, int prmintMaterial, int prmintCentro)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspMM_getDataLogisticaByIdMaterial");
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int32, prmintEmpresa);
            db.AddInParameter(cmd, "@prmintMaterial", DbType.Int32, prmintMaterial);
            db.AddInParameter(cmd, "@prmintCentro", DbType.Int32, prmintCentro);

            try
            {
                return db.ExecuteDataSet(cmd).Tables;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public DataTable Eliminar(int prmintMaterial, int prmintEmpresa, string prmstrLogin)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspMM_EliminarMaterial");
            db.AddInParameter(cmd, "@prmintMaterial", DbType.Int32, prmintMaterial);
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int32, prmintEmpresa);
            db.AddInParameter(cmd, "@prmstrLogin", DbType.String, prmstrLogin);
            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable enable_disable_Material(int prmintMaterial, Boolean future_status, string prmstrLogin)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspMM_enable_disable_Material");
            db.AddInParameter(cmd, "@prmintMaterial", DbType.Int32, prmintMaterial);
            db.AddInParameter(cmd, "@prmbitStatus", DbType.Boolean, future_status);
            db.AddInParameter(cmd, "@prmstrLogin", DbType.String, prmstrLogin);
            db.AddInParameter(cmd, "@prmFecha", DbType.DateTime, DateTime.Now);
            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        


        public DataTable getDataCaracteristicaByTipoMaterial(int prmintEmpresa, int prmintTipoMaterial)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspMM_ListarCaracteristicaPorTipoMaterial_Material");
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int32, prmintEmpresa);
            db.AddInParameter(cmd, "@prmintTipoMaterial", DbType.Int32, prmintTipoMaterial);

            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable Buscar(int prmintEmpresa, int prmintActivo,
                               string prmstrCodigo, string prmstrQuery)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspMM_BuscarMaterial");
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int32, prmintEmpresa);
            db.AddInParameter(cmd, "@prmintActivo", DbType.Int32, prmintActivo);
            db.AddInParameter(cmd, "@prmstrCodigo", DbType.String, prmstrCodigo);
            db.AddInParameter(cmd, "@prmstrQuery", DbType.String, prmstrQuery);
            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetReporteMaterial(int prmintEmpresa, int prmintTipoReporte, int prmintTipoMaterial, int prmintGrupoArticulo,
            int prmintMaterial, int prmintCentro)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspMM_getReporteMateriales");
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int32, prmintEmpresa);
            db.AddInParameter(cmd, "@prmintTipoReporte", DbType.Int32, prmintTipoReporte);
            db.AddInParameter(cmd, "@prmintTipoMaterial", DbType.Int32, prmintTipoMaterial);
            db.AddInParameter(cmd, "@prmintGrupoArticulo", DbType.Int32, prmintGrupoArticulo);
            db.AddInParameter(cmd, "@prmintMaterial", DbType.Int32, prmintMaterial);
            db.AddInParameter(cmd, "@prmintCentro", DbType.Int32, prmintCentro);

            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        
        public DataTable listMateriales_paraOC(int Empresa, string CodMaterial, string Material, string Calidad, string TipoMaterial, string GrupoArticulo)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspMM_listMateriales_paraOC");
            db.AddInParameter(cmd, "@prminEmpresa", DbType.Int64, Empresa);
            if (CodMaterial != null)
            {
                db.AddInParameter(cmd, "@prmstrCodMaterial", DbType.String, CodMaterial);
            }
            if (Material != null)
            {
                db.AddInParameter(cmd, "@prmstrMaterial", DbType.String, Material);
            }
            db.AddInParameter(cmd, "@prmstrCalidad", DbType.String, Calidad);
            db.AddInParameter(cmd, "@prmstrTipoMaterial", DbType.String, TipoMaterial);
            db.AddInParameter(cmd, "@prmstrGrupoArticulo", DbType.String, GrupoArticulo);

            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ListarCalidadMaterial(int Empresa) {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspMM_ListarCalidadMaterial");
            db.AddInParameter(cmd, "@prminEmpresa", DbType.Int64, Empresa);
            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public DataTable getEquivalencia_List(int empresa, int idMaterial)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspMM_getEquivalencia_List");
            db.AddInParameter(cmd, "@prmEmpresa", DbType.Int32, empresa);
            db.AddInParameter(cmd, "@prmidMaterial", DbType.Int32, idMaterial);
            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable getEquivalencia_Data_byMaterial(int empresa, int idMaterial, int idBase, int idEquivalente)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspMM_getEquivalencia_Data_byMaterial");
            db.AddInParameter(cmd, "@prmEmpresa", DbType.Int32, empresa);
            db.AddInParameter(cmd, "@prmidMaterial", DbType.Int32, idMaterial);
            db.AddInParameter(cmd, "@prmidBase", DbType.Int32, idBase);
            db.AddInParameter(cmd, "@prmidEquivalente", DbType.Int32, idEquivalente);
            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable saveUpdateEquivalencia(int IdEmpresa,int idMaterial, string Login, string xml)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspMM_saveUpdateEquivalencia");
            db.AddInParameter(cmd, "@prmEmpresa", DbType.Int32, IdEmpresa);
            db.AddInParameter(cmd, "@prmintMaterial", DbType.Int32, idMaterial);
            db.AddInParameter(cmd, "@prmstrXML", DbType.String, xml);
            db.AddInParameter(cmd, "@prmUsser", DbType.String, Login);
            db.AddInParameter(cmd, "@prmFecha", DbType.DateTime, DateTime.Now);
            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}