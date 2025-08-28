using SunriseShelter.Attributes;
using SunriseShelter.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SunriseShelter.Models
{
    public class Children
    {
        [Key]
        public int ChildrenId { get; set; }

        [Required(ErrorMessage = "Name is required."), MaxLength(25), NoSpacesOrNumbersOrSymbols, Display(Name = "Name")]
        public string Name { get; set; }

        [Required, Display(Name = "Gender")]
        public string Gender { get; set; } // "Male", "Female", "Other"

        [Required(ErrorMessage = "Date of birth is required."), Display(Name = "Date of birth")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Required, MaxLength(25), NoNumbersOrSymbols, Display(Name = "Country of Origin")]
        public string BirthPlace { get; set; }

        [Required(ErrorMessage = "Date of admission is required."), Display(Name = "Date of admission")]
        [DataType(DataType.Date)]
        public DateTime DateOfAdmission { get; set; }

        [Display(Name = "Current Status")]
        public string Status { get; set; } = "Available"; // "Available", "In Process", "Adopted"

        // Foreign key for Orphanage (if children belong to specific orphanages)
        [Display(Name = "Orphanage")]
        public int OrphanageId { get; set; }
        public virtual Orphanage Orphanage { get; set; }

        public ICollection<Adoption> Adoptions { get; set; }
    }
}