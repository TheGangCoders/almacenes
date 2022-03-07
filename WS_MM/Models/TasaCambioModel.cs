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
    public class TasaCambioModel
    {
        private static readonly TasaCambioModel _instancia = new TasaCambioModel();

        public static TasaCambioModel Instancia
        {
            get { return TasaCambioModel._instancia; }
        }

        public DataTable GetTasaCambio(int prmintEmpresa)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspMM_GetTasa");
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int32, prmintEmpresa);
            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetTasaCambioById(int prmintTasa)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspMM_GetTasaCambioById");
            db.AddInParameter(cmd, "@prmintTasa", DbType.Int32, prmintTasa);
            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetTasaCambioByFilters(string prmstrMOrigen, string prmstrMDestino, int prmintActivo, int prmintEmpresa, DateTime prmdtFdesde, DateTime prmdtFhasta)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("[uspMM_GetTasaCambioByFilters]");
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int32, prmintEmpresa);
            db.AddInParameter(cmd, "@prmstrMOrigen", DbType.String, prmstrMOrigen);
            db.AddInParameter(cmd, "@prmstrMDestino", DbType.String, prmstrMDestino);
            db.AddInParameter(cmd, "@prmintActivo", DbType.Int32, prmintActivo);
            db.AddInParameter(cmd, "@prmdateFechaDesde", DbType.DateTime, prmdtFdesde);
            db.AddInParameter(cmd, "@prmdateFechaHasta", DbType.DateTime, prmdtFhasta);

            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int ChangeStatus_TasaCambio(int prmintTasa, Boolean prmbitActivo, string prmstrUsuario)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("[uspMM_ChangeStatusTasaCambio]");
            db.AddInParameter(cmd, "@prmintTasa", DbType.Int32, prmintTasa);
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

        public int SaveUpdate_TasaCambio(int idTasa,int idEmpresa, DateTime prmdateFecha, string prmstrMOrigen, string prmstrMDestino, Decimal prmdecValorCompra,
            Decimal prmdecValorVenta, string ussername)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspMM_SaveUpdate_TasaCambio");
            db.AddInParameter(cmd, "@prmintTasa", DbType.Int32, idTasa);
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int32, idEmpresa);
            db.AddInParameter(cmd, "@prmdateFecha", DbType.DateTime, prmdateFecha);
            db.AddInParameter(cmd, "@prmstrMOrigen", DbType.String, prmstrMOrigen);
            db.AddInParameter(cmd, "@prmstrMDestino", DbType.String, prmstrMDestino);
            db.AddInParameter(cmd, "@prmdecValorCompra", DbType.Decimal, prmdecValorCompra);
            db.AddInParameter(cmd, "@prmdecValorVenta", DbType.Decimal, prmdecValorVenta);
            db.AddInParameter(cmd, "@prmdateFechaRegistro", DbType.DateTime, DateTime.Now);
            db.AddInParameter(cmd, "@prmstrUsser", DbType.String, ussername);

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