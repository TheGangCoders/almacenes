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
    public class MaterialNombreComercialModelDAL
    {
        private static readonly MaterialNombreComercialModelDAL _instancia = new MaterialNombreComercialModelDAL();

        public static MaterialNombreComercialModelDAL Instancia
        {
            get { return MaterialNombreComercialModelDAL._instancia; }
        }

        public DataTable ListarPorEmpresaYMaterialYOrgVentas(int prmintEmpresa,int prmintMaterial, int prmintOrgVentas)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspMM_ListarMaterialNombreComercialPorEmpresaYMaterialYOrgVentas");
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int32, prmintEmpresa);
            db.AddInParameter(cmd, "@prmintMaterial", DbType.Int32, prmintMaterial);
            db.AddInParameter(cmd, "@prmintOrgVentas", DbType.Int32, prmintOrgVentas);

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