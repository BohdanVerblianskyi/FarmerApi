using FarmerApp.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace FarmerApp.Api;

public class FarmerDbContext : DbContext
{
    public DbSet<Location> Locations { get; set; }
    public DbSet<Spend> Spends { get; set; }
    public DbSet<SpendType> SpendTypes { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<MeasurementUnit> MeasurementUnits { get; set; }
    public DbSet<ProductType> ProductTypes { get; set; }
    public DbSet<Warehouse> Warehouses { get; set; }
    public DbSet<WarehouseReception> WarehouseReceptions { get; set; }
    public DbSet<WithdrawalFromWarehouse> WithdrawalFromWarehouses { get; set; }
    
    public DbSet<OwnResource> OwnResources { get; set; }

    public FarmerDbContext(DbContextOptions<FarmerDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<MeasurementUnit>().HasData(new MeasurementUnit { Id = 1, Name = "Кілограми" });
        modelBuilder.Entity<MeasurementUnit>().HasData(new MeasurementUnit { Id = 2, Name = "Літри" });
        modelBuilder.Entity<MeasurementUnit>().HasData(new MeasurementUnit { Id = 3, Name = "Тони" });

        modelBuilder.Entity<ProductType>().HasData(new ProductType { Id = 1, Name = "Мінеральні добрива" });
        modelBuilder.Entity<ProductType>().HasData(new ProductType { Id = 2, Name = "Засоби захисту" });
        modelBuilder.Entity<ProductType>().HasData(new ProductType { Id = 3, Name = "Насіння" });
        modelBuilder.Entity<ProductType>().HasData(new ProductType { Id = 4, Name = "Паливо" });
        modelBuilder.Entity<ProductType>().HasData(new ProductType { Id = 5, Name = "інше" });

        modelBuilder.Entity<OwnResource>().HasData(new OwnResource { Id = 1, Name = "Вода" });
        modelBuilder.Entity<OwnResource>().HasData(new OwnResource { Id = 2, Name = "Зерно" });

        modelBuilder.Entity<SpendType>().HasData(new SpendType { Id = 1, Name = "Продукти із складу" });
        modelBuilder.Entity<SpendType>().HasData(new SpendType { Id = 2, Name = "Власні русурси" });
        modelBuilder.Entity<SpendType>().HasData(new SpendType { Id = 3, Name = "Зарплати працівникам" });
    }
}