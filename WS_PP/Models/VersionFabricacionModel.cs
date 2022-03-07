using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace WS_PP.Models
{
    public class VersionFabricacionModel
    {
        private static readonly VersionFabricacionModel _instancia = new VersionFabricacionModel();

        public static VersionFabricacionModel Instancia
        {
            get { return VersionFabricacionModel._instancia; }
        }

        public DataTable getVersionFabricacion(int? prminCentro, int? prmintNave, int? prmintActivo, int prmintEmpresa, int? prmintVersion)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspPP_getVersionFabricacion");
            db.AddInParameter(cmd, "@prmintCentro", DbType.Int64, prminCentro);
            db.AddInParameter(cmd, "@prmintNave", DbType.Int64, prmintNave);
            db.AddInParameter(cmd, "@prmintActivo", DbType.Int64, prmintActivo);
            db.AddInParameter(cmd, "@prmEmpresa", DbType.Int64, prmintEmpresa);
            db.AddInParameter(cmd, "@prmintVersion", DbType.Int64, prmintVersion);
            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public int putEstadoVersionFabricacion(int Version, bool Estado, string UsuarioRegistro)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspPP_putEstadoVersionFabricacion");
            db.AddInParameter(cmd, "@prmintVersion", DbType.Int64, Version);
            db.AddInParameter(cmd, "@prmbitEstado", DbType.Boolean, Estado);
            db.AddInParameter(cmd, "@prmstrUsuarioRegistro", DbType.String, UsuarioRegistro);
            try
            {
                return Convert.ToInt16(db.ExecuteScalar(cmd));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable getRecetaDetalle(int? prminReceta)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspPP_getRecetaDetalles");
            db.AddInParameter(cmd, "@prmintReceta", DbType.Int64, prminReceta);
            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable getRecetaByMaterial(int? prminCentro, int? prmintNave, Boolean? prmintActivo, int prmintMaterial, Boolean? prmintGenerico)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspPP_getRecetaByMaterial");
            db.AddInParameter(cmd, "@prmintCentro", DbType.Int64, prminCentro);
            db.AddInParameter(cmd, "@prmintNave", DbType.Int64, prmintNave);
            db.AddInParameter(cmd, "@prmActivo", DbType.Boolean, prmintActivo);
            db.AddInParameter(cmd, "@prmGenerico", DbType.Boolean, prmintGenerico);
            db.AddInParameter(cmd, "@prmintMaterial", DbType.Int64, prmintMaterial);
            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        
        public DataTable GetListaMaterialesByMaterial(int? prmintCentro, int? prmintMaterial, int Empresa, int UnidadBase)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspPP_GetListaMaterialesByMaterial");
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int64, Empresa);
            db.AddInParameter(cmd, "@prmintCentro", DbType.Int64, prmintCentro);
            db.AddInParameter(cmd, "@prmintMaterial", DbType.Int64, prmintMaterial);
            db.AddInParameter(cmd, "@prmintUnidadBase", DbType.Int64, UnidadBase);

            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public DataTable postRegistrarVersionFabricacion(int Version, string Codigo, string Nombre, Boolean Activo, Boolean PorDefecto, Boolean Generico, int Centro, int Nave, int TipoMaterial, int MaterialBase, int ListaMaterial, int Receta, string InicioVigencia, string FinVigencia, string UsuarioRegistro, int Empresa, int UnidadMedida)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspPP_postRegistrarVersionFabricacion");
            db.AddInParameter(cmd, "@prmintVersion", DbType.Int64, Version);
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int64, Empresa);            
            db.AddInParameter(cmd, "@prmstrCodigo", DbType.String, Codigo);
            db.AddInParameter(cmd, "@prmstrNombre", DbType.String, Nombre);
            db.AddInParameter(cmd, "@prmbitActivo", DbType.Boolean, Activo);

            db.AddInParameter(cmd, "@prmbitPorDefecto", DbType.Boolean, PorDefecto);
            db.AddInParameter(cmd, "@prmbitGenerico", DbType.Boolean, Generico);

            db.AddInParameter(cmd, "@prmintCentro", DbType.Int64, Centro);
            db.AddInParameter(cmd, "@prmintNave", DbType.Int64, Nave);
            db.AddInParameter(cmd, "@prmintUnidadMedida", DbType.Int64, UnidadMedida);

            db.AddInParameter(cmd, "@prmintTipoMaterial", DbType.Int64, TipoMaterial);
            db.AddInParameter(cmd, "@prmintMaterialBase", DbType.Int64, MaterialBase);
            db.AddInParameter(cmd, "@prmintListaMaterial", DbType.Int64, ListaMaterial);
            db.AddInParameter(cmd, "@prmintReceta", DbType.Int64, Receta);
            db.AddInParameter(cmd, "@prmstrInicioVigencia", DbType.String, InicioVigencia);
            db.AddInParameter(cmd, "@prmstrFinVigencia", DbType.String, FinVigencia);
            db.AddInParameter(cmd, "@prmstrUsuarioRegistro", DbType.String, UsuarioRegistro);
            try
            {
                //return Convert.ToInt16(db.ExecuteScalar(cmd));
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }






    }
}