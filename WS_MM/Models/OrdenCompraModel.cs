using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System;
using System.Configuration;
using System.Data;
using System.Data.Common;

namespace WS_MM.Models
{
    public class OrdenCompraModel
    {
        private static readonly OrdenCompraModel _instancia = new OrdenCompraModel();

        public static OrdenCompraModel Instancia
        {
            get { return OrdenCompraModel._instancia; }
        }

        public DataTable ObtenerPorCorrelativo(int prmintEmpresa, string prmstrCorrelativo)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspMM_ObtenerOrdenCompraPorCorrelativo");
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int32, prmintEmpresa);
            db.AddInParameter(cmd, "@prmstrCorrelativo", DbType.String, prmstrCorrelativo);
            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public DataTable ObtenerOrdenServicioCorrelativo(int prmintEmpresa, string prmstrCorrelativo)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspMM_ObtenerOrdenServicioPorCorrelativo");
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int32, prmintEmpresa);
            db.AddInParameter(cmd, "@prmstrCorrelativo", DbType.String, prmstrCorrelativo);
            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ObtenerOrdenTransporteCorrelativo(int prmintEmpresa, string prmstrCorrelativo)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspMM_ObtenerOrdenTransportePorCorrelativo");
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int32, prmintEmpresa);
            db.AddInParameter(cmd, "@prmstrCorrelativo", DbType.String, prmstrCorrelativo);
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