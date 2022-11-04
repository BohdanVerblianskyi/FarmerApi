namespace FarmerApp.Data.Models.Interfaces;

public interface IModelType : IModelWithId
{
    string Name { get; set; }
}