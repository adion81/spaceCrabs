using System;
using System.ComponentModel.DataAnnotations;
using SpaceCrabs.Validations;

namespace SpaceCrabs.Models
{
    public class Crab
    {
        [Key]
        public int CrabId {get;set;}

        [Required(ErrorMessage="Name is required")]
        public string Name {get;set;}

        [Required(ErrorMessage="Name is required")]
        public string Weapon {get;set;}

        [Required(ErrorMessage="Space Craft is required")]
        [MaxLength(20,ErrorMessage="Space Craft must be less than 20 characterss")]
        public string SpaceCraft {get;set;}

        [DataType(DataType.Date)]
        [Eighteen]
        public DateTime Birthday {get;set;} 

        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;

        public int PlanetId {get;set;}

        public int Age()
        {
            return DateTime.Now.Year - Birthday.Year;
        }
    }
}