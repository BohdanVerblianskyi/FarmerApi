using System.ComponentModel.DataAnnotations;
using FarmerApp.Data.Models.Interfaces;

namespace FarmerApp.Data.Models;

public class WarehouseReception : IModelWithId
{
    public int Id { get; set; }

    [MaxLength(50)] public string? Invoice { get; set; }

    public DateTime Date { get; set; }

    [Required] public Product Product { get; set; }

    [Required] public int ProductId { get; set; }

    [Required] public float Quantity { get; set; }

    [Required] public float Price { get; set; }
}