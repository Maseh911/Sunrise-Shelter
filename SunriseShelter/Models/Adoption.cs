using SunriseShelter.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SunriseShelter.Models
{
    public class Adoption
    {
        [Key]
        [Required]
        public int AdoptionId { get; set; }

        [Required]
        [Display(Name = "Date of Adoption")]
        [DataType(DataType.Date)]
        public DateTime AdoptionDate { get; set; }

        public ICollection<Children> Children { get; set; }

    }
}