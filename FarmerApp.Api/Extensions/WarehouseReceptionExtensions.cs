using FarmerApp.Api.DTO;
using FarmerApp.Api.Models;

namespace FarmerApp.Api.Extensions;

public static class WarehouseReceptionExtensions
{
    public static WarehouseReceptionDto ToWarehouseReceptionDto(this WarehouseReception warehouseReception)
    {
        return new WarehouseReceptionDto
        {
            Date = warehouseReception.Date,
            Invoice = warehouseReception.Invoice,
            Price = warehouseReception.Price,
            Quantity = warehouseReception.Quantity,
            ProductName = warehouseReception.Product.Name,
            MeasurementUnitsName = warehouseReception.Product.MeasurementUnit.Name
        };
    }
}