namespace Infra
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Data.Entity.ModelConfiguration.Conventions;
    using Infra.EntityConfig;
    using Servico;

    public partial class Context : DbContext
    {
        public Context()
            : base("BancoDados")
        {
        }

        public virtual DbSet<Ambiente> Ambiente { get; set; }
        public virtual DbSet<Empresa> Empresa { get; set; }
        public virtual DbSet<Logs> Logs { get; set; }
        public virtual DbSet<Token> Token { get; set; }
        public virtual DbSet<Usuarios> Usuarios { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            modelBuilder.Properties<string>()
               .Configure(p => p.HasColumnType("varchar"));

            modelBuilder.Properties<string>()
                .Configure(p => p.HasMaxLength(100));

            modelBuilder.Configurations.Add(new AmbienteConfiguration());
            modelBuilder.Configurations.Add(new EmpresaConfiguration());
            modelBuilder.Configurations.Add(new LogsConfiguration());
            modelBuilder.Configurations.Add(new TokenConfiguration());
            modelBuilder.Configurations.Add(new UsuarioConfiguration());
        }

        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("Data_Cadastro") != null))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("Data_Cadastro").CurrentValue = DateTime.Now;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Property("Data_Cadastro").IsModified = false;
                }
            }
            return base.SaveChanges();
        }
    }
}
