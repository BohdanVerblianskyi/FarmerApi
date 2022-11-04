using System.ComponentModel.DataAnnotations;
using FarmerApp.Data.Models.Interfaces;

namespace FarmerApp.Data.Models;

public class OwnResource : IModelType
{
    [Required] public int Id { get; set; }
    [Required, MaxLength(50)] public string Name { get; set; }
}