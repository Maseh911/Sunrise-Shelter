using SunriseShelter.Attributes;
using SunriseShelter.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SunriseShelter.Models
{
    public class Children
    {
        [Key]
        public int ChildrenId { get; set; } // (unique ID for each child)

        [Required(ErrorMessage = "Name is required."), MaxLength(25), NoNumbersOrSymbols, Display(Name = "Name")]
        public string Name { get; set; } // Child's name (can't have numbers, or symbols)

        [Required, Display(Name = "Gender")]
        public string Gender { get; set; } // Child's gender ("Male", "Female", "Other")

        [Required(ErrorMessage = "Date of birth is required."), Display(Name = "Date of birth")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; } // Child's date of birth

        [Required, MaxLength(25), NoNumbersOrSymbols, Display(Name = "Country of Origin")]
        public string BirthPlace { get; set; } // Country of origin (no numbers or symbols allowed)

        [Required(ErrorMessage = "Date of admission is required."), Display(Name = "Date of admission")]
        [DataType(DataType.Date)]
        public DateTime DateOfAdmission { get; set; } // Date child was admitted to the orphanage

        [Display(Name = "Current Status")]
        public string Status { get; set; } = "Available"; // Child's adoption status ("Available", "In Process", "Adopted")

        // Foreign key linking child to an orphanage
        [Display(Name = "Orphanage")]
        public int OrphanageId { get; set; }
        public virtual Orphanage Orphanage { get; set; } // Navigation property to orphanage

        // Navigation property for adoptions (one child can have multiple adoption records)
        public ICollection<Adoption> Adoptions { get; set; }
    }
}