using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Tournament.Validation;

public class PhoneValidation : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        if (value is null)
            return false;
        var pattern = @"^\+\d{1,3}\(\d{3}\)\d{3}-\d{2}-\d{2}$";
        if (Regex.IsMatch((string) value, pattern))
            return true;
        return false;
    }
}