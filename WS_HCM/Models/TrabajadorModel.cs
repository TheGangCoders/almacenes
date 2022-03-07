using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace WS_HCM.Models
{
    public class TrabajadorModel
    {
        private static readonly TrabajadorModel _instancia = new TrabajadorModel();

        public static TrabajadorModel Instancia
        {
            get { return TrabajadorModel._instancia; }
        }

        public DataTable GetTrabajadorByFilters(int prmintSociedad, int prmintActivo, int prmintEmpresa)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("[uspHCM_GetTrabajadorByFilters]");
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int32, prmintEmpresa);
            db.AddInParameter(cmd, "@prmintSociedad", DbType.Int32, prmintSociedad);
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

        public DataTableCollection GetTrabajadorById(string prmstrTrabajador)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("[uspHCM_GetTrabajadorById]");
            db.AddInParameter(cmd, "@prmstrTrabajador", DbType.String, prmstrTrabajador);
            try
            {
                return db.ExecuteDataSet(cmd).Tables;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int SaveUpdate_Trabajador(int prmintOpcion, string idTrabajador, int prmintSociedad, string prmstrDocIdentidad, string prmstrNombres,
            string prmstrApePaterno, string prmstrApeMaterno, string prmstrCorreo, string prmstrTelefono, string prmstrDireccion, DateTime prmdateFechaNacimiento,
            DateTime prmdateFechaIngreso, DateTime prmdateFechaCese, string prmstrGenero, int prmintRoot, string xml, string ussername)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspHCM_SaveUpdate_Trabajador");
            db.AddInParameter(cmd, "@prmintOpcion", DbType.Int32, prmintOpcion);
            db.AddInParameter(cmd, "@prmstrTrabajador", DbType.String, idTrabajador);
            db.AddInParameter(cmd, "@prmintSociedad", DbType.Int32, prmintSociedad);
            db.AddInParameter(cmd, "@prmstrDocIdentidad", DbType.String, prmstrDocIdentidad);
            db.AddInParameter(cmd, "@prmstrNombres", DbType.String, prmstrNombres);
            db.AddInParameter(cmd, "@prmstrApePaterno", DbType.String, prmstrApePaterno);
            db.AddInParameter(cmd, "@prmstrApeMaterno", DbType.String, prmstrApeMaterno);
            db.AddInParameter(cmd, "@prmstrCorreo", DbType.String, prmstrCorreo);
            db.AddInParameter(cmd, "@prmstrTelefono", DbType.String, prmstrTelefono);
            db.AddInParameter(cmd, "@prmstrDireccion", DbType.String, prmstrDireccion);
            db.AddInParameter(cmd, "@prmdateFechaNacimiento", DbType.DateTime, prmdateFechaNacimiento);
            db.AddInParameter(cmd, "@prmdateFechaIngreso", DbType.DateTime, prmdateFechaIngreso);
            db.AddInParameter(cmd, "@prmdateFechaCese", DbType.DateTime, prmdateFechaCese);
            db.AddInParameter(cmd, "@prmstrGenero", DbType.String, prmstrGenero);
            db.AddInParameter(cmd, "@prmintRoot", DbType.Int32, prmintRoot);
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

        public int ChangeStatus_Trabajador(string prmstrTrabajador, Boolean prmbitActivo, string prmstrUsuario)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("[uspHCM_ChangeStatusTrabajador]");
            db.AddInParameter(cmd, "@prmstrTrabajador", DbType.String, prmstrTrabajador);
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