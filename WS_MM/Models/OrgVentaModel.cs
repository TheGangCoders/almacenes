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
    public class OrgVentaModelDAL
    {
        private static readonly OrgVentaModelDAL _instancia = new OrgVentaModelDAL();

        public static OrgVentaModelDAL Instancia
        {
            get { return OrgVentaModelDAL._instancia; }
        }

        public DataTable getOrgVenta_list(int empresa, int sociedad, Boolean? status)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspMM_getOrgVentas");
            db.AddInParameter(cmd, "@prmEmpresa", DbType.Int32, empresa);
            db.AddInParameter(cmd, "@prmSociedad", DbType.Int32, sociedad);
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
    }
}