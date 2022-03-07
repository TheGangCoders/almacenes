using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace WS_MM.Models
{
    public class SalidaccModel
    {
        private static readonly SalidaccModel _instancia = new SalidaccModel();

        public static SalidaccModel Instancia
        {
            get { return SalidaccModel._instancia; }
        }

        public DataTable GetTipoMovimientoCC(int prmintEmpresa, Boolean? prmbitActivo)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("[uspMM_GetTipoMovimientoCC]");
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int64, prmintEmpresa);
            db.AddInParameter(cmd, "@prmbitActivo", DbType.Boolean, prmbitActivo);
            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetTipoDocumentoCC(int prmintEmpresa, Boolean? prmbitActivo)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("[uspMM_GetTipoDocumentoCC]");
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int64, prmintEmpresa);
            db.AddInParameter(cmd, "@prmbitActivo", DbType.Boolean, prmbitActivo);
            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable GetReservaDetail(int prmintReserva)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("[uspMM_ListaReservaDetalle]");
            db.AddInParameter(cmd, "@prmintReserva", DbType.Int64, prmintReserva);
            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int SaveUpdate_ReservaMaterial(int prmintMigo, int prmintTipoMovimiento, int prmintTipoDocumento, string prmstrValeConsumo, int? prmintReserva, DateTime prmdateFechaContable, DateTime prmdateFechaDocumento, int idSociedad, int idEmpresa, bool prmintActivo, string xml, string ussername, string prmstrSolicitante)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("[uspMM_SaveUpdateReservasMigo]");
            db.AddInParameter(cmd, "@prmintMigo", DbType.Int32, prmintMigo);
            db.AddInParameter(cmd, "@prmintTipoMovimiento", DbType.Int32, prmintTipoMovimiento);
            db.AddInParameter(cmd, "@prmintTipoDocumento", DbType.Int32, prmintTipoDocumento);
            db.AddInParameter(cmd, "@prmintReserva", DbType.Int32, prmintReserva);
            db.AddInParameter(cmd, "@prmstrValeConsumo", DbType.String, prmstrValeConsumo);
            db.AddInParameter(cmd, "@prmdateFechaContable", DbType.DateTime, prmdateFechaContable);
            db.AddInParameter(cmd, "@prmdateFechaDocumento", DbType.DateTime, prmdateFechaDocumento);
            db.AddInParameter(cmd, "@idSociedad", DbType.Int32, idSociedad);
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int32, idEmpresa);
            db.AddInParameter(cmd, "@prmintActivo", DbType.Int32, prmintActivo);
            db.AddInParameter(cmd, "@prmXML", DbType.String, xml);
            db.AddInParameter(cmd, "@prmstrUsser", DbType.String, ussername);
            db.AddInParameter(cmd, "@prmstrSolicitante", DbType.String, prmstrSolicitante);

            try
            {
                return Convert.ToInt32(db.ExecuteScalar(cmd));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable GetTipoMovimiento()
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("[uspMM_getTipoMovimientoNR]");
            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable getReporteConsumo(DateTime? prmdatefInicio, DateTime? prmdatefFin,
                                            int? prmintCentro,int? prmintTipoMovimiento,
                                            int? prmintTipoMaterial, int? prmintMaterial,
                                            int? prmintGrupoarticulo, int? prmintAlmacen,
                                            string prmstrLote, int? prmintCCosto, int idEmpresa, int pkUser)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspMM_GetReporteConsumos");
            db.AddInParameter(cmd, "@prmdatefInicio", DbType.DateTime, prmdatefInicio);
            db.AddInParameter(cmd, "@prmdatefFin", DbType.DateTime, prmdatefFin);
            db.AddInParameter(cmd, "@prmintCentro", DbType.Int32, prmintCentro);
            db.AddInParameter(cmd, "@prmintTipoMovimiento", DbType.Int32, prmintTipoMovimiento);
            db.AddInParameter(cmd, "@prmintTipoMaterial", DbType.Int32, prmintTipoMaterial);
            db.AddInParameter(cmd, "@prmintMaterial", DbType.Int32, prmintMaterial);
            db.AddInParameter(cmd, "@prmintGrupoarticulo", DbType.Int32, prmintGrupoarticulo);
            db.AddInParameter(cmd, "@prmintAlmacen", DbType.Int32, prmintAlmacen);
            db.AddInParameter(cmd, "@prmstrLote", DbType.String, prmstrLote);
            db.AddInParameter(cmd, "@prmintCCosto", DbType.Int32, prmintCCosto);
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int32, idEmpresa);
            db.AddInParameter(cmd, "@prmintUsuarioSesion", DbType.Int32, pkUser);
            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable getSalidasCC(DateTime? prmdatefInicio, DateTime? prmdatefFin,
                                            int? prmintCentro)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspMM_GetSalidasCC");
            db.AddInParameter(cmd, "@prmdatefInicio", DbType.DateTime, prmdatefInicio);
            db.AddInParameter(cmd, "@prmdatefFin", DbType.DateTime, prmdatefFin);
            db.AddInParameter(cmd, "@prmintCentro", DbType.Int32, prmintCentro);
            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int AnulacionSalidasCC(DateTime prmdatefContable, int prmintMigo, int Empresa, int Sociedad, string Usser)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspMM_AnularSalidaCC");
            db.AddInParameter(cmd, "@prmdateFechaContable", DbType.DateTime, prmdatefContable);
            db.AddInParameter(cmd, "@prmintMigo", DbType.Int32, prmintMigo);
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int32, Empresa);
            db.AddInParameter(cmd, "@idSociedad", DbType.Int32, Sociedad);
            db.AddInParameter(cmd, "@prmstrUsser", DbType.String, Usser);
            try
            {
                return Convert.ToInt32(db.ExecuteScalar(cmd));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable GetAlmacenDestino(int prmintAlmacen, int prmintCentro, int Empresa)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspMM_GetAlmacenDestino");
            db.AddInParameter(cmd, "@prmintAlmacen", DbType.Int32, prmintAlmacen);
            db.AddInParameter(cmd, "@prmintCentro", DbType.Int32, prmintCentro);
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int32, Empresa);
            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int SaveUpdate_TrasladoSimple(int prmintMigo, int prmintAlmacenOrigen, int prmintAlmacenDestino, DateTime prmdateFechaContable, DateTime prmdateFechaDocumento, int idSociedad, int idEmpresa, bool prmintActivo, string xml, string ussername, string prmstrSerie, string prmstrNumero)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspMM_SaveTrasladoSimple");
            db.AddInParameter(cmd, "@prmintMigo", DbType.Int32, prmintMigo);
            db.AddInParameter(cmd, "@prmintAlmacenOrigen", DbType.Int32, prmintAlmacenOrigen);
            db.AddInParameter(cmd, "@prmintAlmacenDestino", DbType.Int32, prmintAlmacenDestino);
            db.AddInParameter(cmd, "@prmdateFechaContable", DbType.DateTime, prmdateFechaContable);
            db.AddInParameter(cmd, "@prmdateFechaDocumento", DbType.DateTime, prmdateFechaDocumento);
            db.AddInParameter(cmd, "@idSociedad", DbType.Int32, idSociedad);
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int32, idEmpresa);
            db.AddInParameter(cmd, "@prmintActivo", DbType.Int32, prmintActivo);
            db.AddInParameter(cmd, "@prmXML", DbType.String, xml);
            db.AddInParameter(cmd, "@prmstrUsser", DbType.String, ussername);
            db.AddInParameter(cmd, "@prmstrSerie", DbType.String, prmstrSerie);
            db.AddInParameter(cmd, "@prmstrNumero", DbType.String, prmstrNumero);

            try
            {
                return Convert.ToInt32(db.ExecuteScalar(cmd));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable getTrasladosSimples(DateTime? prmdatefInicio, DateTime? prmdatefFin,
                                            int? prmintCentro)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspMM_TraspasoSimple");
            db.AddInParameter(cmd, "@prmdatefInicio", DbType.DateTime, prmdatefInicio);
            db.AddInParameter(cmd, "@prmdatefFin", DbType.DateTime, prmdatefFin);
            db.AddInParameter(cmd, "@prmintCentro", DbType.Int32, prmintCentro);
            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable GetWsPerUser(int Usuario)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspIT_GetPerUser");
            db.AddInParameter(cmd, "@prmintUser", DbType.Int32, Usuario);
            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable GetMateriales()
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("[uspMM_getMateriales_Salidas]");
            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetProveedores()
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("[uspMM_ProvedoresList]");
            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int Save_MaterialPrecio(string xml, string ussername)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("[uspMM_SavePrecioMateriales]");
            db.AddInParameter(cmd, "@prmXML", DbType.String, xml);
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
        public DataTable GetPreciosList()
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("[uspMM_PrecioList]");
            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable GetSolicitanteList()
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("[uspMM_ListaSolicitante]");
            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetDataMigoToPdf(int prmintMigo, int prmintEmpresa)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspMM_GetDataMigoToPdf");
            db.AddInParameter(cmd, "@prmintMigo", DbType.Int32, prmintMigo);
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

        public DataTable GetDocumentosApobacion(int prmIntUsuario, int prmintEstado, DateTime prmdateFInicio, DateTime prmdateFFin)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("[uspAGR_getDocumentosAprobacion]");
            db.AddInParameter(cmd, "@prmintUsuario", DbType.Int32, prmIntUsuario);
            db.AddInParameter(cmd, "@prmintEstado", DbType.Int32, prmintEstado);
            db.AddInParameter(cmd, "@prmdateFInicio", DbType.DateTime, prmdateFInicio);
            db.AddInParameter(cmd, "@prmdateFFin", DbType.DateTime, prmdateFFin);
            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable AprobarDocumento(int prmintDetaAprobacion, int prmintUsuario, string prmStrNombreUsuario)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspAGR_AprobacionDocumento");
            db.AddInParameter(cmd, "@prmintDetaAprobacion", DbType.Int32, prmintDetaAprobacion);
            db.AddInParameter(cmd, "@prmintUsuario", DbType.Int32, prmintUsuario);
            db.AddInParameter(cmd, "@prmStrNombreUsuario", DbType.String, prmStrNombreUsuario);
            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable RechazarDocumento(int prmintDetaAprobacion, bool prmbitStatus, string prmStrNombreUsuario)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspAGR_DisableAprobacionDocumento");
            db.AddInParameter(cmd, "@prmintDetaAprobacion", DbType.Int32, prmintDetaAprobacion);
            db.AddInParameter(cmd, "@prmbitStatus", DbType.Boolean, prmbitStatus);
            db.AddInParameter(cmd, "@prmStrNombreUsuario", DbType.String, prmStrNombreUsuario);
            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetMaterialesSolped(int prmintCentro)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspMM_getMaterialesCentro");
            db.AddInParameter(cmd, "@prmintCentro", DbType.Int32, prmintCentro);
            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int GenerarSOLPED(string prmstrXML, int prmintIdEmpresa, int prmintIdSociedad, string prmstrUsuario)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspMM_GenerarSOLPED");
            db.AddInParameter(cmd, "@prmstrXML", DbType.String, prmstrXML);
            db.AddInParameter(cmd, "@prmintIdEmpresa", DbType.Int32, prmintIdEmpresa);
            db.AddInParameter(cmd, "@prmintIdSociedad", DbType.Int32, prmintIdSociedad);
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

        public DataTable GetEstadosSolicitudPedido(int prmintSociedad, int prmintEmpresa)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspMM_getEstadosSolicitudPedido");
            db.AddInParameter(cmd, "@prmintIdEmpresa", DbType.Int32, prmintEmpresa);
            db.AddInParameter(cmd, "@prmintIdSociedad", DbType.Int32, prmintSociedad);
            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetSolicitudesPedidos(int prmintSociedad, int prmintEmpresa, string prmstrEstado, int prmintMaterial, string prmdateFechaDesde, string prmdateFechaHasta, string usuario)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspMM_getSolicitudesPedidos");
            db.AddInParameter(cmd, "@prmintIdEmpresa", DbType.Int32, prmintEmpresa);
            db.AddInParameter(cmd, "@prmintIdSociedad", DbType.Int32, prmintSociedad);
            db.AddInParameter(cmd, "@prmstrEstado", DbType.String, prmstrEstado);
            db.AddInParameter(cmd, "@prmintMaterial", DbType.Int64, prmintMaterial);
            db.AddInParameter(cmd, "@prmdateFechaDesde", DbType.String, prmdateFechaDesde);
            db.AddInParameter(cmd, "@prmdateFechaHasta", DbType.String, prmdateFechaHasta);
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

        public int ActualizarSolicitudPedido(int prmintIdEmpresa, int prmintIdSociedad, int prmintSolicitudPedido, decimal prmdecCantidad, string prmstrUsuario)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspMM_ActualizarSolicitudPedido");
            db.AddInParameter(cmd, "@prmintIdEmpresa", DbType.Int32, prmintIdEmpresa);
            db.AddInParameter(cmd, "@prmintIdSociedad", DbType.Int32, prmintIdSociedad);
            db.AddInParameter(cmd, "@prmintSolicitudPedido", DbType.Int32, prmintSolicitudPedido);
            db.AddInParameter(cmd, "@prmdecCantidad", DbType.Decimal, prmdecCantidad);
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

        public int AnularSolicitudPedido(int prmintIdEmpresa, int prmintIdSociedad, int prmintSolicitudPedido, string prmstrUsuario)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspMM_AnularSolicitudPedido");
            db.AddInParameter(cmd, "@prmintIdEmpresa", DbType.Int32, prmintIdEmpresa);
            db.AddInParameter(cmd, "@prmintIdSociedad", DbType.Int32, prmintIdSociedad);
            db.AddInParameter(cmd, "@prmintSolicitudPedido", DbType.Int32, prmintSolicitudPedido);
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

        public DataTable GetSolpedForOC()
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspMM_getDetalleSolicitudPedidoToOC");

            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetDetalleSolicitudPedidoFromOC(int prmintSociedad, int prmintEmpresa, string prmstrNroSolped)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspMM_getDetalleSolicitudPedidoFromOC");
            db.AddInParameter(cmd, "@prmintIdEmpresa", DbType.Int32, prmintEmpresa);
            db.AddInParameter(cmd, "@prmintIdSociedad", DbType.Int32, prmintSociedad);
            db.AddInParameter(cmd, "@prmstrNroSolped", DbType.String, prmstrNroSolped);

            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetSolicitudPedidoFromOC(int prmintSociedad, int prmintEmpresa)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspMM_getSolicitudPedidoFromOC");
            db.AddInParameter(cmd, "@prmintIdEmpresa", DbType.Int32, prmintEmpresa);
            db.AddInParameter(cmd, "@prmintIdSociedad", DbType.Int32, prmintSociedad);

            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable dataDocumentosAprobacion(int prmintIdDetaAprobacion, string prmstrCorrelativo)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspAGR_getDataAprobador");
            db.AddInParameter(cmd, "@prmintIdDetaAprobacion", DbType.Int32, prmintIdDetaAprobacion);
            db.AddInParameter(cmd, "@prmstrCorrelativo", DbType.String, prmstrCorrelativo);
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