using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace WS_MM.Models
{
    public class TipoMaterialModel
    {
        private static readonly TipoMaterialModel _instancia = new TipoMaterialModel();

        public static TipoMaterialModel Instancia
        {
            get { return TipoMaterialModel._instancia; }
        }

        public DataTable GetTipoMaterial(int prmEmpresa, Boolean? status)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspMM_getTipoMaterial");
            db.AddInParameter(cmd, "@prmEmpresa", DbType.Int64, prmEmpresa);
            db.AddInParameter(cmd, "@prmActivo", DbType.Boolean, status);
            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable Guardar(int prmintTipoMaterial, int prmintEmpresa, string prmstrCodigo, string prmstrNombre, string prmstrDescripcion, 
                                string prmstrRangoInicial, string prmstrRangoFinal, bool prmbitActivo, string prmstrLogin, string prmstrCaracteristicasXml)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspMM_GuardarTipoMaterial");
            db.AddInParameter(cmd, "@prmintTipoMaterial", DbType.Int32, prmintTipoMaterial);
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int32, prmintEmpresa);
            db.AddInParameter(cmd, "@prmstrCodigo", DbType.String, prmstrCodigo);
            db.AddInParameter(cmd, "@prmstrNombre", DbType.String, prmstrNombre);
            db.AddInParameter(cmd, "@prmstrDescripcion", DbType.String, prmstrDescripcion);
            db.AddInParameter(cmd, "@prmstrRangoInicial", DbType.String, prmstrRangoInicial);
            db.AddInParameter(cmd, "@prmstrRangoFinal", DbType.String, prmstrRangoFinal);
            db.AddInParameter(cmd, "@prmbitActivo", DbType.Boolean, prmbitActivo);
            db.AddInParameter(cmd, "@prmstrLogin", DbType.String, prmstrLogin);
            db.AddInParameter(cmd, "@prmxmlCaracteristicas", DbType.Xml, prmstrCaracteristicasXml);
            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ActualizarActivo(int prmintTipoMaterial, int prmintEmpresa, bool prmbitActivo, string prmstrLogin)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspMM_ActualizarActivoTipoMaterial");
            db.AddInParameter(cmd, "@prmintTipoMaterial", DbType.Int32, prmintTipoMaterial);
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int32, prmintEmpresa);
            db.AddInParameter(cmd, "@prmbitActivo", DbType.Boolean, prmbitActivo);
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

        public DataTable Obtener(int prmintTipoMaterial, int prmintEmpresa)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspMM_ObtenerTipoMaterial");
            db.AddInParameter(cmd, "@prmintTipoMaterial", DbType.Int32, prmintTipoMaterial);
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
        public DataTable Listar(int prmintEmpresa, int prmintActivo)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspMM_ListarTipoMaterial");
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int32, prmintEmpresa);
            db.AddInParameter(cmd, "@prmintActivo", DbType.Int32, prmintActivo);
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