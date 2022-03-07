using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace WS_MM.Models
{
    public class CampanyaModel
    {
        private static readonly CampanyaModel _instancia = new CampanyaModel();

        public static CampanyaModel Instancia
        {
            get { return CampanyaModel._instancia; }
        }

        public DataTable getFundoLst(string empresa, string status)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspAGR_getFundoLst");
            db.AddInParameter(cmd, "@prmStatus", DbType.String, status);
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.String, empresa);
            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable getModuloByFundo(string fundo, string status)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspAGR_getModuloByFundo");
            db.AddInParameter(cmd, "@prmintFundo", DbType.String, fundo);
            db.AddInParameter(cmd, "@prmStatus", DbType.String, status);
            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable getTurnoByModulo(string modulo, string status)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspAGR_getTurnoByModulo");
            db.AddInParameter(cmd, "@prmintModulo", DbType.String, modulo);
            db.AddInParameter(cmd, "@prmStatus", DbType.String, status);
            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable getCampanya_list(string status, string fundo, string modulo, string turno, string empresa)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspAGR_getCampanya_list");
            db.AddInParameter(cmd, "@prmStatus", DbType.String, status);
            db.AddInParameter(cmd, "@prmFundo", DbType.String, fundo);
            db.AddInParameter(cmd, "@prmModulo", DbType.String, modulo);
            db.AddInParameter(cmd, "@prmTurno", DbType.String, turno);
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.String, empresa);
            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable saveUpdateCampanya(int ID, int Turno, string codigo, string inicioCamp, string finCamp, string inicioCose, 
            string finCose,int EMPRESA, string USER)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspAGR_saveUpdateCampanya");
            db.AddInParameter(cmd, "@prmCampanya", DbType.String, ID);
            db.AddInParameter(cmd, "@prmTurno", DbType.String, Turno);
            db.AddInParameter(cmd, "@prmCodigo", DbType.String, codigo);
            db.AddInParameter(cmd, "@prmInicioCampanya", DbType.String, inicioCamp);
            db.AddInParameter(cmd, "@prmFinCampanya", DbType.String, finCamp);
            db.AddInParameter(cmd, "@prmInicioCosecha", DbType.String, inicioCose);
            db.AddInParameter(cmd, "@prmFinCosecha", DbType.String, finCose);
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.String, EMPRESA);
            db.AddInParameter(cmd, "@prmUsuario", DbType.String, USER);
            db.AddInParameter(cmd, "@prmFecha", DbType.DateTime, DateTime.Now);

            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable enableDisableCampanya(int ID, bool future_status, string USER)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspAGR_enableDisableCampanya");
            db.AddInParameter(cmd, "@prmCampanya", DbType.String, ID);
            db.AddInParameter(cmd, "@prmStatus", DbType.Boolean, future_status);
            db.AddInParameter(cmd, "@prmUsuario", DbType.String, USER);
            db.AddInParameter(cmd, "@prmFecha", DbType.DateTime, DateTime.Now);

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


