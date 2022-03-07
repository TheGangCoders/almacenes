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
    public class PedidoVentaModel
    {
        private static readonly PedidoVentaModel _instancia = new PedidoVentaModel();

        public static PedidoVentaModel Instancia
        {
            get { return PedidoVentaModel._instancia; }
        }

        public DataTable getClaseVenta(int idEmpresa, int? activo, string Organizacion)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("[uspSD_getClaseDocumentoVenta]");
            db.AddInParameter(cmd, "@prmbitActivo", DbType.String, activo);
            db.AddInParameter(cmd, "@prmstrOrganizacion", DbType.String, Organizacion);
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

        public DataTable getOrganzacionVenta(int idEmpresa, int idSociedad, int? activo)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("[uspSD_getOrganizacionVenta]");
            db.AddInParameter(cmd, "@prmbitActivo", DbType.String, activo);
            db.AddInParameter(cmd, "@prmEmpresa", DbType.Int16, idEmpresa);
            db.AddInParameter(cmd, "@prmSociedad", DbType.Int16, idSociedad);

            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        
        public DataTable getListadoPedidosVenta(int idEmpresa, int Clase, int Organizacion, string Cliente, string CodigoCliente, string FechaInicio, string FechaFin)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("[uspSD_getListadoPedidosVenta]");           
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int16, idEmpresa);
            db.AddInParameter(cmd, "@prmintClase", DbType.Int16, Clase);
            db.AddInParameter(cmd, "@prmintOrganizacion", DbType.Int16, Organizacion);
            db.AddInParameter(cmd, "@prmstrCliente", DbType.String, Cliente);
            db.AddInParameter(cmd, "@prmstrCodigoCliente", DbType.String, CodigoCliente);
            db.AddInParameter(cmd, "@prmstrFechaInicio", DbType.String, FechaInicio);
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
        
        public DataTable getListadoPedidosVentaMigo(int idEmpresa, int Clase, int Organizacion, string Cliente, string CodigoCliente, string FechaInicio, string FechaFin, int Sociedad)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("[uspSD_getListadoPedidosVentaMigo]");
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int16, idEmpresa);
            db.AddInParameter(cmd, "@prmintSociedad", DbType.Int16, Sociedad);
            db.AddInParameter(cmd, "@prmintClase", DbType.Int16, Clase);
            db.AddInParameter(cmd, "@prmintOrganizacion", DbType.Int16, Organizacion);
            db.AddInParameter(cmd, "@prmstrCliente", DbType.String, Cliente);
            db.AddInParameter(cmd, "@prmstrCodigoCliente", DbType.String, CodigoCliente);
            db.AddInParameter(cmd, "@prmstrFechaInicio", DbType.String, FechaInicio);
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


        public DataTable getSectorVenta(int idEmpresa, int? activo)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("[uspSD_getSectorPedidoVenta]");
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

        public DataTable getPaises(int? activo)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("[uspSD_getPaises]");
            db.AddInParameter(cmd, "@prmintActivo", DbType.Int64, activo);

            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public DataTable getSearchClientes(int Empresa, string Cliente, string CodCliente, int? Pais, int? Organizacion, int? Sector, int Tipo)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("[uspSD_searchClientesPedidoVenta]");
            db.AddInParameter(cmd, "@prminEmpresa", DbType.Int64, Empresa);
            db.AddInParameter(cmd, "@prminOrgVentas", DbType.Int64, Organizacion);
            db.AddInParameter(cmd, "@prmintTipo", DbType.Int64, Tipo);
            db.AddInParameter(cmd, "@prminSector", DbType.Int64, Sector);
            db.AddInParameter(cmd, "@prminPais", DbType.Int64, Pais);
            db.AddInParameter(cmd, "@prmstrCliente", DbType.String, Cliente);
            db.AddInParameter(cmd, "@prmstrCodCliente", DbType.String, CodCliente);

            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public DataTable getTipoMoneda(int Empresa)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("[uspSD_getTipoMoneda]");
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int64, Empresa);

            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public DataTable getCondicionPago(int Empresa)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("[uspSD_getFormaPago]");
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int64, Empresa);

            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public DataTable getVaporPedidoVenta(int Empresa, int? Activo)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("[uspSD_getVaporPedidoVenta]");
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int64, Empresa);
            db.AddInParameter(cmd, "@prmintActivo", DbType.Int64, Activo);

            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable getTipoEnvio(int Empresa)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("[uspSD_getTipoEnvio]");
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int64, Empresa);

            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable getSearchProveedor(int Empresa, string CondicionBusqueda, string Documento, string RazonSocial, int? Pais)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("[uspSD_getSearchProveedor]");
            db.AddInParameter(cmd, "@prminEmpresa", DbType.Int64, Empresa);
            db.AddInParameter(cmd, "@prmstrCondicionBusqueda", DbType.String, CondicionBusqueda);
            db.AddInParameter(cmd, "@prmstrDocumento", DbType.String, Documento);
            db.AddInParameter(cmd, "@prmstrRazonSocial", DbType.String, RazonSocial);
            db.AddInParameter(cmd, "@prminPais", DbType.Int64, Pais);

            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable getTipoFecha(int Empresa)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("[uspSD_getTipoFecha]");
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int64, Empresa);

            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public DataTable getSearchMateriales(int Empresa, string CodMaterial, string Material, string Organizacion, string TipoMaterial, string GrupoArticulo)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("[uspSD_getSearchMateriales]");
            db.AddInParameter(cmd, "@prminEmpresa", DbType.Int64, Empresa);
            db.AddInParameter(cmd, "@prmstrCodMaterial", DbType.String, CodMaterial);
            db.AddInParameter(cmd, "@prmstrMaterial", DbType.String, Material);
            db.AddInParameter(cmd, "@prmstrOrganizacion", DbType.String, Organizacion);
            db.AddInParameter(cmd, "@prmstrTipoMaterial", DbType.String, TipoMaterial);
            db.AddInParameter(cmd, "@prmstrGrupoArticulo", DbType.String, GrupoArticulo);

            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int SaveUpdatePedidoVenta(String usuario, String xml, String xmlDPedido, String xmlPagos)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("[uspSD_GuardarPedidoVenta]");
            db.AddInParameter(cmd, "@prmstrUsuario", DbType.String, usuario);
            db.AddInParameter(cmd, "@prmstrXMLCabecera", DbType.String, xml);
            db.AddInParameter(cmd, "@prmstrXMLDetalle", DbType.String, xmlDPedido);
            db.AddInParameter(cmd, "@prmstrXMLPagos", DbType.String, xmlPagos);
            try
            {
                return Convert.ToInt32(db.ExecuteScalar(cmd));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable getDatosPedidoVenta(int PedidoVenta)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("[uspSD_getDatosPedidoVenta]");
            db.AddInParameter(cmd, "@prmintPedidoVenta", DbType.Int64, PedidoVenta);

            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int postUpdateAnularPedidoVenta(int Codigo, String Accion, String Usuario, String Estado, int Empresa)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("[uspSD_AnularCabceraDetallePedidoVenta]");
            db.AddInParameter(cmd, "@prmintCodigo", DbType.Int32, Codigo);
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int32, Empresa);
            db.AddInParameter(cmd, "@prmstrAccion", DbType.String, Accion);
            db.AddInParameter(cmd, "@prmstrUsuario", DbType.String, Usuario);
            db.AddInParameter(cmd, "@prmstrEstado", DbType.String, Estado);

            
            try
            {
                return Convert.ToInt32(db.ExecuteScalar(cmd));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetINCOTERM()
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("[uspSD_GetINCOTERM]");

            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        
        public DataTable GetSeguroVenta(int? activo, int idEmpresa)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("[uspMM_GetSeguroVenta]");
            db.AddInParameter(cmd, "@prmbitActivo", DbType.String, activo);
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int16, idEmpresa);

            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetTipoCarga(int idEmpresa)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("[uspSD_getTiposDeCargas]");
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int16, idEmpresa);

            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetTipoEmbalaje(int idEmpresa)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("[uspSD_getTiposDeEmbalaje]");
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int16, idEmpresa);

            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetListarBancos(int idEmpresa)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("[uspSD_getListarBancos]");
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int16, idEmpresa);

            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetListarCuentasInterbancarias(int Banco, string TipoMoneda)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("[uspSD_getListarCuentasInterbancarias]");
            db.AddInParameter(cmd, "@prmintBanco", DbType.Int16, Banco);
            db.AddInParameter(cmd, "@prmstrTipoMoneda", DbType.String, TipoMoneda);

            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public DataTable GetUnidadMedidaPrecio(int MaterialOrganizacionVenta, int Material, int Empresa)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("[uspSD_getUnidadMedidaPrecio]");
            db.AddInParameter(cmd, "@prmstrMaterialOrgVta", DbType.Int16, MaterialOrganizacionVenta);
            db.AddInParameter(cmd, "@prmintMaterial", DbType.Int16, Material);
            db.AddInParameter(cmd, "@prminEmpresa", DbType.Int16, Empresa);

            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetTipoFechaPago(int Empresa)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("[uspSD_getTipoFechaPago]");
            db.AddInParameter(cmd, "@prminEmpresa", DbType.Int16, Empresa);

            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public DataTableCollection GetProformaPedidoVenta(int prmintPedidoVenta, int prmintOpcIdioma)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspSD_GetProformaPedidoVenta");
            db.AddInParameter(cmd, "@prmintPedidoVenta", DbType.Int64, prmintPedidoVenta);
            db.AddInParameter(cmd, "@prmintOpcIdioma", DbType.Int64, prmintOpcIdioma);
            try
            {
                return db.ExecuteDataSet(cmd).Tables;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ObtenerPorcentajeComision(int prmintPedidoVenta, string prmstrFechaEntrega)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspSD_ObtenerPorcentajeComision");
            db.AddInParameter(cmd, "@prmintCliente", DbType.Int64, prmintPedidoVenta);
            db.AddInParameter(cmd, "@prmstrFecha", DbType.String, prmstrFechaEntrega);
            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public int SaveUpdateBorradorPedidoVenta(String usuario, String xml, String xmlDPedido, String xmlPagos)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("[uspSD_GuardarBorradorPedidoVenta]");
            db.AddInParameter(cmd, "@prmstrUsuario", DbType.String, usuario);
            db.AddInParameter(cmd, "@prmstrXMLCabecera", DbType.String, xml);
            db.AddInParameter(cmd, "@prmstrXMLDetalle", DbType.String, xmlDPedido);
            db.AddInParameter(cmd, "@prmstrXMLPagos", DbType.String, xmlPagos);
            try
            {
                return Convert.ToInt32(db.ExecuteScalar(cmd));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public DataTable GetEquivalenciaUnidadPV(int Material, int UnidadMedida, int Empresa)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("[uspSD_EquivalenciaUnidadPV]");
            db.AddInParameter(cmd, "@prmintMaterial", DbType.Int64, Material);
            db.AddInParameter(cmd, "@prmintUnidadMedida", DbType.Int64, UnidadMedida);
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.String, Empresa);
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