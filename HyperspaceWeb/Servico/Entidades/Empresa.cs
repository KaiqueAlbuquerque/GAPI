namespace Servico
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Empresa")]
    public partial class Empresa
    {      
        public int Id_Empresa { get; set; }
        public string Nome { get; set; }
        public DateTime Data_Cadastro { get; set; }
        public int Codigo_Empresa { get; set; }
        public bool Ativo { get; set; }
        public virtual ICollection<Ambiente> Ambiente { get; set; }
        public virtual ICollection<Logs> Logs { get; set; }
        public virtual ICollection<Token> Tokens { get; set; }
    }
}
