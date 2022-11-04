using FarmerApp.Data.DTO;
using FarmerApp.Data.Models;
using FarmerApp.Data.ViewModels;

namespace FarmerApp.Api.Extensions;

public static class LocationExtensions

{
    public static LocationDTO ToLocationDto(this Location location)
    {
        return new LocationDTO
        {
            Id = location.Id,
            Name = location.Name,
            Sesone = location.Season
        };
    }

    public static Location ToLocation(this LocationVM locationVm)
    {
        return new Location
        {
            Name = locationVm.Name,
            Season = locationVm.Season,
            Size = locationVm.Size
        };
    }
}