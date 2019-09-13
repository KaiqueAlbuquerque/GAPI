using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.ViewModel
{
    public class EmpresaViewModel
    {
        public string Id_Empresa { get; set; }
        public string Nome { get; set; }
        public string Data_Cadastro { get; set; }
        public string Codigo_Empresa { get; set; }
        public bool Ativo { get; set; }
    }
}