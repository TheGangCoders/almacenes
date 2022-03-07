using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Token
{
    public class ValueJWT
    {
        public Int32 IdUsuario { get; set; }
        public String Nombres { get; set; }
        public String ApellidoPaterno { get; set; }
        public String ApellidoMaterno { get; set; }
        public String TipoUsuario { get; set; }
        public String Login { get; set; }
        public String Email { get; set; }
        public String Sociedad { get; set; }
        public String Sociedades { get; set; }
        public Int32 IdSociedad { get; set; }
        public Int32 IdEmpresa { get; set; }
        public Boolean Autenticado { get; set; }

        public List<int> getListSociedades() {
            List<int> lista = new List<int>();
            string[] SociedadesSplit = Sociedades.Split(',');
            for(int i = 0; i < SociedadesSplit.Length; i++) {
                try{
                    lista.Add(Convert.ToInt32(SociedadesSplit[i]));
                }catch(Exception ex){
                }
            }
            return lista;
        }
    }
}
