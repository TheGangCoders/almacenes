using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace WS_MM.Models
{
    public class ConductorModel
    {
        private static readonly ConductorModel _instancia = new ConductorModel();

        public static ConductorModel Instancia
        {
            get { return ConductorModel._instancia; }
        }

        public DataTable GetConductor(int prmintEmpresa, int? prmintActivo)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspMM_GetConductor");
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int64, prmintEmpresa);
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

        public int SaveUpdate_Conductor(int prmintConductor, int prmintProveedor, string prmstrNombres, string prmstrDocIdentidad, string prmstrLicencia, int prmintActivo, int idEmpresa, string username)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspMM_SaveUpdate_Conductor");
            db.AddInParameter(cmd, "@prmintConductor", DbType.Int32, prmintConductor);
            db.AddInParameter(cmd, "@prmintProveedor", DbType.Int32, prmintProveedor);
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int32, idEmpresa);
            db.AddInParameter(cmd, "@prmstrNombres", DbType.String, prmstrNombres);
            db.AddInParameter(cmd, "@prmstrDocIdentidad", DbType.String, prmstrDocIdentidad);
            db.AddInParameter(cmd, "@prmstrLicencia", DbType.String, prmstrLicencia);
            db.AddInParameter(cmd, "@prmintActivo", DbType.String, prmintActivo);
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

        public int Anular_Conductor(int prmintConductor, string prmstrUsuario)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("[uspMM_AnularConductor]");
            db.AddInParameter(cmd, "@prmintConductor", DbType.Int64, prmintConductor);
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