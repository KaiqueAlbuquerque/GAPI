using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades
{
    public class Cliente
    {
        public int ClienteId { get; set; }
        public string NomeCliente { get; set; }
        public bool Ativo { get; set; }
        public int ApiPertencente { get; set; }
        public DateTime DataCadastro { get; set; }
        public virtual IEnumerable<Usuario> Usuarios { get; set; }
    }
}
