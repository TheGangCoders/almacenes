using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace WS_MM.Models
{
    public class TipoUnidadMedidaModel
    {
        private static readonly TipoUnidadMedidaModel _instancia = new TipoUnidadMedidaModel();

        public static TipoUnidadMedidaModel Instancia
        {
            get { return TipoUnidadMedidaModel._instancia; }
        }

        public DataTable GetTipoUnidadMedida(int prmintEmpresa, int? prmintActivo)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspMM_GetTipoUnidadMedida");
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int64, prmintEmpresa);
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

        public int SaveUpdate_TipoUnidadMedida(int prmintTipoUnidadMedida, string prmstrNombre, string prmstrAbreviatura, int prmintActivo, int idEmpresa, string username)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspMM_SaveUpdate_TipoUnidadMedida");
            db.AddInParameter(cmd, "@prmintTipoUnidadMedida", DbType.Int32, prmintTipoUnidadMedida);
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int32, idEmpresa);
            db.AddInParameter(cmd, "@prmstrNombre", DbType.String, prmstrNombre);
            db.AddInParameter(cmd, "@prmstrAbreviatura", DbType.String, prmstrAbreviatura);
            db.AddInParameter(cmd, "@prmintActivo", DbType.String, prmintActivo);
            db.AddInParameter(cmd, "@prmdateFechaRegistro", DbType.DateTime, DateTime.Now);
            db.AddInParameter(cmd, "@prmstrUser", DbType.String, username);

            try
            {
                return Convert.ToInt32(db.ExecuteScalar(cmd));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Anular_TipoUnidadMedida(int prmintTipoUnidadMedida, string prmstrUsuario)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspMM_AnularTipoUnidadMedida");
            db.AddInParameter(cmd, "@prmintTipoUnidadMedida", DbType.Int64, prmintTipoUnidadMedida);
            db.AddInParameter(cmd, "@prmdateFecha", DbType.DateTime, DateTime.Now);
            db.AddInParameter(cmd, "@prmstrUsuario", DbType.String, prmstrUsuario);

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