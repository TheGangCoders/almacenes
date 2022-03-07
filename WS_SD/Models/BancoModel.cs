using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace WS_SD.Models
{
    public class BancoModel
    {
        private static readonly BancoModel _instancia = new BancoModel();

        public static BancoModel Instancia
        {
            get { return BancoModel._instancia; }
        }

        public DataTable GetBanco(int prmintEmpresa, int? prmintActivo)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspSD_GetBanco");
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

        public DataTableCollection GetBancoById(int prmintBanco)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspSD_GetBancoById");
            db.AddInParameter(cmd, "@prmintBanco", DbType.Int64, prmintBanco);

            try
            {
                return db.ExecuteDataSet(cmd).Tables;
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        public int SaveUpdate_Banco(int prmintBanco, int prmintSociedad, string prmstrClaveBanco, string prmstrNombreBanco, string prmstrSwiftCode, string prmstrDireccion, int prmintDefecto, int prmintActivo, int idEmpresa, string xml, string username)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("[uspSD_SaveUpdate_Banco]");
            db.AddInParameter(cmd, "@prmintBanco", DbType.Int32, prmintBanco);
            db.AddInParameter(cmd, "@prmintSociedad", DbType.Int64, prmintSociedad);
            db.AddInParameter(cmd, "@prmstrClaveBanco", DbType.String, prmstrClaveBanco);
            db.AddInParameter(cmd, "@prmstrNombreBanco", DbType.String, prmstrNombreBanco);
            db.AddInParameter(cmd, "@prmstrSwiftCode", DbType.String, prmstrSwiftCode);
            db.AddInParameter(cmd, "@prmstrDireccion", DbType.String, prmstrDireccion);
            db.AddInParameter(cmd, "@prmintActivo", DbType.Int32, prmintActivo);
            db.AddInParameter(cmd, "@prmintDefecto", DbType.Int32, prmintDefecto);
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int32, idEmpresa);
            db.AddInParameter(cmd, "@prmXML", DbType.String, xml);
            db.AddInParameter(cmd, "@prmdateFechaRegistro", DbType.DateTime, DateTime.Now);
            db.AddInParameter(cmd, "@prmstrUser", DbType.String, username);

            try
            {
                return Convert.ToInt32(db.ExecuteScalar(cmd));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Anular_Banco(int prmintBanco, string prmstrUsuario)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspSD_Anular_Banco");
            db.AddInParameter(cmd, "@prmintBanco", DbType.Int64, prmintBanco);
            db.AddInParameter(cmd, "@prmdateFecha", DbType.DateTime, DateTime.Now);
            db.AddInParameter(cmd, "@prmstrUsuario", DbType.String, prmstrUsuario);

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