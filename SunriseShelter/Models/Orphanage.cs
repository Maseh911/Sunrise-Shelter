using SunriseShelter.Attributes;
using SunriseShelter.Migrations;
using SunriseShelter.Models;
using System.ComponentModel.DataAnnotations;

namespace SunriseShelter.Models
{
    public class Orphanage
    {
        [Key]
        public int OrphanageId { get; set; }


        [Required, MaxLength(25), NoSpacesOrNumbersOrSymbols, Display(Name = "Orphanage")]
        public string Name { get; set; }


        [Required, MaxLength(25), Display(Name = "Address"), NoSymbols]
        public string Address { get; set; } 


        [Required, MaxLength(25), Display(Name = "State"), NoSpacesOrNumbersOrSymbols]
        public string State { get; set; }


        [Required, MaxLength(25), Display(Name = "Country"), NoSpacesOrNumbersOrSymbols]

        public string Country { get; set; }


        public ICollection<Staff> Staff { get; set; }

    }
}