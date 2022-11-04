using System.ComponentModel.DataAnnotations;

namespace FarmerApp.Data.DTO;

public class SpendDTO
{
    public string Description { get; set; }

    public float Price { get; set; }

    public DateTime Date { get; set; }
}