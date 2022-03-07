using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace WS_SD.Models
{
    public class GrupoClienteModel
    {
        private static readonly GrupoClienteModel _instancia = new GrupoClienteModel();

        public static GrupoClienteModel Instancia
        {
            get { return GrupoClienteModel._instancia; }
        }

        public DataTable GetGrupoCliente(int prmintEmpresa, int? prmintActivo)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspSD_GetGrupoCliente");
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int64, prmintEmpresa);
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

        public int SaveUpdate_GrupoCliente(int prmintGrupoCliente, string prmstrNombre, string prmstrDescripcion, int prmintActivo, int idEmpresa, string ussername)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspSD_SaveUpdate_GrupoCliente");
            db.AddInParameter(cmd, "@prmintGrupoCliente", DbType.Int32, prmintGrupoCliente);
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int32, idEmpresa);
            db.AddInParameter(cmd, "@prmstrNombre", DbType.String, prmstrNombre);
            db.AddInParameter(cmd, "@prmstrDescripcion", DbType.String, prmstrDescripcion);
            db.AddInParameter(cmd, "@prmintActivo", DbType.Int32, prmintActivo);
            db.AddInParameter(cmd, "@prmdateFechaRegistro", DbType.DateTime, DateTime.Now);
            db.AddInParameter(cmd, "@prmstrUsser", DbType.String, ussername);

            try
            {
                return Convert.ToInt32(db.ExecuteScalar(cmd));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Anular_GrupoCliente(int prmintGrupoCliente, string prmstrUsuario)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspSD_Anular_GrupoCliente");
            db.AddInParameter(cmd, "@prmintGrupoCliente", DbType.Int64, prmintGrupoCliente);
            db.AddInParameter(cmd, "@prmstrUser", DbType.String, prmstrUsuario);
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
    }
}