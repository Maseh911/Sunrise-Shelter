using SunriseShelter.Models;
using System.ComponentModel.DataAnnotations;

namespace SunriseShelter.Models
{
    public class Orphanage
    {
        [Key]
        public int OrphanageId { get; set; }
        [Required]
        [MaxLength(25)]
        [Display(Name = "Orphanage")]
        public string Name { get; set; }
        [Required]
        [MaxLength(25)]
        [Display(Name = "Address")]
        public string Address { get; set; }
        [Required]
        [MaxLength(25)]
        [Display(Name = "State")]
        public string State { get; set; }
        [Required]
        [MaxLength(25)]
        [Display(Name = "Country")]
        public string Country { get; set; }
        public ICollection<Children> Children { get; set; }
        public ICollection<Staff> Staff { get; set; }

    }
}