using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Net;
using HtmlAgilityPack;

namespace WS_PP.Models
{
    public class IngresoMateriaModelDAL
    {
        private static readonly IngresoMateriaModelDAL _instancia = new IngresoMateriaModelDAL();

        public static IngresoMateriaModelDAL Instancia
        {
            get { return IngresoMateriaModelDAL._instancia; }
        }
        #region MATERIAS PRIMAS
        public DataTable getIngresoMaterias_List(string fechaInicio, string fechaFin, int origen, int destino, string prmstrEstado)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspPP_getIngresoMaterias_List");
            db.AddInParameter(cmd, "@prmstrFechaInicio", DbType.String, fechaInicio);
            db.AddInParameter(cmd, "@prmstrFechaFin", DbType.String, fechaFin);
            db.AddInParameter(cmd, "@prmstrOrigen", DbType.Int16, origen);
            db.AddInParameter(cmd, "@prmstrDestino", DbType.Int16, destino);
            db.AddInParameter(cmd, "@prmstrEstado", DbType.String, prmstrEstado);
            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int enableDisable_IngresoMateria(int idGuia,int idMigo, Boolean bitStatus, string Status, string ussername)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspPP_enableDisable_IngresoMateria");
            db.AddInParameter(cmd, "@prmintGuia", DbType.Int32, idGuia);
            db.AddInParameter(cmd, "@prmintMigo", DbType.Int32, idMigo);
            db.AddInParameter(cmd, "@prmbitStatus", DbType.Boolean, bitStatus);
            db.AddInParameter(cmd, "@prmstrEstado", DbType.String, Status);
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
        #endregion


        #region MATERIALES
        public DataTable getIngresoMateriales_List(string fechaInicio, string fechaFin, string prmstrEstado, int prmintEmpresa)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspPP_getIngresoMateriales_List");
            db.AddInParameter(cmd, "@prmstrFechaInicio", DbType.String, fechaInicio);
            db.AddInParameter(cmd, "@prmstrFechaFin", DbType.String, fechaFin);
            db.AddInParameter(cmd, "@prmstrEstado", DbType.String, prmstrEstado);
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.String, prmintEmpresa);
            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int enableDisable_IngresoMateriales(int idOrden, int idMigo, Boolean bitStatus, string Status, string ussername)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspPP_enableDisable_IngresoMateriales");
            db.AddInParameter(cmd, "@prmintOrden", DbType.Int32, idOrden);
            db.AddInParameter(cmd, "@prmintMigo", DbType.Int32, idMigo);
            db.AddInParameter(cmd, "@prmbitStatus", DbType.Boolean, bitStatus);
            db.AddInParameter(cmd, "@prmstrEstado", DbType.String, Status);
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
        #endregion




    }
}