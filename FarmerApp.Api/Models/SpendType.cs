using System.ComponentModel.DataAnnotations;
using FarmerApp.Data.Models.Interfaces;

namespace FarmerApp.Data.Models;

public class SpendType : IModelType
{
    public int Id { get; set; }

    [Required, MaxLength(50)] public string Name { get; set; }

    public List<Spend> Spends { get; set; }
}