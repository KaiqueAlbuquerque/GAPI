namespace Servico
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Usuarios
    {        
        public int Id_User { get; set; }
        public int IdUsuarioDv { get; set; }
        public int Id_Token { get; set; }
        public virtual Token Token { get; set; }
    }
}
