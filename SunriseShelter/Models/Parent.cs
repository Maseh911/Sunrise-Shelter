using SunriseShelter.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SunriseShelter.Models
{
    public class Parent
    {
        [Key]
        public int ParentId { get; set; }


        [Required, Display(Name = "First Name"), MaxLength(25)]
        public string FirstName { get; set; }


        [Required, Display(Name = "Last Name"), MaxLength(25)]
        public string LastName { get; set; }


        [Required, Display(Name = "Date Of Birth")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }


        [Required, Display(Name = "Phone"), RegularExpression("^[1-9]\\d{2}-\\d{3}-\\d{4}")] // Valdiation testing //
        public string Phone { get; set; }


        [Required, Display(Name = "Email"), MaxLength(50), EmailAddress]
        public string Email { get; set; }


        [Required, Display(Name = "Martial Status"), MaxLength(15)]
        public string MartialStatus { get; set; }


        [Required, Display(Name = "Address"), MaxLength(25)]
        public string Address { get; set; }


        [Required, Display(Name = "Country of Origin"), MaxLength(25)]
        public string BirthPlace { get; set; }


        public ICollection<Adoption> Adoption { get; set; }
    }
}