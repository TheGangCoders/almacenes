using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace WS_SD.Models
{
    public class ClaseDocVentaModel
    {
        private static readonly ClaseDocVentaModel _instancia = new ClaseDocVentaModel();

        public static ClaseDocVentaModel Instancia
        {
            get { return ClaseDocVentaModel._instancia; }
        }

        public DataTable GetClaseDocVenta(int prmintEmpresa, int prmintClaseCodVentaExc, int prmintActivo)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspSD_GetClaseDocVenta");
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int32, prmintEmpresa);
            db.AddInParameter(cmd, "@prmintClaseCodVentaExc", DbType.Int32, prmintClaseCodVentaExc);
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

        public int SaveUpdate_ClaseDocVenta(int prmintClaseDocVenta, string prmstrTipoDoc, string prmstrCodigoVta, string prmstrDescripcion, string prmstrRangoInicial, string prmstrRangoFinal, int prmintIGV, int prmintIncoterm, string prmstrClaseDocVtaSgte, int prmintActivo, int idEmpresa, string username)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspSD_SaveUpdate_ClaseDocVenta");
            db.AddInParameter(cmd, "@prmintClaseDocVenta", DbType.Int32, prmintClaseDocVenta);
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int32, idEmpresa);
            db.AddInParameter(cmd, "@prmstrTipoDoc", DbType.String, prmstrTipoDoc);
            db.AddInParameter(cmd, "@prmstrCodigoVta", DbType.String, prmstrCodigoVta);
            db.AddInParameter(cmd, "@prmstrDescripcion", DbType.String, prmstrDescripcion);
            db.AddInParameter(cmd, "@prmstrRangoInicial", DbType.String, prmstrRangoInicial);
            db.AddInParameter(cmd, "@prmstrRangoFinal", DbType.String, prmstrRangoFinal);
            db.AddInParameter(cmd, "@prmintIGV", DbType.Int32, prmintIGV);
            db.AddInParameter(cmd, "@prmintIncoterm", DbType.Int32, prmintIncoterm);
            db.AddInParameter(cmd, "@prmstrClaseDocVtaSgte", DbType.String, prmstrClaseDocVtaSgte);
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

        public int Anular_ClaseDocVenta(int prmintClaseDocVenta, string prmstrUsuario)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspSD_Anular_ClaseDocVenta");
            db.AddInParameter(cmd, "@prmintClaseDocVenta", DbType.Int64, prmintClaseDocVenta);
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