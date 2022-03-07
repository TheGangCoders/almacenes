using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace WS_MM.Models
{
    public class GrupoArticuloModel
    {
        private static readonly GrupoArticuloModel _instancia = new GrupoArticuloModel();

        public static GrupoArticuloModel Instancia
        {
            get { return GrupoArticuloModel._instancia; }
        }

        public DataTable GetGrupoArticulo(int prmEmpresa, Boolean? status)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspMM_getGrupoArticulo");
            db.AddInParameter(cmd, "@prmEmpresa", DbType.Int64, prmEmpresa);
            db.AddInParameter(cmd, "@prmActivo", DbType.Boolean, status);
            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable Guardar(int prmintGrupoArticulo, int prmintEmpresa, string prmstrCodigo, string prmstrNombre,
                                string prmstrDescripcion, string Descripcion_ENG, string prmstrAbreviatura, bool prmbitActivo, string prmstrLogin)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspMM_GuardarGrupoArticulo");
            db.AddInParameter(cmd, "@prmintGrupoArticulo", DbType.Int32, prmintGrupoArticulo);
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int32, prmintEmpresa);
            db.AddInParameter(cmd, "@prmstrCodigo", DbType.String, prmstrCodigo);
            db.AddInParameter(cmd, "@prmstrNombre", DbType.String, prmstrNombre);
            db.AddInParameter(cmd, "@prmstrDescripcion", DbType.String, prmstrDescripcion);
            db.AddInParameter(cmd, "@prmstrDescripcion_ENG", DbType.String, Descripcion_ENG);
            db.AddInParameter(cmd, "@prmstrAbreviatura", DbType.String, prmstrAbreviatura);
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

        public DataTable ActualizarActivo(int prmintGrupoArticulo, int prmintEmpresa, bool prmbitActivo, string prmstrLogin)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspMM_ActualizarActivoGrupoArticulo");
            db.AddInParameter(cmd, "@prmintGrupoArticulo", DbType.Int32, prmintGrupoArticulo);
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

        public DataTable Obtener(int prmintGrupoArticulo, int prmintEmpresa)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspMM_ObtenerGrupoArticulo");
            db.AddInParameter(cmd, "@prmintGrupoArticulo", DbType.Int32, prmintGrupoArticulo);
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
        public DataTable Listar(int prmintEmpresa, int prmintActivo)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspMM_ListarGrupoArticulo");
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