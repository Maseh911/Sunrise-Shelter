using SunriseShelter.Models;
using SunriseShelter.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SunriseShelter.Models
{
    public class Parent
    {
        public int ParentId { get; set; }
        [Required]
        [Display(Name = "First Name")]
        [MaxLength(25)]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        [MaxLength(25)]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Date Of Birth")]
        public DateTime DateOfBirth { get; set; }
        [Required]
        [Display(Name = "Phone")] // Needs Phone  validation //
        public string Phone { get; set; }
        [Required]
        [Display(Name = "Email")]
        [MaxLength(50)]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Martial Status")]
        [MaxLength(15)]
        public string MartialStatus { get; set; }
        [Required]
        [Display(Name = "Address")]
        [MaxLength(25)]
        public string Address { get; set; }
        [Required]
        [Display(Name = "Country")]
        [MaxLength(25)]
        public string Country { get; set; }
        public ICollection<Adoption> Adoption { get; set; }
    }
}