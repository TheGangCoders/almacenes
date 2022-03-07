using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Net;


namespace WS_SD.Models
{
    public class DespachoModel
    {
        private static readonly DespachoModel _instancia = new DespachoModel();

        public static DespachoModel Instancia
        {
            get { return DespachoModel._instancia; }
        }

        public DataTable getTipoSalidaDespacho(int idEmpresa, int? activo)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("[uspSD_getTipoSalidaDespacho]");
            db.AddInParameter(cmd, "@prmbitActivo", DbType.String, activo);
            db.AddInParameter(cmd, "@prmEmpresa", DbType.Int16, idEmpresa);
            

            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable getTipoSalidaFactura(int idEmpresa, int? activo, int ClaseFactura)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("[uspSD_getTipoSalidaFactura]");
            db.AddInParameter(cmd, "@prmbitActivo", DbType.String, activo);
            db.AddInParameter(cmd, "@prmEmpresa", DbType.Int16, idEmpresa);
            db.AddInParameter(cmd, "@prmintClaseFactura", DbType.Int16, ClaseFactura);


            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable getListadoDespachos(int Empresa, int Sociedad, int TipoSalida, string NumeroDocEntrega, string Cliente, string CodCliente, string FechaIncio, string FechaFin)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("[uspSD_getListadoDespachos]");
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int16, Empresa);
            db.AddInParameter(cmd, "@prmintSociedad", DbType.Int16, Sociedad);
            db.AddInParameter(cmd, "@prmintTipoSalida", DbType.Int16, TipoSalida);
            db.AddInParameter(cmd, "@prmstrNumeroDocEntrega", DbType.String, NumeroDocEntrega);
            db.AddInParameter(cmd, "@prmstrCliente", DbType.String, Cliente);
            db.AddInParameter(cmd, "@prmstrCodCliente", DbType.String, CodCliente);
            db.AddInParameter(cmd, "@prmstrFechaIncio", DbType.String, FechaIncio);
            db.AddInParameter(cmd, "@prmstrFechaFin", DbType.String, FechaFin);

            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable getPosicionesPedidoVenta(string PedidoVenta, int Empresa, int Sociedad)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("[uspSD_getPosicionesPedidoVenta]");
            db.AddInParameter(cmd, "@prmstrPedidoVenta", DbType.String, PedidoVenta);
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int32, Empresa);
            db.AddInParameter(cmd, "@prmintSociedad", DbType.Int32, Sociedad);

            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable getValidarPedidoVentaDespacho(string PedidoVenta, string FechaEntrega, int InicioPosicion, int FinPosicion, int Empresa, int Sociedad)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("[uspSD_getValidarPedidoVentaDespacho]");
            db.AddInParameter(cmd, "@prmstrPedidoVenta", DbType.String, PedidoVenta);
            db.AddInParameter(cmd, "@prmstrFecha", DbType.String, FechaEntrega);
            db.AddInParameter(cmd, "@prmintInicioPosicion", DbType.Int64, InicioPosicion);
            db.AddInParameter(cmd, "@prmintFinPosicion", DbType.Int64, FinPosicion);
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int64, Empresa);
            db.AddInParameter(cmd, "@prmintSociedad", DbType.Int64, Sociedad);

            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ObtenerMaterialesPedido(int PedidoVenta, int Busqueda, string Posiciones)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("[uspSD_ObtenerMaterialesPedido]");
            db.AddInParameter(cmd, "@prmintPedidoVenta", DbType.Int64, PedidoVenta);
            db.AddInParameter(cmd, "@prmintBusqueda", DbType.Int64, Busqueda);
            db.AddInParameter(cmd, "@prmstrPosiciones", DbType.String, Posiciones);

            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
                    
        public DataTable ValidarLoteStockMaterial(int Material, int Almacen, string Lote)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("[uspSD_ValidarLoteStockMaterial]");
            db.AddInParameter(cmd, "@prmintMaterial", DbType.Int64, Material);
            db.AddInParameter(cmd, "@prmintAlmacen", DbType.Int64, Almacen);
            db.AddInParameter(cmd, "@prmstrLote", DbType.String, Lote);

            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public DataTable getEstadoDespacho()
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("[uspSD_getEstadoDespacho]");
            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public int SaveUpdateDespachoEntrega(String usuario, String xmlDespacho, String xmlDDespacho, String xmlDDespachoLote)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("[uspSD_SaveUpdateDespachoPedido]");
            db.AddInParameter(cmd, "@prmstrUsuario", DbType.String, usuario);
            db.AddInParameter(cmd, "@prmstrXmlDespacho", DbType.String, xmlDespacho);
            db.AddInParameter(cmd, "@prmstrXmlDDespacho", DbType.String, xmlDDespacho);
            db.AddInParameter(cmd, "@prmstrXmlDDespachoLotes", DbType.String, xmlDDespachoLote);
            try
            {
                return Convert.ToInt16(db.ExecuteScalar(cmd));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public DataTable getDatosDespachoEntrega(int DespachoEntrega)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("[uspSD_getDatosDespachoEntrega]");
            db.AddInParameter(cmd, "@prmintDespachoEntrega", DbType.Int16, DespachoEntrega);
            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public DataTable getDatosPedidoVentaPosiciones(int PedidoVenta)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("[uspSD_getDatosPedidoVentaPosiciones]");
            db.AddInParameter(cmd, "@prmintPedidoVenta", DbType.Int16, PedidoVenta);
            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable getListarLotesAlmacenDespacho(int Material, int Almacen)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("[uspSD_ListarLotesAlmacenDespacho]");
            db.AddInParameter(cmd, "@prmintMaterial", DbType.Int16, Material);
            db.AddInParameter(cmd, "@prmintAlmacen", DbType.Int16, Almacen);
            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable getListarOrigenDestinoDespacho(int Tipo, int Empresa)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("[uspSD_ListarOrigenDestinoDespacho]");
            db.AddInParameter(cmd, "@prmintTipo", DbType.Int16, Tipo);
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int16, Empresa);
            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetRptDespacho(int prmintSociedad, int prmintTipoMaterial, int prmintGrupoArticulo, int prmintMaterial,
   int prmintCliente, int prmintOrgVta, string prmstrEstado, DateTime prmdateFechaInicio, DateTime prmdateFechaFin)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspSD_ReporteDespachoLotes");
            db.AddInParameter(cmd, "@prmintSociedad", DbType.Int32, prmintSociedad);
            db.AddInParameter(cmd, "@prmintTipoMaterial", DbType.Int32, prmintTipoMaterial);
            db.AddInParameter(cmd, "@prmintGrupoArticulo", DbType.Int32, prmintGrupoArticulo);
            db.AddInParameter(cmd, "@prmintMaterial", DbType.Int32, prmintMaterial);
            db.AddInParameter(cmd, "@prmintCliente", DbType.Int32, prmintCliente);
            db.AddInParameter(cmd, "@prmintOrgVentas", DbType.Int32, prmintOrgVta);
            db.AddInParameter(cmd, "@prmstrEstado", DbType.String, prmstrEstado);
            db.AddInParameter(cmd, "@prmdatFechaInicio", DbType.DateTime, prmdateFechaInicio);
            db.AddInParameter(cmd, "@prmdatFechaFin", DbType.DateTime, prmdateFechaFin);

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