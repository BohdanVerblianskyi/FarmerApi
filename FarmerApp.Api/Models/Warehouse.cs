using System.ComponentModel.DataAnnotations;
using FarmerApp.Data.Models;
using FarmerApp.Data.Models.Interfaces;

namespace FarmerApp.Api.Models;

public class Warehouse : IModelWithId
{
    public int Id { get; set; }

    [Required] public Product Product { get; set; }

    [Required] public int ProductId { get; set; }

    [Required] public float Quantity { get; set; }

    public void Add(float quantity)
    {
        Quantity += quantity;
    }

    public bool TrySubtract(float quantity)
    {
        if (Quantity < quantity)
        {
            return false;
        }

        Quantity -= quantity;
        return true;
    }
}