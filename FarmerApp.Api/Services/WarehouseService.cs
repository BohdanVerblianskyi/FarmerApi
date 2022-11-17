using FarmerApp.Api.DTO;
using FarmerApp.Api.Extensions;
using Microsoft.EntityFrameworkCore;

namespace FarmerApp.Api.Services;

public class WarehouseService
{
    private readonly FarmerDbContext _db;

    public WarehouseService(FarmerDbContext db)
    {
        _db = db;
    }

    public async Task<List<WarehouseDto>> GetAllAsync()
    {
        var locations = await _db.Warehouses
            .Include(w => w.Product)
            .ThenInclude(p => p.MeasurementUnit)
            .ToListAsync();
        return Enumerable.ToList(locations.Select(l => l.ToWarehouseDte()));
    }
}