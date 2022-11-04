using FarmerApp.Data.DTO;
using FarmerApp.Data.Models;

namespace FarmerApp.Api.Extensions;

public static class WarehouseExtensions
{
    public static WarehouseDTO ToWarehouseDte(this Warehouse warehouse)
    {
        return new WarehouseDTO
        {
            Price = warehouse.Product.GetPrice(warehouse.Quantity),
            Quantity = warehouse.Quantity,
            ProductName = warehouse.Product.Name,
            MeasurementUnitsName = warehouse.Product.MeasurementUnit.Name
        };
    }
}