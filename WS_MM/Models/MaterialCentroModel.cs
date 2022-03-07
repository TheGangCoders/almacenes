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
    public class MaterialCentroModelDAL
    {
        private static readonly MaterialCentroModelDAL _instancia = new MaterialCentroModelDAL();

        public static MaterialCentroModelDAL Instancia
        {
            get { return MaterialCentroModelDAL._instancia; }
        }

        public DataTable ListarPorEmpresaYMaterialYCentro(int prmintEmpresa,int prmintMaterial, int prmintCentro)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspMM_ListarMaterialCentroPorEmpresaYMaterialYCentro");
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int32, prmintEmpresa);
            db.AddInParameter(cmd, "@prmintMaterial", DbType.Int32, prmintMaterial);
            db.AddInParameter(cmd, "@prmintCentro", DbType.Int32, prmintCentro);

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