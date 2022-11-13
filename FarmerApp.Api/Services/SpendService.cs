using FarmerApp.Api.Extensions;
using FarmerApp.Api.Models;
using FarmerApp.Data;
using FarmerApp.Data.DTO;
using FarmerApp.Data.ViewModels.Spending;
using Microsoft.EntityFrameworkCore;

namespace FarmerApp.Api.Services;

public class SpendService
{
    private const int FromWarehouseSpendType = 1;
    private const int OwnSpendType = 2;
    private const int SalarySpendType = 3;
    private const string FromWarehouseMessage = "Витрати із складу:";
    private const string SalaryMessage = "Витрати із складу:";
    private const string OwnMessage = "Витрати із складу:";

    private readonly FarmerDbContext _db;

    public SpendService(FarmerDbContext db)
    {
        _db = db;
    }

    public async Task<LocationSpendDTO> GetLocationSpendAsync(int id)
    {
        var spending = await _db.Spends.Where(s => s.LocationId == id).ToListAsync();
        var result = Enumerable.ToList(spending.Select(s => s.ToSpendDto()));

        return new LocationSpendDTO
        {
            Spendings = result,
            Sum = result.Sum(s => s.Price)
        };
    }

    public async Task<SpendDTO> AddAsync(SpendFromWarehouseVM fromWarehouse)
    {
        var product = await _db.Products.FirstOrDefaultAsync(p => p.Id == fromWarehouse.ProductId);
        var warehouse = await _db.Warehouses.FirstOrDefaultAsync(w => w.ProductId == product.Id);

        if (!warehouse.TrySubtract(fromWarehouse.Quantity))
        {
            throw new Exception(); //////
        }

        var withdrawalFromWarehouse = await _db.WithdrawalFromWarehouses.AddAsync(new WithdrawalFromWarehouse
        {
            Date = DateTime.UtcNow,
            Price = product.GetPrice(fromWarehouse.Quantity),
            ProductId = product.Id,
            Quantity = fromWarehouse.Quantity
        });
        await _db.SaveChangesAsync();

        var spend = await _db.Spends.AddAsync(new Spend
        {
            WithdrawalFromWarehouseId = withdrawalFromWarehouse.Entity.Id,
            Description = $"{FromWarehouseMessage} {product.Name}",
            LocationId = fromWarehouse.LocationId,
            Price = product.GetPrice(fromWarehouse.Quantity),
            SpendTypeId = FromWarehouseSpendType,
            Date = DateTime.UtcNow
        });

        await _db.SaveChangesAsync();
        return spend.Entity.ToSpendDto();
    }

    public async Task<SpendDTO> AddAsync(SpendSalaryVM salary)
    {
        var spend = await _db.Spends.AddAsync(new Spend
        {
            Description = $"{SalaryMessage}: {salary.Employee}",
            LocationId = salary.LocationId,
            SpendTypeId = SalarySpendType,
            Price = salary.Price,
            Date = DateTime.UtcNow
        });

        await _db.SaveChangesAsync();
        return spend.Entity.ToSpendDto();
    }

    public async Task<SpendDTO> AddAsync(SpendOwnVM own)
    {
        var spend = await _db.Spends.AddAsync(new Spend
        {
            Description = $"{OwnMessage}: {own.OwnResourceName}",
            LocationId = own.LocationId,
            SpendTypeId = OwnSpendType,
            Price = own.Price,
            Date = DateTime.UtcNow
        });

        await _db.SaveChangesAsync();
        return spend.Entity.ToSpendDto();
    }
}