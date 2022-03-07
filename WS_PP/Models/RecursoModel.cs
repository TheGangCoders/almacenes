using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace WS_PP.Models
{
    public class RecursoModel
    {
        private static readonly RecursoModel _instancia = new RecursoModel();

        public static RecursoModel Instancia
        {
            get { return RecursoModel._instancia; }
        }

        public DataTable GetRecursos(int prmintEmpresa, int? prmintCentro, int? prmintNave, int? prmintActivo)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspPP_getRecursos");
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int64, prmintEmpresa);            
            db.AddInParameter(cmd, "@prmintCentro", DbType.Int64, prmintCentro);
            db.AddInParameter(cmd, "@prmintNave", DbType.Int64, prmintNave);
            db.AddInParameter(cmd, "@prmbitActivo", DbType.Int32, prmintActivo);
            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int SaveUpdateRecurso(String usuario, String xml, String prmstrXmlRecursoNave, String prmstrXmlRecursoActividad)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspPP_saveUpdateRecurso");
            db.AddInParameter(cmd, "@prmstrUsuario", DbType.String, usuario);
            db.AddInParameter(cmd, "@prmstrXml", DbType.String, xml);
            db.AddInParameter(cmd, "@prmstrXmlRecursoNave", DbType.String, prmstrXmlRecursoNave);
            db.AddInParameter(cmd, "@prmstrXmlRecursoActividad", DbType.String, prmstrXmlRecursoActividad);

            try
            {
                return Convert.ToInt32(db.ExecuteScalar(cmd));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetRecursoById(int prmintEmpresa, int prmintRecurso)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspPP_getRecursoById");
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int64, prmintEmpresa);
            db.AddInParameter(cmd, "@prmintRecurso", DbType.Int64, prmintRecurso);           
            
            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int EnableDisableRecurso(string prmstrUsuario, int prmintRecurso, Boolean prmbitActivo)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspPP_enableDisableRecurso");
            db.AddInParameter(cmd, "@prmintRecurso", DbType.Int32, prmintRecurso);
            db.AddInParameter(cmd, "@prmbitActivo", DbType.Boolean, prmbitActivo);
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