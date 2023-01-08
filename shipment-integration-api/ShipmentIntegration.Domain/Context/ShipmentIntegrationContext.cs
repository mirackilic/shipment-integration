using Microsoft.EntityFrameworkCore;
using ShipmentIntegration.Domain.Entities;

namespace ShipmentIntegration.Domain.Context;

public class ShipmentIntegrationContext : DbContext, IDisposable, IShipmentIntegrationContext
{
    public ShipmentIntegrationContext(DbContextOptions<ShipmentIntegrationContext> options) : base(options)
    {
    }

    public DbSet<Shipment> Shipments { get; set; }
    public DbSet<Material> Materials { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Shipment>()
            .HasOne(p => p.Material)
            .WithMany(m => m.Shipments)
            .HasForeignKey(p => p.MaterialId);

        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseLazyLoadingProxies();
    }
}
