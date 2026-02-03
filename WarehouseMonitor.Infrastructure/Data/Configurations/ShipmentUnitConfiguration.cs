using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WarehouseMonitor.Domain.Entities;

namespace WarehouseMonitor.Infrastructure.Data.Configurations;

public class ShipmentUnitConfiguration : IEntityTypeConfiguration<ShipmentUnit>
{
    public void Configure(EntityTypeBuilder<ShipmentUnit> builder)
    {
        builder.HasKey(s => s.Id);

        builder.Property(s => s.TrackingNumber)
            .HasMaxLength(30)
            .IsUnicode(false)
            .IsRequired();

        builder.HasIndex(s=>s.TrackingNumber)
            .IsUnique()
            .HasDatabaseName("IX_ShipmentUnit_TrackingNumber");

        builder.Property(s => s.Status)
            .HasConversion<string>()
            .HasMaxLength(30)
            .IsRequired();

        builder.Property(s => s.Weight)
            .HasColumnType("decimal(12,3)"); // <= 1 gramm

        builder.Property(s => s.Volume)
            .HasColumnType("decimal(12,5)"); // high precision for m^3 volumes


        builder.HasOne(s => s.Product)
            .WithMany()
            .HasForeignKey(s => s.ProductId)
            .OnDelete(DeleteBehavior.Restrict); // Prevent product removal in case of existing shipment units

        builder.HasOne(s => s.CurrentWarehouse)
            .WithMany()
            .HasForeignKey(s => s.CurrentWarehouseId)
            .OnDelete(DeleteBehavior.SetNull); // When warehouse is deleted, set CurrentWarehouseId to null
    }
}
