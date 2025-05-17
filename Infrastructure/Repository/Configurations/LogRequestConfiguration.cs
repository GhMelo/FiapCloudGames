using Core.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Repository.Configurations
{
    internal class LogRequestConfiguration : IEntityTypeConfiguration<LogRequest>
    {
        public void Configure(EntityTypeBuilder<LogRequest> builder)
        {
            builder.ToTable("LogRequest");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).HasColumnType("INT");
            builder.Property(p => p.CorrelationId).HasColumnName("CorrelationId").HasColumnType("VARCHAR(100)").IsRequired();
            builder.Property(p => p.Path).HasColumnName("Path").HasColumnType("VARCHAR(100)").IsRequired();
            builder.Property(p => p.Method).HasColumnName("Method").HasColumnType("VARCHAR(100)").IsRequired();
            builder.Property(p => p.StatusCode).HasColumnName("StatusCode").HasColumnType("INT").IsRequired();
            builder.Property(p => p.Timestamp).HasColumnName("Timestamp").HasColumnType("DATETIME").IsRequired();
            builder.Property(p => p.ExecutionTimeMs).HasColumnName("ExecutionTimeMs").HasColumnType("FLOAT").IsRequired();
            builder.Property(p => p.ErrorMessage).HasColumnName("ErrorMessage").HasColumnType("VARCHAR(1000)").IsRequired();
        }
    }
}
