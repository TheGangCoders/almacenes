using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System;
using System.Configuration;
using System.Data;
using System.Data.Common;

namespace WS_IT.Models
{
    public class PaisModelDAL
    {
        private static readonly PaisModelDAL _instancia = new PaisModelDAL();

        public static PaisModelDAL Instancia
        {
            get { return PaisModelDAL._instancia; }
        }

        public DataTable Listar(int prmintActivo)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspIT_ListarPais");
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

        public DataTable getDepartamentos_list(int pais, int prmintActivo)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspIT_getDepartamento_list");
            db.AddInParameter(cmd, "@prmintPais", DbType.Int32, pais);
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


        public DataTable getProvincia_list(int departamento,int prmintActivo)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspIT_getProvincia_list");
            db.AddInParameter(cmd, "@prmintDepartamento", DbType.Int32, departamento);
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

        public DataTable getVereda_list(int provincia, int prmintActivo)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspIT_getVereda_list");
            db.AddInParameter(cmd, "@prmintProvincia", DbType.Int32, provincia);
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