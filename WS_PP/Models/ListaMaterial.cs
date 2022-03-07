using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Net;

namespace WS_PP.Models
{
    public class ListaMaterialDAL
    {
        private static readonly ListaMaterialDAL _instancia = new ListaMaterialDAL();

        public static ListaMaterialDAL Instancia
        {
            get { return ListaMaterialDAL._instancia; }
        }

        public DataTable getListaMaterial_List(int centro,  int activo, int idEmpresa, int Material)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspPP_getListaMaterial_List");
            db.AddInParameter(cmd, "@prmintCentro", DbType.Int32, centro);
            db.AddInParameter(cmd, "@prmintActivo", DbType.Int32, activo);
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int32, idEmpresa);
            db.AddInParameter(cmd, "@prmintMaterial", DbType.Int32, Material);

            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTableCollection getListaMaterial_Data(int idLista)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspPP_getListaMaterial_Data");
            db.AddInParameter(cmd, "@prmintLista", DbType.Int32, idLista);
            try
            {
                return db.ExecuteDataSet(cmd).Tables;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable getCabeceraListaMaterial_list(string Centro, string TipoMaterial, string GrupoArticulo, string CodigoMaterial, string idMaterial)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspPP_getCabeceraListaMaterial_list");
            db.AddInParameter(cmd, "@prmCentro", DbType.String, Centro);
            db.AddInParameter(cmd, "@prmTipoMaterial", DbType.String, TipoMaterial);
            db.AddInParameter(cmd, "@prmGrupoArticulo", DbType.String, GrupoArticulo);
            db.AddInParameter(cmd, "@prmstrCodigoMaterial", DbType.String, CodigoMaterial);
            db.AddInParameter(cmd, "@prmintMaterial", DbType.String, idMaterial);

            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public int saveUpdate_ListaMaterial(int idLista, int idEmpresa, int idCentro, int idMaterial, int idUnidadMedida, string CantidadBase, 
            string Nombre, string xml, string ussername)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspPP_saveUpdate_ListaMaterial");
            db.AddInParameter(cmd, "@prmintLista", DbType.Int32, idLista);
            db.AddInParameter(cmd, "@prmintEmpresa", DbType.Int32, idEmpresa);
            db.AddInParameter(cmd, "@prmintCentro", DbType.Int32, idCentro);
            db.AddInParameter(cmd, "@prmintMaterial", DbType.Int32, idMaterial);

            db.AddInParameter(cmd, "@prmstrUnidadMedida", DbType.Int32, idUnidadMedida);
            db.AddInParameter(cmd, "@prmstrCantidadBase", DbType.String, CantidadBase);
            db.AddInParameter(cmd, "@prmstrNombre", DbType.String, Nombre);
            db.AddInParameter(cmd, "@pmrXML", DbType.String, xml);
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

        public int enableDisable_ListaMaterial(int idLista, string usuario, Boolean status) {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspPP_enableDisable_ListaMaterial");
            db.AddInParameter(cmd, "@prmintLista", DbType.Int32, idLista);
            db.AddInParameter(cmd, "@prmstrUsuario", DbType.String, usuario);
            db.AddInParameter(cmd, "@prmbitStatus", DbType.Boolean, status);
            db.AddInParameter(cmd, "@prmdateFecha", DbType.DateTime, DateTime.Now);

            try
            {
                return Convert.ToInt32(db.ExecuteScalar(cmd));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public DataTable listMateriales_byCentro(int Empresa, string CodMaterial, string Material, string Centro, string TipoMaterial, string GrupoArticulo, string Calidad)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspPP_listMateriales_byCentro");
            db.AddInParameter(cmd, "@prminEmpresa", DbType.Int64, Empresa);
            if (CodMaterial != null) {
                db.AddInParameter(cmd, "@prmstrCodMaterial", DbType.String, CodMaterial);
            }
            if (Material != null){
                db.AddInParameter(cmd, "@prmstrMaterial", DbType.String, Material);
            }
            db.AddInParameter(cmd, "@prmintCentro", DbType.String, Centro);
            db.AddInParameter(cmd, "@prmstrTipoMaterial", DbType.String, TipoMaterial);
            db.AddInParameter(cmd, "@prmstrGrupoArticulo", DbType.String, GrupoArticulo);
            db.AddInParameter(cmd, "@prmstrCalidad", DbType.String, Calidad);

            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable searchListaMateriales(int Empresa, int Material, int Centro)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspPP_searchListaMateriales");//1.2.5
            db.AddInParameter(cmd, "@prminEmpresa", DbType.Int64, Empresa);
            db.AddInParameter(cmd, "@prmintMaterial", DbType.String, Material);
            db.AddInParameter(cmd, "@prmintCentro", DbType.String, Centro);

            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable compararListaMateriales(int Lista_1, int Lista_2)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspPP_compararListaMateriales");//1.2.5
            db.AddInParameter(cmd, "@prmintLista1", DbType.Int64, Lista_1);
            db.AddInParameter(cmd, "@prmintLista2", DbType.Int64, Lista_2);

            try
            {
                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable getMaterialByCodigo(int Empresa, int Centro, string Codigo)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspPP_getMaterialByCodigo");//1.2.5
            db.AddInParameter(cmd, "@prminEmpresa", DbType.Int64, Empresa);
            db.AddInParameter(cmd, "@prmintCentro", DbType.Int64, Centro);
            db.AddInParameter(cmd, "@prmstrCodigo", DbType.String, Codigo);
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