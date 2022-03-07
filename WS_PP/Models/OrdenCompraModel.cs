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
    public class OrdenCompraModelDAL
    {
        private static readonly OrdenCompraModelDAL _instancia = new OrdenCompraModelDAL();

        public static OrdenCompraModelDAL Instancia
        {
            get { return OrdenCompraModelDAL._instancia; }
        }

        public DataTable getFromMultitabla(string tabla, int idEmpresa)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspPP_getFromMultitabla");
            db.AddInParameter(cmd, "@prmstrTabla", DbType.String, tabla);
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

        public DataTable getFormaValorizacion(int idEmpresa)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspMM_getFormaValorizacion");
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
        
        public DataTable getEstadoOrdenCompra(int prmintEmpresa)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspMM_getEstadoOrdenCompra");
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
        

        public DataTable getOrdenCompra_List(string fechaInicio, string fechaFin, string status)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspPP_getOrdenCompra_List");
            db.AddInParameter(cmd, "@prmstrFechaInicio", DbType.String, fechaInicio);
            db.AddInParameter(cmd, "@prmstrFechaFin", DbType.String, fechaFin);
            db.AddInParameter(cmd, "@prmstrStatus", DbType.String, status);
            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }        

        public DataTable GetOrdenCompraListMigo(string fechaInicio, string fechaFin, int Empresa, int Sociedad)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspPP_GetOrdenCompraListMigo");
            db.AddInParameter(cmd, "@prmstrFechaInicio", DbType.String, fechaInicio);
            db.AddInParameter(cmd, "@prmstrFechaFin", DbType.String, fechaFin);
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

        public DataTableCollection getOrdenCompra_Data(int idOrdenCompra)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspPP_getOrdenCompra_Data");
            db.AddInParameter(cmd, "@prmintOrdenCompra", DbType.Int32, idOrdenCompra);
            try
            {
                return db.ExecuteDataSet(cmd).Tables;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int saveUpdate_OrdenCompra(int idOrden, int idSociedad, int idEmpresa, int idProveedor, Boolean IncluyeIgv, Boolean IncluyeRenta,Boolean Maquila, string codigo, string Valorizacion,
            string tipoDocumento, string formaPago, string fechaEmision, string fechaEntrega, string tipoMoneda, string tasaCambio,
            string subTotal, string descuento, string igvPorcentual, string rentaPorcentual, string igv, string total, string centro, string almacen, string nave, string xml, string xml_MAQUILA, string ussername, string Estado,
            string FechaInicio, string FechaFin,string Observacion, string AlmacenDestino, string DireccionDestino, int Area)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspPP_saveUpdate_OrdenCompra");
            db.AddInParameter(cmd, "@prmintOrdenCompra", DbType.Int32, idOrden);
            db.AddInParameter(cmd, "@prmintSociedad", DbType.Int32, idSociedad);
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int32, idEmpresa);
            db.AddInParameter(cmd, "@prmintProveedor", DbType.Int32, idProveedor);
            db.AddInParameter(cmd, "@prmbitIncluyeIGV", DbType.Boolean, IncluyeIgv);

            db.AddInParameter(cmd, "@prmbitIncluyeRENTA", DbType.Boolean, IncluyeRenta);
            db.AddInParameter(cmd, "@prmbitMaquila", DbType.Boolean, Maquila);

            db.AddInParameter(cmd, "@prmstrObservacion", DbType.String, Observacion);

            db.AddInParameter(cmd, "@prmstrCodigoCotizacion", DbType.String, codigo);
            
            db.AddInParameter(cmd, "@prmstrFormaValorizar", DbType.String, Valorizacion);

            db.AddInParameter(cmd, "@prmstrTipoDocumento", DbType.String, tipoDocumento);
            db.AddInParameter(cmd, "@prmstrFormaPago", DbType.String, formaPago);
            db.AddInParameter(cmd, "@prmstrFechaEmision", DbType.String, fechaEmision);
            db.AddInParameter(cmd, "@prmstrFechaEntrega", DbType.String, fechaEntrega);
            db.AddInParameter(cmd, "@prmstrTipoMoneda", DbType.String, tipoMoneda);

            db.AddInParameter(cmd, "@prmstrTasaCambio", DbType.String, tasaCambio);
            db.AddInParameter(cmd, "@prmstrSubTotal", DbType.String, subTotal);
            db.AddInParameter(cmd, "@prmstrDescuento", DbType.String, descuento);
            db.AddInParameter(cmd, "@prmstrIgvPorcentaje", DbType.String, igvPorcentual);

            db.AddInParameter(cmd, "@prmstrRentaPorcentaje", DbType.String, rentaPorcentual);

            db.AddInParameter(cmd, "@prmstrIgv", DbType.String, igv);
            db.AddInParameter(cmd, "@prmstrTotal", DbType.String, total);

            db.AddInParameter(cmd, "@prmintCentro", DbType.String, centro);
            db.AddInParameter(cmd, "@prmintAlmacen", DbType.String, almacen);
            db.AddInParameter(cmd, "@prmintNave", DbType.String, nave);

            db.AddInParameter(cmd, "@prmstrFechaInicio", DbType.String, FechaInicio);
            db.AddInParameter(cmd, "@prmstrFechaFin", DbType.String, FechaFin);

            db.AddInParameter(cmd, "@pmrXML", DbType.String, xml);
            db.AddInParameter(cmd, "@prmXML_MAQUILA", DbType.String, xml_MAQUILA);
            db.AddInParameter(cmd, "@prmdateFechaRegistro", DbType.DateTime, DateTime.Now);
            db.AddInParameter(cmd, "@prmstrUsser", DbType.String, ussername);
            db.AddInParameter(cmd, "@prmstrEstado", DbType.String, Estado);

            db.AddInParameter(cmd, "@prmintAlmacenDestino", DbType.String, AlmacenDestino);
            db.AddInParameter(cmd, "@prmstrDireccionDestino", DbType.String, DireccionDestino);
            db.AddInParameter(cmd, "@prmintArea", DbType.Int32, Area);
            try
            {
                return Convert.ToInt32(db.ExecuteScalar(cmd));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public int enableDisable_OrdenCompra(int idOrden, Boolean bitStatus, string Status, string ussername, string idUsuario) {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspPP_enableDisable_OrdenCompra");
            db.AddInParameter(cmd, "@prmintOrdenCompra", DbType.Int32, idOrden);
            db.AddInParameter(cmd, "@prmbitStatus", DbType.Boolean, bitStatus);
            db.AddInParameter(cmd, "@prmstrEstado", DbType.String, Status);
            db.AddInParameter(cmd, "@prmdateFechaRegistro", DbType.DateTime, DateTime.Now);
            db.AddInParameter(cmd, "@prmstrUsser", DbType.String, ussername);
            db.AddInParameter(cmd, "@prmintIdUsser", DbType.String, idUsuario);

            try
            {
                return Convert.ToInt32(db.ExecuteScalar(cmd));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public DataTable generarOrdenProduccion_byOC(int idOrden, int idEmpresa, int idSociedad, string ussername)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspPP_generarOrdenProduccion_byOC");
            db.AddInParameter(cmd, "@prmintOrdenCompra", DbType.Int32, idOrden);
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int32, idEmpresa);
            db.AddInParameter(cmd, "@prmintSociedad", DbType.Int32, idSociedad);
            
            db.AddInParameter(cmd, "@prmFecha", DbType.DateTime, DateTime.Now);
            db.AddInParameter(cmd, "@prmUsser", DbType.String, ussername);

            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        


        public int liberar_masivo_OrdenCompra(string XML, string ussername, int idUsuario, string status)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspPP_liberar_masivo_OrdenCompra");
            db.AddInParameter(cmd, "@prmstrXML", DbType.String, XML);
            db.AddInParameter(cmd, "@prmdateFechaRegistro", DbType.DateTime, DateTime.Now);
            db.AddInParameter(cmd, "@prmstrUsser", DbType.String, ussername);
            db.AddInParameter(cmd, "@prmintUsser", DbType.Int32, idUsuario);
            db.AddInParameter(cmd, "@future_status", DbType.String, status);
            try
            {
                return Convert.ToInt32(db.ExecuteScalar(cmd));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        


        public DataTableCollection getTasaActual(int idEmpresa,string compra, string venta, string fecha, string usr)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspPP_setgetTasasActual");
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int16, idEmpresa);
            db.AddInParameter(cmd, "@prmstrCompra", DbType.String, compra);
            db.AddInParameter(cmd, "@prmstrVenta", DbType.String, venta);

            db.AddInParameter(cmd, "@prmstrFecha", DbType.String, fecha);
            db.AddInParameter(cmd, "@prmdateFechaRegistro", DbType.DateTime, DateTime.Now);
            db.AddInParameter(cmd, "@prmstrUsuario", DbType.String, usr);
            try
            {
                return db.ExecuteDataSet(cmd).Tables;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private const string URL = "http://www.sunat.gob.pe/cl-at-ittipcam/tcS01Alias?mes={0}&anho={1}";
        private string html = string.Empty;
        public DataTable obtenerDatos_TASA_SUNAT(int dia, int mes, int anio )
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Dia", typeof(String));
            dt.Columns.Add("Compra", typeof(String));
            dt.Columns.Add("Venta", typeof(String));

            string urlcomplete = string.Format(URL, mes, anio);
            this.html = new WebClient().DownloadString(urlcomplete);

            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(this.html);

            HtmlNodeCollection NodesTr = document.DocumentNode.SelectNodes("//table[@class='class=\"form-table\"']//tr");
            if (NodesTr != null)
            {

                int iNumFila = 0;
                foreach (HtmlNode Node in NodesTr)
                {
                    if (iNumFila > 0)
                    {
                        int iNumColumna = 0;
                        DataRow dr = dt.NewRow();
                        foreach (HtmlNode subNode in Node.Elements("td"))
                        {

                            if (iNumColumna == 0) dr = dt.NewRow();

                            string sValue = subNode.InnerHtml.ToString().Trim();
                            sValue = System.Text.RegularExpressions.Regex.Replace(sValue, "<.*?>", " ");
                            dr[iNumColumna] = sValue.Trim();

                            iNumColumna++;

                            if (iNumColumna == 3)
                            {
                                dt.Rows.Add(dr);
                                iNumColumna = 0;
                            }
                        }
                    }
                    iNumFila++;
                }

                dt.AcceptChanges();
            }

            return dt;
        }

        public DataTable getOrdenCompra_List_Maquila(string fechaInicio, string fechaFin, string status)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspPP_getOrdenCompra_List_Maquila");
            db.AddInParameter(cmd, "@prmstrFechaInicio", DbType.String, fechaInicio);
            db.AddInParameter(cmd, "@prmstrFechaFin", DbType.String, fechaFin);
            db.AddInParameter(cmd, "@prmstrStatus", DbType.String, status);
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
            DbCommand cmd = db.GetStoredProcCommand("uspSD_ListarLotesAlmacenDespacho");
            db.AddInParameter(cmd, "@prmintMaterial", DbType.Int64, Material);
            db.AddInParameter(cmd, "@prmintAlmacen", DbType.Int64, Almacen);
            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable getRecetasActividad(int Empresa)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspPP_getRecetasActividad");
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

        public DataTable getDatosOrdenCompraMaquila(int OrdenCompra)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspPP_getDatosOrdenCompraMaquila");
            db.AddInParameter(cmd, "@prmintOrdenMaquila", DbType.Int64, OrdenCompra);
            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
         
        public int SaveUpdateOrdenCompraMaquila(String usuario, String xml, String prmstrXmlDOrdenCompra, String prmstrXmlDOrdenCompraMaterial)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspMM_SaveUpdateOrdenCompraMaquila");
            db.AddInParameter(cmd, "@prmstrUsuario", DbType.String, usuario);
            db.AddInParameter(cmd, "@prmstrXml", DbType.String, xml);
            db.AddInParameter(cmd, "@prmstrXmlDOrdenCompra", DbType.String, prmstrXmlDOrdenCompra);
            db.AddInParameter(cmd, "@prmstrXmlDOrdenCompraMaterial", DbType.String, prmstrXmlDOrdenCompraMaterial);

            try
            {
                return Convert.ToInt32(db.ExecuteScalar(cmd));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int LiberarOrdenCompraMaquila(Int32 prmintEmpresa, Int32 prmintUsuarioLibera, String usuario, String prmstrXmlOrdenCompra)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspMM_LiberarOrdenCompraMaquila");
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int32, prmintEmpresa);
            db.AddInParameter(cmd, "@prmintUsuarioLibera", DbType.Int32, prmintUsuarioLibera);
            db.AddInParameter(cmd, "@prmstrUsuario", DbType.String, usuario);
            db.AddInParameter(cmd, "@prmstrXmlOrdenCompra", DbType.String, prmstrXmlOrdenCompra);
            try
            {
                return Convert.ToInt32(db.ExecuteScalar(cmd));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int CerrarOrdenCompraMaquila(Int32 prmintEmpresa, Int32 prmintUsuarioCierre, String usuario, String prmstrXmlOrdenCompra)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspMM_CerrarOrdenCompraMaquila");
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int32, prmintEmpresa);
            db.AddInParameter(cmd, "@prmintUsuarioCierre", DbType.Int32, prmintUsuarioCierre);
            db.AddInParameter(cmd, "@prmstrUsuario", DbType.String, usuario);
            db.AddInParameter(cmd, "@prmstrXmlOrdenCompra", DbType.String, prmstrXmlOrdenCompra);
            try
            {
                return Convert.ToInt32(db.ExecuteScalar(cmd));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int AnularOrdenCompraMaquila(Int32 prmintEmpresa, Int32 prmintOrdenCompra, String usuario)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspMM_AnularOrdenCompraMaquila");
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int32, prmintEmpresa);
            db.AddInParameter(cmd, "@prmintOrdenCompra", DbType.Int32, prmintOrdenCompra);
            db.AddInParameter(cmd, "@prmstrUsuario", DbType.String, usuario);
            try
            {
                return Convert.ToInt32(db.ExecuteScalar(cmd));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTableCollection GetDatosNotificacionConsumoMaquila(int OrdenMaquila)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("[uspMM_GetOrdenMaquilaNotificacionById]");
            db.AddInParameter(cmd, "@prmintOrdenCompra", DbType.Int64, OrdenMaquila);
            try
            {
                return db.ExecuteDataSet(cmd).Tables;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int SaveUpdateNotificacionConsumoMaquila(int idUsuario, String usuario, int idEmpresa, int idSociedad, Int32 prmintOrdenCompra, String prmdatFechaDocumento, String prmdatFechaContabilizacion, String xmlMaterialesEntregados)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspMM_SaveUpdateNotificacionConsumoMaquila");

            db.AddInParameter(cmd, "@prmintUsuarioCierre ", DbType.Int32, idUsuario);
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int32, idEmpresa);
            db.AddInParameter(cmd, "@prmintSociedad", DbType.Int32, idSociedad);
            db.AddInParameter(cmd, "@prmstrUsuario", DbType.String, usuario);
            db.AddInParameter(cmd, "@prmintOrdenCompra", DbType.Int32, prmintOrdenCompra);
            db.AddInParameter(cmd, "@prmdatFechaDocumento", DbType.String, prmdatFechaDocumento);
            db.AddInParameter(cmd, "@prmdatFechaContabilizacion", DbType.String, prmdatFechaContabilizacion);
            db.AddInParameter(cmd, "@xmlMaterialesEntregados", DbType.String, xmlMaterialesEntregados);
            try
            {
                return Convert.ToInt32(db.ExecuteScalar(cmd));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTableCollection getOrdenCompraMaquila_Data(int idOrdenCompra)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspPP_getOrdenCompraMaquila_Data");
            db.AddInParameter(cmd, "@prmintOrdenCompra", DbType.Int32, idOrdenCompra);
            try
            {
                return db.ExecuteDataSet(cmd).Tables;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetRptIngresoOrdenCompra(int prmintSociedad, int prmintTipoMaterial, int prmintGrupoArticulo, int prmintMaterial,
            int prmintProveedor, string prmstrEstado, DateTime prmdateFechaInicio, DateTime prmdateFechaFin)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspPP_getReporteIngresoOrdenCompra");
            db.AddInParameter(cmd, "@prmintSociedad", DbType.Int32, prmintSociedad);
            db.AddInParameter(cmd, "@prmintTipoMaterial", DbType.Int32, prmintTipoMaterial);
            db.AddInParameter(cmd, "@prmintGrupoArticulo", DbType.Int32, prmintGrupoArticulo);
            db.AddInParameter(cmd, "@prmintMaterial", DbType.Int32, prmintMaterial);
            db.AddInParameter(cmd, "@prmintProveedor", DbType.Int32, prmintProveedor);
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

        public DataTable GetRptServicioMaquila(int prmintSociedad, int prmintTipoMaterial, int prmintGrupoArticulo, int prmintMaterial,
           int prmintProveedor, string prmstrEstado, DateTime prmdateFechaInicio, DateTime prmdateFechaFin)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspPP_getReporteServicioMaquila");
            db.AddInParameter(cmd, "@prmintSociedad", DbType.Int32, prmintSociedad);
            db.AddInParameter(cmd, "@prmintTipoMaterial", DbType.Int32, prmintTipoMaterial);
            db.AddInParameter(cmd, "@prmintGrupoArticulo", DbType.Int32, prmintGrupoArticulo);
            db.AddInParameter(cmd, "@prmintMaterial", DbType.Int32, prmintMaterial);
            db.AddInParameter(cmd, "@prmintProveedor", DbType.Int32, prmintProveedor);
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

        public DataTable GetRptOrdenTransporte(int prmintSociedad, int prmintTipoMaterial, int prmintGrupoArticulo, int prmintMaterial,
            int prmintProveedor, int prmintOrigen, int prmintDestino, string prmstrEstado, DateTime prmdateFechaInicio, DateTime prmdateFechaFin)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspPP_getReporteOrdenTransporte");
            db.AddInParameter(cmd, "@prmintSociedad", DbType.Int32, prmintSociedad);
            db.AddInParameter(cmd, "@prmintTipoMaterial", DbType.Int32, prmintTipoMaterial);
            db.AddInParameter(cmd, "@prmintGrupoArticulo", DbType.Int32, prmintGrupoArticulo);
            db.AddInParameter(cmd, "@prmintMaterial", DbType.Int32, prmintMaterial);
            db.AddInParameter(cmd, "@prmintProveedor", DbType.Int32, prmintProveedor);
            db.AddInParameter(cmd, "@prmintOrigen", DbType.Int32, prmintOrigen);
            db.AddInParameter(cmd, "@prmintDestino", DbType.Int32, prmintDestino);
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


        public int update_estatus_OrdenCompra(Int32 OrdenCompra, String usuario, string Tipo)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspPP_update_estatus_OrdenCompra");
            db.AddInParameter(cmd, "@prmintOrdenCompra", DbType.Int32, OrdenCompra);
            db.AddInParameter(cmd, "@prmstrUsuario", DbType.String, usuario);
            db.AddInParameter(cmd, "@prmstrTipo", DbType.String, Tipo);
            

            try
            {
                return Convert.ToInt32(db.ExecuteScalar(cmd));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetAreasOC()
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("[uspHCM_getAreasPersonalOC]");
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