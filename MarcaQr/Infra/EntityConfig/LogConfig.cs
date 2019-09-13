using Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.EntityConfig
{
    public class LogConfig : EntityTypeConfiguration<Log>
    {
        public LogConfig()
        {
            HasKey(l => l.LogId);

            Property(l => l.NomeArquivo)
                .IsRequired()
                .HasMaxLength(1000);

            Property(l => l.TipoAlteracao)
                .IsRequired();

            Property(l => l.QuatindadePaginas)
                .IsRequired();

            Property(l => l.DataCadastro)
                .IsRequired();

            HasRequired(l => l.Usuario)
                .WithMany()
                .HasForeignKey(l => l.UsuarioId);
        }
    }
}
