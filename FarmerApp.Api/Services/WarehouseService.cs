using FarmerApp.Api.Extensions;
using FarmerApp.Data;
using FarmerApp.Data.DTO;
using Microsoft.EntityFrameworkCore;

namespace FarmerApp.Api.Services;

public class WarehouseService
{
    private readonly FarmerDbContext _db;

    public WarehouseService(FarmerDbContext db)
    {
        _db = db;
    }

    public async Task<List<WarehouseDTO>> GetAllAsync()
    {
        var locations = await _db.Warehouses
            .Include(w => w.Product)
            .ThenInclude(p => p.MeasurementUnit)
            .ToListAsync();
        return Enumerable.ToList(locations.Select(l => l.ToWarehouseDte()));
    }
}