using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System;
using System.Configuration;
using System.Data;
using System.Data.Common;

namespace WS_MM.Models
{
    public class DetalleOrdenCompraModel
    {
        private static readonly DetalleOrdenCompraModel _instancia = new DetalleOrdenCompraModel();

        public static DetalleOrdenCompraModel Instancia
        {
            get { return DetalleOrdenCompraModel._instancia; }
        }

        public DataTable Obtener(int prmintEmpresa, int prmintOrdenCompra)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspMM_ObtenerDetalleOrdenCompra");
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int32, prmintEmpresa);
            db.AddInParameter(cmd, "@prmintOrdenCompra", DbType.Int32, prmintOrdenCompra);
            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ObtenerFaltante(int prmintEmpresa, int prmintOrdenCompra)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspMM_ObtenerDetalleOrdenCompraFaltante");
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int32, prmintEmpresa);
            db.AddInParameter(cmd, "@prmintOrdenCompra", DbType.Int32, prmintOrdenCompra);
            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ObtenerFaltanteServicio(int prmintEmpresa, int prmintOrdenCompra)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspMM_ObtenerDetalleOrdenServicioFaltante");
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int32, prmintEmpresa);
            db.AddInParameter(cmd, "@prmintOrdenCompra", DbType.Int32, prmintOrdenCompra);
            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public DataTable ObtenerFaltanteTransporte(int prmintEmpresa, int prmintOrdenCompra)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspMM_ObtenerDetalleOrdenTransporteFaltante");
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int32, prmintEmpresa);
            db.AddInParameter(cmd, "@prmintOrdenCompra", DbType.Int32, prmintOrdenCompra);
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