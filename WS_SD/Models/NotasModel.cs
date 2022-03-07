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
    public class NotasModel
    {
        private static readonly NotasModel _instancia = new NotasModel();

        public static NotasModel Instancia
        {
            get { return NotasModel._instancia; }
        }

        public DataTable getClaseNotas(int idEmpresa)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("[uspSD_getClaseNotas]");
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

        public DataTable getListNotas(int idEmpresa, int idSociedad, int ClaseNota, string Estado, string FechaInicio, string FechaFin)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("[uspSD_getListNotas]");
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int16, idEmpresa);
            db.AddInParameter(cmd, "@prmintSociedad", DbType.Int16, idSociedad);
            db.AddInParameter(cmd, "@prmintClaseNota", DbType.Int16, ClaseNota);
            db.AddInParameter(cmd, "@prmstrEstado", DbType.String, Estado);
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

        public DataTable getListadoFacturaVenta(int idEmpresa, int idSociedad, string ClaseFactura, string CodigoFactura, string FechaInicio, string FechaFin)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("[uspSD_getListadoFacturaVenta]");
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int16, idEmpresa);
            db.AddInParameter(cmd, "@prmintSociedad", DbType.Int16, idSociedad);
            db.AddInParameter(cmd, "@prmintClaseFactura", DbType.String, ClaseFactura);
            db.AddInParameter(cmd, "@prmstrCodigoFactura", DbType.String, CodigoFactura);
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

        public DataTable getDetallesFacturas(string Detalles)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("[uspSD_getDetallesFacturas]");
            db.AddInParameter(cmd, "@prmstrXmlDetalles", DbType.String, Detalles);           

            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int SaveUpdateNotaFactura(String usuario, String xml, String xmlMateriales)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("[uspSD_SaveUpdateNotaFactura]");
            db.AddInParameter(cmd, "@prmstrUsuario", DbType.String, usuario);
            db.AddInParameter(cmd, "@prmstrXml", DbType.String, xml);
            db.AddInParameter(cmd, "@prmstrXmlMateriales", DbType.String, xmlMateriales);
            try
            {
                return Convert.ToInt16(db.ExecuteScalar(cmd));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable getDatosNotaFactura(int NotaFactura)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("[uspSD_getDatosNotaFactura]");
            db.AddInParameter(cmd, "@prmintNotaFactura", DbType.Int32, NotaFactura);

            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int postUpdateAnularNotaFactura(int Documento, String Usuario, String Estado)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("[uspSD_postUpdateAnularNotaFactura]");
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

        







    }
}