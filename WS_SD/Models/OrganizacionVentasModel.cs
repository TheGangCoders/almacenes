using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace WS_SD.Models
{
    public class OrganizacionVentasModel
    {
        private static readonly OrganizacionVentasModel _instancia = new OrganizacionVentasModel();

        public static OrganizacionVentasModel Instancia
        {
            get { return OrganizacionVentasModel._instancia; }
        }

        public DataTable GetOrgVentas(int prmintEmpresa, int? prmintSociedad, int? prmintActivo)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspSD_GetOrgVentas");
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int64, prmintEmpresa);
            db.AddInParameter(cmd, "@prmintSociedad", DbType.Int64, prmintSociedad);
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

        public int SaveUpdate_OrgVentas(int prmintOrgVentas, int prmintSociedad, string prmstrDescripcion, string prmstrCodigo, string prmstrMoneda, int prmintActivo, int idEmpresa, string ussername)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspSD_SaveUpdate_OrgVentas");
            db.AddInParameter(cmd, "@prmintOrgVentas", DbType.Int32, prmintOrgVentas);
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int32, idEmpresa);
            db.AddInParameter(cmd, "@prmintSociedad", DbType.Int32, prmintSociedad);
            db.AddInParameter(cmd, "@prmstrDescripcion", DbType.String, prmstrDescripcion);
            db.AddInParameter(cmd, "@prmstrCodigo", DbType.String, prmstrCodigo);
            db.AddInParameter(cmd, "@prmstrMoneda", DbType.String, prmstrMoneda);
            db.AddInParameter(cmd, "@prmintActivo", DbType.Int32, prmintActivo);
            db.AddInParameter(cmd, "@prmdateFechaRegistro", DbType.DateTime, DateTime.Now);
            db.AddInParameter(cmd, "@prmstrUsser", DbType.String, ussername);

            try
            {
                return Convert.ToInt32(db.ExecuteScalar(cmd));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Anular_OrgVentas(int prmintOrgVentas, string prmstrUsuario)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspSD_Anular_OrgVentas");
            db.AddInParameter(cmd, "@prmintOrgVentas", DbType.Int64, prmintOrgVentas);
            db.AddInParameter(cmd, "@prmstrUser", DbType.String, prmstrUsuario);
            db.AddInParameter(cmd, "@prmdateFecha", DbType.DateTime, DateTime.Now);

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