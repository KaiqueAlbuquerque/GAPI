using Servico;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.EntityConfig
{
    public class EmpresaConfiguration : EntityTypeConfiguration<Empresa>
    {
        public EmpresaConfiguration()
        {
            HasKey(e => e.Id_Empresa);

            Property(e => e.Nome)
                .IsUnicode(false)
                .HasMaxLength(100)
                .IsRequired();

            Property(e => e.Data_Cadastro)
                .HasColumnType("smalldatetime")
                .IsRequired();
                        
            Property(e => e.Codigo_Empresa)
                .IsRequired();

            Property(e => e.Ativo)
                .IsRequired();

            HasMany(e => e.Ambiente)
                .WithRequired(e => e.Empresa)
                .WillCascadeOnDelete(false);

            HasMany(e => e.Logs)
                .WithRequired(e => e.Empresa)
                .WillCascadeOnDelete(false);
        }
    }
}
