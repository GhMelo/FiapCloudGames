using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Repository.Configurations
{
    public class PromocaoConfiguration : IEntityTypeConfiguration<Promocao>
    {
        public void Configure(EntityTypeBuilder<Promocao> builder)
        {
            builder.ToTable("Promocao", b =>
            {
                b.HasCheckConstraint("CK_Promocao_Porcentagem", "[Porcentagem] >= 0 AND [Porcentagem] <= 100");
            });
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).HasColumnType("INT");
            builder.Property(p => p.DataCriacao).HasColumnName("DataCriacao").HasColumnType("DATETIME").IsRequired();
            builder.Property(p => p.NomePromocao).HasColumnName("NomePromocao").HasColumnType("VARCHAR(100)").IsRequired();
            builder.Property(p => p.Porcentagem).HasColumnName("Porcentagem").HasColumnType("INT").IsRequired();
            builder.Property(p => p.PromocaoAtiva).HasColumnName("PromocaoAtiva").HasColumnType("BIT").IsRequired();
            builder.Property(p => p.JogoId).HasColumnName("JogoId").HasColumnType("INT").IsRequired();

            builder.HasOne(p => p.JogoPromocao)
                .WithMany(p=>p.PromocoesAderidas)
                .HasForeignKey(p => p.JogoId);
        }
    }
}
