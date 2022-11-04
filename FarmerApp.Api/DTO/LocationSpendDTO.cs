namespace FarmerApp.Data.DTO;

public class LocationSpendDTO
{
    public IEnumerable<SpendDTO> Spendings { get; set; }

    public float Sum { get; set; }
}