using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System;
using System.Configuration;
using System.Data;
using System.Data.Common;

namespace WS_MM.Models
{
    public class FacturaCompraModel
    {
        private static readonly FacturaCompraModel _instancia = new FacturaCompraModel();

        public static FacturaCompraModel Instancia
        {
            get { return FacturaCompraModel._instancia; }
        }

        public DataTable Guardar(int prmintFacturaCompra, int prmintEmpresa, int prmintSociedad, int prmintProveedor,
                                int? prmintOrdenCompra, string prmstrCodigoCotizacion, string prmstrTipoDocumento, string prmstrFormaPago,
                                string prmdatFechaEmision, string prmdatFechaEntrega, string prmstrTipoMoneda, decimal prmdecTasaCambio,
                                bool prmbitIncluyeIgv, bool prmbitIncluyeRenta, decimal prmdecSubTotal, decimal prmdecDescuento, decimal prmdecIgvPorcentaje,
                                decimal prmdecIgv, decimal prmdecRentaPorcentaje, decimal prmdecRenta, decimal prmdecTotal, decimal prmdecDetraccion, string prmstrEstado, 
                                string prmstrLogin, string Serie, string Numero, int? OrdenServicio, int? OrdenTransporte, string ValorDoc, string prmxmlDetalle)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspMM_GuardarFacturaCompra");
            db.AddInParameter(cmd, "@prmintFacturaCompra", DbType.Int32, prmintFacturaCompra);
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int32, prmintEmpresa);
            db.AddInParameter(cmd, "@prmintSociedad", DbType.Int32, prmintSociedad);
            db.AddInParameter(cmd, "@prmintProveedor", DbType.Int32, prmintProveedor);
            db.AddInParameter(cmd, "@prmintOrdenCompra", DbType.Int32, prmintOrdenCompra);
            db.AddInParameter(cmd, "@prmintOrdenServicio", DbType.Int32, OrdenServicio);
            db.AddInParameter(cmd, "@prmintOrdenTransporte", DbType.Int32, OrdenTransporte);
            db.AddInParameter(cmd, "@prmstrValorDoc", DbType.String, ValorDoc);
            db.AddInParameter(cmd, "@prmstrCodigoCotizacion", DbType.String, prmstrCodigoCotizacion);
            db.AddInParameter(cmd, "@prmstrTipoDocumento", DbType.String, prmstrTipoDocumento);
            db.AddInParameter(cmd, "@prmstrFormaPago", DbType.String, prmstrFormaPago);
            db.AddInParameter(cmd, "@prmdatFechaEmision", DbType.String, prmdatFechaEmision);
            db.AddInParameter(cmd, "@prmdatFechaEntrega", DbType.String, prmdatFechaEntrega);
            db.AddInParameter(cmd, "@prmstrTipoMoneda", DbType.String, prmstrTipoMoneda);
            db.AddInParameter(cmd, "@prmdecTasaCambio", DbType.Decimal, prmdecTasaCambio);
            db.AddInParameter(cmd, "@prmbitIncluyeIgv", DbType.Boolean, prmbitIncluyeIgv);
            db.AddInParameter(cmd, "@prmbitIncluyeRenta", DbType.Boolean, prmbitIncluyeRenta);
            db.AddInParameter(cmd, "@prmdecSubTotal", DbType.Decimal, prmdecSubTotal);
            db.AddInParameter(cmd, "@prmdecDescuento", DbType.Decimal, prmdecDescuento);
            db.AddInParameter(cmd, "@prmdecIgvPorcentaje", DbType.Decimal, prmdecIgvPorcentaje);
            db.AddInParameter(cmd, "@prmdecIgv", DbType.Decimal, prmdecIgv);
            db.AddInParameter(cmd, "@prmdecRentaPorcentaje", DbType.Decimal, prmdecRentaPorcentaje);
            db.AddInParameter(cmd, "@prmdecRenta", DbType.Decimal, prmdecRenta);
            db.AddInParameter(cmd, "@prmdecTotal", DbType.Decimal, prmdecTotal);
            db.AddInParameter(cmd, "@prmdecDetraccion", DbType.Decimal, prmdecDetraccion);
            db.AddInParameter(cmd, "@prmstrEstado", DbType.String, prmstrEstado);
            db.AddInParameter(cmd, "@prmstrLogin", DbType.String, prmstrLogin);
            db.AddInParameter(cmd, "@prmstrSerie", DbType.String, Serie);
            db.AddInParameter(cmd, "@prmstrNumero", DbType.String, Numero);
            db.AddInParameter(cmd, "@prmxmlDetalle", DbType.String, prmxmlDetalle);
            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable Anular(int prmintFacturaCompra, int prmintEmpresa, string prmstrLogin)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspMM_AnularFacturaCompra");
            db.AddInParameter(cmd, "@prmintFacturaCompra", DbType.Int32, prmintFacturaCompra);
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int32, prmintEmpresa);
            db.AddInParameter(cmd, "@prmstrLogin", DbType.String, prmstrLogin);
            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable Obtener(int prmintFacturaCompra, int prmintEmpresa)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspMM_ObtenerFacturaCompra");
            db.AddInParameter(cmd, "@prmintFacturaCompra", DbType.Int32, prmintFacturaCompra);
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

        public DataTable Listar(int prmintEmpresa, string prmstrEstado, string prmdatFechaDesde, string prmdatFechaHasta)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspMM_ListarFacturaCompra");
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int32, prmintEmpresa);
            db.AddInParameter(cmd, "@prmstrEstado", DbType.String, prmstrEstado);
            db.AddInParameter(cmd, "@prmdatFechaDesde", DbType.Date, prmdatFechaDesde);
            db.AddInParameter(cmd, "@prmdatFechaHasta", DbType.Date, prmdatFechaHasta);
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