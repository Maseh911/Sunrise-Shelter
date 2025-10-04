using SunriseShelter.Attributes;
using SunriseShelter.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SunriseShelter.Models
{
    public class Staff
    {
        [Key]
        public int StaffId { get; set; } // Unique ID for each staff member

        [Required, Display(Name = "First Name"), NoNumbersOrSymbols, MaxLength(25)]
        public string FirstName { get; set; } // Staff's first name (letters only, max 25 characters)

        [Required, Display(Name = "Last Name"), NoNumbersOrSymbols, MaxLength(25)]
        public string LastName { get; set; } // Staff's last name (letters only, max 25 characters)

        [Required, Display(Name = "Role"), NoSpacesOrNumbersOrSymbols, MaxLength(25)]
        public string Role { get; set; } // Staff role (e.g. "Caretaker", "Manager", no spaces or symbols)

        [Required, Display(Name = "Phone Number"), NewZealandPhone]
        public string Phone { get; set; } // Staff's phone number (must match NZ format)

        [Required, Display(Name = "Email"), MaxLength(50), EmailAddress]
        public string Email { get; set; } // Staff's email address (validated format, max 50 characters)

        // Foreign key linking staff to an orphanage
        [Required]
        [Display(Name = "Orphanage")]
        [ForeignKey("OrphanageId")]
        public int OrphanageId { get; set; }
        public Orphanage Orphanage { get; set; } // Navigation property to orphanage

    }
}