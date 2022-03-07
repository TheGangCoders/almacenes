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
    public class ClienteModel
    {
        private static readonly ClienteModel _instancia = new ClienteModel();

        public static ClienteModel Instancia
        {
            get { return ClienteModel._instancia; }
        }

        public DataTable getCategoriaCliente_list(int idEmpresa)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspSD_getCategoriaCliente_list");
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

        public DataTableCollection getCliente_data(int idCliente)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspSD_getCliente_data");
            db.AddInParameter(cmd, "@prmintCliente", DbType.Int16, idCliente);

            try
            {
                return db.ExecuteDataSet(cmd).Tables;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable getCliente_list(int idEmpresa, int categoriacliente, int status)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspSD_getCliente_list");
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int16, idEmpresa);
            db.AddInParameter(cmd, "@prmintCategoriaCliente", DbType.Int16, categoriacliente);
            if (status != -1) {
                db.AddInParameter(cmd, "@prmbitActivo", DbType.Int16, status); 
            }
            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int enable_disableCliente(int idCliente, int future_status, string usser)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspSD_enable_disableCliente");
            db.AddInParameter(cmd, "@prmintCliente", DbType.Int32, idCliente);
            db.AddInParameter(cmd, "@prmintStatus", DbType.Int32, future_status);
            db.AddInParameter(cmd, "@prmUsser", DbType.String, usser);
            db.AddInParameter(cmd, "@prmFecha", DbType.DateTime, DateTime.Now);
            try
            {
                return Convert.ToInt16(db.ExecuteScalar(cmd));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable getTipoDireccionList(int idEmpresa)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspSD_getTipoDireccionList");
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

        public DataTable getCondicionPago_list(int idEmpresa, int status)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspSD_getCondicionPago_list");
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int16, idEmpresa);
            db.AddInParameter(cmd, "@prmActivo", DbType.Int16, status);
            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable getGrupoCliente_list(int idEmpresa, int status)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspSD_getGrupoCliente_list");
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int16, idEmpresa);
            db.AddInParameter(cmd, "@prmActivo", DbType.Int16, status);
            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable getCanal_list(int idEmpresa, int status)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspSD_getCanal_list");
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int16, idEmpresa);
            db.AddInParameter(cmd, "@prmActivo", DbType.Int16, status);
            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable getSector_list(int idEmpresa, int status)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspSD_getSector_list");
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int16, idEmpresa);
            db.AddInParameter(cmd, "@prmActivo", DbType.Int16, status);
            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable RptClientes_General(string prmstrCodigo, string prmstrNombre, Boolean prmbitStatus)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspSD_RptClientes_General");
            db.AddInParameter(cmd, "@prmstrCodigo", DbType.String, prmstrCodigo);
            db.AddInParameter(cmd, "@prmstrNombre", DbType.String, prmstrNombre);
            db.AddInParameter(cmd, "@prmbitStatus", DbType.Boolean, prmbitStatus);
            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable RptClientes_Organizacion(string prmstrCodigo, string prmstrNombre, string prmstrSociedad, Boolean prmbitStatus)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspSD_RptClientes_Organizacion");
            db.AddInParameter(cmd, "@prmstrCodigo", DbType.String, prmstrCodigo);
            db.AddInParameter(cmd, "@prmstrNombre", DbType.String, prmstrNombre);
            db.AddInParameter(cmd, "@prmstrSociedad", DbType.String, prmstrSociedad);
            db.AddInParameter(cmd, "@prmbitStatus", DbType.Boolean, prmbitStatus);
            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable RptClientes_ContactosxSector(string prmstrCodigo, string prmstrNombre, string prmstrSociedad, Boolean prmbitStatus)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspSD_RptClientes_ContactosxSector");
            db.AddInParameter(cmd, "@prmstrCodigo", DbType.String, prmstrCodigo);
            db.AddInParameter(cmd, "@prmstrNombre", DbType.String, prmstrNombre);
            db.AddInParameter(cmd, "@prmstrSociedad", DbType.String, prmstrSociedad);
            db.AddInParameter(cmd, "@prmbitStatus", DbType.Boolean, prmbitStatus);
            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable RptClientes_ClientesxSector(string prmstrCodigo, string prmstrNombre, string prmstrSociedad, Boolean prmbitStatus)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspSD_RptClientes_ClientesxSector");
            db.AddInParameter(cmd, "@prmstrCodigo", DbType.String, prmstrCodigo);
            db.AddInParameter(cmd, "@prmstrNombre", DbType.String, prmstrNombre);
            db.AddInParameter(cmd, "@prmstrSociedad", DbType.String, prmstrSociedad);
            db.AddInParameter(cmd, "@prmbitStatus", DbType.Boolean, prmbitStatus);
            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }






        public DataTable getOrganizacionVentas_list(int idEmpresa, int status)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspSD_getOrganizacionVentas_list");
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int16, idEmpresa);
            db.AddInParameter(cmd, "@prmActivo", DbType.Int16, status);
            
            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int saveUpdate_Cliente(int prmintCliente, int prmintEmpresa, int prmintCategoriaCliente, string prmstrCodCliente ,
            string prmstrRazonSocial, string prmstrRazonComercial, string prmstrTipoDocumento, string prmstrNroDocumento,
            string prmstrTipoDireccion, string prmstrDireccion, string prmstrTelefono, string prmstrCelular, string prmstrEmail,
            string prmstrCuentaContable, int prmintPais, int? prmintDepartamento, int? prmintProvincia, string prmstrIdioma,
            int prmintOrgVenta, string prmstrMoneda, int prmstrCondicionPago, int prmstrGrupoCliente, 
            string prmxmlCliente_Sector, 
            string prmxmlContacto_Cliente, string prmUsser, int? unidadMedida,string prmstrObs) {

            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspSD_saveUpdate_Cliente");
            db.AddInParameter(cmd, "@prmintCliente", DbType.Int32, prmintCliente);
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int32, prmintEmpresa);
            db.AddInParameter(cmd, "@prmintCategoriaCliente", DbType.Int32, prmintCategoriaCliente);

            db.AddInParameter(cmd, "@prmstrCodCliente", DbType.String, prmstrCodCliente);
            db.AddInParameter(cmd, "@prmstrRazonSocial", DbType.String, prmstrRazonSocial);
            db.AddInParameter(cmd, "@prmstrRazonComercial", DbType.String, prmstrRazonComercial);
            db.AddInParameter(cmd, "@prmstrTipoDocumento", DbType.String, prmstrTipoDocumento);
            db.AddInParameter(cmd, "@prmstrNroDocumento", DbType.String, prmstrNroDocumento); //

            db.AddInParameter(cmd, "@prmstrTipoDireccion", DbType.String, prmstrTipoDireccion);
            db.AddInParameter(cmd, "@prmstrDireccion", DbType.String, prmstrDireccion);
            db.AddInParameter(cmd, "@prmstrTelefono", DbType.String, prmstrTelefono);
            db.AddInParameter(cmd, "@prmstrCelular", DbType.String, prmstrCelular);
            db.AddInParameter(cmd, "@prmstrEmail", DbType.String, prmstrEmail);

            db.AddInParameter(cmd, "@prmstrCuentaContable", DbType.String, prmstrCuentaContable); //
            db.AddInParameter(cmd, "@prmintPais", DbType.Int32, prmintPais);
            db.AddInParameter(cmd, "@prmintDepartamento", DbType.Int32, prmintDepartamento); //
            db.AddInParameter(cmd, "@prmintProvincia", DbType.Int32, prmintProvincia);  //
            db.AddInParameter(cmd, "@prmstrIdioma", DbType.String, prmstrIdioma);

            db.AddInParameter(cmd, "@prmintOrgVenta", DbType.Int32, prmintOrgVenta);
            db.AddInParameter(cmd, "@prmstrMoneda", DbType.String, prmstrMoneda);
            db.AddInParameter(cmd, "@prmstrCondicionPago", DbType.Int32, prmstrCondicionPago);
            db.AddInParameter(cmd, "@prmstrGrupoCliente", DbType.Int32, prmstrGrupoCliente);

            db.AddInParameter(cmd, "@prmxmlCliente_Sector", DbType.String, prmxmlCliente_Sector);
            db.AddInParameter(cmd, "@prmxmlContacto_Cliente", DbType.String, prmxmlContacto_Cliente);
            db.AddInParameter(cmd, "@prmUsser", DbType.String, prmUsser);
            db.AddInParameter(cmd, "@prmintUnidadMedida", DbType.Int32, unidadMedida);
            db.AddInParameter(cmd, "@prmstrObs", DbType.String, prmstrObs);
            


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