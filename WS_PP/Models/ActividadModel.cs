using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace WS_PP.Models
{
    public class ActividadModel
    {
        private static readonly ActividadModel _instancia = new ActividadModel();

        public static ActividadModel Instancia
        {
            get { return ActividadModel._instancia; }
        }

        public DataTable GetActividades(int prmintEmpresa, int? prmintActividad, int? prmintActivo)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspPP_getActividades");
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int64, prmintEmpresa);
            db.AddInParameter(cmd, "@prmintActividad", DbType.Int64, prmintActividad);
            db.AddInParameter(cmd, "@prmbitActivo", DbType.Int32, prmintActivo);
            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int SaveUpdateActividad(String prmstrUsuario, Int64 prmintActividad, int prmintEmpresa, String prmstrCodigo, String prmstrNombre, 
            String prmstrDescripcion, Int64 prmintUnidadMedida, int prmintActivo)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspPP_saveUpdateActividad");            
            db.AddInParameter(cmd, "@prmintActividad", DbType.Int64, prmintActividad);
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int64, prmintEmpresa);
            db.AddInParameter(cmd, "@prmstrCodigo", DbType.String, prmstrCodigo);
            db.AddInParameter(cmd, "@prmstrNombre", DbType.String, prmstrNombre);
            db.AddInParameter(cmd, "@prmstrDescripcion", DbType.String, prmstrDescripcion);
            db.AddInParameter(cmd, "@prmintUnidadMedida", DbType.Int64, prmintUnidadMedida);
            db.AddInParameter(cmd, "@prmbitActivo", DbType.Int32, prmintActivo);
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

        public int EnableDisableActividad(string prmstrUsuario, int prmintActividad, Boolean prmbitActivo)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspPP_enableDisableActividad");
            db.AddInParameter(cmd, "@prmintActividad", DbType.Int32, prmintActividad);
            db.AddInParameter(cmd, "@prmbitActivo", DbType.Boolean, prmbitActivo);
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