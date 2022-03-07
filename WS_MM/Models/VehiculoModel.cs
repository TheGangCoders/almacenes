using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace WS_MM.Models
{
    public class VehiculoModel
    {
        private static readonly VehiculoModel _instancia = new VehiculoModel();

        public static VehiculoModel Instancia
        {
            get { return VehiculoModel._instancia; }
        }

        public DataTable GetVehiculo(int prmintEmpresa, int? prmintActivo)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspMM_GetVehiculo");
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int64, prmintEmpresa);
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

        public int SaveUpdate_Vehiculo(int prmintVehiculo, int prmintProveedor, string prmstrPlaca, string prmstrMarca, string prmstrModelo, string prmstrCapacidad, string prmstrConstancia, int prmintActivo, int idEmpresa, string username)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspMM_SaveUpdate_Vehiculo");
            db.AddInParameter(cmd, "@prmintVehiculo", DbType.Int32, prmintVehiculo);
            db.AddInParameter(cmd, "@prmintProveedor", DbType.Int32, prmintProveedor);
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int32, idEmpresa);
            db.AddInParameter(cmd, "@prmstrPlaca", DbType.String, prmstrPlaca);
            db.AddInParameter(cmd, "@prmstrMarca", DbType.String, prmstrMarca);
            db.AddInParameter(cmd, "@prmstrModelo", DbType.String, prmstrModelo);
            db.AddInParameter(cmd, "@prmstrCapacidad", DbType.String, prmstrCapacidad);
            db.AddInParameter(cmd, "@prmstrConstancia", DbType.String, prmstrConstancia);
            db.AddInParameter(cmd, "@prmintActivo", DbType.String, prmintActivo);
            db.AddInParameter(cmd, "@prmdateFechaRegistro", DbType.DateTime, DateTime.Now);
            db.AddInParameter(cmd, "@prmstrUser", DbType.String, username);

            try
            {
                return Convert.ToInt32(db.ExecuteScalar(cmd));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Anular_Vehiculo(int prmintVehiculo, string prmstrUsuario)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspMM_AnularVehiculo");
            db.AddInParameter(cmd, "@prmintVehiculo", DbType.Int64, prmintVehiculo);
            db.AddInParameter(cmd, "@prmdateFecha", DbType.DateTime, DateTime.Now);
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