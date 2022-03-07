using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace WS_MM.Models
{
    public class PuntoAcopioModel
    {
        private static readonly PuntoAcopioModel _instancia = new PuntoAcopioModel();

        public static PuntoAcopioModel Instancia
        {
            get { return PuntoAcopioModel._instancia; }
        }

        public DataTable GetPuntoAcopio(int prmintEmpresa, int prmintCentro, Boolean? prmbitActivo)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspMM_GetPuntosAcopio");
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int64, prmintEmpresa);
            db.AddInParameter(cmd, "@prmintCentro", DbType.Int64, prmintCentro);
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
    }
}