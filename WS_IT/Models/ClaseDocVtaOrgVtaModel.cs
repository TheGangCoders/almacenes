using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System;
using System.Configuration;
using System.Data;
using System.Data.Common;

namespace WS_IT.Models
{
    public class ClaseDocVtaOrgVtaModel
    {
        private static readonly ClaseDocVtaOrgVtaModel _instancia = new ClaseDocVtaOrgVtaModel();

        public static ClaseDocVtaOrgVtaModel Instancia
        {
            get { return ClaseDocVtaOrgVtaModel._instancia; }
        }

        public DataTable getClaseDocOrganizacionVta_list(int intEmpresa)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspIT_getClaseDocOrganizacionVta_list");
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int32, intEmpresa);
            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable getClaseDocVta_list(int intEmpresa, Boolean ?status)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspIT_getClaseDocVta_list");
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int32, intEmpresa);
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

        public int enable_disableClaseDocOrganizacionVta(int id, Boolean status, string usser)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspIT_enable_disableClaseDocOrganizacionVta");
            db.AddInParameter(cmd, "@prmintClaseDocVtaOrgVta", DbType.Int32, id);
            db.AddInParameter(cmd, "@prmbitStatus", DbType.Boolean, status);
            db.AddInParameter(cmd, "@prmUsser", DbType.String, usser);
            db.AddInParameter(cmd, "@prmFecha", DbType.DateTime, DateTime.Now);
            try
            {
                return Convert.ToInt32(db.ExecuteScalar(cmd));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int saveUpdate_ClaseDocOrganizacionVta(int id, int idClase, int idOrg, string usserr)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspIT_saveUpdate_ClaseDocOrganizacionVta");
            db.AddInParameter(cmd, "@prmintClaseDocVtaOrgVta", DbType.Int32, id);
            db.AddInParameter(cmd, "@pmtintClaseDocVta", DbType.Int32, idClase);
            db.AddInParameter(cmd, "@prmintOrgVenta", DbType.Int32, idOrg);
            db.AddInParameter(cmd, "@prmUsser", DbType.String, usserr);
            db.AddInParameter(cmd, "@prmFecha", DbType.DateTime, DateTime.Now);
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