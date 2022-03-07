using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Shared.Util;
using System;
using System.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using WS_IT.DTOs;
using Shared.Token;

namespace WS_IT.Models
{
    public class SeguridadModel
    {
        private static readonly SeguridadModel _instancia = new SeguridadModel();

        public static SeguridadModel Instancia
        {
            get { return SeguridadModel._instancia; }
        }

        public DataTable ValidarUsuario(string prmstrLogin, string prmstrPassword)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("usp_ValidarUsuario");
            db.AddInParameter(cmd, "@prmstrLogin", DbType.String, prmstrLogin);
            db.AddInParameter(cmd, "@prmstrPassword", DbType.String, prmstrPassword);
            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ValueJWT AutenticarUsuario(/*string prmstrDominio,*/ string prmstrLogin, string prmstrPassword)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspIT_AutenticarUsuarioWeb");
            //db.AddInParameter(cmd, "@prmstrDominio", DbType.String, prmstrDominio);
            db.AddInParameter(cmd, "@prmstrLogin", DbType.String, prmstrLogin);
            db.AddInParameter(cmd, "@prmstrPassword", DbType.String, prmstrPassword);
            try
            {
                List<ValueJWT> listValueJWT = MetodoComun.ConvertDataTableToList<ValueJWT>(db.ExecuteDataSet(cmd).Tables[0]);
                if (listValueJWT.Count > 0)
                {
                    return listValueJWT[0];
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        

        public DataTable ObtenerPermisos(int prmprmintUserId)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("usp_ObtenerAccesosUsuario");
            db.AddInParameter(cmd, "@prmintUserId", DbType.String, prmprmintUserId);
            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable getLogeoUsuario(string prmstrUsuario, string prmstrcontrasena)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspIT_LogeoUsuario");
            db.AddInParameter(cmd, "@prmstrUsuario", DbType.String, prmstrUsuario);
            db.AddInParameter(cmd, "@prmstrcontrasena", DbType.String, prmstrcontrasena);
            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public UsuarioSimpleDTO ValidarUsuarioPorDominioEmailLogin(string prmstrDominio, string prmstrEmail, string prmstrLogin)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspIT_ValidarUsuarioPorDominioEmailLogin");
            db.AddInParameter(cmd, "@prmstrDominio", DbType.String, prmstrDominio);
            db.AddInParameter(cmd, "@prmstrEmail", DbType.String, prmstrEmail);
            db.AddInParameter(cmd, "@prmstrLogin", DbType.String, prmstrLogin);
            try
            {
                List<UsuarioSimpleDTO> listUsuario = MetodoComun.ConvertDataTableToList<UsuarioSimpleDTO>(db.ExecuteDataSet(cmd).Tables[0]);
                if (listUsuario.Count > 0)
                {
                    return listUsuario[0];
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int updatePassword(int idUsuario, string password, string prmstrUsuario) {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspIT_updatePassword");
            db.AddInParameter(cmd, "@prmintUsuario", DbType.String, idUsuario);
            db.AddInParameter(cmd, "@prmstrPassword", DbType.String, password);
            db.AddInParameter(cmd, "@prmstrUsuario", DbType.String, prmstrUsuario);
            db.AddInParameter(cmd, "@prmdateFecha", DbType.DateTime, DateTime.Now);
            try
            {
                return Convert.ToInt32(db.ExecuteScalar(cmd));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ResponseSimpleDTO ActualizarPassword(int prmintIdEmpresa, int prmintIdUsuario, string prmstrPasswordActual, string prmstrPasswordNueva, string prmstrTipoRegistro, string prmstrLoginSesion)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspIT_ActualizarPassword");
            db.AddInParameter(cmd, "@prmintIdEmpresa", DbType.Int32, prmintIdEmpresa);
            db.AddInParameter(cmd, "@prmintIdUsuario", DbType.Int32, prmintIdUsuario);
            db.AddInParameter(cmd, "@prmstrPasswordActual", DbType.String, prmstrPasswordActual);
            db.AddInParameter(cmd, "@prmstrPasswordNueva", DbType.String, prmstrPasswordNueva);
            db.AddInParameter(cmd, "@prmstrTipoRegistro", DbType.String, prmstrTipoRegistro);
            db.AddInParameter(cmd, "@prmstrLoginSesion", DbType.String, prmstrLoginSesion);
            try
            {
                List<ResponseSimpleDTO> listResponse = MetodoComun.ConvertDataTableToList<ResponseSimpleDTO>(db.ExecuteDataSet(cmd).Tables[0]);
                if (listResponse.Count > 0)
                {
                    return listResponse[0];
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ResponseSimpleDTO ValidarPassword(int prmintUsuario, int prmintEmpresa, int prmintUsuarioSeleccionado, string prmstrPassword)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspIT_ValidarPassword");
            db.AddInParameter(cmd, "@prmintUsuario", DbType.Int32, prmintUsuario);
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int32, prmintEmpresa);
            db.AddInParameter(cmd, "@prmintUsuarioSeleccionado", DbType.Int32, prmintUsuarioSeleccionado);
            db.AddInParameter(cmd, "@prmstrPassword", DbType.String, prmstrPassword);
            try
            {
                List<ResponseSimpleDTO> listResponse = MetodoComun.ConvertDataTableToList<ResponseSimpleDTO>(db.ExecuteDataSet(cmd).Tables[0]);
                if (listResponse.Count > 0)
                {
                    return listResponse[0];
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ResponseSimpleDTO ValidarPasswordNueva(int prmintIdUsuario, string prmstrPasswordNuevaOriginal, string prmstrPasswordNueva)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspIT_ValidarPasswordNueva");
            db.AddInParameter(cmd, "@prmintIdUsuario", DbType.String, prmintIdUsuario);
            db.AddInParameter(cmd, "@prmstrPasswordNuevaOriginal", DbType.String, prmstrPasswordNuevaOriginal);
            db.AddInParameter(cmd, "@prmstrPasswordNueva", DbType.String, prmstrPasswordNueva);   
            try
            {
                List<ResponseSimpleDTO> listResponse = MetodoComun.ConvertDataTableToList<ResponseSimpleDTO>(db.ExecuteDataSet(cmd).Tables[0]);
                if (listResponse.Count > 0)
                {
                    return listResponse[0];
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public int verifyPassword(int idusuario, string pass) {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspIT_verifyPassword");
            db.AddInParameter(cmd, "@prmintUsuario", DbType.String, idusuario);
            db.AddInParameter(cmd, "@prmstrPassword", DbType.String, pass);
            try
            {
                return Convert.ToInt32(db.ExecuteScalar(cmd));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}