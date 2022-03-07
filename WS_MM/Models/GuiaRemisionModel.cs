using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace WS_MM.Models
{
    public class GuiaRemisionModel
    {
        private static readonly GuiaRemisionModel _instancia = new GuiaRemisionModel();

        public static GuiaRemisionModel Instancia
        {
            get { return GuiaRemisionModel._instancia; }
        }

        public DataTable GetSociedades(int prmintEmpresa, Boolean? prmbitActivo)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("[uspMM_getSociedad]");
            db.AddInParameter(cmd, "@prmEmpresa", DbType.Int64, prmintEmpresa);
            db.AddInParameter(cmd, "@prmbitActivo", DbType.Boolean, prmbitActivo);
            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetGuiaRemision(string prmbstrFechaInicio, string prmbstrFechaFin, int prmbintOrigen, int prmbintDestino, int prmintEmpresa, int prmintSociedad)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("[uspMM_GetGuiaRemision]");
            db.AddInParameter(cmd, "@prmbstrFechaInicio", DbType.String, prmbstrFechaInicio);
            db.AddInParameter(cmd, "@prmbstrFechaFin", DbType.String, prmbstrFechaFin);
            db.AddInParameter(cmd, "@prmbintOrigen", DbType.Int64, prmbintOrigen);
            db.AddInParameter(cmd, "@prmbintDestino", DbType.Int64, prmbintDestino);
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int64, prmintEmpresa);
            db.AddInParameter(cmd, "@prmintSociedad", DbType.Int64, prmintSociedad);
            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int SaveUpdateGuiaRemision(String usuario, String xml, String xmlDGuia)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspMM_SaveUpdateGuiaRemision");
            db.AddInParameter(cmd, "@prmstrUsuario", DbType.String, usuario);
            db.AddInParameter(cmd, "@prmstrXml", DbType.String, xml);
            db.AddInParameter(cmd, "@prmstrXmlDGuia", DbType.String, xmlDGuia);
            try
            {
                return Convert.ToInt16(db.ExecuteScalar(cmd));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetCabeceraDetalleGuia(int prmintGuia)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("[uspMM_GetCabeceraDetalleGuia]");
            db.AddInParameter(cmd, "@prmintGuia", DbType.Int64, prmintGuia);
            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public int EnableDisable_GuiaRemision(int GuiaRemision, Boolean Activo, String Usuario)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspMM_EnableDisable_GuiaRemision");
            db.AddInParameter(cmd, "@prmintGuia", DbType.Int64, GuiaRemision);
            db.AddInParameter(cmd, "@prmbitEstado", DbType.Boolean, Activo);
            db.AddInParameter(cmd, "@prmstrUsuarioRegistro", DbType.String, Usuario);
            try
            {
                return Convert.ToInt16(db.ExecuteScalar(cmd));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public DataTable GetOrdenCompraGuia(string FechaInicio, string FechaFin, int Proveedor)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("[uspMM_GetOrdenCompraGuia]");
            db.AddInParameter(cmd, "@prmstrFechaInicio", DbType.String, FechaInicio);
            db.AddInParameter(cmd, "@prmstrFechaFin", DbType.String, FechaFin);
            db.AddInParameter(cmd, "@prmintProveedor", DbType.Int64, Proveedor);
            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ObtenerDespacho()
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("[uspMM_ObtenerDespacho]");
            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public DataTable ObtenerClaseTranspote()
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("[uspMM_ObtenerClaseTranspote]");
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