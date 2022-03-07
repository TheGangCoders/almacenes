using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System;
using System.Configuration;
using System.Data;
using System.Data.Common;

namespace WS_MM.Models
{
    public class DetalleFacturaCompraModel
    {
        private static readonly DetalleFacturaCompraModel _instancia = new DetalleFacturaCompraModel();

        public static DetalleFacturaCompraModel Instancia
        {
            get { return DetalleFacturaCompraModel._instancia; }
        }

        public DataTable Obtener(int prmintEmpresa, int prmintFacturaCompra)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspMM_ObtenerDetalleFacturaCompra");
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int32, prmintEmpresa);
            db.AddInParameter(cmd, "@prmintFacturaCompra", DbType.Int32, prmintFacturaCompra);
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