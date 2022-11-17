﻿using System.ComponentModel.DataAnnotations;
using FarmerApp.Api.Models.Interfaces;

namespace FarmerApp.Api.Models;

public class Location : IModelWithId
{
    public int Id { get; set; }

    [Required, MaxLength(30)] public string Name { get; set; }

    [Required] public float Size { get; set; }

    [Required, MaxLength(10)] public string Seson { get; set; }
    
     public List<Spend> Spends { get; set; }
}