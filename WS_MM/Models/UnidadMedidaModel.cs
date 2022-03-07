using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace WS_MM.Models
{
    public class UnidadMedidaModel
    {
        private static readonly UnidadMedidaModel _instancia = new UnidadMedidaModel();

        public static UnidadMedidaModel Instancia
        {
            get { return UnidadMedidaModel._instancia; }
        }

        public DataTable GetUnidadMedida(int prmintEmpresa, Boolean? prmbitActivo)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspMM_GetUnidadMedida");
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int64, prmintEmpresa);
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

        public DataTable GetUnidadMedidaByFilters(int prmintTipoUnidadMedida, int prmintActivo, int prmintEmpresa)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("[uspMM_GetUnidadMedidaByFilters]");
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int32, prmintEmpresa);
            db.AddInParameter(cmd, "@prmintTipoUnidadMedida", DbType.Int32, prmintTipoUnidadMedida);
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

        public DataTable GetTipoUnidad(int prmintEmpresa)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("[uspMM_GetTipoUnidad]");
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

        public DataTable GetUMedida(int prmintUnidadMedida,int prmintEmpresa)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("[uspMM_GetUMedida]");
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int32, prmintEmpresa);
            db.AddInParameter(cmd, "@prmintUnidadMedida", DbType.Int32, prmintUnidadMedida);

            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int ChangeStatus_UnidadMedida(int prmintUnidadMedida, Boolean prmbitActivo, string prmstrUsuario)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("[uspMM_ChangeStatusUnidadMedida]");
            db.AddInParameter(cmd, "@prmintUnidadMedida", DbType.Int32, prmintUnidadMedida);
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

        public DataTableCollection GetUnidadMedidaById(int prmintUnidadMedida)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("[uspMM_GetUnidadMedidaById]");
            db.AddInParameter(cmd, "@prmintUnidadMedida", DbType.Int32, prmintUnidadMedida);
            try
            {
                return db.ExecuteDataSet(cmd).Tables;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int SaveUpdate_UnidadMedida(int prmintUnidadMedida, string prmstrNombre, string prmstrNombreIngles, string prmstrAbreviatura, int prmintTipoUnidad, int prmintActivo, int idEmpresa, string xml, string ussername)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("[uspMM_SaveUpdate_UnidadMedida]");
            db.AddInParameter(cmd, "@prmintUnidadMedida", DbType.Int32, prmintUnidadMedida);
            db.AddInParameter(cmd, "@prmstrNombre", DbType.String, prmstrNombre);
            db.AddInParameter(cmd, "@prmstrNombreIngles", DbType.String, prmstrNombreIngles);
            db.AddInParameter(cmd, "@prmstrAbreviatura", DbType.String, prmstrAbreviatura);
            db.AddInParameter(cmd, "@prmintTipoUnidad", DbType.Int32, prmintTipoUnidad);
            db.AddInParameter(cmd, "@prmintActivo", DbType.Int32, prmintActivo);
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int32, idEmpresa);
            db.AddInParameter(cmd, "@prmXML", DbType.String, xml);
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
    }
}