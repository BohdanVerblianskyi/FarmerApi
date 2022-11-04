using FarmerApp.Api.Extensions;
using FarmerApp.Data;
using FarmerApp.Data.DTO;
using FarmerApp.Data.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace FarmerApp.Api.Services;

public class LocationService
{
    private readonly FarmerDbContext _db;

    public LocationService(FarmerDbContext db)
    {
        _db = db;
    }

    public async Task<List<LocationDTO>> GetAllAsync()
    {
        var locations = await _db.Location.ToListAsync();
        return locations.Select(l => l.ToLocationDto()).ToList();
    }

    public async Task<LocationDTO> CreateLocation(LocationVM locationVm)
    {
        var location = await _db.Location.AddAsync(locationVm.ToLocation());
        await _db.SaveChangesAsync();
        return location.Entity.ToLocationDto();
    }
}