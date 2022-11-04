using FarmerApp.Data.DTO;
using FarmerApp.Data.Models;
using FarmerApp.Data.ViewModels.WarehouseReception;

namespace FarmerApp.Api.Extensions;

public static class WarehouseReceptionExtensions
{
    public static WarehouseReceptionDTO ToWarehouseReceptionDto(this WarehouseReception warehouseReception)
    {
        return new WarehouseReceptionDTO
        {
            Date = warehouseReception.Date,
            Invoice = warehouseReception.Invoice,
            Price = warehouseReception.Price,
            Quantity = warehouseReception.Quantity,
            ProductName = warehouseReception.Product.Name,
            MeasurementUnitsName = warehouseReception.Product.MeasurementUnit.Name
        };
    }

    public static WarehouseReception ToWarehouseReception(this WarehouseReceptionWithNewVM reception, int productId)
    {
        return new WarehouseReception
        {
            Date = DateTime.Now,
            Invoice = reception.Invoice,
            Price = reception.Price,
            ProductId = productId,
            Quantity = reception.Quantity,
        };
    }
}