using SunriseShelter.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SunriseShelter.Models
{
    public class Staff
    {
        [Key]
        public int StaffId { get; set; }


        [Required, Display(Name = "First Name"), MaxLength(25)]
        public string FirstName { get; set; }


        [Required, Display(Name = "Last Name"), MaxLength(25)]
        public string LastName { get; set; }


        [Required, Display(Name = "Role"), MaxLength(25)]
        public string Role { get; set; }


        [Required, Display(Name = "Phone Number"), RegularExpression("^[1-9]\\d{2}-\\d{3}-\\d{4}")]
        public string Phone { get; set; }


        [Required, Display(Name = "Email"), MaxLength(50)]
        public string Email { get; set; }


        [Required]
        public int OrphanageId { get; set; }
        [ForeignKey("OrphanageId"), Display(Name = "Orphanage")]
        public Orphanage Orphanage { get; set; }

    }
}