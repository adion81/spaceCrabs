using System;
using System.Collections.Generic;
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

        // Foreign Key - One To Many
        public int PlanetId {get;set;}

        // Navigational Prop - One To Many - A crab can only live on one planet
        public Planet HomePlanet {get;set;}

        // Navigational Prop -  Many To Many - A crab can visit many planets
        public List<Trip> MyTrips {get;set;}

        public int Age()
        {
            return DateTime.Now.Year - Birthday.Year;
        }
    }
}