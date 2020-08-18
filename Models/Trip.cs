using System.ComponentModel.DataAnnotations;

namespace SpaceCrabs.Models
{
    public class Trip
    {
        [Key]
        public int TripId {get;set;}

        public int CrabId {get;set;}

        public int PlanetId {get;set;}


        // Navigational Prop - Add on Crab to Planets Tourists - Access with .ThenInclude
        public Crab Tourist {get;set;}

        // Navigational Prop - Add on Planet to Crabs MyTrips - Access with .ThenInclude
        public Planet Visit {get;set;}
    }
}