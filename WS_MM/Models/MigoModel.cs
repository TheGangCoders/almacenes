using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace WS_MM.Models
{
    public class MigoModel
    {
        private static readonly MigoModel _instancia = new MigoModel();

        public static MigoModel Instancia
        {
            get { return MigoModel._instancia; }
        }

        public DataTable GetTipoMovimiento(int prmintEmpresa, Boolean? prmbitActivo)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("[uspMM_GetTipoMovimiento]");
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int64, prmintEmpresa);
            db.AddInParameter(cmd, "@prmbitActivo", DbType.Boolean, prmbitActivo);
            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetCabeceraDetalleDocuemento(string prmstrTipoMovimiento, string prmstrTipoDocumento, string prmstrDocumento, int prmintEmpresa)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("[uspMM_GetCabeceraDetalleDocumento]");
            db.AddInParameter(cmd, "@prmstrTipoMovimiento", DbType.String, prmstrTipoMovimiento);
            db.AddInParameter(cmd, "@prmstrTipoDocumento", DbType.String, prmstrTipoDocumento);
            db.AddInParameter(cmd, "@prmstrDocumento", DbType.String, prmstrDocumento);
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int32, prmintEmpresa);
            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int SaveUpdateMigo(String usuario, String xml, String xmlDMigo)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspMM_SaveUpdateMigo");
            db.AddInParameter(cmd, "@prmstrUsuario", DbType.String, usuario);
            db.AddInParameter(cmd, "@prmstrXml", DbType.String, xml);
            db.AddInParameter(cmd, "@prmstrXmlDMigo", DbType.String, xmlDMigo);
            try
            {
                return Convert.ToInt16(db.ExecuteScalar(cmd));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public DataTable GetCabeceraDetalleDocumentoMigo(int prmintoDocumentoMigo)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("[uspMM_GetCabeceraDetalleMigo]");
            db.AddInParameter(cmd, "@prmintMigo", DbType.String, prmintoDocumentoMigo);
            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetEstadoMIGO(int prmintEmpresa)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspMM_GetEstadoMIGO");
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int32, prmintEmpresa);
            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ValidarStockNegativo(String xmlDMigo)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspMM_ValidarStockMigo");
           db.AddInParameter(cmd, "@prmstrXmlDMigo", DbType.String, xmlDMigo);
            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetPorcentajeIGV(int prmintEmpresa, int? prmbitActivo)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspMM_GetPorcentajeIGV");
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int32, prmintEmpresa);
            db.AddInParameter(cmd, "@prmbitActivo", DbType.Int32, prmbitActivo);
            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetDocumentoMaterial(string FechaInicio, string FechaFin, string TipoMovimiento, string TipoDocumento)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("[uspMM_GetDocumentoMaterial]");
            db.AddInParameter(cmd, "@prmstrFechaInicio", DbType.String, FechaInicio);
            db.AddInParameter(cmd, "@prmstrFechaFin", DbType.String, FechaFin);
            db.AddInParameter(cmd, "@prmstrTipoMovimiento", DbType.String, TipoMovimiento);
            db.AddInParameter(cmd, "@prmstrTipoDocumento", DbType.String, TipoDocumento);
            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetUnidadMedidaEquivalencia(int Material,  int Almacen)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("[uspMM_UnidadMedidaEquivalencia]");
            db.AddInParameter(cmd, "@prmintMaterial", DbType.Int16, Material);
            db.AddInParameter(cmd, "@prmintAlmacen", DbType.Int16, Almacen);
            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetDocumentoMaterialMigo(string FechaInicio, string FechaFin, string TipoMovimiento, string TipoDocumento, int Empresa, int Sociedad)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("[uspMM_GetDocumentoMaterial_Migo]");
            db.AddInParameter(cmd, "@prmstrFechaInicio", DbType.String, FechaInicio);
            db.AddInParameter(cmd, "@prmstrFechaFin", DbType.String, FechaFin);
            db.AddInParameter(cmd, "@prmstrTipoMovimiento", DbType.String, TipoMovimiento);
            db.AddInParameter(cmd, "@prmstrTipoDocumento", DbType.String, TipoDocumento);
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.String, Empresa);
            db.AddInParameter(cmd, "@prmintSociedad", DbType.String, Sociedad);
            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable getSearchMaterialesMigo(int Empresa, string CodMaterial, string Material, string TipoMaterial, string GrupoArticulo, int? Almacen)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("[uspSD_getSearchMaterialesMigo]");
            db.AddInParameter(cmd, "@prminEmpresa", DbType.Int64, Empresa);
            db.AddInParameter(cmd, "@prmstrCodMaterial", DbType.String, CodMaterial);
            db.AddInParameter(cmd, "@prmstrMaterial", DbType.String, Material);
            db.AddInParameter(cmd, "@prmstrTipoMaterial", DbType.String, TipoMaterial);
            db.AddInParameter(cmd, "@prmstrGrupoArticulo", DbType.String, GrupoArticulo);
            db.AddInParameter(cmd, "@prmintAlmacen", DbType.String, Almacen);

            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetListadoOrdenesTransporteMigo(string FechaInicio, string FechaFin, int Empresa, int Sociedad)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("[uspSD_ListarOrdenesTransporte]");
            db.AddInParameter(cmd, "@prmstrFechaInicio", DbType.String, FechaInicio);
            db.AddInParameter(cmd, "@prmstrFechaFin", DbType.String, FechaFin);
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int32, Empresa);
            db.AddInParameter(cmd, "@prmintSociedad", DbType.Int32, Sociedad);
            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    
        public DataTable GetListarTrasladosAlmacenes(string FechaInicio, string FechaFin, int AlmacenOrigen, int AlmacenDestino, int Empresa, int Sociedad)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("[uspSD_ListarTrasladosAlmacenes]");
            db.AddInParameter(cmd, "@prmstrFechaInicio", DbType.String, FechaInicio);
            db.AddInParameter(cmd, "@prmstrFechaFin", DbType.String, FechaFin);
            db.AddInParameter(cmd, "@prmintAlmacenOrigen", DbType.Int32, AlmacenOrigen);
            db.AddInParameter(cmd, "@prmintAlmacenDestino", DbType.Int32, AlmacenDestino);
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int32, Empresa);
            db.AddInParameter(cmd, "@prmintSociedad", DbType.Int32, Sociedad);
            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }        

        public DataTable GetTasaCambioByFechaContabilizacion(string FechaContable, string MonedaOrigen, string MonedaDestino, int Empresa, int OrdenCompra, int OrdenTransporte)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("[uspMM_GetTasaCambioByFechaContabilizacion]");
            db.AddInParameter(cmd, "@prmdatFechaContabilizacion", DbType.String, FechaContable);
            db.AddInParameter(cmd, "@prmstrTipoMonedaOrigen", DbType.String, MonedaOrigen);
            db.AddInParameter(cmd, "@prmstrTipoMonedaDestino", DbType.String, MonedaDestino);
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int16, Empresa);
            db.AddInParameter(cmd, "@prmintOrdenCompra", DbType.Int16, OrdenCompra);
            db.AddInParameter(cmd, "@prmintOrdenTransporte", DbType.Int16, OrdenTransporte);
            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable GetCentroCosto()
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("[uspMM_ListaCentroCosto]");
            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable GetCentroCostoCampanya()
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("[uspMM_ListaCentroCampanya]");
            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable GetReservasMigo(string fechaInicio, string fechaFin, int Empresa, int Sociedad)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspMM_GetReservasMigo");
            db.AddInParameter(cmd, "@prmstrFechaInicio", DbType.String, fechaInicio);
            db.AddInParameter(cmd, "@prmstrFechaFin", DbType.String, fechaFin);
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int32, Empresa);
            db.AddInParameter(cmd, "@prmintSociedad", DbType.Int32, Sociedad);
            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetClaseValor(int Empresa, int Sociedad)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspMM_GetClaseValor");
            db.AddInParameter(cmd, "@prmintIdEmpresa", DbType.Int32, Empresa);
            db.AddInParameter(cmd, "@prmintIdSociedad", DbType.Int32, Sociedad);
            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetItemValor(int Empresa, int Sociedad)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspMM_GetItemValor");
            db.AddInParameter(cmd, "@prmintIdEmpresa", DbType.Int32, Empresa);
            db.AddInParameter(cmd, "@prmintIdSociedad", DbType.Int32, Sociedad);
            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable GetReporteGuiasVentas(string prmdatFechaDesde, string prmdatFechaHasta, int Empresa, int Sociedad)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspMM_GetReporteGuiasVentas");
            db.AddInParameter(cmd, "@prmdatFechaDesde", DbType.String, prmdatFechaDesde);
            db.AddInParameter(cmd, "@prmdatFechaHasta", DbType.String, prmdatFechaHasta);
            db.AddInParameter(cmd, "@prmintIdEmpresa", DbType.Int32, Empresa);
            db.AddInParameter(cmd, "@prmintIdSociedad", DbType.Int32, Sociedad);
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