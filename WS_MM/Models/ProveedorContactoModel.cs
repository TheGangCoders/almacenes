using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace WS_MM.Models
{
    public class ProveedorContactoModel
    {
        private static readonly ProveedorContactoModel _instancia = new ProveedorContactoModel();

        public static ProveedorContactoModel Instancia
        {
            get { return ProveedorContactoModel._instancia; }
        }

        public DataTable Listar(int prmintEmpresa, int prmintProveedorSociedad)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspMM_ListarProveedorContacto");
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int32, prmintEmpresa);
            db.AddInParameter(cmd, "@prmintProveedorSociedad", DbType.Int32, prmintProveedorSociedad);
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