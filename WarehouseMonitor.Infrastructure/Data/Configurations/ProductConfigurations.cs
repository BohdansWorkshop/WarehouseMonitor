using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WarehouseMonitor.Domain.Constants;
using WarehouseMonitor.Domain.Entities;

namespace WarehouseMonitor.Infrastructure.Persistence.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name)
            .HasMaxLength(ValidationConstants.Product.NameMaxLength)
            .IsRequired();

        builder.Property(p => p.SKU)
            .HasMaxLength(ValidationConstants.Product.SkuMaxLength)
            .IsUnicode(false)
            .IsRequired();

        builder.HasIndex(p => p.SKU)
            .IsUnique();

        builder.Property(p => p.Description)
            .HasMaxLength(ValidationConstants.Product.DescriptionMaxLength);

        builder.Property(p => p.Type)
            .HasConversion<string>()
            .IsRequired();
    }
}