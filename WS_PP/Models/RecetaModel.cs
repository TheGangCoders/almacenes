using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace WS_PP.Models
{
    public class RecetaModel
    {
        private static readonly RecetaModel _instancia = new RecetaModel();

        public static RecetaModel Instancia
        {
            get { return RecetaModel._instancia; }
        }

        public DataTable GetRecetas(int prmintEmpresa, int? prmintCentro, int? prmintNave, int? prmintActivo)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspPP_getRecetas");
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

        public DataTable GetRecetaById(int prmintEmpresa, int prmintReceta)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspPP_getRecetaById");
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int64, prmintEmpresa);
            db.AddInParameter(cmd, "@prmintReceta", DbType.Int64, prmintReceta);

            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int SaveUpdateReceta(String usuario, String xml, String prmstrXmlRecetaMaterial, String prmstrXmlRecetaRecursoActividad)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspPP_saveUpdateReceta");
            db.AddInParameter(cmd, "@prmstrUsuario", DbType.String, usuario);
            db.AddInParameter(cmd, "@prmstrXml", DbType.String, xml);
            db.AddInParameter(cmd, "@prmstrXmlRecetaMaterial", DbType.String, prmstrXmlRecetaMaterial);
            db.AddInParameter(cmd, "@prmstrXmlRecetaRecursoActividad", DbType.String, prmstrXmlRecetaRecursoActividad);

            try
            {
                return Convert.ToInt32(db.ExecuteScalar(cmd));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int EnableDisableReceta(string prmstrUsuario, int prmintReceta, Boolean prmbitActivo)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspPP_enableDisableReceta");
            db.AddInParameter(cmd, "@prmintReceta", DbType.Int32, prmintReceta);
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

        
        public DataTable GetValidacionUnidadMedidaEquivalencia(int prmintEmpresa, int prmintMaterial, int prminUnidadMedida)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspPP_GetValidacionUnidadMedidaEquivalencia");
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int64, prmintEmpresa);
            db.AddInParameter(cmd, "@prmintMaterial", DbType.Int64, prmintMaterial);
            db.AddInParameter(cmd, "@prmintUnidadMedida", DbType.Int64, prminUnidadMedida);

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