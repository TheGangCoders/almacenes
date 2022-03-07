using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Shared.Util;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web;
using WS_IT.DTOs;

namespace WS_IT.Models
{
    public class DGrupoSeguridadModel
    {
        private static readonly DGrupoSeguridadModel _instancia = new DGrupoSeguridadModel();
        public static DGrupoSeguridadModel Instancia
        {
            get { return DGrupoSeguridadModel._instancia; }
        }

        public DGrupoSeguridadDTO ObtenerPorIdUsuario(int prmintIdUsuario)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspIT_ObtenerDetalleGrupoSeguridadPorIdUsuario");
            db.AddInParameter(cmd, "@prmintIdUsuario", DbType.Int32, prmintIdUsuario);
            try
            {
                List<DGrupoSeguridadDTO> listDetalleGrupoSeguridad = MetodoComun.ConvertDataTableToList<DGrupoSeguridadDTO>(db.ExecuteDataSet(cmd).Tables[0]);
                if (listDetalleGrupoSeguridad.Count > 0)
                {
                    return listDetalleGrupoSeguridad[0];
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DGrupoSeguridadDTO ObtenerPorIdEmpresa(int prmintIdEmpresa)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspIT_ObtenerDetalleGrupoSeguridadPorIdEmpresa");
            db.AddInParameter(cmd, "@prmintIdEmpresa", DbType.Int32, prmintIdEmpresa);
            try
            {
                List<DGrupoSeguridadDTO> listDetalleGrupoSeguridad = MetodoComun.ConvertDataTableToList<DGrupoSeguridadDTO>(db.ExecuteDataSet(cmd).Tables[0]);
                if (listDetalleGrupoSeguridad.Count > 0)
                {
                    return listDetalleGrupoSeguridad[0];
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}