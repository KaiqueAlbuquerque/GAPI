namespace Servico
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Logs
    {
        public int Id_Logs { get; set; }
        public string Pasta { get; set; }
        public string Nome { get; set; }
        public byte Baixando { get; set; }
        public byte Tipo { get; set; }
        public int Tamanho { get; set; }
        public short UsuarioId { get; set; }
        public int Id_Empresa { get; set; }
        public DateTime criacao { get; set; }
        public virtual Empresa Empresa { get; set; }
    }
}
