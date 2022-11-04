using System.ComponentModel.DataAnnotations;
using FarmerApp.Data.Models.Interfaces;

namespace FarmerApp.Data.Models;

public class WithdrawalFromWarehouse : IModelWithId
{
    public int Id { get; set; }

    [Required, DataType("MM.dd.yyyy HH:mm")]
    public DateTime Date { get; set; }

    [Required] public Product Product { get; set; }

    [Required] public int ProductId { get; set; }

    [Required] public float Quantity { get; set; }

    [Required] public float Price { get; set; }

    public List<Spend> Spendings { get; set; }
}