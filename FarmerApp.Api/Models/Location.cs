using System.ComponentModel.DataAnnotations;
using FarmerApp.Api.Models;
using FarmerApp.Data.Models.Interfaces;

namespace FarmerApp.Data.Models;

public class Location : IModelWithId
{
    public int Id { get; set; }

    [Required, MaxLength(50)] public string Name { get; set; }

    [Required] public float Size { get; set; }

    [Required, MinLength(4), MaxLength(4)] public string Season { get; set; }

    public List<Spend> Spends { get; set; }
}