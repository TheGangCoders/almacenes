using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace WS_MM.Models
{
    public class ReservaMaterialModel
    {
        private static readonly ReservaMaterialModel _instancia = new ReservaMaterialModel();

        public static ReservaMaterialModel Instancia
        {
            get { return ReservaMaterialModel._instancia; }
        }


        public DataTable getSearchMaterialesAlmacen(int Empresa, string CodMaterial, string Material, string TipoMaterial, string GrupoArticulo, string Almacen)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspSD_getSearchMaterialesAlmacen");
            db.AddInParameter(cmd, "@prminEmpresa", DbType.Int64, Empresa);
            db.AddInParameter(cmd, "@prmstrCodMaterial", DbType.String, CodMaterial);
            db.AddInParameter(cmd, "@prmstrMaterial", DbType.String, Material);
            db.AddInParameter(cmd, "@prmstrTipoMaterial", DbType.String, TipoMaterial);
            db.AddInParameter(cmd, "@prmstrGrupoArticulo", DbType.String, GrupoArticulo);
            db.AddInParameter(cmd, "@prmintAlmacen", DbType.String, Almacen);

            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable saveUpdateReservaMaterial(int ID, string FechaReserva, string FechaPlanificada, /*string FechaSalida,*/ int idAlmacen, int idEmpresa, string XML, string Usuario)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspMM_saveUpdateReservaMaterial");
            db.AddInParameter(cmd, "@prmintReservaMaterial", DbType.Int32, ID);
            db.AddInParameter(cmd, "@prmFechaReserva", DbType.String, FechaReserva);
            db.AddInParameter(cmd, "@prmFechaPlanificada", DbType.String, FechaPlanificada);
            //db.AddInParameter(cmd, "@prmFechaSalida", DbType.String, FechaSalida);
            db.AddInParameter(cmd, "@prmintAlmacen", DbType.Int32, idAlmacen);

            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int32, idEmpresa);
            db.AddInParameter(cmd, "@pmrXML", DbType.String, XML);

            db.AddInParameter(cmd, "@prmUsuario", DbType.String, Usuario);
            db.AddInParameter(cmd, "@prmFecha", DbType.DateTime, DateTime.Now);


            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public DataTableCollection getReservaMaterial_Data(int ID, int empresa)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspMM_getReservaMaterial_Data");
            db.AddInParameter(cmd, "@prmintReserva", DbType.Int32, ID);
            db.AddInParameter(cmd, "@prminEmpresa", DbType.Int32, empresa);


            try
            {
                return db.ExecuteDataSet(cmd).Tables;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public DataTable getEstadoReservaMaterial_list()
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspMM_getEstadoReservaMaterial_list");
            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable getReservaMaterial_list(int idEmpresa, string FechaInicio, string FechaFin, string Almacen, string Estado, string usuario)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspMM_getReservaMaterial_list");
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int32, idEmpresa);
            db.AddInParameter(cmd, "@prmFechaReservaInicio", DbType.String, FechaInicio);
            db.AddInParameter(cmd, "@prmFechaReservaFin", DbType.String, FechaFin);
            db.AddInParameter(cmd, "@prmAlmacen", DbType.String, Almacen);
            db.AddInParameter(cmd, "@prmstrStado", DbType.String, Estado);
            db.AddInParameter(cmd, "@prmstrUsuario", DbType.String, usuario);
            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable enableDisable_ReservaMaterial(int ReservaMaterial, bool FutureStatus, string Usuario)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspMM_enableDisable_ReservaMaterial");
            db.AddInParameter(cmd, "@prmintReservaMaterial", DbType.Int32, ReservaMaterial);
            db.AddInParameter(cmd, "@prmFutureStatus", DbType.Boolean, FutureStatus);
            db.AddInParameter(cmd, "@prmUsuario", DbType.String, Usuario);
            db.AddInParameter(cmd, "@prmFecha", DbType.DateTime, DateTime.Now);
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