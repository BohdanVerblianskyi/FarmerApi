namespace FarmerApp.Api.DTO;

public class CreateProductDto
{
    public string Name { get; set; }

    public int MeasurementUnitId { get; set; }

    public int ProductTypeId { get; set; }

    public float Price { get; set; }
}