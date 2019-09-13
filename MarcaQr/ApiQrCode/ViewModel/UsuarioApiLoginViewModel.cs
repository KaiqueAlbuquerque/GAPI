using System;
using System.Collections.Generic;

namespace ApiAlteraPDF.ViewModel
{
    public class UsuarioApiLoginViewModel
    {
        public UsuarioApiLoginViewModel()
        {
            Acessos = new HashSet<AcessosApiLoginViewModel>();
        }

        public int UsuarioId { get; set; }

        public string Email { get; set; }

        public string Senha { get; set; }

        public DateTime DataCadastro { get; set; }

        public bool Ativo { get; set; }

        public virtual ICollection<AcessosApiLoginViewModel> Acessos { get; set; }
    }
}