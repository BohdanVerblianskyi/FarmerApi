using FarmerApp.Api.Extensions;
using FarmerApp.Api.Models;
using FarmerApp.Data;
using FarmerApp.Data.DTO;
using FarmerApp.Data.Models;
using FarmerApp.Data.ViewModels.WarehouseReception;
using Microsoft.EntityFrameworkCore;

namespace FarmerApp.Api.Services;

public class WarehouseReceptionService
{
    private readonly FarmerDbContext _db;

    public WarehouseReceptionService(FarmerDbContext db)
    {
        _db = db;
    }

    public async Task<List<WarehouseReceptionDTO>> GetAllAsync()
    {
        var warehouseReceptions = await _db.WarehouseReceptions
            .Include(w => w.Product)
            .ThenInclude(p => p.MeasurementUnit)
            .OrderBy(w => w.Date)
            .ToListAsync();

        return Enumerable.Reverse(warehouseReceptions.Select(w => w.ToWarehouseReceptionDto())).ToList();
    }

    public async Task<List<WarehouseReceptionDTO>> GetAllAsync(int pageNumber, int pageSize)
    {
        var warehouseReceptions = await _db.WarehouseReceptions
            .Include(w => w.Product)
            .ThenInclude(p => p.MeasurementUnit)
            .OrderBy(w => w.Date)
            .Skip(pageNumber * (pageNumber - 1))
            .Take(pageSize)
            .ToListAsync();

        return Enumerable.Reverse(warehouseReceptions.Select(w => w.ToWarehouseReceptionDto())).ToList();
    }

    public async Task<WarehouseReceptionDTO> ProcessingNetProductAsync(WarehouseReceptionWithNewVM reception)
    {
        var theSameProduct = await _db.Products
            .Include(p => p.MeasurementUnit)
            .FirstOrDefaultAsync(p => p.Name == reception.ProductName);

        if (theSameProduct != null)
        {
            if (theSameProduct.MeasurementUnit.Id != reception.MeasurementUnitId)
            {
                throw new DirectoryNotFoundException();
            }

            return await ProcessTheSameProductAsync(new WarehouseReceptionWithTheSameVM
            {
                Invoice = reception.Invoice,
                Price = reception.Price,
                Quantity = reception.Quantity,
                ProductId = theSameProduct.Id
            });
        }
        
        var measurementUnit = await _db.MeasurementUnits.FirstOrDefaultAsync(m => m.Id == reception.MeasurementUnitId);
        
        var newProduct = await _db.Products.AddAsync(new Product
        {
            Name = reception.ProductName,
            Price = (float)Math.Round(reception.Price / reception.Quantity, 2),
            MeasurementUnitId = reception.MeasurementUnitId,
            ProductTypeId = reception.ProductTypeId,
        });

        await _db.SaveChangesAsync();

        await _db.WarehouseReceptions
            .AddAsync(new WarehouseReception
            {
                Date = DateTime.UtcNow,
                Invoice = reception.Invoice,
                Price = reception.Price,
                ProductId = newProduct.Entity.Id,
                Quantity = reception.Quantity,
            });

        await _db.SaveChangesAsync();

        await _db.Warehouses.AddAsync(new Warehouse
        {
            ProductId = newProduct.Entity.Id,
            Quantity = reception.Quantity
        });
        
        await _db.SaveChangesAsync();
        
        var result = new WarehouseReceptionDTO
        {
            Date = DateTime.UtcNow,
            Invoice = reception.Invoice,
            Price = reception.Price,
            Quantity = reception.Quantity,
            ProductName = newProduct.Entity.Name,
            MeasurementUnitsName = measurementUnit.Name
        };

        return result;
    }

    public async Task<WarehouseReceptionDTO> ProcessTheSameProductAsync(WarehouseReceptionWithTheSameVM reception)
    {
        var currentProduct = await _db.Products.Include(p => p.MeasurementUnit)
            .FirstOrDefaultAsync(p => p.Id == reception.ProductId);

        if (currentProduct == null)
        {
            throw new ArgumentNullException(nameof(_db.Products));
        }

        var currentWarehouse = await _db.Warehouses.FirstOrDefaultAsync(w => w.ProductId == currentProduct.Id);

        var actualPrice = reception.Price;

        if (currentWarehouse == null)
        {
            await _db.Warehouses.AddAsync(new Warehouse
            {
                ProductId = reception.ProductId,
                Quantity = reception.Quantity
            });

            await _db.SaveChangesAsync();
        }
        else
        {
            var allQuantity = currentWarehouse.Quantity + reception.Quantity;
            var allPrice = (currentProduct.Price * currentWarehouse.Quantity) + reception.Price ;

            actualPrice = (float)Math.Round(allPrice / allQuantity, 2);

            currentWarehouse.Quantity = allQuantity;

            _db.Warehouses.Update(currentWarehouse);

            await _db.SaveChangesAsync();
        }


        currentProduct.Price = actualPrice;
        _db.Products.Update(currentProduct);

        var warehouseReception = await _db.WarehouseReceptions.AddAsync(new WarehouseReception
        {
            Date = DateTime.UtcNow,
            Invoice = reception.Invoice,
            Price = reception.Price,
            ProductId = reception.ProductId,
            Quantity = reception.Quantity,
        });

        await _db.SaveChangesAsync();

        return new WarehouseReceptionDTO
        {
            Date = DateTime.Now,
            Invoice = reception.Invoice,
            Price = reception.Price,
            Quantity = reception.Quantity,
            ProductName = currentProduct.Name,
            MeasurementUnitsName = currentProduct.MeasurementUnit.Name
        };
    }
}