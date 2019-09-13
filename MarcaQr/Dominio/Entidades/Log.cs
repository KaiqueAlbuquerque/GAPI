using System;

namespace Dominio.Entidades
{
    public class Log
    {
        public int LogId { get; set; }
        public string NomeArquivo { get; set; }
        public int TipoAlteracao { get; set; }
        public int QuatindadePaginas { get; set; }
        public DateTime DataCadastro { get; set; }
        public int UsuarioId { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
