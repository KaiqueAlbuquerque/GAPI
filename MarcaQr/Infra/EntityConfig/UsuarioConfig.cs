using Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.EntityConfig
{
    public class UsuarioConfig : EntityTypeConfiguration<Usuario>
    {
        public UsuarioConfig()
        {
            HasKey(u => u.UsuarioId);

            Property(u => u.IdLogin)
                .IsRequired();

            Property(u => u.DataCadastro)
                .IsRequired();

            Property(u => u.Ativo)
                .IsRequired();

            HasRequired(u => u.Cliente)
                .WithMany()
                .HasForeignKey(u => u.ClienteId);
        }
    }
}
