using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace WS_MM.Models
{
    public class ProveedorSociedadModel
    {
        private static readonly ProveedorSociedadModel _instancia = new ProveedorSociedadModel();

        public static ProveedorSociedadModel Instancia
        {
            get { return ProveedorSociedadModel._instancia; }
        }

        public DataTable Guardar(int prmintProveedorSociedad, int prmintEmpresa, int prmintProveedor, int prmintSociedad,
                                string prmstrFormaPago, string prmstrCuentaAcreedor, string prmstrIdioma, string prmstrMoneda,
                                string prmstrLogin, string prmxmlContactos)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspMM_GuardarProveedorSociedad");
            db.AddInParameter(cmd, "@prmintProveedorSociedad", DbType.Int32, prmintProveedorSociedad);
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int32, prmintEmpresa);
            db.AddInParameter(cmd, "@prmintProveedor", DbType.Int32, prmintProveedor);
            db.AddInParameter(cmd, "@prmintSociedad", DbType.Int32, prmintSociedad);
            db.AddInParameter(cmd, "@prmstrFormaPago", DbType.String, prmstrFormaPago);
            db.AddInParameter(cmd, "@prmstrCuentaAcreedor", DbType.String, prmstrCuentaAcreedor);
            db.AddInParameter(cmd, "@prmstrIdioma", DbType.String, prmstrIdioma);
            db.AddInParameter(cmd, "@prmstrMoneda", DbType.String, prmstrMoneda);
            db.AddInParameter(cmd, "@prmstrLogin", DbType.String, prmstrLogin);
            db.AddInParameter(cmd, "@prmxmlContactos", DbType.String, prmxmlContactos);
            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable Obtener(int prmintEmpresa, int prmintProveedor, int prmintSociedad)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspMM_ObtenerProveedorSociedad");
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int32, prmintEmpresa);
            db.AddInParameter(cmd, "@prmintProveedor", DbType.Int32, prmintProveedor);
            db.AddInParameter(cmd, "@prmintSociedad", DbType.Int32, prmintSociedad);
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