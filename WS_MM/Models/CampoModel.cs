using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace WS_MM.Models
{
    public class CampoModel
    {
        private static readonly CampoModel _instancia = new CampoModel();

        public static CampoModel Instancia
        {
            get { return CampoModel._instancia; }
        }

        public DataTable GetUnidadAgricola(int prmintSociedad, Boolean? prmbitActivo)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("[uspMM_GetUnidadAgricola]");
            db.AddInParameter(cmd, "@prmintSociedad", DbType.Int64, prmintSociedad);
            db.AddInParameter(cmd, "@prmbitActivo", DbType.Boolean, prmbitActivo);
            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetModulos(int prmintSociedad, int prmintUnidadAgricola, Boolean? prmbitActivo)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("[uspMM_GetModulos]");
            db.AddInParameter(cmd, "@prmintSociedad", DbType.Int64, prmintSociedad);
            db.AddInParameter(cmd, "@prmintUnidadAgricola", DbType.Int64, prmintUnidadAgricola);
            db.AddInParameter(cmd, "@prmbitActivo", DbType.Boolean, prmbitActivo);
            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetCampos(int prmintSociedad, int prmintModulo, Boolean? prmbitActivo)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("[uspMM_GetCampos]");
            db.AddInParameter(cmd, "@prmintSociedad", DbType.Int64, prmintSociedad);
            db.AddInParameter(cmd, "@prmintModulo", DbType.Int64, prmintModulo);
            db.AddInParameter(cmd, "@prmbitActivo", DbType.Boolean, prmbitActivo);
            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ListarLotes(int prmintSociedad, int prmintCampo, Boolean? prmbitActivo)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("[uspMM_ListarLotes]");
            db.AddInParameter(cmd, "@prmintSociedad", DbType.Int64, prmintSociedad);
            db.AddInParameter(cmd, "@prmintCampo", DbType.Int64, prmintCampo);
            db.AddInParameter(cmd, "@prmbitActivo", DbType.Boolean, prmbitActivo);
            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ListarRutas(int ruta, int origen, int destino)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("[uspMM_ListarRutas]");
            db.AddInParameter(cmd, "@prmintRuta", DbType.Int32, ruta);
            db.AddInParameter(cmd, "@prmintOrigen", DbType.Int32, origen);
            db.AddInParameter(cmd, "@prmintDestino", DbType.Int32, destino);

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