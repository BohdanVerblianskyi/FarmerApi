using AutoMapper;
using FarmerApp.Api.DTO;
using FarmerApp.Api.Extensions;
using FarmerApp.Api.Models;
using FarmerApp.Api.ViewModels.Spending;
using Microsoft.EntityFrameworkCore;

namespace FarmerApp.Api.Services;

public class SpendService
{
    private const int FromWarehouseSpendType = 1;
    private const int OwnSpendType = 2;
    private const int SalarySpendType = 3;
    private const string FromWarehouseMessage = "Витрати із складу:";
    private const string SalaryMessage = "Зарплата:";
    private const string OwnMessage = "Власні витрати:";

    private readonly FarmerDbContext _db;
    private readonly IMapper _mapper;

    public SpendService(FarmerDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<LocationSpendDto> GetLocationSpendAsync(int id)
    {
        var spending = await _db.Spends.Where(s => s.LocationId == id).ToListAsync();
        var result = spending.Select(ToSpendDto).ToList();

        return new LocationSpendDto
        {
            Spendings = result,
            Sum = result.Sum(s => s.Price)
        };
    }

    public async Task<SpendDto> AddAsync(SpendFromWarehouseVM fromWarehouse)
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
        return ToSpendDto(spend.Entity);
    }

    public async Task<SpendDto> AddAsync(SpendSalaryVM salary)
    {
        var spend = await _db.Spends.AddAsync(new Spend
        {
            Description = $"{SalaryMessage} {salary.Employee}",
            LocationId = salary.LocationId,
            SpendTypeId = SalarySpendType,
            Price = salary.Price,
            Date = DateTime.UtcNow
        });

        await _db.SaveChangesAsync();
        return ToSpendDto(spend.Entity);
    }

    public async Task<SpendDto> AddAsync(SpendOwnVM own)
    {
        var spend = await _db.Spends.AddAsync(new Spend
        {
            Description = $"{OwnMessage} {own.OwnResourceName}",
            LocationId = own.LocationId,
            SpendTypeId = OwnSpendType,
            Price = own.Price,
            Date = DateTime.UtcNow
        });

        await _db.SaveChangesAsync();
        return ToSpendDto(spend.Entity);
    }
    
    private SpendDto ToSpendDto(Spend spend)
    {
        return _mapper.Map<SpendDto>(spend);
    }
}