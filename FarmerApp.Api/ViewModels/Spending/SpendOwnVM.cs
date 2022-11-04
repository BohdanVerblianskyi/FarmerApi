using System.ComponentModel.DataAnnotations;
using FarmerApp.Data.Validations;

namespace FarmerApp.Data.ViewModels.Spending;

public class SpendOwnVM
{
    [Required] public int LocationId { get; set; }
    [Required] public string OwnResourceName { get; set; }
    [Required, NotZeroAndNotLessZero] public float Price { get; set; }
}