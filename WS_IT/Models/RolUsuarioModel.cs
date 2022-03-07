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
    public class RolUsuarioModel
    {
        private static readonly RolUsuarioModel _instancia = new RolUsuarioModel();

        public static RolUsuarioModel Instancia
        {
            get { return RolUsuarioModel._instancia; }
        }

        public DataTable Guardar(int prmintRolUsuario, int prmintEmpresa, int prmintUsuario, int prmintRol, string prmstrLogin)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspIT_GuardarRolUsuario");
            db.AddInParameter(cmd, "@prmintRolUsuario", DbType.Int32, prmintRolUsuario);
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int32, prmintEmpresa);
            db.AddInParameter(cmd, "@prmintUsuario", DbType.Int32, prmintUsuario);
            db.AddInParameter(cmd, "@prmintRol", DbType.Int32, prmintRol);
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

        public DataTable ListarPorUsuario(int prmintUsuario, int prmintEmpresa)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspIT_ListarRolUsuarioPorUsuario");
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
    }
}