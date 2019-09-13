using Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.EntityConfig
{
    public class AcessoConfig : EntityTypeConfiguration<Acesso>
    {
        public AcessoConfig()
        {
            HasKey(a => a.AcessoId);

            Property(a => a.NomeAcesso)
                .IsRequired();

            Property(a => a.DescricaoAcesso)
                .IsRequired();

            Property(a => a.Ativo)
                .IsRequired();

            Property(a => a.DataCadastro)
                .IsRequired();

            HasMany(u => u.Usuarios)
                .WithMany(u => u.Acessos)
                .Map(u =>
                {
                    u.ToTable("UsuarioAcesso");
                    u.MapLeftKey("AcessoId"); 
                    u.MapRightKey("UsuarioId");
                });
        }
    }
}
