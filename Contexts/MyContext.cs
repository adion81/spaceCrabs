using Microsoft.EntityFrameworkCore;
using SpaceCrabs.Models;

namespace SpaceCrabs.Contexts
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions options) : base(options){}

        public DbSet<Crab> Crabs {get;set;}
        public DbSet<Planet> Planets {get;set;}
    }
}