using System.ComponentModel.DataAnnotations;

namespace FarmerApp.Api.DTO;

public class WarehouseCreateDto
{
    [Required] public int ProductId { get; set; }

    [Required] public float Quantity { get; set; }
}