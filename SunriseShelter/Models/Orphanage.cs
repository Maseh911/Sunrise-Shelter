using SunriseShelter.Migrations;
using SunriseShelter.Models;
using System.ComponentModel.DataAnnotations;

namespace SunriseShelter.Models
{
    public class Orphanage
    {
        [Key]
        public int OrphanageId { get; set; }


        [Required, MaxLength(25), Display(Name = "Orphanage")]
        [RegularExpression(@"^[a-zA-Z ]+$")] // this ensures that no numbers, or special characters are added //
        public string Name { get; set; }


        [Required, MaxLength(25), Display(Name = "Address")]
        [RegularExpression("^[a-zA-Z0-9] +$")] // this ensures that no special characters are added //
        public string Address { get; set; } 


        [Required, MaxLength(25), Display(Name = "State"), RegularExpression("^[a-zA-Z0-9] +$")]
        public string State { get; set; }


        [Required, MaxLength(25), Display(Name = "Country"), RegularExpression(@"^[a-zA-Z ]+$")]
        public string Country { get; set; }

        public ICollection<Children> Children { get; set; }
        public ICollection<Staff> Staff { get; set; }

    }
}