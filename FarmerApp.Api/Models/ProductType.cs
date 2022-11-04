using System.ComponentModel.DataAnnotations;
using FarmerApp.Data.Models.Interfaces;

namespace FarmerApp.Data.Models;

public class ProductType : IModelType
{
    public int Id { get; set; }

    [Required, MaxLength(50)] public string Name { get; set; }

    public List<Product> Products { get; set; }
}