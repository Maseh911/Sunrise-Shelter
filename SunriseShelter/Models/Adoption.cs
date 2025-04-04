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
        [Display(Name = "Parent")]
        [ForeignKey("ParentId")]
        public int ParentId { get; set; }
        public Parent Parent { get; set; }


        [Required]
        [Display(Name = "Child")]
        [ForeignKey("ChildrenId")]
        public int ChildrenId { get; set; }
        public Children Children { get; set; }


        [Required]
        [Display(Name = "Orphanage")]
        [ForeignKey("OrphanageId")]
        public int OrphanageId { get; set; }
        public Orphanage Orphanage { get; set; }

    }
}