namespace FarmerApp.Data.DTO;

public class CreateProductDTO
{
    public string Name { get; set; }

    public int MeasurementUnitId { get; set; }

    public int ProductTypeId { get; set; }

    public float Price { get; set; }
}