using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Net;

namespace WS_PP.Models
{
    public class OrdenProduccionModelDAL
    {
        private static readonly OrdenProduccionModelDAL _instancia = new OrdenProduccionModelDAL();

        public static OrdenProduccionModelDAL Instancia
        {
            get { return OrdenProduccionModelDAL._instancia; }
        } 

        public DataTable getOrdenProduccion_List(string fechaInicio, string fechaFin, string status, int tipo, int idEmpresa)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspPP_getOrdenProduccion_List");
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int16, idEmpresa);
            db.AddInParameter(cmd, "@prmstrFechaInicio", DbType.String, fechaInicio);
            db.AddInParameter(cmd, "@prmstrFechaFin", DbType.String, fechaFin);
            db.AddInParameter(cmd, "@prmstrStatus", DbType.String, status);
            db.AddInParameter(cmd, "@prmintTipo", DbType.Int32, tipo);
            
            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        
        public DataTable getEstadoOrdenProduccion(int idEmpresa)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspPP_getEstadoOrdenProduccion");
            db.AddInParameter(cmd, "@prmEmpresa", DbType.Int16, idEmpresa);

            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable getTipoOrdenProduccion(int idEmpresa)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("[uspPP_getTipoOrdenProduccion]");
            db.AddInParameter(cmd, "@prmEmpresa", DbType.Int16, idEmpresa);

            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable getVersionNave(int idEmpresa, int prmintCentro, int prmintNave, int prmintMaterial, int prmintUnidadBase)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("[uspPP_getVersionNave]");
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int16, idEmpresa);
            db.AddInParameter(cmd, "@prmintCentro", DbType.Int16, prmintCentro);
            db.AddInParameter(cmd, "@prmintNave", DbType.Int16, prmintNave);
            db.AddInParameter(cmd, "@prmintMaterial", DbType.Int16, prmintMaterial);
            db.AddInParameter(cmd, "@prmintUnidadBase", DbType.Int16, prmintUnidadBase);
            

            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable getDatosAdicionalesMaterial(int idEmpresa, int prmintCentro, int prmintMaterial)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("[uspPP_getDatosAdicionalesMaterial]");
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int16, idEmpresa);
            db.AddInParameter(cmd, "@prmintCentro", DbType.Int16, prmintCentro);
            db.AddInParameter(cmd, "@prmintMaterial", DbType.Int16, prmintMaterial);

            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        
        public DataTable getCentroBeneficioMaterial(int idEmpresa, int prmintCentro, int prmintMaterial, int prmintAlmacen)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("[uspPP_getCentroBeneficioMaterial]");
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int16, idEmpresa);
            db.AddInParameter(cmd, "@prmintCentro", DbType.Int16, prmintCentro);
            db.AddInParameter(cmd, "@prmintMaterial", DbType.Int16, prmintMaterial);
            db.AddInParameter(cmd, "@prmintAlmacen", DbType.Int16, prmintAlmacen);

            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public DataTable getPlanificador(int idEmpresa)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("[uspPP_getPlanificador]");
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

        public DataTable getDetalleListaMaterial(int ListaMaterial, float CantidadProducir,int OrdenProduccion, int Version)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("[uspPP_getDetalleListaMaterial]");
            db.AddInParameter(cmd, "@prmintListaMaterial", DbType.Int16, ListaMaterial);
            db.AddInParameter(cmd, "@prmdecProduccion", DbType.Decimal, CantidadProducir);
            db.AddInParameter(cmd, "@prmintOrdenProduccion", DbType.Int16, OrdenProduccion);
            db.AddInParameter(cmd, "@prmintVersion", DbType.Int16, Version);

            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        
        public DataTable getPosicionPedidoVenta(int Material)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("[uspPP_getPosicionPedidoVenta]");
            db.AddInParameter(cmd, "@prmstrMaterial", DbType.Int64, Material);

            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int SaveUpdateOrdenProduccion(String usuario, String xml, String xmlDOrden)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspPP_SaveUpdateOrdenProduccion");
            db.AddInParameter(cmd, "@prmstrUsuario", DbType.String, usuario);
            db.AddInParameter(cmd, "@prmstrXml", DbType.String, xml);
            db.AddInParameter(cmd, "@prmstrXmlDOrden", DbType.String, xmlDOrden);
            try
            {
                return Convert.ToInt16(db.ExecuteScalar(cmd));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        
        public DataTable getListadoOrdenProduccion(string Fecha1, string Fecha2, int Centro, int Nave, int Clase)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("[uspPP_getListadoOrdenProduccion]");
            db.AddInParameter(cmd, "@prmstrFechaInicio", DbType.String, Fecha1);
            db.AddInParameter(cmd, "@prmstrFechaFin", DbType.String, Fecha2);
            db.AddInParameter(cmd, "@prmintCentro", DbType.Int64, Centro);
            db.AddInParameter(cmd, "@prmintNave", DbType.Int64, Nave);
            db.AddInParameter(cmd, "@prmintClase", DbType.Int64, Clase);

            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetDatosOrdenProduccion(int OrdenProduccion)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("[uspPP_GetDatosOrdenProduccion]");
            db.AddInParameter(cmd, "@prmstrOrdenProduccion", DbType.Int64, OrdenProduccion);

            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        
        public int postUpdateEstadoOrdenProduccion(int OrdenProduccion, String Estado, String UsuarioRegistro)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspPP_UpdateEstadoOrdenProduccion");
            db.AddInParameter(cmd, "@prmintOrdenProduccion", DbType.Int64, OrdenProduccion);
            db.AddInParameter(cmd, "@prmstrEstado", DbType.String, Estado);
            db.AddInParameter(cmd, "@prmstrUsuarioRegistro", DbType.String, UsuarioRegistro);
            try
            {
                return Convert.ToInt16(db.ExecuteScalar(cmd));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        
        public DataTable getReporteProduccion(int prmintMaterial, int prmintCentro, int prmintAlmacen, int prmintClaseOP, string prmstrEstado, string prmstrTipoRpt, DateTime prmdatFechaInicio, DateTime prmdatFechaFin)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspPP_getReporteCabeceraProduccion");
            if (prmstrTipoRpt == "detalle") {
                cmd = db.GetStoredProcCommand("uspPP_getReporteDetalladoProduccion");
            }
            db.AddInParameter(cmd, "@prmintMaterial", DbType.Int32, prmintMaterial);
            db.AddInParameter(cmd, "@prmintCentro", DbType.Int32, prmintCentro);
            db.AddInParameter(cmd, "@prmintAlmacen", DbType.Int32, prmintAlmacen);
            db.AddInParameter(cmd, "@prmintClaseOP", DbType.Int32, prmintClaseOP);
            db.AddInParameter(cmd, "@prmstrEstado", DbType.String, prmstrEstado);
            db.AddInParameter(cmd, "@prmdatFechaInicio", DbType.DateTime, prmdatFechaInicio);
            db.AddInParameter(cmd, "@prmdatFechaFin", DbType.DateTime, prmdatFechaFin);

            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public DataTable getListadoOrdenProduccionMigo(string Fecha1, string Fecha2, int Centro, int Nave, int Clase, int Tipo, int Empresa, int Sociedad)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("[uspPP_getListadoOrdenProduccionMigo]");
            db.AddInParameter(cmd, "@prmstrFechaInicio", DbType.String, Fecha1);
            db.AddInParameter(cmd, "@prmstrFechaFin", DbType.String, Fecha2);
            db.AddInParameter(cmd, "@prmintCentro", DbType.Int64, Centro);
            db.AddInParameter(cmd, "@prmintNave", DbType.Int64, Nave);
            db.AddInParameter(cmd, "@prmintClase", DbType.Int64, Clase);
            db.AddInParameter(cmd, "@prmintTipo", DbType.Int64, Tipo);
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int64, Empresa);
            db.AddInParameter(cmd, "@prmintSociedad", DbType.Int64, Sociedad);

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