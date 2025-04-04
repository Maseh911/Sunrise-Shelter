using SunriseShelter.Attributes;
using SunriseShelter.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SunriseShelter.Models
{
    public class Staff
    {
        [Key]
        public int StaffId { get; set; }


        [Required, Display(Name = "First Name"), NoSpacesOrNumbersOrSymbols ,MaxLength(25)]
        public string FirstName { get; set; }


        [Required, Display(Name = "Last Name"), NoSpacesOrNumbersOrSymbols, MaxLength(25)]
        public string LastName { get; set; }


        [Required, Display(Name = "Role"), NoSpacesOrNumbersOrSymbols, MaxLength(25)]
        public string Role { get; set; }


        [Required, Display(Name = "Phone Number"), NewZealandPhone]
        public string Phone { get; set; }


        [Required, Display(Name = "Email"), MaxLength(50), EmailAddress]
        public string Email { get; set; }


        [Required]
        [Display(Name = "Orphanage")]
        [ForeignKey("OrphanageId")]
        public int OrphanageId { get; set; }
        public Orphanage Orphanage { get; set; }

    }
}