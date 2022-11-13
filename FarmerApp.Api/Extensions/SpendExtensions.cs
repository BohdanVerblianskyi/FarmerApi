using FarmerApp.Api.Models;
using FarmerApp.Data.DTO;
using FarmerApp.Data.Models;

namespace FarmerApp.Api.Extensions;

public static class SpendExtensions
{
    public static SpendDTO ToSpendDto(this Spend spend)
    {
        return new SpendDTO
        {
            Date = spend.Date,
            Description = spend.Description,
            Price = spend.Price
        };
    }
}