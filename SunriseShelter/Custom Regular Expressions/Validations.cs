using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

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
            ErrorMessage = "The field must contain only letters and spaces and no numbers";
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