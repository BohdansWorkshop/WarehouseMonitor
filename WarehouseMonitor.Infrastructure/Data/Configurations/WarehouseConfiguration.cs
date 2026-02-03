using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WarehouseMonitor.Domain.Entities;

namespace WarehouseMonitor.Infrastructure.Data.Configurations;

public class WarehouseConfiguration : IEntityTypeConfiguration<Warehouse>
{
    public void Configure(EntityTypeBuilder<Warehouse> builder)
    {
        builder.HasKey(w => w.Id);

        builder.Property(w => w.Name)
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(w => w.BranchCode)
            .HasMaxLength(20)
            .IsUnicode(false)
            .IsRequired();

        builder.Property(w => w.Address)
            .HasMaxLength(500)
            .IsRequired();

        builder.HasIndex(w => w.BranchCode)
            .IsUnique();
    }

}
