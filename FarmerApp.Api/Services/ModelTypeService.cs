using FarmerApp.Api.Extensions;
using FarmerApp.Data;
using FarmerApp.Data.DTO;
using FarmerApp.Data.Models;
using FarmerApp.Data.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FarmerApp.Api.Services;

public class ModelTypeService
{
    private readonly FarmerDbContext _db;

    public ModelTypeService(FarmerDbContext db)
    {
        _db = db;
    }

    public async Task<List<ModelTypeDTO>> GetAllAsync<TModel>() where TModel : IModelType
    {
        if (typeof(TModel) == typeof(MeasurementUnit))
        {
            var model = await _db.MeasurementUnits.ToListAsync();
            return model.Select(l => l.ToModelTypeDto()).ToList();
        }

        if (typeof(TModel) == typeof(ProductType))
        {
            var model = await _db.ProductTypes.ToListAsync();
            return model.Select(l => l.ToModelTypeDto()).ToList();
        }

        if (typeof(TModel) == typeof(OwnResource))
        {
            var model = await _db.OwnResources.ToListAsync();
            return model.Select(l => l.ToModelTypeDto()).ToList();
        }

        if (typeof(TModel) == typeof(Product))
        {
            var model = await _db.Products.ToListAsync();
            return model.Select(l => l.ToModelTypeDto()).ToList();
        }

        throw new NotImplementedException();
    }
}