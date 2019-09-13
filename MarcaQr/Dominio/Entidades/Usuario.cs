using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades
{
    public class Usuario
    {
        public int UsuarioId { get; set; }
        public int IdLogin { get; set; }
        public DateTime DataCadastro { get; set; }
        public bool Ativo { get; set; }
        public int ClienteId { get; set; }        
        public virtual Cliente Cliente { get; set; }
        public virtual IEnumerable<Log> Logs { get; set; }
    }
}
