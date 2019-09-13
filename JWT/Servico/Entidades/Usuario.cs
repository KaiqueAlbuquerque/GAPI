using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dominio.Entidades
{
    public class Usuario
    {
        public Usuario()
        {
            Acessos = new HashSet<Acesso>();
        }

        public int UsuarioId { get; set; }

        public string Nome { get; set; }

        public string Email { get; set; }

        public string Senha { get; set; }

        public DateTime DataCadastro { get; set; }

        public bool Ativo { get; set; }

        public virtual ICollection<Acesso> Acessos { get; set; }
    }
}