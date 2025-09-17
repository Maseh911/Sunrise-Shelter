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
        public string FirstName { get; set; }
        
        [Required, Display(Name = "Last Name"), NoNumbersOrSymbols, MaxLength(25)]
        public string LastName { get; set; }

        [Required, Display(Name = "Date Of Birth")]
        [DataType(DataType.Date)]
        [AgeRange(18, 100, ErrorMessage = "You must be between 18 to 100 years old to register.")]
        public DateTime DateOfBirth { get; set; }

        // Phone is already inherited from IdentityUser

        // Email is already inherited from IdentityUser

        [Required, Display(Name = "Martial Status")]
        public string MaritalStatus { get; set; }

        [Required, Display(Name = "Address"), NoSymbols, MaxLength(25)]
        public string Address { get; set; }

        [Required, Display(Name = "Country of Origin"), NoNumbersOrSymbols, MaxLength(25)]
        public string BirthPlace { get; set; }

        // Navigation property for adoptions
        public virtual ICollection<Adoption> Adoptions { get; set; }
    }
}