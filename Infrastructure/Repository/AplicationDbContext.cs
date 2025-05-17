using Core.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Infrastructure.Repository
{
    public class ApplicationDbContext : DbContext
    {
        private readonly string _connectionString;

        public ApplicationDbContext()
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

            _connectionString = configuration.GetConnectionString("ConnectionString");
        }

        public ApplicationDbContext(string connectionString)
        {
            _connectionString = connectionString;

        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Jogo> Jogos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_connectionString);
                optionsBuilder.UseLazyLoadingProxies();
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

            //modelBuilder.ApplyConfiguration(new JogoConfiguration());
            //modelBuilder.ApplyConfiguration(new UsuarioConfiguration());
            //modelBuilder.ApplyConfiguration(new UsuarioJogoAdquiridoConfiguration());

            //modelBuilder.Entity<Usuario>(e =>
            //{
            //    e.ToTable("Usuario");
            //    e.HasKey(p => p.Id);
            //    e.Property(p => p.Id).HasColumnType("INT");
            //    e.Property(p => p.DataCriacao).HasColumnName("DataCriacao").HasColumnType("DATETIME").IsRequired();
            //    e.Property(p => p.Nome).HasColumnName("Nome").HasColumnType("VARCHAR(100)").IsRequired();
            //    e.Property(p => p.Email).HasColumnName("Email").HasColumnType("VARCHAR(100)").IsRequired();
            //    e.Property(p => p.Tipo).HasColumnName("Tipo").HasColumnType("INT").IsRequired();

            //});

            //modelBuilder.Entity<Jogo>(e =>
            //{
            //    e.ToTable("Jogo");
            //    e.HasKey(p => p.Id);
            //    e.Property(p => p.Id).HasColumnType("INT");
            //    e.Property(p => p.DataCriacao).HasColumnName("DataCriacao").HasColumnType("DATETIME").IsRequired();
            //    e.Property(p => p.Titulo).HasColumnName("Titulo").HasColumnType("VARCHAR(100)").IsRequired();
            //    e.Property(p => p.Produtora).HasColumnName("Produtora").HasColumnType("VARCHAR(100)").IsRequired();
            //    e.Property(p => p.UsuarioCadastroId).HasColumnName("UsuarioCadastroId").HasColumnType("INT").IsRequired();

            //    e.HasOne(j => j.UsuarioCadastro)
            //    .WithMany(u => u.JogosCadastrados)
            //    .HasForeignKey(j => j.UsuarioCadastroId);
            //});

            //modelBuilder.Entity<UsuarioJogoAdquirido>(e =>
            //{
            //    e.ToTable("UsuarioJogoAdquirido");
            //    e.HasKey(p => p.Id);
            //    e.Property(p => p.Id).HasColumnType("INT");
            //    e.Property(p => p.DataCriacao).HasColumnName("DataCriacao").HasColumnType("DATETIME").IsRequired();
            //    e.Property(p => p.UsuarioId).HasColumnName("UsuarioCadastroId").HasColumnType("INT").IsRequired();
            //    e.Property(p => p.JogoId).HasColumnName("JogoId").HasColumnType("INT").IsRequired();

            //    e.HasOne(p => p.Usuario)
            //   .WithMany(u => u.JogosAdquiridos)
            //   .HasForeignKey(p => p.UsuarioId);

            //    e.HasOne(p => p.Jogo)
            //   .WithMany(u => u.UsuariosQueAdquiriram)
            //   .HasForeignKey(p => p.JogoId);
            //});
        }
    }
}
