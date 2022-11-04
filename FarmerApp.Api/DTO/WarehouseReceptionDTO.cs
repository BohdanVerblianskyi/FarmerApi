using System.ComponentModel.DataAnnotations;

namespace FarmerApp.Data.DTO;

public class WarehouseReceptionDTO
{
    public string Invoice { get; set; }

    public string ProductName { get; set; }

    public string MeasurementUnitsName { get; set; }

    public float Quantity { get; set; }

    public float Price { get; set; }

    [DataType("MM.dd.yyyy HH:mm")] public DateTime Date { get; set; }
}