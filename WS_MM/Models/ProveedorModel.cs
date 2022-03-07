using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace WS_MM.Models
{
    public class ProveedorModel
    {
        private static readonly ProveedorModel _instancia = new ProveedorModel();

        public static ProveedorModel Instancia
        {
            get { return ProveedorModel._instancia; }
        }

        public DataTable GetProveedores(int prmintEmpresa, string prmstrGrupoProveedor, Boolean? prmbitActivo)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspMM_GetProveedores");
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int64, prmintEmpresa);
            db.AddInParameter(cmd, "@prmstrGrupoProveedor", DbType.String, prmstrGrupoProveedor);
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

        public DataTable Guardar(int prmintProveedor, int prmintEmpresa, string prmstrCodProveedor, int prmintGrupoProveedor, string prmstrGrupoCuenta, 
                                int prmintPais, string prmstrCondicionBusqueda, string prmstrRazonSocial, string prmstrNombreComercial,
                                string prmstrTipoDocumento, string prmstrDocumento, string prmstrDireccion, string prmstrTelefono1,
                                string prmstrTelefono2, string prmstrTelefono3, string prmstrCelular, string prmstrEmail,
                                string prmstrObservacion, string prmstrFax, bool prmbitActivo, string prmstrLogin)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspMM_GuardarProveedor");
            db.AddInParameter(cmd, "@prmintProveedor", DbType.Int32, prmintProveedor);
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int32, prmintEmpresa);
            db.AddInParameter(cmd, "@prmstrCodProveedor", DbType.String, prmstrCodProveedor);
            db.AddInParameter(cmd, "@prmintGrupoProveedor", DbType.Int32, prmintGrupoProveedor);
            db.AddInParameter(cmd, "@prmstrGrupoCuenta", DbType.String, prmstrGrupoCuenta);
            db.AddInParameter(cmd, "@prmintPais", DbType.Int32, prmintPais);
            db.AddInParameter(cmd, "@prmstrCondicionBusqueda", DbType.String, prmstrCondicionBusqueda);
            db.AddInParameter(cmd, "@prmstrRazonSocial", DbType.String, prmstrRazonSocial);
            db.AddInParameter(cmd, "@prmstrNombreComercial", DbType.String, prmstrNombreComercial);
            db.AddInParameter(cmd, "@prmstrTipoDocumento", DbType.String, prmstrTipoDocumento);
            db.AddInParameter(cmd, "@prmstrDocumento", DbType.String, prmstrDocumento);
            db.AddInParameter(cmd, "@prmstrDireccion", DbType.String, prmstrDireccion);
            db.AddInParameter(cmd, "@prmstrTelefono1", DbType.String, prmstrTelefono1);
            db.AddInParameter(cmd, "@prmstrTelefono2", DbType.String, prmstrTelefono2);
            db.AddInParameter(cmd, "@prmstrTelefono3", DbType.String, prmstrTelefono3);
            db.AddInParameter(cmd, "@prmstrCelular", DbType.String, prmstrCelular);
            db.AddInParameter(cmd, "@prmstrEmail", DbType.String, prmstrEmail);
            db.AddInParameter(cmd, "@prmstrObservacion", DbType.String, prmstrObservacion);
            db.AddInParameter(cmd, "@prmstrFax", DbType.String, prmstrFax);
            db.AddInParameter(cmd, "@prmbitActivo", DbType.Boolean, prmbitActivo);
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

        public DataTable ActualizarActivo(int prmintProveedor, int prmintEmpresa, bool prmbitActivo, string prmstrLogin)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspMM_ActualizarActivoProveedor");
            db.AddInParameter(cmd, "@prmintProveedor", DbType.Int32, prmintProveedor);
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int32, prmintEmpresa);
            db.AddInParameter(cmd, "@prmbitActivo", DbType.Boolean, prmbitActivo);
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

        public DataTable Obtener(int prmintProveedor, int prmintEmpresa)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspMM_ObtenerProveedor");
            db.AddInParameter(cmd, "@prmintProveedor", DbType.Int32, prmintProveedor);
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

        public DataTable Listar(int prmintEmpresa, int prmintGrupoProveedor, int prmintActivo)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspMM_ListarProveedor");
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int32, prmintEmpresa);
            db.AddInParameter(cmd, "@prmintGrupoProveedor", DbType.Int32, prmintGrupoProveedor);
            db.AddInParameter(cmd, "@prmintActivo", DbType.Int32, prmintActivo);
            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable Buscar(int prmintEmpresa, int prmintGrupoProveedor, int prmintActivo, 
                                string prmstrCondicionBusqueda, string prmstrQuery)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspMM_BuscarProveedor");
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int32, prmintEmpresa);
            db.AddInParameter(cmd, "@prmintGrupoProveedor", DbType.Int32, prmintGrupoProveedor);
            db.AddInParameter(cmd, "@prmintActivo", DbType.Int32, prmintActivo);
            db.AddInParameter(cmd, "@prmstrCondicionBusqueda", DbType.String, prmstrCondicionBusqueda);
            db.AddInParameter(cmd, "@prmstrQuery", DbType.String, prmstrQuery);
            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable getPredioProveedor(int prmintProveedor)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspMM_getPredioProveedor");
            db.AddInParameter(cmd, "@prmintProveedor", DbType.Int32, prmintProveedor);
            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable getProveedorAcopiador_List(int prmintProveedor)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspMM_getProveedorAcopiador_List");
            db.AddInParameter(cmd, "@prmintProveedor", DbType.Int32, prmintProveedor);
            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }




        public DataTable saveUpdatePredioProveedor(int prmintProveedor, string XML, string Usser)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspMM_saveUpdatePredioProveedor");
            db.AddInParameter(cmd, "@prmintProveedor", DbType.Int32, prmintProveedor);
            db.AddInParameter(cmd, "@prmXML", DbType.String, XML);
            db.AddInParameter(cmd, "@prmUsser", DbType.String, Usser);
            db.AddInParameter(cmd, "@Fecha", DbType.DateTime, DateTime.Now);

            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable Listar_Acopiadores(int prmintEmpresa)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspMM_ListarAcopiadores");
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
        public DataTable saveUpdateProveedorAcopio(int prmintProveedor, string XML, string Usser)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspMM_saveUpdateProveedorAcopio");
            db.AddInParameter(cmd, "@prmintProveedor", DbType.Int32, prmintProveedor);
            db.AddInParameter(cmd, "@prmXML", DbType.String, XML);
            db.AddInParameter(cmd, "@prmUsser", DbType.String, Usser);
            db.AddInParameter(cmd, "@Fecha", DbType.DateTime, DateTime.Now);

            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable getTipoCertificacion_list(int prmintEmpresa)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspMM_getTipoCertificacion_list");
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
        public DataTable getProveedorCertificaciones_list(int prmintProveedor)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspMM_getProveedorCertificaciones_list");
            db.AddInParameter(cmd, "@prmintProveedor", DbType.Int32, prmintProveedor);
            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable saveUpdateProveedorCertificado(int prmintProveedor, string XML, string Usser)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspMM_saveUpdateProveedorCertificado");
            db.AddInParameter(cmd, "@prmintProveedor", DbType.Int32, prmintProveedor);
            db.AddInParameter(cmd, "@prmXML", DbType.String, XML);
            db.AddInParameter(cmd, "@prmUsser", DbType.String, Usser);
            db.AddInParameter(cmd, "@Fecha", DbType.DateTime, DateTime.Now);

            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetAgricultoresPredios(int prmintEmpresa)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspMM_GetAgricultoresPredios");
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

        public DataTable getTipoPredio_list(int prmintEmpresa, int prmintActivo)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspMM_getTipoPredio_list");
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int32, prmintEmpresa);
            db.AddInParameter(cmd, "@prmintActivo", DbType.Int32, prmintActivo);
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