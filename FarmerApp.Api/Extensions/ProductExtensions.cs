using FarmerApp.Data.DTO;
using FarmerApp.Data.Models;

namespace FarmerApp.Api.Extensions;

public static class ProductExtensions
{
    public static ProductDTO ToProductDto(this Product product)
    {
        return new ProductDTO
        {
            Id = product.Id,
            Name = product.Name
        };
    }
}