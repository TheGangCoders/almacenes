using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace WS_MM.Models
{
    public class NotaAlmacenModel
    {
        private static readonly NotaAlmacenModel _instancia = new NotaAlmacenModel();

        public static NotaAlmacenModel Instancia
        {
            get { return NotaAlmacenModel._instancia; }
        }

        public DataTable GetTipoNota(int prmintEmpresa)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspMM_GetTipoNota");
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

        public DataTable GetNotasAlmacen(int prmintCentro, int prmintAlmacen, string prmstrTipoNota, string prmstrFechaInicio, string prmstrFechaFin)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspMM_GetNotasAlmacen");
            db.AddInParameter(cmd, "@prmintCentro", DbType.Int64, prmintCentro);
            db.AddInParameter(cmd, "@prmintAlmacen", DbType.Int64, prmintAlmacen);
            db.AddInParameter(cmd, "@prmstrTipoNota", DbType.String, prmstrTipoNota);
            db.AddInParameter(cmd, "@prmstrFechaInicio", DbType.String, prmstrFechaInicio);
            db.AddInParameter(cmd, "@prmstrFechaFin", DbType.String, prmstrFechaFin);
            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTableCollection GetNotasAlmacenByIdNotaAlmacen(int prmintNotaAlmacen)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspMM_GetNotasAlmacenByIdNotaAlmacen");
            db.AddInParameter(cmd, "@prmintNotaAlmacen", DbType.Int64, prmintNotaAlmacen);
            try
            {
                return db.ExecuteDataSet(cmd).Tables;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}