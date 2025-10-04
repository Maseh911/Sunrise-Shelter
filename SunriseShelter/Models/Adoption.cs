using SunriseShelter.Areas.Identity.Data;
using SunriseShelter.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SunriseShelter.Models
{
    public class Adoption
    {
        [Key]
        public int AdoptionId { get; set; } // Unique ID for each adoption record

        [Display(Name = "Date of Adoption")]
        public DateTime? AdoptionDate { get; set; } // Date the adoption was finalized (nullable until completed)

        [Required, Display(Name = "Application Date")]
        [DataType(DataType.Date)]
        public DateTime ApplicationDate { get; set; } = DateTime.Now; // Date the adoption application was submitted

        [Required]
        [Display(Name = "Status")]
        public string Status { get; set; } = "Pending"; // Current status of the adoption ("Pending", "Approved", "Rejected", "Completed")

        // Foreign key linking adoption to a parent user
        [Required]
        [Display(Name = "Parent")]
        [ForeignKey("Parent")]
        public string ParentId { get; set; }
        public virtual SunriseShelterUser Parent { get; set; } // Navigation property to parent user

        // Foreign key linking adoption to a child
        [Required]
        [Display(Name = "Child")]
        [ForeignKey("ChildrenId")]
        public int ChildrenId { get; set; }
        public Children Children { get; set; } // Navigation property to child


    }
}