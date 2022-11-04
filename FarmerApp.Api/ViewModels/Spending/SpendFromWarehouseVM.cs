using System.ComponentModel.DataAnnotations;
using FarmerApp.Data.Validations;

namespace FarmerApp.Data.ViewModels.Spending;

public class SpendFromWarehouseVM
{
    [Required] public int LocationId { get; set; }
    [Required] public int ProductId { get; set; }
    [Required, NotZeroAndNotLessZero] public float Quantity { get; set; }
}