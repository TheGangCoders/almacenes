using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace WS_PP.Models
{
    public class ClaseOrdenProduccionModel
    {
        private static readonly ClaseOrdenProduccionModel _instancia = new ClaseOrdenProduccionModel();

        public static ClaseOrdenProduccionModel Instancia
        {
            get { return ClaseOrdenProduccionModel._instancia; }
        }

        public DataTable GetClaseOrdenProduccion(int prmintEmpresa, int prmintActivo)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspPP_GetClaseOrdenProduccion");
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

        public int SaveUpdate_ClaseOrdenProduccion(int prmintClaseOrdenProduccion, int prmintTipoMaterial, string prmstrCodigo, string prmstrNombre, string prmstrDescripcion, string prmstrRangoInicial, string prmstrRangoFinal, int prmintActivo, int idEmpresa, string username)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspPP_SaveUpdate_ClaseOrdenProduccion");
            db.AddInParameter(cmd, "@prmintClaseOrdenProduccion", DbType.Int32, prmintClaseOrdenProduccion);
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int32, idEmpresa);
            db.AddInParameter(cmd, "@prmintTipoMaterial", DbType.Int64, prmintTipoMaterial);
            db.AddInParameter(cmd, "@prmstrCodigo", DbType.String, prmstrCodigo);
            db.AddInParameter(cmd, "@prmstrNombre", DbType.String, prmstrNombre);
            db.AddInParameter(cmd, "@prmstrDescripcion", DbType.String, prmstrDescripcion);
            db.AddInParameter(cmd, "@prmstrRangoInicial", DbType.String, prmstrRangoInicial);
            db.AddInParameter(cmd, "@prmstrRangoFinal", DbType.String, prmstrRangoFinal);
            db.AddInParameter(cmd, "@prmintActivo", DbType.Int32, prmintActivo);
            db.AddInParameter(cmd, "@prmdateFechaRegistro", DbType.DateTime, DateTime.Now);
            db.AddInParameter(cmd, "@prmstrUser", DbType.String, username);

            try
            {
                return Convert.ToInt32(db.ExecuteScalar(cmd));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Anular_ClaseOrdenProduccion(int prmintClaseOrdenProduccion, string prmstrUsuario)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspPP_Anular_ClaseOrdenProduccion");
            db.AddInParameter(cmd, "@prmintClaseOrdenProduccion", DbType.Int64, prmintClaseOrdenProduccion);
            db.AddInParameter(cmd, "@prmdateFecha", DbType.DateTime, DateTime.Now);
            db.AddInParameter(cmd, "@prmstrUsuario", DbType.String, prmstrUsuario);

            try
            {
                return Convert.ToInt32(db.ExecuteScalar(cmd));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}