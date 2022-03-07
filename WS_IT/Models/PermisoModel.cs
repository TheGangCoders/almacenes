using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Shared.Util;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web;
using WS_IT.DTOs;

namespace WS_IT.Models
{
    public class PermisoModel
    {
        private static readonly PermisoModel _instancia = new PermisoModel();
        public static PermisoModel Instancia
        {
            get { return PermisoModel._instancia; }
        }

        public DataTable Guardar(int prmintPermiso, int prmintEmpresa, int prmintOpcion, int prmintVista, int prmintAccion, int prmintRol, string prmstrLogin)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspIT_GuardarPermiso");
            db.AddInParameter(cmd, "@prmintPermiso", DbType.Int32, prmintPermiso);
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int32, prmintEmpresa);
            db.AddInParameter(cmd, "@prmintOpcion", DbType.Int32, prmintOpcion);
            db.AddInParameter(cmd, "@prmintVista", DbType.Int32, prmintVista);
            db.AddInParameter(cmd, "@prmintAccion", DbType.Int32, prmintAccion);
            db.AddInParameter(cmd, "@prmintRol", DbType.Int32, prmintRol);
            db.AddInParameter(cmd, "@prmstrLogin", DbType.String, prmstrLogin);
            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PermisoDTO> ObtenerPorIdUsuario(int prmintIdUsuario)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspIT_ObtenerPermisosPorIdUsuario");
            db.AddInParameter(cmd, "@prmintIdUsuario", DbType.Int32, prmintIdUsuario);
            try
            {
                List<PermisoDTO> listPermisos = MetodoComun.ConvertDataTableToList<PermisoDTO>(db.ExecuteDataSet(cmd).Tables[0]);
                return listPermisos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<OpcionVistaAccionDTO> ListarPorRol(int prmintRol, int prmintEmpresa)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspIT_ListarPermisoPorRol");
            db.AddInParameter(cmd, "@prmintRol", DbType.Int32, prmintRol);
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int32, prmintEmpresa);
            try
            {
                List<OpcionVistaAccionDTO> listPermisos = MetodoComun.ConvertDataTableToList<OpcionVistaAccionDTO>(db.ExecuteDataSet(cmd).Tables[0]);
                return listPermisos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}