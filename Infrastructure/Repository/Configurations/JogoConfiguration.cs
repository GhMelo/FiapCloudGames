using Core.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Repository.Configurations
{
    public class JogoConfiguration : IEntityTypeConfiguration<Jogo>
    {
        public void Configure(EntityTypeBuilder<Jogo> builder)
        {
            builder.ToTable("Jogo");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).HasColumnType("INT");
            builder.Property(p => p.DataCriacao).HasColumnName("DataCriacao").HasColumnType("DATETIME").IsRequired();
            builder.Property(p => p.Titulo).HasColumnName("Titulo").HasColumnType("VARCHAR(100)").IsRequired();
            builder.Property(p => p.Produtora).HasColumnName("Produtora").HasColumnType("VARCHAR(100)").IsRequired();
            builder.Property(p => p.UsuarioCadastroId).HasColumnName("UsuarioCadastroId").HasColumnType("INT").IsRequired();

            builder.HasOne(j => j.UsuarioCadastro)
            .WithMany(u => u.JogosCadastrados)
            .HasForeignKey(j => j.UsuarioCadastroId);
        }
    }
}
