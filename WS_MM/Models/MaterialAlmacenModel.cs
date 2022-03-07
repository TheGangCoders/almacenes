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
    public class MaterialAlmacenModelDAL
    {
        private static readonly MaterialAlmacenModelDAL _instancia = new MaterialAlmacenModelDAL();

        public static MaterialAlmacenModelDAL Instancia
        {
            get { return MaterialAlmacenModelDAL._instancia; }
        }

        public DataTable ListarPorEmpresaYMaterialYCentro(int prmintEmpresa,int prmintMaterial, int prmintCentro)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspMM_ListarMaterialAlmacenPorEmpresaYMaterialYCentro");
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