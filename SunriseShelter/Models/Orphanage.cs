using SunriseShelter.Attributes;
using SunriseShelter.Models;
using System.ComponentModel.DataAnnotations;

namespace SunriseShelter.Models
{
    public class Orphanage
    {
        [Key]
        public int OrphanageId { get; set; } // Unique ID for each orphanage

        [Required, MaxLength(50), NoNumbersOrSymbols, Display(Name = "Orphanage")]
        public string Name { get; set; } // Orphanage name (letters only, max 50 characters)

        [Required, MaxLength(50), Display(Name = "Address"), NoSymbols]
        public string Address { get; set; } // Physical address (no symbols allowed)

        [Required, MaxLength(25), Display(Name = "State"), NoNumbersOrSymbols]
        public string State { get; set; } // State or region (letters only, max 25 characters)

        [Required, MaxLength(25), Display(Name = "Country"), NoNumbersOrSymbols]
        public string Country { get; set; } // Country name (letters only, max 25 characters)

        // Navigation property for staff assigned to this orphanage
        public ICollection<Staff> Staff { get; set; } // One orphanage can have multiple staff members


    }
}