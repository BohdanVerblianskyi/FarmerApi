using System.ComponentModel.DataAnnotations;
using FarmerApp.Data.Validations;

namespace FarmerApp.Data.ViewModels.WarehouseReception;

public class WarehouseReceptionWithTheSameVM
{
    [MaxLength(50)] public string? Invoice { get; set; }

    [Required] public int ProductId { get; set; }

    [Required, NotZeroAndNotLessZero] public float Quantity { get; set; }

    [Required, NotZeroAndNotLessZero] public float Price { get; set; }
}