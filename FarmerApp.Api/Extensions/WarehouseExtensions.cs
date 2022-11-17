using FarmerApp.Api.DTO;
using FarmerApp.Api.Models;

namespace FarmerApp.Api.Extensions;

public static class WarehouseExtensions
{
    public static WarehouseDto ToWarehouseDte(this Warehouse warehouse)
    {
        return new WarehouseDto
        {
            Price = warehouse.Product.GetPrice(warehouse.Quantity),
            Quantity = warehouse.Quantity,
            ProductName = warehouse.Product.Name,
            MeasurementUnitName = warehouse.Product.MeasurementUnit.Name,
            PriceByUnit = warehouse.Product.Price
        };
    }
}