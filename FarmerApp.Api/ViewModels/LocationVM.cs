using System.ComponentModel.DataAnnotations;
using FarmerApp.Api.Validations;
using FarmerApp.Data.Validations;

namespace FarmerApp.Data.ViewModels;

public class LocationVM
{
    [Required] public string Name { get; set; }
    [Required, NotZeroAndNotLessZero] public float Size { get; set; }

    [Required, MinLength(4), MaxLength(4), NumberOnly]
    public string Season { get; set; }
}