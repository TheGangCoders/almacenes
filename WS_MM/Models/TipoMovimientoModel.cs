using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace WS_MM.Models
{
    public class TipoMovimientoModel
    {
        private static readonly TipoMovimientoModel _instancia = new TipoMovimientoModel();

        public static TipoMovimientoModel Instancia
        {
            get { return TipoMovimientoModel._instancia; }
        }

        public DataTable getTipoMovimiento(int prmEmpresa, Boolean? status)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspMM_GetTipoMovimiento");
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int64, prmEmpresa);
            db.AddInParameter(cmd, "@prmbitActivo", DbType.Boolean, status);
            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable getTipoMovimiento_cbo(int prmEmpresa, Boolean? status)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspMM_GetTipoMovimiento_cbo");
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int64, prmEmpresa);
            db.AddInParameter(cmd, "@prmbitActivo", DbType.Boolean, status);
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