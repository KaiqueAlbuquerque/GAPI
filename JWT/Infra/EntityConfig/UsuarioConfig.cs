using Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace Infra.EntityConfig
{
    public class UsuarioConfig : EntityTypeConfiguration<Usuario>
    {
        public UsuarioConfig()
        {
            HasKey(u => u.UsuarioId);

            Property(u => u.Nome)
                .IsRequired();

            Property(u => u.Email)
                .IsRequired();

            Property(u => u.Senha)
                .IsRequired();

            Property(u => u.DataCadastro)
                .IsRequired();

            Property(a => a.Ativo)
                .IsRequired();
        }
    }
}