using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace WS_PP.Models
{
    public class CostosPorActividadModel
    {
        private static readonly CostosPorActividadModel _instancia = new CostosPorActividadModel();

        public static CostosPorActividadModel Instancia
        {
            get { return CostosPorActividadModel._instancia; }
        }

        public DataTable GetActividades(int prmintEmpresa)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspPP_ListarActividades");
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int64, prmintEmpresa);

            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetCostoActividad(int prmintEmpresa, int prmintActivo)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspPP_GetCostoActividad");
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int64, prmintEmpresa);
            db.AddInParameter(cmd, "@prmintActivo", DbType.Int64, prmintActivo);

            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int SaveUpdate_CostoActividad(int prmintCostoActividad, int prmintActividad, Decimal prmdecCosto, DateTime prmdateFechaInicio, DateTime prmdateFechaFin, int prmintActivo, int prmintEmpresa, string prmstrUser)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspPP_SaveUpdate_CostoActividad");
            db.AddInParameter(cmd, "@prmintCostoActividad", DbType.Int32, prmintCostoActividad);
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int64, @prmintEmpresa);
            db.AddInParameter(cmd, "@prmintActividad", DbType.Int64, @prmintActividad);
            db.AddInParameter(cmd, "@prmdecCosto", DbType.Decimal, prmdecCosto);
            db.AddInParameter(cmd, "@prmdateFechaInicio", DbType.DateTime, prmdateFechaInicio);
            db.AddInParameter(cmd, "@prmdateFechaFin", DbType.DateTime, prmdateFechaFin);
            db.AddInParameter(cmd, "@prmintActivo", DbType.Int32, prmintActivo);
            db.AddInParameter(cmd, "@prmdateFechaRegistro", DbType.DateTime, DateTime.Now);
            db.AddInParameter(cmd, "@prmstrUser", DbType.String, prmstrUser);

            try
            {
                return Convert.ToInt32(db.ExecuteScalar(cmd));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Anular_CostoActividad(int prmintCostoActividad, string prmstrUsuario)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspPP_Anular_CostoActividad");
            db.AddInParameter(cmd, "@prmintCostoActividad", DbType.Int64, prmintCostoActividad);
            db.AddInParameter(cmd, "@prmstrUser", DbType.String, prmstrUsuario);
            db.AddInParameter(cmd, "@prmdateFecha", DbType.DateTime, DateTime.Now);

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