using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace WS_PP.Models
{
    public class InstructivoProduccionModel
    {
        private static readonly InstructivoProduccionModel _instancia = new InstructivoProduccionModel();

        public static InstructivoProduccionModel Instancia
        {
            get { return InstructivoProduccionModel._instancia; }
        }

        public DataTable GetInstructivosProduccion(int prmintEmpresa, int prmintCentro, int prmintNave, string prmstrMetodoProduccion, int prmintClaseOrden,
                string prmstrEstado, int prmintTipoFecha, string prmstrFechaInicio, string prmstrFechaFin)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspPP_getInstructivosProduccion");
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int32, prmintEmpresa);
            db.AddInParameter(cmd, "@prmintCentro", DbType.Int32, prmintCentro);
            db.AddInParameter(cmd, "@prmintNave", DbType.Int32, prmintNave);
            db.AddInParameter(cmd, "@prmstrMetodoProduccion", DbType.String, prmstrMetodoProduccion);
            db.AddInParameter(cmd, "@prmintClaseOrden", DbType.Int32, prmintClaseOrden);
            db.AddInParameter(cmd, "@prmstrEstado", DbType.String, prmstrEstado);
            db.AddInParameter(cmd, "@prmintTipoFecha", DbType.Int32, prmintTipoFecha);
            db.AddInParameter(cmd, "@prmdatFechaInicio", DbType.Date, prmstrFechaInicio);
            db.AddInParameter(cmd, "@prmdatFechaFin", DbType.Date, prmstrFechaFin);

            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public DataTable GetEstadosInstructivoProduccion(int prmintEmpresa)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspPP_getEstadosInstructivoProduccion");
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

        public DataTable GetLineasProduccion(int prmintEmpresa, int prmintNave)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspPP_getLineasProduccion");
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int32, prmintEmpresa);
            db.AddInParameter(cmd, "@prmintNave", DbType.Int32, prmintNave);

            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetBuscarMaterialStock(int prmintEmpresa, int prmintTipoMaterial, string prmstrCodigoMaterial, string prmstrDescripcionMaterial)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspPP_getBuscarMaterialStock");
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int32, prmintEmpresa);
            db.AddInParameter(cmd, "@prmintTipoMaterial", DbType.Int32, prmintTipoMaterial);
            db.AddInParameter(cmd, "@prmstrCodigoMaterial", DbType.String, prmstrCodigoMaterial);
            db.AddInParameter(cmd, "@prmstrDescripcionMaterial", DbType.String, prmstrDescripcionMaterial);

            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetBuscarPedidoVenta(int prmintEmpresa)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspPP_getBuscarPedidoVenta");
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

        public DataTable GetBuscarOrdenMaquila(int prmintEmpresa)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspPP_getBuscarOrdenMaquila");
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

        public int SaveUpdateInstructivoProduccion(String usuario, String xml, String xmlLanzadoProduccion, String xmlNotificacionProduccion, String xmlDistribucionProduccion)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspPP_SaveUpdateInstructivoProduccion");
            db.AddInParameter(cmd, "@prmstrUsuario", DbType.String, usuario);
            db.AddInParameter(cmd, "@prmstrXml", DbType.String, xml);
            db.AddInParameter(cmd, "@prmstrXmlLanzadoProduccion", DbType.String, xmlLanzadoProduccion);
            db.AddInParameter(cmd, "@prmstrXmlNotificacionProduccion", DbType.String, xmlNotificacionProduccion);
            db.AddInParameter(cmd, "@prmstrXmlDistribucionProduccion", DbType.String, xmlDistribucionProduccion);

            cmd.CommandTimeout = 300;

            try
            {
                return Convert.ToInt32(db.ExecuteScalar(cmd));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetMaterialAlmacenFromInstructivo(int prmintEmpresa, int? prmintCentro, int? prmintMaterial)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspPP_getMaterialAlmacenFromInstructivo");
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int32, prmintEmpresa);
            db.AddInParameter(cmd, "@prmintCentro", DbType.Int32, prmintCentro);
            db.AddInParameter(cmd, "@prmintMaterial", DbType.Int32, prmintMaterial);

            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTableCollection GetDatosInstructivoProduccion(int prmintInstructivoProduccion)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("spPP_GetDatosInstructivoProduccion");
            db.AddInParameter(cmd, "@prmintInstructivoProduccion", DbType.Int32, prmintInstructivoProduccion);

            try
            {
                return db.ExecuteDataSet(cmd).Tables;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetInstructivosProduccionForConsumoMateriales(int prmintEmpresa, int? prmintCentro, int? prmintNave, string prmstrFecha)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspPP_getInstructivosProduccionForConsumoMateriales");
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int32, prmintEmpresa);
            db.AddInParameter(cmd, "@prmintCentro", DbType.Int32, prmintCentro);
            db.AddInParameter(cmd, "@prmintNave", DbType.Int32, prmintNave);
            db.AddInParameter(cmd, "@prmstrFecha", DbType.Date, prmstrFecha);

            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetConsumoMaterialesByInstructivo(int? prmintInstructivoProduccion)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspPP_getConsumoMaterialesByInstructivo");
            db.AddInParameter(cmd, "@prmintInstructivoProduccion", DbType.Int32, prmintInstructivoProduccion);

            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetPreDistribucionConsumoMateriaPrima(int prmintEmpresa, int prmintCentro, int prmintNave, string prmstrXmlLanzado,
            string prmstrXmlNotificacionProduccion, string prmstrXmlDistribucionProduccion)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspPP_GetPreDistribucionConsumoMateriaPrima");
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int32, prmintEmpresa);
            db.AddInParameter(cmd, "@prmintCentro", DbType.Int32, prmintCentro);
            db.AddInParameter(cmd, "@prmintNave", DbType.Int32, prmintNave);
            db.AddInParameter(cmd, "@prmstrXmlLanzado", DbType.String, prmstrXmlLanzado);
            db.AddInParameter(cmd, "@prmstrXmlNotificacionProduccion", DbType.String, prmstrXmlNotificacionProduccion);
            db.AddInParameter(cmd, "@prmstrXmlDistribucionProduccion", DbType.String, prmstrXmlDistribucionProduccion);

            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int SaveUpdateConsumoDistribucionLanzado(String usuario, String xml, String xmlLanzadoProduccion, String xmlNotificacionProduccion, String xmlDistribucionProduccion)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspPP_SaveUpdateConsumoDistribucionLanzado");
            db.AddInParameter(cmd, "@prmstrUsuario", DbType.String, usuario);
            db.AddInParameter(cmd, "@prmstrXml", DbType.String, xml);
            db.AddInParameter(cmd, "@prmstrXmlLanzadoProduccion", DbType.String, xmlLanzadoProduccion);
            db.AddInParameter(cmd, "@prmstrXmlNotificacionProduccion", DbType.String, xmlNotificacionProduccion);
            db.AddInParameter(cmd, "@prmstrXmlDistribucionProduccion", DbType.String, xmlDistribucionProduccion);

            cmd.CommandTimeout = 300;

            try
            {
                return Convert.ToInt32(db.ExecuteScalar(cmd));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int updateEstadoOrdenProduccion(int InstructivoProduccion, String Estado, String UsuarioRegistro)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspPP_UpdateEstadoInstructivoProduccion");
            db.AddInParameter(cmd, "@prmintInstructivoProduccion", DbType.Int64, InstructivoProduccion);
            db.AddInParameter(cmd, "@prmstrEstado", DbType.String, Estado);
            db.AddInParameter(cmd, "@prmstrUsuarioRegistro", DbType.String, UsuarioRegistro);

            cmd.CommandTimeout = 300;

            try
            {
                return Convert.ToInt32(db.ExecuteScalar(cmd));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTableCollection GetLanzadoNotificacionByOrdenCompra(int prmintOrdenCompra)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspPP_GetLanzadoNotificacionByOrdenCompra");
            db.AddInParameter(cmd, "@prmintOrdenCompra", DbType.Int32, prmintOrdenCompra);

            try
            {
                return db.ExecuteDataSet(cmd).Tables;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int CerrarInstructivoProduccion(String usuario, String xml, String xmlLanzadoProduccion, String xmlNotificacionProduccion, String xmlDistribucionProduccion)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspPP_CerrarInstructivoProduccion");
            db.AddInParameter(cmd, "@prmstrUsuario", DbType.String, usuario);
            db.AddInParameter(cmd, "@prmstrXml", DbType.String, xml);
            db.AddInParameter(cmd, "@prmstrXmlLanzadoProduccion", DbType.String, xmlLanzadoProduccion);
            db.AddInParameter(cmd, "@prmstrXmlNotificacionProduccion", DbType.String, xmlNotificacionProduccion);
            db.AddInParameter(cmd, "@prmstrXmlDistribucionProduccion", DbType.String, xmlDistribucionProduccion);

            cmd.CommandTimeout = 300;

            try
            {
                return Convert.ToInt32(db.ExecuteScalar(cmd));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetProveedoresList()
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspIT_GetProveedoresList");

            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetPredioList(int prmIntProveedor)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspIT_GetPredioList");
            db.AddInParameter(cmd, "@prmIntProveedor", DbType.Int16, prmIntProveedor);
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