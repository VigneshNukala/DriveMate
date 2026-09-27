using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace DriveMate.Application.Validation;

public class StrongPasswordAttribute : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        if (value is not string password)
            return false;

        return Regex.IsMatch(
            password,
            @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^a-zA-Z\d]).{8,}$");
    }

    public override string FormatErrorMessage(string name)
    {
        return $"{name} must contain at least 8 characters, " +
               "one uppercase letter, one lowercase letter, " +
               "one number, and one special character.";
    }
}