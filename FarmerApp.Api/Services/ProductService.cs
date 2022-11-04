using FarmerApp.Api.Extensions;
using FarmerApp.Data;
using FarmerApp.Data.DTO;
using Microsoft.EntityFrameworkCore;

namespace FarmerApp.Api.Services;

public class ProductService
{
    private readonly FarmerDbContext _db;

    public ProductService(FarmerDbContext db)
    {
        _db = db;
    }

    public async Task<IEnumerable<ProductDTO>> GetByType(int id)
    {
        var products = await _db.Products.Where(p => p.ProductTypeId == id).ToListAsync();
        return Enumerable.ToList(products.Select(l => l.ToProductDto()));
    }

    public async Task<IEnumerable<ProductDTO>> Get()
    {
        var locations = await _db.Products.ToListAsync();
        return locations.Select(l => l.ToProductDto()).ToList();
    }

    public async Task<ProductDTO> Get(int id)
    {
        var products = await _db.Products.Where(p => p.Id == id).ToListAsync();
        return products.Select(l => l.ToProductDto()).Single();
    }
}