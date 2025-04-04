using SunriseShelter.Attributes;
using SunriseShelter.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SunriseShelter.Models
{
    public class Children
    {
        [Key]
        public int ChildrenId { get; set; }

        [Required(ErrorMessage = "Name is required."), MaxLength(25), NoSpacesOrNumbersOrSymbols, Display(Name = "Name")]
        public string Name { get; set; }


        [Required(ErrorMessage = "Date of birth of child is required."), Display(Name = "Date of birth")]
        [DataType(DataType.Date)]       //Validation//
        public DateTime DateOfBirth { get; set; }


        [Required, MaxLength(25), NoNumbersOrSymbols, Display(Name = "Country of Origin")]
        public string BirthPlace { get; set; }


        [Required(ErrorMessage = "Date of admission is required."), Display(Name = "Date of admission")]
        [DataType(DataType.Date)]       //Validation//
        public DateTime DateOfAdmission { get; set; }


        public ICollection<Adoption> Adoption { get; set; }


    }
}