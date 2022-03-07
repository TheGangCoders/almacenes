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
    public class MaterialPrecioModelDAL
    {
        private static readonly MaterialPrecioModelDAL _instancia = new MaterialPrecioModelDAL();

        public static MaterialPrecioModelDAL Instancia
        {
            get { return MaterialPrecioModelDAL._instancia; }
        }

        public DataTable ListarPorEmpresaYMaterialOrgVentas(int prmintEmpresa,int prmintMaterialOrgVentas)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspMM_ListarMaterialPrecioPorEmpresaYMaterialOrgVentas");
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int32, prmintEmpresa);
            db.AddInParameter(cmd, "@prmintMaterialOrgVentas", DbType.Int32, prmintMaterialOrgVentas);

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