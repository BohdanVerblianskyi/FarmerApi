using System.ComponentModel.DataAnnotations;

namespace FarmerApp.Data.Validations;

public class NumberOnlyAttribute : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        if (value is string line)
        {
            if (!line.All(char.IsNumber))
            {
                ErrorMessage = "the must is a number";
                return false;
            }
            else
            {
                return true;
            }
        }

        return base.IsValid(value);
    }
}