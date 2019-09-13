using System;
using System.Collections.Generic;

namespace Dominio.Entidades
{
    public class Acesso
    {
        public Acesso()
        {
            Usuarios = new HashSet<Usuario>();
        }

        public int AcessoId { get; set; }

        public string NomeAcesso { get; set; }

        public string DescricaoAcesso { get; set;}

        public DateTime DataCadastro { get; set; }

        public bool Ativo { get; set; }

        public virtual ICollection<Usuario> Usuarios { get; set; }
    }
}