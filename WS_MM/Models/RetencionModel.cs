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
    public class RetencionModelDAL
    {
        private static readonly RetencionModelDAL _instancia = new RetencionModelDAL();

        public static RetencionModelDAL Instancia
        {
            get { return RetencionModelDAL._instancia; }
        }

        public DataTable getRetencion_list(int empresa, Boolean? status)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspMM_getRetencion");
            db.AddInParameter(cmd, "@prmEmpresa", DbType.Int32, empresa);
            db.AddInParameter(cmd, "@prmActivo", DbType.Boolean, status);

            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetRetencion(int prmintEmpresa, int? prmintActivo)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspMM_ObtenerRetencion");
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

        public int SaveUpdate_Retencion(int prmintRetencion, int prmintTipoRetencion, string prmstrCodigo, string prmstrNombre, Decimal prmdecPorcentaje, int prmintActivo, int idEmpresa, string username)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspMM_SaveUpdate_Retencion");
            db.AddInParameter(cmd, "@prmintRetencion", DbType.Int32, prmintRetencion);
            db.AddInParameter(cmd, "@prmintTipoRetencion", DbType.Int32, prmintTipoRetencion);
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int32, idEmpresa);
            db.AddInParameter(cmd, "@prmstrCodigo", DbType.String, prmstrCodigo);
            db.AddInParameter(cmd, "@prmstrNombre", DbType.String, prmstrNombre);
            db.AddInParameter(cmd, "@prmdecPorcentaje", DbType.Decimal, prmdecPorcentaje);
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

        public int Anular_Retencion(int prmintRetencion, string prmstrUsuario)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspMM_AnularRetencion");
            db.AddInParameter(cmd, "@prmintRetencion", DbType.Int64, prmintRetencion);
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