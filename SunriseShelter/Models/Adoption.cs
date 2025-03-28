using SunriseShelter.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SunriseShelter.Models
{
    public class Adoption
    {
        [Key]
        public int AdoptionId { get; set; }


        [Required, Display(Name = "Date of Adoption"), DataType(DataType.Date)] // Validating date * //
        public DateTime AdoptionDate { get; set; }


        [Required]
        public int ParentId { get; set; }
        [ForeignKey("ParentId")]
        [Display(Name = "Parent")]
        public Parent Parent { get; set; }


        [Required]
        public int ChildrenId { get; set; }
        [ForeignKey("ChildrenId"), Display(Name = "Child")]
        public Children Children { get; set; }


        [Required]
        public int OrphanageId { get; set; }
        [ForeignKey("OrphanageId"), Display(Name = "Orphanage")]
        public Orphanage Orphanage { get; set; }

    }
}