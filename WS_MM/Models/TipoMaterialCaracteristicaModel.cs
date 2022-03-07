using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace WS_MM.Models
{
    public class TipoMaterialCaracteristicaModelDAL
    {
        private static readonly TipoMaterialCaracteristicaModelDAL _instancia = new TipoMaterialCaracteristicaModelDAL();

        public static TipoMaterialCaracteristicaModelDAL Instancia
        {
            get { return TipoMaterialCaracteristicaModelDAL._instancia; }
        }

        public DataTable Listar(int prmintEmpresa, int prmintTipoMaterial)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspMM_ListarTipoMaterialCaracteristica");
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int32, prmintEmpresa);
            db.AddInParameter(cmd, "@prmintTipoMaterial", DbType.Int32, prmintTipoMaterial);
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