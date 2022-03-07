using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Net;


namespace WS_SD.Models
{
    public class FacturaVentaModel
    {
        private static readonly FacturaVentaModel _instancia = new FacturaVentaModel();

        public static FacturaVentaModel Instancia
        {
            get { return FacturaVentaModel._instancia; }
        }

        public DataTable getClaseFactura(int idEmpresa)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("[uspSD_getClaseFactura]");
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int16, idEmpresa);

            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable getEstadoFactura(int idEmpresa)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("[uspSD_getEstadoFactura]");
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int16, idEmpresa);

            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable getListadoFacturasVenta(int idEmpresa, int ClaseFactura, string EstadoFactura, string FechaInicio, string FechaFin)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("[uspSD_getListadoFacturasVenta]");
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int16, idEmpresa);
            db.AddInParameter(cmd, "@prmintClaseFactura", DbType.Int16, ClaseFactura);
            db.AddInParameter(cmd, "@prmstrEstadoFactura", DbType.String, EstadoFactura);
            db.AddInParameter(cmd, "@prmstrFechaInicio", DbType.String, FechaInicio);
            db.AddInParameter(cmd, "@prmstrFechaFin", DbType.String, FechaFin);

            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public DataTable getListadoDespachos_Factura(int Empresa, int Sociedad, int TipoSalida, string NumeroDocEntrega, string Cliente, string CodCliente, string FechaIncio, string FechaFin, int ClaseFactura)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("[uspSD_getListadoDespachosFacturaVenta]");
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int16, Empresa);
            db.AddInParameter(cmd, "@prmintSociedad", DbType.Int16, Sociedad);
            db.AddInParameter(cmd, "@prmintTipoSalida", DbType.Int16, TipoSalida);
            db.AddInParameter(cmd, "@prmstrNumeroDocEntrega", DbType.String, NumeroDocEntrega);
            db.AddInParameter(cmd, "@prmstrCliente", DbType.String, Cliente);
            db.AddInParameter(cmd, "@prmstrCodCliente", DbType.String, CodCliente);
            db.AddInParameter(cmd, "@prmstrFechaIncio", DbType.String, FechaIncio);
            db.AddInParameter(cmd, "@prmstrFechaFin", DbType.String, FechaFin);
            db.AddInParameter(cmd, "@prmintClaseFactura", DbType.Int16, ClaseFactura);            

            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable getResponsablesPago(int Empresa, int OrganizacionVenta, int Sector, int Cliente)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("[uspSD_getResponsablesPago]");
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int16, Empresa);
            db.AddInParameter(cmd, "@prmintCliente", DbType.Int16, Cliente);
            db.AddInParameter(cmd, "@prmintOrganizacionVenta", DbType.Int16, OrganizacionVenta);
            db.AddInParameter(cmd, "@prmintSector", DbType.Int16, Sector);

            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable getDetallesDespachoFactura(string Detalles)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("[uspSD_getDetallesDespachoFactura]");
            db.AddInParameter(cmd, "@prmstrXml", DbType.String, Detalles);

            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public int SaveUpdateFacturaVenta(String usuario, String xml, String xmlDetalle, String xmlLotes)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("[uspSD_SaveUpdateFacturaVenta]");
            db.AddInParameter(cmd, "@prmstrUsuario", DbType.String, usuario);
            db.AddInParameter(cmd, "@prmstrXml", DbType.String, xml);
            db.AddInParameter(cmd, "@prmstrXmlDetalle", DbType.String, xmlDetalle);
            db.AddInParameter(cmd, "@prmstrXmlLotes", DbType.String, xmlLotes);
            try
            {
                return Convert.ToInt16(db.ExecuteScalar(cmd));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetDatosFacturaVenta(int FacturaVenta)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("[uspSD_getDatosFacturaVenta]");
            db.AddInParameter(cmd, "@prmintFacturaVenta", DbType.Int64, FacturaVenta);

            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int postUpdateAnularFacturaVenta(int Documento, String Usuario, String Estado)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("[uspSD_PostUpdateAnularFacturaVenta]");
            db.AddInParameter(cmd, "@prmintDocumento", DbType.Int64, Documento);
            db.AddInParameter(cmd, "@prmstrUsuario", DbType.String, Usuario);
            db.AddInParameter(cmd, "@prmstrEstado", DbType.String, Estado);
            try
            {
                return Convert.ToInt16(db.ExecuteScalar(cmd));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public DataTableCollection GetPdfFacturaVenta(int prmintFacturaVenta, int prmintOpcIdioma)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspSD_GetPdfFacturaVenta");
            db.AddInParameter(cmd, "@prmintFacturaVenta", DbType.Int64, prmintFacturaVenta);
            db.AddInParameter(cmd, "@prmintOpcIdioma", DbType.Int64, prmintOpcIdioma);
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