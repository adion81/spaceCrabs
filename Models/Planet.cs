using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SpaceCrabs.Models
{
    public class Planet
    {
        [Key]
        public int PlanetId {get;set;}

        [Required(ErrorMessage="Name is required")]
        public string Name {get;set;}

        [Required(ErrorMessage="System is required")]
        public string System {get;set;}

        [Required(ErrorMessage="Galaxy is required")]
        public string Galaxy {get;set;}

        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;

        public List<Crab> Inhabitants {get;set;} 
    }
}