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
    public class OrdenTransporteModel
    {
        private static readonly OrdenTransporteModel _instancia = new OrdenTransporteModel();

        public static OrdenTransporteModel Instancia
        {
            get { return OrdenTransporteModel._instancia; }
        }

        public int SaveUpdate_OrdenTransporte(int prmintOrdenTransporte, int prmintSociedad, int prmintEmpresa, int prmintTransportista, int? prmintVehiculo, int? prmintConductor, int prminAlmacenOrigen, int prmintAlmacenDestino, 
            DateTime prmdateFechaEmision, string prmstrTipoDocumento, string prmstrFormaPago, string prmstrTipoMoneda, /*Decimal prmdecTasaCambio, */int prmintIGV, int prmintRenta, Decimal prmdecSubTotal, Decimal prmdecDescuento,
            Decimal prmdecPorcentajeIGV, Decimal prmdecValorIGV, Decimal prmdecPorcentajeRenta, Decimal prmdecValorRenta, Decimal prmdecTotal, int prmintActivo, string prmstrEstado, string prmstrUsuario, string prmstrXml)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspMM_SaveUpdateOrdenTransporte");
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int32, prmintEmpresa);
            db.AddInParameter(cmd, "@prmintSociedad", DbType.Int32, prmintSociedad);
            db.AddInParameter(cmd, "@prmintOrdenTransporte", DbType.Int32, prmintOrdenTransporte);
            db.AddInParameter(cmd, "@prmintTransportista", DbType.Int32, prmintTransportista);
            db.AddInParameter(cmd, "@prmintVehiculo", DbType.Int32, prmintVehiculo);
            db.AddInParameter(cmd, "@prmintConductor", DbType.Int32, prmintConductor);
            db.AddInParameter(cmd, "@prmintAlmacenOrigen", DbType.Int32, prminAlmacenOrigen);
            db.AddInParameter(cmd, "@prmintAlmacenDestino", DbType.Int32, prmintAlmacenDestino);

            db.AddInParameter(cmd, "@prmdateFechaEmision", DbType.Date, prmdateFechaEmision);

            db.AddInParameter(cmd, "@prmstrTipoDocumento", DbType.String, prmstrTipoDocumento);
            db.AddInParameter(cmd, "@prmstrFormaPago", DbType.String, prmstrFormaPago);
            db.AddInParameter(cmd, "@prmstrTipoMoneda", DbType.String, prmstrTipoMoneda);

            //db.AddInParameter(cmd, "@prmdecTasaCambio", DbType.Decimal, prmdecTasaCambio);

            db.AddInParameter(cmd, "@prmintIGV", DbType.Int32, prmintIGV);
            db.AddInParameter(cmd, "@prmintRenta", DbType.Int32, prmintRenta);

            db.AddInParameter(cmd, "@prmdecSubTotal", DbType.Decimal, prmdecSubTotal);
            db.AddInParameter(cmd, "@prmdecDescuento", DbType.Decimal, prmdecDescuento);
            db.AddInParameter(cmd, "@prmdecPorcentajeIGV", DbType.Decimal, prmdecPorcentajeIGV);
            db.AddInParameter(cmd, "@prmdecValorIGV", DbType.Decimal, prmdecValorIGV);
            db.AddInParameter(cmd, "@prmdecPorcentajeRenta", DbType.Decimal, prmdecPorcentajeRenta);
            db.AddInParameter(cmd, "@prmdecValorRenta", DbType.Decimal, prmdecValorRenta);
            db.AddInParameter(cmd, "@prmdecTotal", DbType.Decimal, prmdecTotal);

            db.AddInParameter(cmd, "@prmintActivo", DbType.Int32, prmintActivo);

            db.AddInParameter(cmd, "@prmstrEstado", DbType.String, prmstrEstado);
            db.AddInParameter(cmd, "@prmstrUsuario", DbType.String, prmstrUsuario);

            db.AddInParameter(cmd, "@prmstrXml", DbType.String, prmstrXml);

            try
            {
                return Convert.ToInt32(db.ExecuteScalar(cmd));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int ChangeStatusOrdenTransporte(string prmstrUsuario, int prmintUsuario, string prmstrEstado, string prmstrXml)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspMM_ChangeStatusOrdenTransporte");

            db.AddInParameter(cmd, "@prmstrEstado", DbType.String, prmstrEstado);
            db.AddInParameter(cmd, "@prmstrUsuario", DbType.String, prmstrUsuario);
            db.AddInParameter(cmd, "@prmintUsuario", DbType.Int64, prmintUsuario);
            db.AddInParameter(cmd, "@prmstrXml", DbType.String, prmstrXml);

            try
            {
                return Convert.ToInt32(db.ExecuteScalar(cmd));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetOrdenTransporte(int prmintSociedad, string prmstrEstado, DateTime prmdateFechaDesde, DateTime prmdateFechaHasta)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspMM_GetOrdenTransporte");
            db.AddInParameter(cmd, "@prmintSociedad", DbType.Int64, prmintSociedad);
            db.AddInParameter(cmd, "@prmstrEstado", DbType.String, prmstrEstado);
            db.AddInParameter(cmd, "@prmdateFechaDesde", DbType.DateTime, prmdateFechaDesde);
            db.AddInParameter(cmd, "@prmdateFechaHasta", DbType.DateTime, prmdateFechaHasta);

            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        public DataTableCollection GetOrdenTransporteById(int prmintOrdenTransporte)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspMM_GetOrdenTransporteById");
            db.AddInParameter(cmd, "@prmintOrdenTransporte", DbType.Int64, prmintOrdenTransporte);

            try
            {
                return db.ExecuteDataSet(cmd).Tables;
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        public DataTable GetEstado(int prmintEmpresa)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspMM_GetEstadoOT");
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

        public DataTable GetMotivoTranspote(int prmintEmpresa)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspMM_ListarMotivoTransporte");
            db.AddInParameter(cmd, "@paramintEmpresa", DbType.Int64, prmintEmpresa);

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