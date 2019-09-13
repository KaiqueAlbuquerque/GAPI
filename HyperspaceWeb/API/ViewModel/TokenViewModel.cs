using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.ViewModel
{
    public class TokenViewModel
    {
        public string Id_Token { get; set; }
        public string Token_Key { get; set; }
        public string Id_Empresa { get; set; }
        public string Data_Expiracao { get; set; }
        public string Data_Ativacao { get; set; }
        public string Quantidade { get; set; }
        public bool Checked { get; set; }
    }
}