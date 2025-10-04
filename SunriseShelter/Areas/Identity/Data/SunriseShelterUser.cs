using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using SunriseShelter.Attributes;
using System.ComponentModel.DataAnnotations.Schema;
using SunriseShelter.Models;

namespace SunriseShelter.Areas.Identity.Data
{
    public class SunriseShelterUser : IdentityUser
    {
        [Required, Display(Name = "First Name"), NoNumbersOrSymbols, MaxLength(25)]
        public string FirstName { get; set; } // User's first name (letters only, max 25 characters)

        [Required, Display(Name = "Last Name"), NoNumbersOrSymbols, MaxLength(25)]
        public string LastName { get; set; } // User's last name (letters only, max 25 characters)

        [Required, Display(Name = "Date Of Birth")]
        [DataType(DataType.Date)]
        [AgeRange(18, 100, ErrorMessage = "You must be between 18 to 100 years old to register.")]
        public DateTime DateOfBirth { get; set; } // User's birth date (must be between 18 and 100 years old)

        // Phone is inherited from IdentityUser

        // Email is inherited from IdentityUser

        [Required, Display(Name = "Martial Status")]
        public string MaritalStatus { get; set; } // User's marital status ("Single", "Married", etc.)

        [Required, Display(Name = "Address"), NoSymbols, MaxLength(25)]
        public string Address { get; set; } // Residential address (no symbols, max 25 characters)

        [Required, Display(Name = "Country of Origin"), NoNumbersOrSymbols, MaxLength(25)]
        public string BirthPlace { get; set; } // Country of origin (letters only, max 25 characters)

        // Navigation property for adoptions submitted by this user
        public virtual ICollection<Adoption> Adoptions { get; set; }
    }
}