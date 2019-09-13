using Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.EntityConfig
{
    public class ClienteConfig : EntityTypeConfiguration<Cliente>
    {
        public ClienteConfig()
        {
            HasKey(c => c.ClienteId);

            Property(c => c.NomeCliente)
                .IsRequired()
                .HasMaxLength(255);

            Property(c => c.Ativo)
                .IsRequired();

            Property(c => c.ApiPertencente)
                .IsRequired();

            Property(c => c.DataCadastro)
                .IsRequired();
        }
    }
}
