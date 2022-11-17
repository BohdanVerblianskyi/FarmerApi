using System.ComponentModel.DataAnnotations;
using FarmerApp.Api.Models.Interfaces;

namespace FarmerApp.Api.Models;

public class WarehouseReception : IModelWithId
{
    public int Id { get; set; }

    [MaxLength(50)] public string Invoice { get; set; } = string.Empty;

    public DateTime Date { get; set; }
    
    [Required] public float Quantity { get; set; }

    [Required] public float Price { get; set; }
    
    [Required,MaxLength(50)] public string ProductName { get; set; }

    [Required,MaxLength(50)] public string MeasurementUnitName { get; set; }
}