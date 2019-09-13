using System;
using System.Collections.Generic;

namespace ApiAlteraPDF.ViewModel
{
    public class AcessosApiLoginViewModel
    {
        public AcessosApiLoginViewModel()
        {
            Usuarios = new HashSet<UsuarioApiLoginViewModel>();
        }

        public int AcessoId { get; set; }

        public string NomeAcesso { get; set; }

        public string DescricaoAcesso { get; set; }

        public DateTime DataCadastro { get; set; }

        public bool Ativo { get; set; }

        public virtual ICollection<UsuarioApiLoginViewModel> Usuarios { get; set; }
    }
}