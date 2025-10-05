using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using SunriseShelter.Models;

namespace SunriseShelter.Attributes
{
    public class NoSpacesOrNumbersOrSymbolsAttribute : RegularExpressionAttribute
    {
        public NoSpacesOrNumbersOrSymbolsAttribute()
            : base(@"^[a-zA-Z]+$") // This makes it to where only characters can be entered and nothing else //
        {
            ErrorMessage = "The field must contain only letters and no numbers, spaces, or special characters.";
        }
    }

    public class NoNumbersOrSymbolsAttribute : RegularExpressionAttribute
    {
        public NoNumbersOrSymbolsAttribute()
            : base(@"^[a-zA-Z ]+$")   // This makes it to where only characters and spaces can be entered and nothing else //

        {
            ErrorMessage = "The field must contain only letters and spaces and no numbers and no special characters";
        }
    }
}

    public class NoSymbolsAttribute : RegularExpressionAttribute
{
    public NoSymbolsAttribute()
        : base("^[a-zA-Z0-9 ]*$") // This makes it to where no symbols can be entered //
    {
        ErrorMessage = "The field must cotain only letters, numbers, and spaces and no special characters";
    }
}

public class NoNumbersAttribute : RegularExpressionAttribute
{
    public NoNumbersAttribute()
        : base(@"^[a-zA-Z\s\p{P}\p{S}]+$")   // This makes it to where only numbers can not enter //

    {
        ErrorMessage = "The field must contain only letters, spaces and special characters and no numbers";
    }
}

public class NewZealandPhone : RegularExpressionAttribute
{
    public NewZealandPhone()
        : base(@"^\+?\d{1,3}[- ]?\(?\d{3}\)?[- ]?\d{3}[- ]?\d{4}$") // This ensures that the phone number is only in New Zealand format //
    {
        ErrorMessage = "Please enter a valid phone number.";
    }
}

public class AgeRangeAttribute : ValidationAttribute 
{
    private readonly int _minAge;
    private readonly int _maxAge;

    public AgeRangeAttribute(int minAge, int maxAge)
    {
        _minAge = minAge;
        _maxAge = maxAge;
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value == null)
        {
            return ValidationResult.Success;
        }

        if (value is DateTime dateOfBirth)
        {
            var today = DateTime.Today;
            var age = today.Year - dateOfBirth.Year;

            // Check if birthday hasn't occurred yet this year
            if (dateOfBirth > today.AddYears(-age))
            {
                age--;
            }

            if (age < _minAge)
            {
                return new ValidationResult(ErrorMessage ?? $"You must be at least {_minAge} years old to register.");
            }

            if (age >= _maxAge)
            {
                return new ValidationResult(ErrorMessage ?? $"You must be under {_maxAge} years old to register.");
            }

            return ValidationResult.Success;
        }

        return new ValidationResult("Please enter a valid date of birth.");
    }
}
