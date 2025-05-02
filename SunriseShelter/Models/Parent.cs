using SunriseShelter.Attributes;
using SunriseShelter.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SunriseShelter.Models
{
    public class Parent
    {
        [Key]
        public int ParentId { get; set; }


        [Required, Display(Name = "First Name"), NoSpacesOrNumbersOrSymbols, MaxLength(25)]
        public string FirstName { get; set; }


        [Required, Display(Name = "Last Name"), NoSpacesOrNumbersOrSymbols, MaxLength(25)]
        public string LastName { get; set; }


        [Required, Display(Name = "Date Of Birth")] // validation //
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }


        [Required, Display(Name = "Phone"), NewZealandPhone] // Valdiation testing //
        public string Phone { get; set; }


        [Required, Display(Name = "Email"), MaxLength(50), EmailAddress]
        public string Email { get; set; }


        [Required, Display(Name = "Martial Status"), NoSpacesOrNumbersOrSymbols, MaxLength(15)]
        public string MartialStatus { get; set; }


        [Required, Display(Name = "Address"), NoSymbols, MaxLength(25)]
        public string Address { get; set; }


        [Required, Display(Name = "Country of Origin"), NoNumbersOrSymbols ,MaxLength(25)]
        public string BirthPlace { get; set; }


        public ICollection<Adoption> Adoption { get; set; }
    }
}