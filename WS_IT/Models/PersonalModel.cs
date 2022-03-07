using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace WS_IT.Models
{
    public class PersonalModelDAL
    {
        private static readonly PersonalModelDAL _instancia = new PersonalModelDAL();

        public static PersonalModelDAL Instancia
        {
            get { return PersonalModelDAL._instancia; }
        }

        public DataTable getPersonalData(int prmintUsuario)
        {
            Database db = new SqlDatabase(ConfigurationManager.ConnectionStrings["BDPROD"].ToString());
            DbCommand cmd = db.GetStoredProcCommand("uspIT_getPersonalData");
            db.AddInParameter(cmd, "@prmintUsuario", DbType.Int32, prmintUsuario);
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