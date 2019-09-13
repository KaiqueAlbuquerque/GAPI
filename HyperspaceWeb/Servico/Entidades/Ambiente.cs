namespace Servico
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Ambiente")]
    public partial class Ambiente
    {        
        public int Id_Ambiente { get; set; }
        public int Id_Empresa { get; set; }                
        public string Logo { get; set; }
        public string Color { get; set; }
        public string LogoClass { get; set; }
        public string URL { get; set; }
        public virtual Empresa Empresa { get; set; }
    }
}
