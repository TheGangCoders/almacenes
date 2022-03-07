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
    public class PlanVentaModel
    {
        private static readonly PlanVentaModel _instancia = new PlanVentaModel();

        public static PlanVentaModel Instancia
        {
            get { return PlanVentaModel._instancia; }
        }

        public DataTable RptPlanVentas(int idEmpresa, string prmSociedad, string prmTipoVenta, string prmTipoFecha, string pmrFechaInicio, string prmFechaFin, string pmrintCliente, string pmrGrupoArticulo, string pmrTipoMaterial)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspSD_ConsultaPlanVtas_PorFechaFactura");
            if (prmTipoFecha == "P")
            {
                cmd = db.GetStoredProcCommand("uspSD_ConsultaPlanVtas_PorFechaPedido");
            }
            
            db.AddInParameter(cmd, "@printempresa", DbType.String, idEmpresa);
            db.AddInParameter(cmd, "@prmintSociedad", DbType.String, prmSociedad);
            db.AddInParameter(cmd, "@prmstrTipoVenta", DbType.String, prmTipoVenta);
            db.AddInParameter(cmd, "@prmstrFechaI", DbType.String, pmrFechaInicio);
            db.AddInParameter(cmd, "@prmstrFechaF", DbType.String, prmFechaFin);
            db.AddInParameter(cmd, "@prmintCliente", DbType.String, pmrintCliente);

            db.AddInParameter(cmd, "@prmintTipoMaterial", DbType.String, pmrTipoMaterial);
            db.AddInParameter(cmd, "@prmintGrupoArticulo", DbType.String, pmrGrupoArticulo);

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