namespace Servico
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Token")]
    public partial class Token
    {
        public int Id_Token { get; set; }
        public string Token_Key { get; set; }
        public int Id_Empresa { get; set; }
        public DateTime? Data_Expiracao { get; set; }
        public DateTime? Data_Ativacao { get; set; }
        public virtual Empresa Empresas { get; set; }
    }
}
