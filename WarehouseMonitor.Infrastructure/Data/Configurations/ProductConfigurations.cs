using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WarehouseMonitor.Domain.Entities;

namespace WarehouseMonitor.Infrastructure.Persistence.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name)
            .HasMaxLength(250)
            .IsRequired();

        builder.Property(p => p.SKU)
            .HasMaxLength(50)
            .IsUnicode(false)
            .IsRequired();

        builder.HasIndex(p => p.SKU)
            .IsUnique();

        builder.Property(p => p.Description)
            .HasMaxLength(1000);

        builder.Property(p => p.Type)
            .HasConversion<string>()
            .HasMaxLength(50)
            .IsRequired();
    }
}