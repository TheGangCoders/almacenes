using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System;
using System.Configuration;
using System.Data;
using System.Data.Common;

namespace WS_MM.Models
{
    public class TipoDocumentoModel
    {
        private static readonly TipoDocumentoModel _instancia = new TipoDocumentoModel();

        public static TipoDocumentoModel Instancia
        {
            get { return TipoDocumentoModel._instancia; }
        }

        public DataTable getTipoDocumento(int prmintEmpresa)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspMM_getTipoDocumento");
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

    }
}