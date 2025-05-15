using Core.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Repository.Configurations
{
    public class UsuarioJogoAdquiridoConfiguration : IEntityTypeConfiguration<UsuarioJogoAdquirido>
    {
        public void Configure(EntityTypeBuilder<UsuarioJogoAdquirido> builder)
        {

            builder.ToTable("UsuarioJogoAdquirido");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).HasColumnType("INT");
            builder.Property(p => p.DataCriacao).HasColumnName("DataCriacao").HasColumnType("DATETIME").IsRequired();
            builder.Property(p => p.UsuarioId).HasColumnName("UsuarioId").HasColumnType("INT").IsRequired();
            builder.Property(p => p.JogoId).HasColumnName("JogoId").HasColumnType("INT").IsRequired();

            builder.HasOne(p => p.Usuario)
           .WithMany(u => u.JogosAdquiridos)
           .HasForeignKey(p => p.UsuarioId);

            builder.HasOne(p => p.Jogo)
           .WithMany(u => u.UsuariosQueAdquiriram)
           .HasForeignKey(p => p.JogoId);

        }
    }
}
