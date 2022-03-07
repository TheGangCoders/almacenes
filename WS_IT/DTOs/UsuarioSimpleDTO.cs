using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WS_IT.DTOs
{
    public class UsuarioSimpleDTO
    {
        public int IdUsuario { get; set; }
        public int IdEmpresa { get; set; }
        public string Email { get; set; }
    }
}