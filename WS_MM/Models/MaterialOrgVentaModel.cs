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
    public class MaterialOrgVentaModelDAL
    {
        private static readonly MaterialOrgVentaModelDAL _instancia = new MaterialOrgVentaModelDAL();

        public static MaterialOrgVentaModelDAL Instancia
        {
            get { return MaterialOrgVentaModelDAL._instancia; }
        }

        public DataTable ListarPorEmpresaYMaterialYSociedad(int prmintEmpresa,int prmintMaterial, int prmintSociedad)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspMM_ListarMaterialOrgVentasPorEmpresaYMaterialYSociedad");
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int32, prmintEmpresa);
            db.AddInParameter(cmd, "@prmintMaterial", DbType.Int32, prmintMaterial);
            db.AddInParameter(cmd, "@prmintSociedad", DbType.Int32, prmintSociedad);

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