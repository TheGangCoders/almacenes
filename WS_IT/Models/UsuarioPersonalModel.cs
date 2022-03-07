using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace WS_IT.Models
{
    public class UsuarioPersonalModel
    {
        private static readonly UsuarioPersonalModel _instancia = new UsuarioPersonalModel();

        public static UsuarioPersonalModel Instancia
        {
            get { return UsuarioPersonalModel._instancia; }
        }

        public DataTable Guardar(int prmintUsuario, string NroPersonal, int prmintEmpresa, string prmstrNombres, string prmstrLogin,
                                    string prmstrPassword, string prmstrDocIdentidad, int prmintSociedad, string prmstrApellidoPaterno, 
                                    string prmstrApellidoMaterno, string prmstrCorreo, string prmstrTelefono, string prmstrDireccion, string prmdatFechaNacimiento, 
                                    string prmdatFechaIngreso, string prmdatFechaCese, string prmstrGenero, string prmstrLoginSesion)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspIT_GuardarUsuarioPersonal");
            db.AddInParameter(cmd, "@prmintUsuario", DbType.Int32, prmintUsuario);
            db.AddInParameter(cmd, "@prmstrNroPersonal", DbType.Int32, NroPersonal);
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int32, prmintEmpresa);
            db.AddInParameter(cmd, "@prmstrNombres", DbType.String, prmstrNombres);
            db.AddInParameter(cmd, "@prmstrLogin", DbType.String, prmstrLogin);
            db.AddInParameter(cmd, "@prmstrPassword", DbType.String, prmstrPassword);
            db.AddInParameter(cmd, "@prmstrDocIdentidad", DbType.String, prmstrDocIdentidad);
            db.AddInParameter(cmd, "@prmintSociedad", DbType.Int32, prmintSociedad);
            db.AddInParameter(cmd, "@prmstrApellidoPaterno", DbType.String, prmstrApellidoPaterno);
            db.AddInParameter(cmd, "@prmstrApellidoMaterno", DbType.String, prmstrApellidoMaterno);
            db.AddInParameter(cmd, "@prmstrCorreo", DbType.String, prmstrCorreo);
            db.AddInParameter(cmd, "@prmstrTelefono", DbType.String, prmstrTelefono);
            db.AddInParameter(cmd, "@prmstrDireccion", DbType.String, prmstrDireccion);
            db.AddInParameter(cmd, "@prmdatFechaNacimiento", DbType.String, prmdatFechaNacimiento);
            db.AddInParameter(cmd, "@prmdatFechaIngreso", DbType.String, prmdatFechaIngreso);
            db.AddInParameter(cmd, "@prmdatFechaCese", DbType.String, prmdatFechaCese);
            db.AddInParameter(cmd, "@prmstrGenero", DbType.String, prmstrGenero);
            db.AddInParameter(cmd, "@prmstrLoginSesion", DbType.String, prmstrLoginSesion);
            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ActualizarAnulado(int prmintUsuario, int prmintEmpresa, bool prmbitAnulado, string prmstrLogin)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspIT_ActualizarAnuladoUsuarioPersonal");
            db.AddInParameter(cmd, "@prmintUsuario", DbType.Int32, prmintUsuario);
            //db.AddInParameter(cmd, "@prmstrNroPersonal", DbType.String, prmstrNroPersonal);
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int32, prmintEmpresa);
            db.AddInParameter(cmd, "@prmbitAnulado", DbType.Boolean, prmbitAnulado);
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

        public DataTable Obtener(int prmintUsuario, int prmintEmpresa)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspIT_ObtenerUsuarioPersonal");
            db.AddInParameter(cmd, "@prmintUsuario", DbType.Int32, prmintUsuario);
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
        public DataTable Listar(int prmintEmpresa)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspIT_ListarUsuarioPersonal");
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
        public DataTable ListarPorSociedad(int prmintSociedad, int prmintEmpresa)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspIT_ListarUsuarioPersonalPorSociedad");
            db.AddInParameter(cmd, "@prmintSociedad", DbType.Int32, prmintSociedad);
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
        public DataTable ListarUsuariosActivos(int prmintEmpresa)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspIT_ListarUsuarioPersonalActivo");
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
    }
}