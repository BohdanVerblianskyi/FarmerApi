using FarmerApp.Data.DTO;
using FarmerApp.Data.Models.Interfaces;

namespace FarmerApp.Api.Extensions;

public static class ModelTypeExtensions
{
    public static ModelTypeDTO ToModelTypeDto<T>(this T model) where T : IModelType
    {
        return new ModelTypeDTO
        {
            Id = model.Id,
            Name = model.Name
        };
    }
}