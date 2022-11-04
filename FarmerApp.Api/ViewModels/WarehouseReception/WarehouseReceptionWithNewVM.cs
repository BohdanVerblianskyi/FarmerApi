using System.ComponentModel.DataAnnotations;
using FarmerApp.Data.Validations;

namespace FarmerApp.Data.ViewModels.WarehouseReception;

public class WarehouseReceptionWithNewVM
{
    [MaxLength(50)] public string? Invoice { get; set; }

    [Required] public int ProductTypeId { get; set; }

    [Required, MaxLength(50)] public string ProductName { get; set; }

    [Required] public int MeasurementUnitId { get; set; }

    [Required, NotZeroAndNotLessZero] public float Quantity { get; set; }

    [Required, NotZeroAndNotLessZero] public float Price { get; set; }
}