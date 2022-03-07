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
    public class LogAccesosModel
    {
        private static readonly LogAccesosModel _instancia = new LogAccesosModel();
        public static LogAccesosModel Instancia
        {
            get { return LogAccesosModel._instancia; }
        }

        public DataTable Guardar(int prmintUsuario, int prmintEmpresa, int prmintOpcion, int prmintVista, int prmintAccion, string prmstrLogin)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspIT_GuardarLogAccesos");
            db.AddInParameter(cmd, "@prmintUsuario", DbType.Int32, prmintUsuario);
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int32, prmintEmpresa);
            db.AddInParameter(cmd, "@prmintOpcion", DbType.Int32, prmintOpcion);
            db.AddInParameter(cmd, "@prmintVista", DbType.Int32, prmintVista);
            db.AddInParameter(cmd, "@prmintAccion", DbType.Int32, prmintAccion);
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
    }
}
