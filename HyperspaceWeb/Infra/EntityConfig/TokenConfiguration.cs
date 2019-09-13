using Servico;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.EntityConfig
{
    public class TokenConfiguration : EntityTypeConfiguration<Token>
    {
        public TokenConfiguration()
        {
            HasKey(e => e.Id_Token);

            Property(e => e.Token_Key)
                .IsUnicode(false)
                .HasMaxLength(25)
                .IsRequired();

            Property(e => e.Data_Expiracao)
                .IsOptional();

            Property(e => e.Data_Ativacao)
                .IsOptional();

            HasRequired(e => e.Empresas)
                .WithMany()
                .HasForeignKey(e => e.Id_Empresa);
        }
    }
}
