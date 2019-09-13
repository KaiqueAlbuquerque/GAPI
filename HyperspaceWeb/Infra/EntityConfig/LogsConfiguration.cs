using Servico;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.EntityConfig
{
    public class LogsConfiguration : EntityTypeConfiguration<Logs>
    {
        public LogsConfiguration()
        {
            HasKey(e => e.Id_Logs);

            Property(e => e.Pasta)
               .IsUnicode(false)
               .HasMaxLength(200)
               .IsRequired();

            Property(e => e.Nome)
                .IsUnicode(false)
                .HasMaxLength(250)
                .IsRequired();

            Property(e => e.Baixando)
                .IsRequired();

            Property(e => e.Tipo)
                .IsRequired();

            Property(e => e.Tamanho)
                .IsRequired();

            Property(e => e.UsuarioId)
                .IsRequired();

            Property(e => e.criacao)
                .IsRequired();

            HasRequired(e => e.Empresa)
                .WithMany()
                .HasForeignKey(e => e.Id_Empresa);
        }
    }
}
