using System.ComponentModel.DataAnnotations;

namespace FarmerApp.Data.DTO;

public class SpendDTO
{
    public string Description { get; set; }

    public float Price { get; set; }

    [DataType("MM.dd.yyyy HH:mm")] public DateTime Date { get; set; }
}