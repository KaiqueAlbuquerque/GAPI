using Servico;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.EntityConfig
{
    public class AmbienteConfiguration : EntityTypeConfiguration<Ambiente>
    {
        public AmbienteConfiguration()
        {
            HasKey(e => e.Id_Ambiente);
                        
            Property(e => e.Logo)
                .IsUnicode(false)
                .HasMaxLength(50)
                .IsRequired();
            
            Property(e => e.Color)
                .IsUnicode(false)
                .HasMaxLength(10)
                .IsRequired();

            Property(e => e.LogoClass)
                .IsUnicode(false)
                .HasMaxLength(150)
                .IsRequired();

            Property(e => e.URL)
                .IsUnicode(false)
                .HasMaxLength(150)
                .IsRequired();

            HasRequired(e => e.Empresa)
                .WithMany()
                .HasForeignKey(e => e.Id_Empresa);
        }
    }
}
