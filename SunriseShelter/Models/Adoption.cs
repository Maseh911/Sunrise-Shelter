using SunriseShelter.Areas.Identity.Data;
using SunriseShelter.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SunriseShelter.Models
{
    public class Adoption
    {
        [Key]
        public int AdoptionId { get; set; }


        [Display(Name = "Date of Adoption")] 
        public DateTime? AdoptionDate { get; set; }

        [Required, Display(Name = "Application Date")]
        [DataType(DataType.Date)]
        public DateTime ApplicationDate { get; set; } = DateTime.Now;

        [Required]
        [Display(Name = "Status")]
        public string Status { get; set; } = "Pending"; // "Pending", "Approved", "Rejected", "Completed"


        [Required]
        [Display(Name = "Parent")]
        [ForeignKey("Parent")]
        public string ParentId { get; set; }
        public virtual SunriseShelterUser Parent { get; set; }


        [Required]
        [Display(Name = "Child")]
        [ForeignKey("ChildrenId")]
        public int ChildrenId { get; set; }
        public Children Children { get; set; }

    }
}