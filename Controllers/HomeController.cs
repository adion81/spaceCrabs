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
            List<Crab> AllCrabs = _context.Crabs.ToList();
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

        [HttpGet("planets")]
        public IActionResult Planets()
        {
            List<Planet> AllPlanets = _context.Planets.Include( p => p.Inhabitants ).ToList();
            return View(AllPlanets);
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
