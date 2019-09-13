using Servico;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.EntityConfig
{
    public class UsuarioConfiguration : EntityTypeConfiguration<Usuarios>
    {
        public UsuarioConfiguration()
        {
            HasKey(e => e.Id_User);
                        
            Property(e => e.IdUsuarioDv)
                .IsRequired();

            HasRequired(e => e.Token)
                .WithMany()
                .HasForeignKey(e => e.Id_Token);
        }
    }
}
