using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Configuration;
using System.Data.Common;

namespace WS_IT.Models
{
    public class MultitablaModel
    {
        private static readonly MultitablaModel _instancia = new MultitablaModel();

        public static MultitablaModel Instancia
        {
            get { return MultitablaModel._instancia; }
        }
        public DataTable GetMultitabla(int prmintEmpresa)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspIT_GetMultitabla");
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

        public DataTable GetModulos()
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspIT_GetModulos");
            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTableCollection GetDetailMultitabla(string prmstrTabla)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspIT_GetDetailMultitabla");
            db.AddInParameter(cmd, "@prmstrTabla", DbType.String, prmstrTabla);
            try
            {
                return db.ExecuteDataSet(cmd).Tables;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int SaveUpdate_Multitabla(int prmintOpcion, string idTabla, string prmstrModulo, /*string newidTabla, */int idEmpresa, string Nombre, string Descripcion,string xml, 
            string ussername)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspIT_SaveUpdate_Multitabla");
            db.AddInParameter(cmd, "@prmintOpcion", DbType.Int32, prmintOpcion);
            db.AddInParameter(cmd, "@prmstrTabla", DbType.String, idTabla);
            db.AddInParameter(cmd, "@prmstrModulo", DbType.String, prmstrModulo);
            //db.AddInParameter(cmd, "@prmstrNewTabla", DbType.String, newidTabla);
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int32, idEmpresa);
            db.AddInParameter(cmd, "@prmstrNombre", DbType.String, Nombre);
            db.AddInParameter(cmd, "@prmstrDescripcion", DbType.String, Descripcion);
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

        public int ChangeStatus_Multitabla(string prmstrTabla, Boolean prmbitActivo, string prmstrUsuario)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("[uspIT_ChangeStatusMultitabla]");
            db.AddInParameter(cmd, "@prmstrTabla", DbType.String, prmstrTabla);
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

        public DataTable GetMultitablaByStatus(int prmintEmpresa, int prmintActivo)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("[uspIT_GetMultitablaByStatus]");
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