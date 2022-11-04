﻿using System.ComponentModel.DataAnnotations;
using FarmerApp.Data.Models.Interfaces;

namespace FarmerApp.Data.Models;

public class Spend : IModelWithId
{
    public int Id { get; set; }

    [Required, MaxLength(150)] public string Description { get; set; }

    [Required] public Location Location { get; set; }

    [Required] public int LocationId { get; set; }

    [Required] public SpendType SpendType { get; set; }

    [Required] public int SpendTypeId { get; set; }

    [Required] public float Price { get; set; }

    [Required, DataType("MM.dd.yyyy HH:mm")]
    public DateTime Date;

    public WithdrawalFromWarehouse? WithdrawalFromWarehouse { get; set; }

    public int? WithdrawalFromWarehouseId { get; set; }
}