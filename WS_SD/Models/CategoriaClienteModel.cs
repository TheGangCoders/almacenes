using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace WS_SD.Models
{
    public class CategoriaClienteModel
    {
        private static readonly CategoriaClienteModel _instancia = new CategoriaClienteModel();

        public static CategoriaClienteModel Instancia
        {
            get { return CategoriaClienteModel._instancia; }
        }

        public DataTable GetCategoriaCliente(int prmintEmpresa, int? prmintActivo)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspSD_GetCategoriaCliente");
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

        public int SaveUpdate_CategoriaCliente(int prmintCategoriaCliente, string prmstrCodigo, string prmstrDescripcion, int prmintActivo, int idEmpresa, string ussername)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspSD_SaveUpdate_CategoriaCliente");
            db.AddInParameter(cmd, "@prmintCategoriaCliente", DbType.Int32, prmintCategoriaCliente);
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int32, idEmpresa);
            db.AddInParameter(cmd, "@prmstrCodigo", DbType.String, prmstrCodigo);
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

        public int Anular_CategoriaCliente(int prmintCategoriaCliente, string prmstrUsuario)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspSD_Anular_CategoriaCliente");
            db.AddInParameter(cmd, "@prmintCategoriaCliente", DbType.Int64, prmintCategoriaCliente);
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