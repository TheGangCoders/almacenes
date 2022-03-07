using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;


namespace WS_MM.Models
{
    public class PeriodoAlmacenModel
    {
        private static readonly PeriodoAlmacenModel _instancia = new PeriodoAlmacenModel();

        public static PeriodoAlmacenModel Instancia
        {
            get { return PeriodoAlmacenModel._instancia; }
        }

        public DataTable GetPeriodosAlmacen(int prmintEmpresa)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspMM_getPeriodosAlmacen");
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

        public DataTable GetCierreAlmacen(int prmintEmpresa, int prmintAnio, int prmintCerrado)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspMM_GetCierreAlmacen");
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int64, prmintEmpresa);
            db.AddInParameter(cmd, "@prmintAnio", DbType.Int64, prmintAnio);
            db.AddInParameter(cmd, "@prmintCerrado", DbType.Int64, prmintCerrado);
            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetAnio()
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspIT_GetAnio");
            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetMes()
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspIT_GetMes");
            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetDatosUltimoCierre(int prmintEmpresa, int prmintAnioPeriodo, string prmstrMesPeriodo, int prmintAlmacen, string prmstrLote, int prmintMaterial, Boolean prmbitMostrarAnulados, Boolean prmbitAgruparAlmacen)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspMM_getDatosUltimoCierre");
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int64, prmintEmpresa);
            db.AddInParameter(cmd, "@prmintAnioPeriodo", DbType.Int32, prmintAnioPeriodo);
            db.AddInParameter(cmd, "@prmintMesPeriodo", DbType.String, prmstrMesPeriodo);
            db.AddInParameter(cmd, "@prmintAlmacen", DbType.Int32, prmintAlmacen);
            db.AddInParameter(cmd, "@prmstrLote", DbType.String, prmstrLote);
            db.AddInParameter(cmd, "@prmintMaterial", DbType.Int32, prmintMaterial);
            db.AddInParameter(cmd, "@prmbitMostrarAnulados", DbType.Boolean, prmbitMostrarAnulados);
            db.AddInParameter(cmd, "@prmbitAgruparAlmacen", DbType.Boolean, prmbitAgruparAlmacen);
            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetKardexValorizado(int prmintEmpresa, int prmintAnioPeriodo, string prmstrMesPeriodo, int prmintAlmacen, string prmstrLote, int prmintMaterial, Boolean prmbitMostrarAnulados, Boolean prmbitAgruparAlmacen)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspMM_getKardexValorizado");
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int64, prmintEmpresa);
            db.AddInParameter(cmd, "@prmintAnioPeriodo", DbType.Int32, prmintAnioPeriodo);
            db.AddInParameter(cmd, "@prmintMesPeriodo", DbType.String, prmstrMesPeriodo);
            db.AddInParameter(cmd, "@prmintAlmacen", DbType.Int32, prmintAlmacen);
            db.AddInParameter(cmd, "@prmstrLote", DbType.String, prmstrLote);
            db.AddInParameter(cmd, "@prmintMaterial", DbType.Int32, prmintMaterial);
            db.AddInParameter(cmd, "@prmbitMostrarAnulados", DbType.Boolean, prmbitMostrarAnulados);
            db.AddInParameter(cmd, "@prmbitAgruparAlmacen", DbType.Boolean, prmbitAgruparAlmacen);
            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int ChangeStatus_Cierre(int prmintPeriodoContable, int prmintAnio, string prmstrMes, Boolean prmbitCerrado, string prmstrUsuario, int prmintEmpresa)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspMM_ChangeStatus_Cierre");
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int64, prmintEmpresa);
            db.AddInParameter(cmd, "@prmintPeriodoContable", DbType.Int64, prmintPeriodoContable);
            db.AddInParameter(cmd, "@prmintAnio", DbType.Int64, prmintAnio);
            db.AddInParameter(cmd, "@prmstrMes", DbType.String, prmstrMes);
            db.AddInParameter(cmd, "@prmbitCerrado", DbType.Boolean, prmbitCerrado);
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

        public int Anular_CierreAlmacen(int prmintPeriodoContable, string prmstrUsuario)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspMM_Anular_CierreAlmacen");
            db.AddInParameter(cmd, "@prmintPeriodoContable", DbType.Int64, prmintPeriodoContable);
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

        public int SaveUpdate_Cierre(int prmintPeriodoAlmacen, int prmintAnio, string prmstrMes, int idEmpresa, string prmstrDesMes, int prmintCerrado, string ussername)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspMM_saveUpdatePeriodoAlmacen");
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int32, idEmpresa);
            db.AddInParameter(cmd, "@prmintPeriodoAlmacen", DbType.Int64, prmintPeriodoAlmacen);
            db.AddInParameter(cmd, "@prmintAnio", DbType.Int64, prmintAnio);
            db.AddInParameter(cmd, "@prmstrMes", DbType.String, prmstrMes);
            db.AddInParameter(cmd, "@prmstrDesMes", DbType.String, prmstrDesMes);
            db.AddInParameter(cmd, "@prmintCerrado", DbType.Int64, prmintCerrado);
            db.AddInParameter(cmd, "@prmstrUsuario", DbType.String, ussername);

            try
            {
                return Convert.ToInt32(db.ExecuteScalar(cmd));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetReporteSaldoInventarios(int prmintEmpresa, int prmintAnioPeriodo, string prmstrMesPeriodo, int? prmintAlmacen, int? prmintTipoMaterial, int? prmintMaterial)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("[uspMM_getReporteSaldoInventarios]");
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int64, prmintEmpresa);
            db.AddInParameter(cmd, "@prmintAnio", DbType.Int32, prmintAnioPeriodo);
            db.AddInParameter(cmd, "@prmstrMes", DbType.String, prmstrMesPeriodo);
            db.AddInParameter(cmd, "@prmintAlmacen", DbType.Int32, prmintAlmacen);
            db.AddInParameter(cmd, "@prmintTipoMaterial", DbType.Int32, prmintTipoMaterial);
            db.AddInParameter(cmd, "@prmintMaterial", DbType.Int32, prmintMaterial);
            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public DataTableCollection GetKardexValorizadoSunat(int prmintEmpresa, int prmintAnioPeriodo, string prmstrMesPeriodo, int? prmintAlmacen, int? prmintMaterial)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("[uspMM_getReporteKardexSunat]");
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int64, prmintEmpresa);
            db.AddInParameter(cmd, "@prmintAnioPeriodo", DbType.Int32, prmintAnioPeriodo);
            db.AddInParameter(cmd, "@prmstrMesPeriodo", DbType.String, prmstrMesPeriodo);
            db.AddInParameter(cmd, "@prmintAlmacen", DbType.Int32, prmintAlmacen);
            db.AddInParameter(cmd, "@prmintMaterial", DbType.Int32, prmintMaterial);
            try
            {
                return db.ExecuteDataSet(cmd).Tables;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetReporteSaldoInventarioMasivo(int prmintEmpresa, int prmintAnioPeriodo, string prmstrMesPeriodo, int? prmintAlmacen, int? prmintMaterial, int? prmintTipoMaterial)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("[uspMM_getReporteKardexSunatMasivo]");
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int64, prmintEmpresa);
            db.AddInParameter(cmd, "@prmintAnio", DbType.Int32, prmintAnioPeriodo);
            db.AddInParameter(cmd, "@prmstrMes", DbType.String, prmstrMesPeriodo);
            db.AddInParameter(cmd, "@prmintAlmacen", DbType.Int32, prmintAlmacen);
            db.AddInParameter(cmd, "@prmintTipoMaterial", DbType.Int32, prmintTipoMaterial);
            db.AddInParameter(cmd, "@prmintMaterial", DbType.Int32, prmintMaterial);
            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTableCollection RecalcularCierreAlmacen(int prmintEmpresa, int prmintAnio, string prmstrMes)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspMM_RecalcularCierreAlmacen");
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int32, prmintEmpresa);
            db.AddInParameter(cmd, "@prmintAnio", DbType.Int32, prmintAnio);
            db.AddInParameter(cmd, "@prmstrMes", DbType.String, prmstrMes);

            cmd.CommandTimeout = 800;

            try
            {
                return db.ExecuteDataSet(cmd).Tables;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTableCollection RevisarSaldosFinales(int prmintEmpresa, int prmintAnio, string prmstrMes)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspMM_RevisarSaldosFinales");
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int32, prmintEmpresa);
            db.AddInParameter(cmd, "@prmintAnio", DbType.Int32, prmintAnio);
            db.AddInParameter(cmd, "@prmstrMes", DbType.String, prmstrMes);

            cmd.CommandTimeout = 800;

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