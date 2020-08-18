using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SpaceCrabs.Contexts;
using SpaceCrabs.Models;

namespace SpaceCrabs.Controllers
{
    public class HomeController : Controller
    {
        private MyContext _context;

        public HomeController(MyContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("new/crab")]
        public IActionResult NewCrab()
        {
            ViewBag.Planets = _context.Planets.ToList();
            return View();
        }

        [HttpPost("create/crab")]
        public IActionResult CreateCrab(Crab newCrab)
        {
            if(ModelState.IsValid)
            {
                _context.Crabs.Add(newCrab);
                _context.SaveChanges();
                return RedirectToAction("Crabs");
            }
            else
            {
                ViewBag.Planets = _context.Planets.ToList();
                return View("NewCrab");
            }
        }

        [HttpGet("crabs")]
        public IActionResult Crabs()
        {
            List<Crab> AllCrabs = _context.Crabs
                                    .Include( c => c.HomePlanet )
                                    .Include( c => c.MyTrips )
                                    .ThenInclude( t => t.Visit)
                                    .ToList();
            ViewBag.Planets = _context.Planets
                                    .Include( p => p.Tourists)
                                    .ToList();
            return View(AllCrabs);
        }

        [HttpPost("create/planet")]
        public IActionResult CreatePlanet(Planet newPlanet)
        {
            if(ModelState.IsValid)
            {
                _context.Planets.Add(newPlanet);
                _context.SaveChanges();
                return RedirectToAction("Planets");
            }
            else
            {
                return View("Index");
            }
        }

        [HttpGet("planet/{planetId}")]
        public IActionResult ShowPlanet(int planetId)
        {
            Planet show = _context.Planets
                                .Include( p => p.Tourists )
                                .ThenInclude( t=> t.Tourist )
                                .FirstOrDefault( p => p.PlanetId == planetId );

            ViewBag.CrabsNotTourists = _context.Crabs
                                            .Include( c => c.MyTrips)
                                            .Where( c => c.MyTrips.All( t => t.PlanetId != planetId ) )
                                            .ToList();

            return View(show);
        }

        [HttpGet("planets")]
        public IActionResult Planets()
        {
            List<Planet> AllPlanets = _context.Planets.Include( p => p.Inhabitants )
                                                        .Include( p => p.Tourists)
                                                        .ThenInclude( t => t.Tourist )
                                                        .ToList();
            return View(AllPlanets);
        }

        [HttpPost("trip")]
        public IActionResult Trip(int crabId, int planetId)
        {
            Trip newTrip = new Trip();
            newTrip.CrabId = crabId;
            newTrip.PlanetId = planetId;
            _context.Trips.Add(newTrip);
            _context.SaveChanges();
            return RedirectToAction("Crabs");
        }

        [HttpGet("cancel/trip/{planetId}/{crabId}")]
        public IActionResult Cancel(int planetId, int crabId)
        {
            Trip ending = _context.Trips.FirstOrDefault( t => t.PlanetId == planetId && t.CrabId == crabId);
            _context.Remove(ending);
            _context.SaveChanges();
            return RedirectToAction("Planets");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
