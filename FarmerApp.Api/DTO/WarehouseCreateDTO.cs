using System.ComponentModel.DataAnnotations;

namespace FarmerApp.Data.DTO;

public class WarehouseCreateDTO
{
    [Required] public int ProductId { get; set; }

    [Required] public float Quantity { get; set; }
}